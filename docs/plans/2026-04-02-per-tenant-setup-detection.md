# Per-Tenant Setup Detection Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Make fresh multi-tenant tenants return 503 `setupRequired` until the owner registers a passkey, then unlock normally.

**Architecture:** A new `TenantSetupMiddleware` slots into the pipeline immediately after `TenantResolutionMiddleware`. It checks whether the current tenant has any `PasskeyCredentials` (tenant-scoped via EF query filter). If none, it returns 503 `{ setupRequired: true }` — the same shape that `hooks.server.ts` in nocturne-web already handles. The passkey setup endpoints in `PasskeyController` are updated to use the resolved tenant (`ITenantAccessor.TenantId`) instead of the hardcoded default tenant, and their guard is updated to check credentials rather than the global `RecoveryModeState.IsSetupRequired` flag.

**Tech Stack:** ASP.NET Core 10 middleware, EF Core 10 (Sqlite for tests), xUnit + FluentAssertions + Moq, `ITenantAccessor`, `NocturneDbContext`

---

### Task 1: `TenantSetupMiddleware` — failing tests first

**Files:**
- Create: `tests/Unit/Nocturne.API.Tests/Middleware/TenantSetupMiddlewareTests.cs`

**Step 1: Write the failing tests**

```csharp
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Nocturne.API.Middleware;
using Nocturne.Core.Contracts.Multitenancy;
using Nocturne.Infrastructure.Data;
using Nocturne.Infrastructure.Data.Entities;
using Xunit;

namespace Nocturne.API.Tests.Middleware;

public class TenantSetupMiddlewareTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly NocturneDbContext _dbContext;
    private readonly Mock<ITenantAccessor> _tenantAccessor;
    private readonly Guid _tenantId = Guid.CreateVersion7();

    public TenantSetupMiddlewareTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<NocturneDbContext>()
            .UseSqlite(_connection)
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
            .Options;

        _dbContext = new NocturneDbContext(options);
        _dbContext.TenantId = _tenantId;
        _dbContext.Database.EnsureCreated();

        _tenantAccessor = new Mock<ITenantAccessor>();
        _tenantAccessor.Setup(t => t.IsResolved).Returns(true);
        _tenantAccessor.Setup(t => t.TenantId).Returns(_tenantId);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        _connection.Dispose();
    }

    private (TenantSetupMiddleware middleware, DefaultHttpContext context) Build(
        string path = "/api/status",
        bool nextCalled = false,
        RequestDelegate? next = null)
    {
        var wasCalled = false;
        next ??= ctx => { wasCalled = true; return Task.CompletedTask; };

        var middleware = new TenantSetupMiddleware(
            next,
            NullLogger<TenantSetupMiddleware>.Instance);

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = path;
        httpContext.Response.Body = new MemoryStream();

        return (middleware, httpContext);
    }

    [Fact]
    public async Task WhenTenantHasNoCredentials_Returns503WithSetupRequired()
    {
        // Arrange — no passkey credentials in db
        var (middleware, ctx) = Build("/api/status");

        // Act
        await middleware.InvokeAsync(ctx, _tenantAccessor.Object, _dbContext);

        // Assert
        ctx.Response.StatusCode.Should().Be(503);
        ctx.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(ctx.Response.Body).ReadToEndAsync();
        body.Should().Contain("setup_required");
        body.Should().Contain("\"setupRequired\":true");
    }

    [Fact]
    public async Task WhenTenantHasCredential_CallsNext()
    {
        // Arrange — seed a passkey credential for this tenant
        var subjectId = Guid.CreateVersion7();
        _dbContext.PasskeyCredentials.Add(new PasskeyCredentialEntity
        {
            Id = Guid.CreateVersion7(),
            TenantId = _tenantId,
            SubjectId = subjectId,
            CredentialId = "cred-id",
            PublicKey = [],
            SignCount = 0,
        });
        await _dbContext.SaveChangesAsync();

        var nextCalled = false;
        var (middleware, ctx) = Build("/api/status",
            next: async c => { nextCalled = true; await Task.CompletedTask; });

        // Re-build with captured next
        var mw = new TenantSetupMiddleware(
            async c => { nextCalled = true; await Task.CompletedTask; },
            NullLogger<TenantSetupMiddleware>.Instance);
        ctx.Request.Path = "/api/status";

        // Act
        await mw.InvokeAsync(ctx, _tenantAccessor.Object, _dbContext);

        // Assert
        nextCalled.Should().BeTrue();
        ctx.Response.StatusCode.Should().NotBe(503);
    }

    [Theory]
    [InlineData("/api/auth/passkey/setup/options")]
    [InlineData("/api/auth/passkey/setup/complete")]
    [InlineData("/api/auth/passkey/register")]
    [InlineData("/api/auth/totp/setup")]
    [InlineData("/api/metadata")]
    [InlineData("/api/admin/tenants/validate-slug")]
    [InlineData("/api/v4/me/tenants/validate-slug")]
    public async Task AllowListPaths_AreNotBlocked_EvenWithNoCredentials(string path)
    {
        // Arrange — no credentials
        var nextCalled = false;
        var mw = new TenantSetupMiddleware(
            async c => { nextCalled = true; await Task.CompletedTask; },
            NullLogger<TenantSetupMiddleware>.Instance);

        var ctx = new DefaultHttpContext();
        ctx.Request.Path = path;
        ctx.Response.Body = new MemoryStream();

        // Act
        await mw.InvokeAsync(ctx, _tenantAccessor.Object, _dbContext);

        // Assert
        nextCalled.Should().BeTrue();
    }

    [Fact]
    public async Task WhenTenantNotResolved_CallsNext()
    {
        // Arrange — unresolved tenant (e.g. slug validation path with no tenant)
        _tenantAccessor.Setup(t => t.IsResolved).Returns(false);
        var nextCalled = false;
        var mw = new TenantSetupMiddleware(
            async c => { nextCalled = true; await Task.CompletedTask; },
            NullLogger<TenantSetupMiddleware>.Instance);

        var ctx = new DefaultHttpContext();
        ctx.Request.Path = "/api/status";
        ctx.Response.Body = new MemoryStream();

        // Act
        await mw.InvokeAsync(ctx, _tenantAccessor.Object, _dbContext);

        // Assert
        nextCalled.Should().BeTrue();
    }
}
```

