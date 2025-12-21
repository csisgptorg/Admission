/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Settings;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Persistence;
using Csis.Admission.Services.BackgroundServices;
using Csis.Admission.Services.Cache;
using Csis.Utilities;
using Csis.Utilities.Extensions;
using HealthChecks.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace Csis.Admission.Services;

/// <summary>
/// Dependency registrar for services layer
/// </summary>
public static partial class DependencyInjection
{
    public static void AddServicesLayer(this IServiceCollection services, IConfiguration configuration) {
        services.AddScoped(typeof(ITrackingCodeService<>), typeof(TrackingCodeService<>));
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        services.AddTransient<ISettingsService, SettingsService>();
        services.AddSingleton<IDateTimeService, DateTimeService>();
        services.AddSingleton<IDistributedRequestQueue, DistributedRequestQueue>();
        services.AddSingleton<IDistributedPubSubService, DistributedPubSubService>();
        services.AddHttpClient<IStudentDataService, StudentDataService>();
        services.AddHttpClient<IEmployeeDataService, EmployeeDataService>();
        services.AddTransient<IPersonInfoService, PersonInfoService>();
        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IExcelFileService, ExcelFileService>();
        services.AddTransient<IIpAddressService, IpAddressService>();
        services.AddCustomServices(configuration);

        if ( GlobalOptions.RunBackgroundServices ) {
            services.AddHostedService<SendNotificationBackgroundService>();

            if ( !GlobalOptions.IsDevelopment ) {
                services.AddHostedService<SyncPermissionsBackgroundService>();
            }

            services.AddBackgroundServices(configuration);
        }

        services.AddCacheServices(configuration);
        services.AddHealthChecks(configuration);
    }

    private static void AddCacheServices(this IServiceCollection services, IConfiguration configuration) {
        var redisOptions = configuration.GetSection(nameof(RedisOptions)).Get<RedisOptions>();
        if ( redisOptions is null || redisOptions.Host is null ) {
            throw new Exception("Invalid redis host provided");
        }

        services.AddMemoryCache();
        var projectName = GetProjectName();
        var prefix = $"{projectName}_";
        GlobalOptions.SetRedisPrefix(prefix);
        services.AddStackExchangeRedisCache(options => {
            options.InstanceName = prefix;
            options.ConfigurationOptions = GetRedisConfiguration(redisOptions, projectName);
        });
        services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.ConnectAsync(GetRedisConfiguration(redisOptions, projectName)).GetAwaiter().GetResult());
        services.AddSingleton<ICacheKeyService, CacheKeyService>();
        services.AddSingleton(typeof(ICacheKeyService<>), typeof(CacheKeyService<>));
        services.AddSingleton<IDistributedCacheService, DistributedCacheService>();
        services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
    }

    private static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration) {
        var elasticSearchOptions = configuration.GetSection(nameof(ElasticSearchOptions)).Get<ElasticSearchOptions>() ?? new();
        var redisOptions = configuration.GetSection(nameof(RedisOptions)).Get<RedisOptions>() ?? new();
        var dbOptions = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();

        var healthChecks = services.AddHealthChecks();

        healthChecks
            .AddDbContextCheck<AppDbContext>(
                name: "Db Context",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["database", "dbcontext"],
                customTestQuery: async (dbContext, cancellationToken) => {
                    var text = StringHelper.Random(50, CharacterSet.AlphaNumeric);
                    var checkEntity = new HealthCheckTest {
                        CheckText = text
                    };

                    // Insert test record
                    await dbContext.Set<HealthCheckTest>().AddAsync(checkEntity, cancellationToken);
                    await dbContext.SaveChangesAsync(cancellationToken);

                    if ( checkEntity.Id == 0 ) {
                        return false;
                    }

                    // Read inserted entity from database
                    var checkEntityFromDb = await dbContext.Set<HealthCheckTest>().AsTracking().FirstOrDefaultAsync(x => x.Id == checkEntity.Id, cancellationToken: cancellationToken);
                    if ( checkEntityFromDb is null || !checkEntityFromDb.CheckText.Equals(text) ) {
                        return false;
                    }

                    // Cleanup
                    dbContext.Remove(checkEntityFromDb);
                    await dbContext.SaveChangesAsync(cancellationToken);

                    return true;
                })
            .AddSqlServer(
                new SqlServerHealthCheckOptions {
                    ConnectionString = dbOptions.ConnectionStrings.SqlServer
                },
                name: "SQL Server",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["database", "sqlserver"],
                timeout: TimeSpan.FromSeconds(2));

        if ( elasticSearchOptions.Enabled ) {
            foreach ( var node in elasticSearchOptions.Nodes ) {
                healthChecks
                    .AddElasticsearch(
                        node,
                        $"Elastic Search Node {node}",
                        failureStatus: HealthStatus.Unhealthy,
                        tags: ["log", "elastic"],
                        timeout: TimeSpan.FromSeconds(3));
            }
        }

        if ( redisOptions.Host.HasValue() ) {
            healthChecks
                .AddRedis(
                    sp => {
                        return ConnectionMultiplexer.Connect(GetRedisConfiguration(redisOptions, GetProjectName()));
                    },
                    name: "Redis",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: ["redis", "cache"]);
        }

        AddHealthChecks(healthChecks, configuration);
    }

    private static string GetProjectName() {
        return typeof(DependencyInjection).Assembly.GetName().Name.Replace(".Services", "");
    }

    private static ConfigurationOptions GetRedisConfiguration(RedisOptions redisOptions, string projectName) {
        return new ConfigurationOptions {
            EndPoints = {
                { redisOptions.Host, redisOptions.Port }
            },
            KeepAlive = redisOptions.KeepAliveInSeconds,
            ConnectRetry = redisOptions.ConnectRetry,
            ConnectTimeout = redisOptions.TimeOutInSeconds * 1000,
            ClientName = projectName,
            User = redisOptions.Username,
            Password = redisOptions.Password
        };
    }
}
