using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nocturne.Core.Contracts;
using Nocturne.Core.Contracts.Multitenancy;
using Nocturne.Core.Contracts.Repositories;
using Nocturne.Infrastructure.Data.Abstractions;
using Nocturne.Infrastructure.Data.Configuration;
using Nocturne.Infrastructure.Data.Entities;
using Nocturne.Infrastructure.Data.Interceptors;
using Nocturne.Infrastructure.Data.Repositories;
using Nocturne.Infrastructure.Data.Services;

namespace Nocturne.Infrastructure.Data.Extensions;

/// <summary>
/// Service collection extensions for PostgreSQL data infrastructure
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add PostgreSQL data services to the service collection
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection AddPostgreSqlInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Register configuration
        var configSection = configuration.GetSection(PostgreSqlConfiguration.SectionName);
        services.Configure<PostgreSqlConfiguration>(configSection);

        var postgreSqlConfig =
            configSection.Get<PostgreSqlConfiguration>() ?? new PostgreSqlConfiguration();

        // Validate configuration
        if (string.IsNullOrEmpty(postgreSqlConfig.ConnectionString))
        {
            throw new InvalidOperationException(
                "PostgreSQL connection string must be provided in configuration section 'PostgreSql:ConnectionString'"
            );
        }

        // Register NpgsqlDataSource as a singleton - this manages the connection pool
        var dataSourceBuilder = new Npgsql.NpgsqlDataSourceBuilder(
            postgreSqlConfig.ConnectionString
        );
        var dataSource = dataSourceBuilder.Build();
        services.AddSingleton(dataSource);

        // Use AddPooledDbContextFactory for multitenant context pooling
        services.AddPooledDbContextFactory<NocturneDbContext>(
            options =>
            {
                options.UseNpgsql(
                    dataSource,
                    npgsqlOptions =>
                    {
                        npgsqlOptions.EnableRetryOnFailure(
                            maxRetryCount: postgreSqlConfig.MaxRetryCount,
                            maxRetryDelay: TimeSpan.FromSeconds(postgreSqlConfig.MaxRetryDelaySeconds),
                            errorCodesToAdd: null
                        );

                        npgsqlOptions.CommandTimeout(postgreSqlConfig.CommandTimeoutSeconds);
                    }
                );

                if (postgreSqlConfig.EnableSensitiveDataLogging)
                {
                    options.EnableSensitiveDataLogging();
                }

                if (postgreSqlConfig.EnableDetailedErrors)
                {
                    options.EnableDetailedErrors();
                }

                options.EnableServiceProviderCaching();
                options.AddInterceptors(new TenantConnectionInterceptor());
            },
            poolSize: 128
        );

        // Register scoped NocturneDbContext that sets TenantId from ITenantAccessor.
        // All existing constructor injections of NocturneDbContext continue to work.
        // The context is returned to the pool when the scope ends.
        services.AddScoped(sp =>
        {
            var factory = sp.GetRequiredService<IDbContextFactory<NocturneDbContext>>();
            var context = factory.CreateDbContext();
            var tenantAccessor = sp.GetService<ITenantAccessor>();
            if (tenantAccessor?.IsResolved == true)
            {
                context.TenantId = tenantAccessor.TenantId;
            }
            return context;
        });

        // Register deduplication service (required by repositories)
        services.AddScoped<IDeduplicationService, DeduplicationService>();

        // Register all repositories via their port interfaces
        services.AddScoped<IEntryRepository, EntryRepository>();
        services.AddScoped<ITreatmentRepository, TreatmentRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IDeviceStatusRepository, DeviceStatusRepository>();
        services.AddScoped<IFoodRepository, FoodRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();
        services.AddScoped<ISettingsRepository, SettingsRepository>();

        // Register Nightscout query parser
        services.AddScoped<IQueryParser, QueryParser>();

        return services;
    }

    /// <summary>
    /// Add PostgreSQL data services with explicit connection string
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="connectionString">PostgreSQL connection string</param>
    /// <param name="configure">Optional configuration action</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection AddPostgreSqlInfrastructure(
        this IServiceCollection services,
        string connectionString,
        Action<PostgreSqlConfiguration>? configure = null
    )
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException(
                "Connection string cannot be null or empty",
                nameof(connectionString)
            );
        }

        // Create and configure options
        var config = new PostgreSqlConfiguration { ConnectionString = connectionString };
        configure?.Invoke(config);

        // Validate connection string is still set after configure action
        if (string.IsNullOrEmpty(config.ConnectionString))
        {
            throw new InvalidOperationException(
                "Connection string was cleared by the configure action"
            );
        }

        // Register configuration
        services.Configure<PostgreSqlConfiguration>(options =>
        {
            options.ConnectionString = config.ConnectionString;
            options.EnableSensitiveDataLogging = config.EnableSensitiveDataLogging;
            options.EnableDetailedErrors = config.EnableDetailedErrors;
            options.MaxRetryCount = config.MaxRetryCount;
            options.MaxRetryDelaySeconds = config.MaxRetryDelaySeconds;
            options.CommandTimeoutSeconds = config.CommandTimeoutSeconds;
        });

        // Register NpgsqlDataSource as a singleton - this manages the connection pool
        var dataSourceBuilder = new Npgsql.NpgsqlDataSourceBuilder(config.ConnectionString);
        var dataSource = dataSourceBuilder.Build();
        services.AddSingleton(dataSource);

        // Use AddPooledDbContextFactory for multitenant context pooling
        services.AddPooledDbContextFactory<NocturneDbContext>(
            options =>
            {
                options.UseNpgsql(
                    dataSource,
                    npgsqlOptions =>
                    {
                        npgsqlOptions.EnableRetryOnFailure(
                            maxRetryCount: config.MaxRetryCount,
                            maxRetryDelay: TimeSpan.FromSeconds(config.MaxRetryDelaySeconds),
                            errorCodesToAdd: null
                        );

                        npgsqlOptions.CommandTimeout(config.CommandTimeoutSeconds);
                    }
                );

                if (config.EnableSensitiveDataLogging)
                {
                    options.EnableSensitiveDataLogging();
                }

                if (config.EnableDetailedErrors)
                {
                    options.EnableDetailedErrors();
                }

                options.EnableServiceProviderCaching();
                options.AddInterceptors(new TenantConnectionInterceptor());
            },
            poolSize: 128
        );

        // Register scoped NocturneDbContext that sets TenantId from ITenantAccessor.
        services.AddScoped(sp =>
        {
            var factory = sp.GetRequiredService<IDbContextFactory<NocturneDbContext>>();
            var context = factory.CreateDbContext();
            var tenantAccessor = sp.GetService<ITenantAccessor>();
            if (tenantAccessor?.IsResolved == true)
            {
                context.TenantId = tenantAccessor.TenantId;
            }
            return context;
        });

        // Register deduplication service (required by repositories)
        services.AddScoped<IDeduplicationService, DeduplicationService>();

        // Register all repositories via their port interfaces
        services.AddScoped<IEntryRepository, EntryRepository>();
        services.AddScoped<ITreatmentRepository, TreatmentRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IDeviceStatusRepository, DeviceStatusRepository>();
        services.AddScoped<IFoodRepository, FoodRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();
        services.AddScoped<ISettingsRepository, SettingsRepository>();

        // Register Nightscout query parser
        services.AddScoped<IQueryParser, QueryParser>();

        return services;
    }

    /// <summary>
    /// Ensure the database is created and up to date
    /// </summary>
    /// <param name="serviceProvider">Service provider</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task</returns>
    public static async Task EnsureDatabaseCreatedAsync(
        this IServiceProvider serviceProvider,
        CancellationToken cancellationToken = default
    )
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NocturneDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<NocturneDbContext>>();

        try
        {
            logger.LogInformation("Ensuring PostgreSQL database is created and up to date");
            await context.Database.EnsureCreatedAsync(cancellationToken);
            logger.LogInformation("PostgreSQL database is ready");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to ensure PostgreSQL database is created");
            throw;
        }
    }

    /// <summary>
    /// Run database migrations
    /// </summary>
    /// <param name="serviceProvider">Service provider</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task</returns>
    public static async Task MigrateDatabaseAsync(
        this IServiceProvider serviceProvider,
        CancellationToken cancellationToken = default
    )
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NocturneDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<NocturneDbContext>>();

        try
        {
            logger.LogInformation("Running PostgreSQL database migrations");
            await context.Database.MigrateAsync(cancellationToken);
            logger.LogInformation("PostgreSQL database migrations completed");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to run PostgreSQL database migrations");
            throw;
        }

        await VerifyRowLevelSecurityAsync(scope.ServiceProvider, context, logger, cancellationToken);
    }

    /// <summary>
    /// Verifies that every table backing an <see cref="ITenantScoped"/> entity has
    /// Row Level Security enabled, forced, and at least one policy. Also warns if
    /// the current database user is a superuser or has BYPASSRLS — both silently
    /// defeat all RLS enforcement regardless of FORCE ROW LEVEL SECURITY.
    ///
    /// This runs on every startup after migrations so that accidentally adding a
    /// new tenant-scoped table without an accompanying RLS migration fails loud
    /// instead of silently leaking PHI across tenants.
    /// </summary>
    private static async Task VerifyRowLevelSecurityAsync(
        IServiceProvider services,
        NocturneDbContext context,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        // Discover every tenant-scoped table name by walking the EF model rather
        // than a hardcoded list — that way we can never drift out of sync with
        // new entities.
        var tenantScopedTables = context.Model.GetEntityTypes()
            .Where(et => typeof(ITenantScoped).IsAssignableFrom(et.ClrType))
            .Select(et => et.GetTableName())
            .Where(name => !string.IsNullOrEmpty(name))
            .Distinct()
            .ToArray();

        if (tenantScopedTables.Length == 0)
        {
            return;
        }

        // pg_class.relrowsecurity = ENABLE ROW LEVEL SECURITY
        // pg_class.relforcerowsecurity = FORCE ROW LEVEL SECURITY (applies to table owner too)
        // A table without a policy silently rejects all rows instead of filtering,
        // which is safer but still a bug worth catching.
        const string sql = """
            SELECT c.relname,
                   c.relrowsecurity,
                   c.relforcerowsecurity,
                   (SELECT COUNT(*) FROM pg_policy p WHERE p.polrelid = c.oid) AS policy_count
            FROM pg_class c
            JOIN pg_namespace n ON n.oid = c.relnamespace
            WHERE n.nspname = 'public'
              AND c.relkind = 'r'
              AND c.relname = ANY({0});
            """;

        var rows = new List<(string Table, bool RlsEnabled, bool RlsForced, long PolicyCount)>();

        await using (var cmd = context.Database.GetDbConnection().CreateCommand())
        {
            if (cmd.Connection!.State != System.Data.ConnectionState.Open)
            {
                await cmd.Connection.OpenAsync(cancellationToken);
            }

            cmd.CommandText = sql.Replace("{0}", "@tables");
            var param = cmd.CreateParameter();
            param.ParameterName = "@tables";
            param.Value = tenantScopedTables;
            cmd.Parameters.Add(param);

            await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                rows.Add((
                    reader.GetString(0),
                    reader.GetBoolean(1),
                    reader.GetBoolean(2),
                    reader.GetInt64(3)));
            }
        }

        var foundTables = rows.Select(r => r.Table).ToHashSet(StringComparer.Ordinal);
        var missing = tenantScopedTables.Where(t => !foundTables.Contains(t!)).ToArray();
        var notEnabled = rows.Where(r => !r.RlsEnabled).Select(r => r.Table).ToArray();
        var notForced = rows.Where(r => r.RlsEnabled && !r.RlsForced).Select(r => r.Table).ToArray();
        var noPolicy = rows.Where(r => r.RlsEnabled && r.PolicyCount == 0).Select(r => r.Table).ToArray();

        var problems = new List<string>();
        if (missing.Length > 0)
        {
            problems.Add($"tables not found in database: {string.Join(", ", missing)}");
        }
        if (notEnabled.Length > 0)
        {
            problems.Add($"RLS not enabled on: {string.Join(", ", notEnabled)}");
        }
        if (notForced.Length > 0)
        {
            problems.Add($"FORCE ROW LEVEL SECURITY not set on: {string.Join(", ", notForced)}");
        }
        if (noPolicy.Length > 0)
        {
            problems.Add($"no policy defined on: {string.Join(", ", noPolicy)}");
        }

        if (problems.Count > 0)
        {
            var message =
                "Row Level Security self-check failed. Tenant-scoped tables must have RLS enabled, forced, and at least one policy. " +
                string.Join("; ", problems) +
                ". Add a migration that runs ENABLE + FORCE ROW LEVEL SECURITY and creates a tenant_isolation policy.";
            logger.LogCritical("{Message}", message);
            throw new InvalidOperationException(message);
        }

        // Secondary check: if the connected role bypasses RLS, all of the above
        // is cosmetic. This is the single most common silent failure mode — in
        // dev the app typically connects as the Postgres bootstrap superuser.
        await using (var roleCmd = context.Database.GetDbConnection().CreateCommand())
        {
            if (roleCmd.Connection!.State != System.Data.ConnectionState.Open)
            {
                await roleCmd.Connection.OpenAsync(cancellationToken);
            }

            roleCmd.CommandText =
                "SELECT current_user, rolsuper, rolbypassrls FROM pg_roles WHERE rolname = current_user";
            await using var reader = await roleCmd.ExecuteReaderAsync(cancellationToken);
            if (await reader.ReadAsync(cancellationToken))
            {
                var user = reader.GetString(0);
                var isSuper = reader.GetBoolean(1);
                var bypassRls = reader.GetBoolean(2);

                if (isSuper || bypassRls)
                {
                    logger.LogWarning(
                        "Database user '{User}' bypasses Row Level Security (superuser={IsSuper}, bypassrls={BypassRls}). " +
                        "Tenant isolation is NOT enforced at runtime. Switch the runtime connection to the non-privileged " +
                        "'nocturne_app' role to enable RLS enforcement.",
                        user, isSuper, bypassRls);
                }
                else
                {
                    logger.LogInformation(
                        "Row Level Security self-check passed for {Count} tenant-scoped tables (runtime role: {User})",
                        tenantScopedTables.Length, user);
                }
            }
        }
    }

    /// <summary>
    /// Add discrepancy analysis repository services
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection AddDiscrepancyAnalysisRepository(
        this IServiceCollection services
    )
    {
        services.AddScoped<IDiscrepancyAnalysisRepository, DiscrepancyAnalysisRepository>();
        return services;
    }

    /// <summary>
    /// Add alert-related repository services
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection AddAlertRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAlertTrackerRepository, AlertTrackerRepository>();
        services.AddScoped<ITrackerRepository, TrackerRepository>();
        services.AddScoped<IStateSpanRepository, StateSpanRepository>();
        services.AddScoped<ISystemEventRepository, SystemEventRepository>();
        services.AddScoped<ITreatmentFoodRepository, TreatmentFoodRepository>();
        services.AddScoped<IUserFoodFavoriteRepository, UserFoodFavoriteRepository>();
        services.AddScoped<EntryRepository>();
        services.AddScoped<TreatmentRepository>();
        return services;
    }
}