**Step 2: Run to confirm they fail**

```
cd C:\Users\rhysg\Documents\Github\nocturne
dotnet test tests/Unit/Nocturne.API.Tests -v minimal --filter "FullyQualifiedName~TenantSetupMiddlewareTests"
```

Expected: compile error — `TenantSetupMiddleware` does not exist.

---

### Task 2: `TenantSetupMiddleware` — implementation

**Files:**
- Create: `src/API/Nocturne.API/Middleware/TenantSetupMiddleware.cs`

**Step 1: Write the implementation**

```csharp
using Nocturne.Core.Contracts.Multitenancy;
using Nocturne.Infrastructure.Data;

namespace Nocturne.API.Middleware;

/// <summary>
/// Middleware that returns 503 setupRequired for freshly provisioned tenants
/// that have no passkey credentials yet. Allows passkey setup and metadata
/// endpoints through so the setup wizard can complete.
///
/// Only active in multi-tenant mode (runs after TenantResolutionMiddleware).
/// Single-tenant setup is handled by RecoveryModeMiddleware.
/// </summary>
public class TenantSetupMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantSetupMiddleware> _logger;

    private static readonly string[] AllowedPrefixes =
    [
        "/api/auth/passkey/",
        "/api/auth/totp/",
        "/api/metadata",
    ];

    private static readonly string[] AllowedPaths =
    [
        "/api/admin/tenants/validate-slug",
        "/api/v4/me/tenants/validate-slug",
    ];

    public TenantSetupMiddleware(
        RequestDelegate next,
        ILogger<TenantSetupMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ITenantAccessor tenantAccessor,
        NocturneDbContext db)
    {
        // Only check when a tenant has been resolved
        if (!tenantAccessor.IsResolved)
        {
            await _next(context);
            return;
        }

        var path = context.Request.Path.Value ?? "";

        // Allow passkey, TOTP, metadata, and slug validation paths
        if (AllowedPaths.Any(p => path.Equals(p, StringComparison.OrdinalIgnoreCase)) ||
            AllowedPrefixes.Any(p => path.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
        {
            await _next(context);
            return;
        }

        // Only block API paths
        if (!path.StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        // Check if this tenant has completed setup (has at least one passkey credential)
        // PasskeyCredentialEntity is ITenantScoped — query filter applies automatically
        var hasCredentials = await db.PasskeyCredentials.AnyAsync();
        if (hasCredentials)
        {
            await _next(context);
            return;
        }

        _logger.LogDebug(
            "Tenant {TenantId} has no passkey credentials — returning setup required",
            tenantAccessor.TenantId);

        context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        await context.Response.WriteAsJsonAsync(new
        {
            error = "setup_required",
            message = "Initial setup required. Please register a passkey to secure your account.",
            setupRequired = true,
            recoveryMode = false,
        });
    }
}
```

