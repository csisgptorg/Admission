/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Domain.Common;
using Csis.Admission.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System.Linq.Expressions;

namespace Csis.Admission.IntegrationTests;

[SetUpFixture]
[SetCulture("en")]
internal partial class Testing
{
    private static CustomWebApplicationFactory _factory;
    private static IConfiguration _configuration;
    internal static IServiceScopeFactory _scopeFactory;
    private static Respawner _respawn;

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests() {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();

        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();

        _respawn = await Respawner.CreateAsync(_configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>().ConnectionStrings.SqlServerTest, new RespawnerOptions {
            TablesToIgnore = [
                Template.Application.Common.Constants.Db.MigrationsTableName,
                "SettingsHistory"
            ]
        });
    }

    public static async Task SendAsync(IRequest request) {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(request);
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        return await mediator.Send(request);
    }

    public static async Task ResetState() {
        await _respawn.ResetAsync(_configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>().ConnectionStrings.SqlServerTest);
        _factory.ClearCache();
        CustomWebApplicationFactory.ResetMocks();

        await SeedData();
    }

    public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task<TEntity> GetOneAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
        where TEntity : class {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var query = context.Set<TEntity>().AsQueryable();

        if ( predicate is not null ) {
            query = query.Where(predicate);
        }

        return await query.FirstOrDefaultAsync();
    }

    public static async Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null, bool includeDeleted = false)
        where TEntity : class {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var query = context.Set<TEntity>().AsQueryable();

        if ( predicate is not null ) {
            query = query.Where(predicate);
        }

        if ( includeDeleted ) {
            return await query.IgnoreQueryFilters().ToListAsync();
        }

        return await query.ToListAsync();
    }

    public static async Task<TEntity> GetByIdAsync<TEntity>(int id, Expression<Func<TEntity, object>> include = null, bool includeDeleted = false)
        where TEntity : class, IEntity {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var query = context.Set<TEntity>()
            .Where(x => x.Id == id);

        if ( include is not null ) {
            query = query.Include(include);
        }

        if ( includeDeleted ) {
            return await query.IgnoreQueryFilters().FirstOrDefaultAsync();
        }

        return await query.AsTracking().FirstOrDefaultAsync();
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public static async Task UpdateAsync<TEntity>(TEntity entity)
        where TEntity : class {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Update(entity);
        await context.SaveChangesAsync();
    }

    public static async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    public static async Task SeedData() {
        await Task.CompletedTask;
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests() {
        _factory?.Dispose();
    }
}
