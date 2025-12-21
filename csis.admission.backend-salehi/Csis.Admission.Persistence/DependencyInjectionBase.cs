/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common;
using Csis.Admission.Persistence.DataSeeders;
using Csis.Admission.Persistence.Interceptors;
using Csis.Admission.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Csis.Admission.Persistence;

/// <summary>
/// Dependency registrar for persistence layer
/// </summary>
public static partial class DependencyInjection
{
    /// <summary>
    /// Adds persistence layer services and repositories
    /// </summary>
    public static void AddPersistenceAsync(this IServiceCollection services, IConfiguration config) {
        services.AddPersistenceServices(config);
        services.AddAppDbContext(config);
        services.AddGenericRepositories();
        services.AddMainRepositories();
        services.AddRepositories(config);
    }

    /// <summary>
    /// Registers AppDbContext services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="dbOptions">Database options from configuration</param>
    /// <param name="loggerFactory"></param>
    /// <returns></returns>
    private static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration) {
        var dbOptions = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();

        void ConfigureOptions(DbContextOptionsBuilder opts) {
            if ( dbOptions.UseInMemoryDatabase ) {
                opts.UseInMemoryDatabase("AppDb");
            } else {
                opts.UseSqlServer(dbOptions.ConnectionStrings.SqlServer, sql =>
                    sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                    .MigrationsHistoryTable(Constants.Db.MigrationsTableName, Constants.Db.DefaultSchema));
            }

            if ( dbOptions.EnableLogging ) {
                opts.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }));
                if ( dbOptions.EnableSensitiveDataLogging ) {
                    opts.EnableSensitiveDataLogging().EnableDetailedErrors();
                }
            }
        }

        services.AddSingleton<BaseEntitySaveChangesInterceptor>();

        if ( dbOptions.EnablePooling ) {

            var poolSize = dbOptions.MaxPoolSize.HasValue &&
                dbOptions.MaxPoolSize.Value > 0 ?
                dbOptions.MaxPoolSize.Value : 1024;
            services.AddDbContextPool<AppDbContext>((serviceProvider, optionsBuilder) => {
                ConfigureOptions(optionsBuilder);
                optionsBuilder.AddInterceptors(serviceProvider.GetRequiredService<BaseEntitySaveChangesInterceptor>());

                AddInterceptors(serviceProvider, optionsBuilder, configuration);
            }, poolSize);
        } else {
            services.AddDbContext<AppDbContext>((serviceProvider, optionsBuilder) => {
                ConfigureOptions(optionsBuilder);
                optionsBuilder.AddInterceptors(serviceProvider.GetRequiredService<BaseEntitySaveChangesInterceptor>());

                AddInterceptors(serviceProvider, optionsBuilder, configuration);
            });
        }
    }

    /// <summary>
    /// Registers generic repositories
    /// </summary>
    /// <param name="services"></param>
    private static void AddGenericRepositories(this IServiceCollection services) {
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }

    private static void AddMainRepositories(this IServiceCollection services) {
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<IBackgroundServiceReportRepository, BackgroundServiceReportRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
    }

    /// <summary>
    /// Run data seeders if <see cref="DatabaseOptions.RunSeeders"/> is enabled
    /// </summary>
    /// <param name="services"></param>
    public static async Task RunDataSeedersAsync(this IServiceProvider serviceProvider, IConfiguration configuration) {
        var seeders = typeof(IDataSeeder).Assembly
            .GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract &&
                type.GetInterfaces().Contains(typeof(IDataSeeder)))
            .ToArray();

        if ( seeders.Length > 0 ) {
            foreach ( var seeder in seeders ) {
                await ((IDataSeeder) Activator.CreateInstance(seeder)).SeedAsync(serviceProvider, configuration);
            }
        }
    }
}