**Step 2: Run tests**

```
dotnet test tests/Unit/Nocturne.API.Tests -v minimal --filter "FullyQualifiedName~TenantSetupMiddlewareTests"
```

Expected: all pass.

**Step 3: Commit**

```bash
git add tests/Unit/Nocturne.API.Tests/Middleware/TenantSetupMiddlewareTests.cs
git add src/API/Nocturne.API/Middleware/TenantSetupMiddleware.cs
git commit -m "feat: add TenantSetupMiddleware for per-tenant setup detection"
```

---

### Task 3: Register middleware in pipeline

**Files:**
- Modify: `src/API/Nocturne.API/Program.cs:240`

**Step 1: Insert one line after `TenantResolutionMiddleware`**

Find this block:
```csharp
// Resolve tenant from subdomain (must run before authentication)
app.UseMiddleware<TenantResolutionMiddleware>();

// Add Nightscout authentication middleware
app.UseMiddleware<AuthenticationMiddleware>();
```

Replace with:
```csharp
// Resolve tenant from subdomain (must run before authentication)
app.UseMiddleware<TenantResolutionMiddleware>();

// Block API traffic for freshly provisioned tenants with no passkey credentials
app.UseMiddleware<TenantSetupMiddleware>();

// Add Nightscout authentication middleware
app.UseMiddleware<AuthenticationMiddleware>();
```

**Step 2: Build to confirm no compilation errors**

```
dotnet build src/API/Nocturne.API -v minimal
```

Expected: Build succeeded, 0 errors.

**Step 3: Commit**

```bash
git add src/API/Nocturne.API/Program.cs
git commit -m "feat: register TenantSetupMiddleware after TenantResolutionMiddleware"
```

---

### Task 4: Update `PasskeyController` — failing tests first

The setup endpoints have two problems in multi-tenant mode:
1. The guard checks `state.IsSetupRequired` which is always `false` in multi-tenant
2. The tenant lookup is hardcoded to `IsDefault = true`

**Files:**
- Modify: `tests/Unit/Nocturne.API.Tests/Controllers/PasskeyControllerTests.cs`

**Step 1: Add failing tests that cover multi-tenant behaviour**

Add these tests to the `PasskeyControllerTests` class. They set `IsSetupRequired = false` (simulating multi-tenant) but seed a resolved tenant via `_tenantAccessor` — the existing `_tenantId` field is already wired up in the constructor.

