using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using Moq;
using Nocturne.API.Middleware.Handlers;
using Nocturne.Connectors.Core.Utilities;
using Nocturne.Core.Contracts.Multitenancy;
using Nocturne.Core.Models.Authorization;
using Nocturne.Infrastructure.Data;
using Nocturne.Infrastructure.Data.Entities;
using Xunit;

namespace Nocturne.API.Tests.Middleware.Handlers;

public class ApiKeyHandlerTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<NocturneDbContext> _dbOptions;
    private readonly Mock<IDbContextFactory<NocturneDbContext>> _dbContextFactory;
    private readonly ApiKeyHandler _handler;

    private readonly Guid _testTenantId = Guid.CreateVersion7();
    private readonly Guid _subjectId = Guid.CreateVersion7();

    public ApiKeyHandlerTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _dbOptions = new DbContextOptionsBuilder<NocturneDbContext>()
            .UseSqlite(_connection)
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
            .Options;

        using (var ctx = new NocturneDbContext(_dbOptions) { TenantId = _testTenantId })
        {
            ctx.Database.EnsureCreated();

            ctx.Tenants.Add(new TenantEntity
            {
                Id = _testTenantId,
                Slug = "default",
                DisplayName = "Default",
                IsActive = true,
            });
            ctx.Subjects.Add(new SubjectEntity
            {
                Id = _subjectId,
                Name = "Test User",
                IsActive = true,
            });
            ctx.SaveChanges();
        }

        _dbContextFactory = new Mock<IDbContextFactory<NocturneDbContext>>();
        _dbContextFactory
            .Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new NocturneDbContext(_dbOptions) { TenantId = _testTenantId });

        var logger = new Mock<ILogger<ApiKeyHandler>>();
        _handler = new ApiKeyHandler(_dbContextFactory.Object, logger.Object);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    [Fact]
    public async Task AuthenticateAsync_NocPrefixedToken_LooksUpBySha256TokenHash()
    {
        var token = "noc_myapikey12345";
        var tokenHash = DirectGrantTokenHandler.ComputeSha256Hex(token);

        await using (var ctx = new NocturneDbContext(_dbOptions) { TenantId = _testTenantId })
        {
            ctx.OAuthGrants.Add(new OAuthGrantEntity
            {
                Id = Guid.CreateVersion7(),
                SubjectId = _subjectId,
                TenantId = _testTenantId,
                GrantType = OAuthGrantTypes.Direct,
                TokenHash = tokenHash,
                Scopes = ["entries.read", "treatments.readwrite"],
                CreatedAt = DateTime.UtcNow,
            });
            await ctx.SaveChangesAsync();
        }

        var context = CreateHttpContext();
        context.Request.Headers["api-secret"] = token;

        var result = await _handler.AuthenticateAsync(context);

        Assert.True(result.Succeeded);
        Assert.NotNull(result.AuthContext);
        Assert.Equal(AuthType.ApiKey, result.AuthContext!.AuthType);
        Assert.Equal(_subjectId, result.AuthContext.SubjectId);
        Assert.Contains("entries.read", result.AuthContext.Scopes);
        Assert.Contains("treatments.readwrite", result.AuthContext.Scopes);
    }

    [Fact]
    public async Task AuthenticateAsync_NonPrefixedValue_LooksUpBySha1LegacySecretHash()
    {
        var legacySecret = "myplaintextsecret";
        var sha1Hash = HashUtils.Sha1Hex(legacySecret);

        await using (var ctx = new NocturneDbContext(_dbOptions) { TenantId = _testTenantId })
        {
            ctx.OAuthGrants.Add(new OAuthGrantEntity
            {
                Id = Guid.CreateVersion7(),
                SubjectId = _subjectId,
                TenantId = _testTenantId,
                GrantType = OAuthGrantTypes.Direct,
                LegacySecretHash = sha1Hash,
                Scopes = ["entries.read"],
                CreatedAt = DateTime.UtcNow,
            });
            await ctx.SaveChangesAsync();
        }

        var context = CreateHttpContext();
        context.Request.Headers["api-secret"] = sha1Hash;

        var result = await _handler.AuthenticateAsync(context);

        Assert.True(result.Succeeded);
        Assert.NotNull(result.AuthContext);
        Assert.Equal(AuthType.ApiKey, result.AuthContext!.AuthType);
        Assert.Equal(_subjectId, result.AuthContext.SubjectId);
        Assert.Contains("entries.read", result.AuthContext.Scopes);
    }

    [Fact]
    public async Task AuthenticateAsync_RevokedGrant_ReturnsFailure()
    {
        var token = "noc_revokedkey123";
        var tokenHash = DirectGrantTokenHandler.ComputeSha256Hex(token);

        await using (var ctx = new NocturneDbContext(_dbOptions) { TenantId = _testTenantId })
        {
            ctx.OAuthGrants.Add(new OAuthGrantEntity
            {
                Id = Guid.CreateVersion7(),
                SubjectId = _subjectId,
                TenantId = _testTenantId,
                GrantType = OAuthGrantTypes.Direct,
                TokenHash = tokenHash,
                Scopes = ["entries.read"],
                CreatedAt = DateTime.UtcNow,
                RevokedAt = DateTime.UtcNow,
            });
            await ctx.SaveChangesAsync();
        }

        var context = CreateHttpContext();
        context.Request.Headers["api-secret"] = token;

        var result = await _handler.AuthenticateAsync(context);

        Assert.False(result.Succeeded);
        Assert.False(result.ShouldSkip);
        Assert.NotNull(result.Error);
    }

    [Fact]
    public async Task AuthenticateAsync_MissingHeader_ReturnsSkip()
    {
        var context = CreateHttpContext();

        var result = await _handler.AuthenticateAsync(context);

        Assert.True(result.ShouldSkip);
        Assert.False(result.Succeeded);
    }

    [Fact]
    public async Task AuthenticateAsync_NoMatchingGrant_ReturnsFailure()
    {
        var context = CreateHttpContext();
        context.Request.Headers["api-secret"] = "noc_nonexistentkey";

        var result = await _handler.AuthenticateAsync(context);

        Assert.False(result.Succeeded);
        Assert.False(result.ShouldSkip);
        Assert.NotNull(result.Error);
    }

    [Fact]
    public async Task AuthenticateAsync_ResolvedGrantScopes_AreUsedNotHardcoded()
    {
        var token = "noc_scopedkey456";
        var tokenHash = DirectGrantTokenHandler.ComputeSha256Hex(token);

        await using (var ctx = new NocturneDbContext(_dbOptions) { TenantId = _testTenantId })
        {
            ctx.OAuthGrants.Add(new OAuthGrantEntity
            {
                Id = Guid.CreateVersion7(),
                SubjectId = _subjectId,
                TenantId = _testTenantId,
                GrantType = OAuthGrantTypes.Direct,
                TokenHash = tokenHash,
                Scopes = ["entries.read"],
                CreatedAt = DateTime.UtcNow,
            });
            await ctx.SaveChangesAsync();
        }

        var context = CreateHttpContext();
        context.Request.Headers["api-secret"] = token;

        var result = await _handler.AuthenticateAsync(context);

        Assert.True(result.Succeeded);
        Assert.Single(result.AuthContext!.Scopes);
        Assert.Equal("entries.read", result.AuthContext.Scopes[0]);
        Assert.DoesNotContain("*", result.AuthContext.Permissions);
    }

    [Fact]
    public async Task AuthenticateAsync_SecretQueryParam_CheckedWhenHeaderAbsent()
    {
        var token = "noc_queryparam789";
        var tokenHash = DirectGrantTokenHandler.ComputeSha256Hex(token);

        await using (var ctx = new NocturneDbContext(_dbOptions) { TenantId = _testTenantId })
        {
            ctx.OAuthGrants.Add(new OAuthGrantEntity
            {
                Id = Guid.CreateVersion7(),
                SubjectId = _subjectId,
                TenantId = _testTenantId,
                GrantType = OAuthGrantTypes.Direct,
                TokenHash = tokenHash,
                Scopes = ["entries.read", "treatments.read"],
                CreatedAt = DateTime.UtcNow,
            });
            await ctx.SaveChangesAsync();
        }

        var context = CreateHttpContext();
        context.Request.QueryString = new QueryString($"?secret={token}");

        var result = await _handler.AuthenticateAsync(context);

        Assert.True(result.Succeeded);
        Assert.NotNull(result.AuthContext);
        Assert.Equal(AuthType.ApiKey, result.AuthContext!.AuthType);
        Assert.Equal(_subjectId, result.AuthContext.SubjectId);
    }

    [Fact]
    public void Priority_Is400()
    {
        Assert.Equal(400, _handler.Priority);
    }

    [Fact]
    public void Name_IsApiKeyHandler()
    {
        Assert.Equal("ApiKeyHandler", _handler.Name);
    }

    [Fact]
    public async Task AuthenticateAsync_NoTenantContext_ReturnsFailure()
    {
        var context = new DefaultHttpContext();
        context.Request.Headers["api-secret"] = "noc_sometoken";

        var result = await _handler.AuthenticateAsync(context);

        Assert.False(result.Succeeded);
        Assert.False(result.ShouldSkip);
    }

    private DefaultHttpContext CreateHttpContext()
    {
        var context = new DefaultHttpContext();
        context.Items["TenantContext"] = new TenantContext(_testTenantId, "default", "Default", true);
        return context;
    }
}