```csharp
[Fact]
public async Task SetupOptions_MultiTenant_CreatesUserInResolvedTenant()
{
    // Arrange — seed the resolved tenant (not IsDefault)
    var tenant = new TenantEntity
    {
        Id = _tenantId,
        Slug = "rhys",
        DisplayName = "Rhys",
        IsDefault = false,
    };
    _dbContext.Tenants.Add(tenant);
    await _dbContext.SaveChangesAsync();

    // IsSetupRequired is false (multi-tenant mode)
    var state = new RecoveryModeState { IsSetupRequired = false };

    _passkeyService
        .Setup(s => s.GenerateRegistrationOptionsAsync(
            It.IsAny<Guid>(), "rhys", _tenantId))
        .ReturnsAsync(new PasskeyRegistrationOptions("{\"challenge\":\"mt\"}", "mt-token"));

    _subjectService
        .Setup(s => s.AssignRoleAsync(It.IsAny<Guid>(), "admin", null))
        .ReturnsAsync(true);

    var request = new SetupOptionsRequest
    {
        Username = "rhys",
        DisplayName = "Rhys",
    };

    // Act
    var result = await _controller.SetupOptions(request, state);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result.Result);
    var response = Assert.IsType<PasskeyOptionsResponse>(okResult.Value);
    response.ChallengeToken.Should().Be("mt-token");

    var members = await _dbContext.TenantMembers
        .IgnoreQueryFilters()
        .Where(tm => tm.TenantId == _tenantId)
        .ToListAsync();
    members.Should().HaveCount(1);
}

[Fact]
public async Task SetupComplete_MultiTenant_UsesResolvedTenant()
{
    // Arrange
    var tenant = new TenantEntity
    {
        Id = _tenantId,
        Slug = "rhys",
        DisplayName = "Rhys",
        IsDefault = false,
    };
    _dbContext.Tenants.Add(tenant);
    await _dbContext.SaveChangesAsync();

    var state = new RecoveryModeState { IsSetupRequired = false };
    var subjectId = Guid.CreateVersion7();

    _passkeyService
        .Setup(s => s.CompleteRegistrationAsync("{}", "token", _tenantId))
        .ReturnsAsync(new PasskeyCredentialResult(Guid.CreateVersion7(), subjectId));

    _recoveryCodeService
        .Setup(s => s.GenerateCodesAsync(subjectId))
        .ReturnsAsync(["CODE1", "CODE2", "CODE3"]);

    _subjectService
        .Setup(s => s.GetSubjectByIdAsync(subjectId))
        .ReturnsAsync(new Subject { Id = subjectId, Name = "Rhys" });

    _subjectService
        .Setup(s => s.GetSubjectRolesAsync(subjectId))
        .ReturnsAsync([]);

    _subjectService
        .Setup(s => s.GetSubjectPermissionsAsync(subjectId))
        .ReturnsAsync([]);

    _jwtService
        .Setup(s => s.GenerateAccessToken(
            It.IsAny<SubjectInfo>(), It.IsAny<IEnumerable<string>>(), It.IsAny<IEnumerable<string>>()))
        .Returns("access-token");

    _jwtService
        .Setup(s => s.GetAccessTokenLifetime())
        .Returns(TimeSpan.FromMinutes(15));

    _refreshTokenService
        .Setup(s => s.CreateRefreshTokenAsync(
            subjectId, null, It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<string?>()))
        .ReturnsAsync("refresh-token");

    var request = new SetupCompleteRequest
    {
        ChallengeToken = "token",
        AttestationResponseJson = "{}",
    };

    // Act
    var result = await _controller.SetupComplete(request, state);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result.Result);
    var response = Assert.IsType<SetupCompleteResponse>(okResult.Value);
    response.Success.Should().BeTrue();
    response.RecoveryCodes.Should().HaveCount(3);

    // State is unchanged (multi-tenant doesn't use global state)
    state.IsSetupRequired.Should().BeFalse();
}

[Fact]
public async Task SetupOptions_MultiTenant_WhenAlreadyHasCredential_ReturnsForbidden()
{
    // Arrange — tenant already has a passkey credential (setup already done)
    var tenant = new TenantEntity
    {
        Id = _tenantId,
        Slug = "rhys",
        DisplayName = "Rhys",
        IsDefault = false,
    };
    _dbContext.Tenants.Add(tenant);

    // Seed a passkey credential to simulate completed setup
    _dbContext.PasskeyCredentials.Add(new PasskeyCredentialEntity
    {
        Id = Guid.CreateVersion7(),
        TenantId = _tenantId,
        SubjectId = Guid.CreateVersion7(),
        CredentialId = "existing-cred",
        PublicKey = [],
        SignCount = 0,
    });
    await _dbContext.SaveChangesAsync();

    var state = new RecoveryModeState { IsSetupRequired = false };
    var request = new SetupOptionsRequest { Username = "admin", DisplayName = "Admin" };

    // Act
    var result = await _controller.SetupOptions(request, state);

    // Assert
    var objectResult = Assert.IsType<ObjectResult>(result.Result);
    objectResult.StatusCode.Should().Be(403);
}
```

**Step 2: Run to confirm they fail**

```
dotnet test tests/Unit/Nocturne.API.Tests -v minimal --filter "FullyQualifiedName~PasskeyControllerTests.SetupOptions_MultiTenant|FullyQualifiedName~PasskeyControllerTests.SetupComplete_MultiTenant"
```

Expected: fail — current code returns 403 (IsSetupRequired is false) instead of OK.

---

### Task 5: Update `PasskeyController` — implementation

**Files:**
- Modify: `src/API/Nocturne.API/Controllers/PasskeyController.cs`

#### `SetupOptions` (around line 487)

**Step 1: Replace the guard and tenant lookup in `SetupOptions`**

Find:
```csharp
if (!state.IsSetupRequired)
{
    return Problem(detail: "Setup mode is not active", statusCode: 403, title: "Forbidden");
}
```

Replace with:
```csharp
// Single-tenant: RecoveryModeState.IsSetupRequired flag
// Multi-tenant: no passkey credentials yet for this tenant (PasskeyCredentialEntity is ITenantScoped)
var tenantNeedsSetup = state.IsSetupRequired ||
    (_tenantAccessor.IsResolved && !await _dbContext.PasskeyCredentials.AnyAsync());
if (!tenantNeedsSetup)
{
    return Problem(detail: "Setup mode is not active", statusCode: 403, title: "Forbidden");
}
```

Find:
```csharp
// Find the default tenant (created by DefaultTenantSeeder on startup)
var defaultTenant = await _dbContext.Tenants
    .IgnoreQueryFilters()
    .FirstOrDefaultAsync(t => t.IsDefault);

if (defaultTenant == null)
{
    return Problem(detail: "Default tenant not found — restart the application", statusCode: 500, title: "Server Error");
}
```

Replace with:
```csharp
// Use the resolved tenant (multi-tenant) or fall back to the default tenant (single-tenant)
var tenantId = _tenantAccessor.IsResolved
    ? _tenantAccessor.TenantId
    : (await _dbContext.Tenants.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.IsDefault))?.Id
        ?? Guid.Empty;

if (tenantId == Guid.Empty)
{
    return Problem(detail: "Tenant not found — restart the application", statusCode: 500, title: "Server Error");
}
```

Then replace every reference to `defaultTenant.Id` with `tenantId` in `SetupOptions`:
- `r.TenantId == defaultTenant.Id` → `r.TenantId == tenantId`
- `await _tenantService.AddMemberAsync(defaultTenant.Id, ...)` → `await _tenantService.AddMemberAsync(tenantId, ...)`
- `subjectId, request.Username.Trim(), defaultTenant.Id` → `subjectId, request.Username.Trim(), tenantId`

#### `SetupComplete` (around line 578)

**Step 2: Replace the guard and tenant lookup in `SetupComplete`**

Find:
```csharp
if (!state.IsSetupRequired)
{
    return Problem(detail: "Setup mode is not active", statusCode: 403, title: "Forbidden");
}
```

Replace with:
```csharp
var tenantNeedsSetup = state.IsSetupRequired ||
    (_tenantAccessor.IsResolved && !await _dbContext.PasskeyCredentials.AnyAsync());
if (!tenantNeedsSetup)
{
    return Problem(detail: "Setup mode is not active", statusCode: 403, title: "Forbidden");
}
```

Find:
```csharp
// Find the default tenant
var defaultTenant = await _dbContext.Tenants
    .IgnoreQueryFilters()
    .FirstOrDefaultAsync(t => t.IsDefault);

if (defaultTenant == null)
{
    return Problem(detail: "Default tenant not found", statusCode: 500, title: "Server Error");
}
```

Replace with:
```csharp
var tenantId = _tenantAccessor.IsResolved
    ? _tenantAccessor.TenantId
    : (await _dbContext.Tenants.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.IsDefault))?.Id
        ?? Guid.Empty;

if (tenantId == Guid.Empty)
{
    return Problem(detail: "Tenant not found", statusCode: 500, title: "Server Error");
}
```

Then replace `defaultTenant.Id` with `tenantId`:
- `request.AttestationResponseJson, request.ChallengeToken, defaultTenant.Id` → `request.AttestationResponseJson, request.ChallengeToken, tenantId`

**Step 3: Run all passkey controller tests**

```
dotnet test tests/Unit/Nocturne.API.Tests -v minimal --filter "FullyQualifiedName~PasskeyControllerTests"
```

Expected: all pass, including the three new multi-tenant tests and all pre-existing tests.

**Step 4: Run the full test suite**

```
dotnet test tests/Unit/Nocturne.API.Tests -v minimal
```

Expected: all pass.

**Step 5: Commit**

```bash
git add src/API/Nocturne.API/Controllers/PasskeyController.cs
git add tests/Unit/Nocturne.API.Tests/Controllers/PasskeyControllerTests.cs
git commit -m "feat: update passkey setup endpoints for per-tenant setup in multi-tenant mode"
```

---

### Task 6: Final build and full test run

**Step 1: Full solution build**

```
dotnet build -v minimal
```

Expected: Build succeeded, 0 errors, 0 warnings relevant to this change.

**Step 2: Full test run**

```
dotnet test -v minimal
```

Expected: all pass.

**Step 3: Commit if anything was missed**

If any adjustments were made, commit them:
```bash
git add -A
git commit -m "fix: address issues found during final build and test run"
```
