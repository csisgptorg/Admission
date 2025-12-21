/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Csis.Admission.Application.Common;
using Csis.Admission.Domain.Common;
using System.Linq.Expressions;

namespace Csis.Admission.Persistence.Repositories;

internal class Repository<TEntity, TKey>(
    AppDbContext dbContext,
    IMemoryCacheService cache,
    ICacheKeyService<TKey> cacheKeyService,
    IOptions<CacheOptions> cacheOptions,
    ICurrentUserService currentUserService) : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    #region Fields
    protected readonly AppDbContext _dbContext = dbContext;
    protected readonly IMemoryCacheService _cache = cache;
    protected readonly ICacheKeyService<TKey> _cacheKeyService = cacheKeyService;
    protected readonly ICurrentUserService _currentUserService = currentUserService;
    protected readonly CacheOptions _cacheOptions = cacheOptions.Value ?? new();
    protected readonly IConfigurationProvider _mappingProvider = MapperProvider.MapperConfiguration;
    protected bool _disposed = false;
    protected static readonly string[] _emptyNavigations = [""]; // Used to overcome ambiguous method calls
    #endregion

    #region Properties
    /// <summary>
    /// Get entity table query as no tracking
    /// </summary>
    protected IQueryable<TEntity> QueryNoTracking => _dbContext.Set<TEntity>().AsNoTracking();

    /// <summary>
    /// Get entity table query as tracking
    /// </summary>
    protected IQueryable<TEntity> QueryTracking => _dbContext.Set<TEntity>().AsTracking();

    /// <summary>
    /// Get entity DbSet
    /// </summary>
    protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();
    #endregion

    #region GetById    
    /// <inheritdoc/>
    public async Task<TEntity> GetByIdAsync(TKey id, bool includeDeleted = false, bool ignoreCache = false, CancellationToken cancellationToken = default) {
        if ( ignoreCache ) {
            return await AddDeletedFilter(QueryNoTracking, includeDeleted).FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        return await _cache.GetOrSetAsync(_cacheKeyService.GetEntityKey<TEntity>(id), async () => {
            return await AddDeletedFilter(QueryNoTracking, includeDeleted).FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }, GetCacheOptions(), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByIdAsTrackingAsync(TKey id, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetById(id, includeDeleted, true, cancellationToken, _emptyNavigations);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByIdAsync(TKey id, Expression<Func<TEntity, object>> navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetById(id, includeDeleted, false, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByIdAsync(TKey id, string navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetById(id, includeDeleted, false, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByIdAsTrackingAsync(TKey id, Expression<Func<TEntity, object>> navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetById(id, includeDeleted, true, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByIdAsTrackingAsync(TKey id, string navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetById(id, includeDeleted, true, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        return await GetById(id, false, false, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default, params string[] navigations) {
        return await GetById(id, false, false, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByIdAsTrackingAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        return await GetById(id, false, true, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetByIdAsTrackingAsync(TKey id, CancellationToken cancellationToken = default, params string[] navigations) {
        return await GetById(id, false, true, cancellationToken, navigations);
    }

    /// <summary>
    /// Helper method to get entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="asTracking"></param>
    /// <param name="navigations"></param>
    /// <returns></returns>
    private async Task<TEntity> GetById(TKey id, bool includeDeleted, bool asTracking, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        var query = AddDeletedFilter(asTracking ? QueryTracking : QueryNoTracking, includeDeleted);
        query = ApplyIncludes(query, navigations);

        return await query.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    /// <summary>
    /// Helper method to get entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="asTracking"></param>
    /// <param name="navigations"></param>
    /// <returns></returns>
    private async Task<TEntity> GetById(TKey id, bool includeDeleted, bool asTracking, CancellationToken cancellationToken = default, params string[] navigations) {
        var query = AddDeletedFilter(asTracking ? QueryTracking : QueryNoTracking, includeDeleted);
        query = ApplyIncludes(query, navigations);

        return await query.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TDto> GetByIdAsync<TDto>(TKey id, bool includeDeleted = false, bool ignoreCache = false, CancellationToken cancellationToken = default)
        where TDto : class, new() {
        if ( ignoreCache ) {
            return await AddDeletedFilter(QueryNoTracking, includeDeleted)
                .Where(x => x.Id.Equals(id))
                .ProjectTo<TDto>(_mappingProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        return await _cache.GetOrSetAsync(_cacheKeyService.GetDtoKey<TEntity, TDto>(id), async () => {
            return await AddDeletedFilter(QueryNoTracking, includeDeleted)
                .Where(x => x.Id.Equals(id))
                .ProjectTo<TDto>(_mappingProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }, GetCacheOptions(), cancellationToken);
    }
    #endregion

    #region GetByIds
    /// <inheritdoc/>
    public async Task<List<TEntity>> GetByIdsAsync(List<TKey> ids, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetByIds(ids, includeDeleted, false, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetByIdsAsTrackingAsync(List<TKey> ids, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetByIds(ids, includeDeleted, true, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetByIdsAsync(List<TKey> ids, Expression<Func<TEntity, object>> navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetByIds(ids, includeDeleted, false, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetByIdsAsTrackingAsync(List<TKey> ids, Expression<Func<TEntity, object>> navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetByIds(ids, includeDeleted, true, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetByIdsAsync(List<TKey> ids, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        return await GetByIds(ids, false, false, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetByIdsAsTrackingAsync(List<TKey> ids, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        return await GetByIds(ids, false, true, cancellationToken, navigations);
    }

    /// <summary>
    /// Helper method to get entities by id list
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="asTracking"></param>
    /// <param name="navigations"></param>
    /// <returns></returns>
    private async Task<List<TEntity>> GetByIds(List<TKey> ids, bool includeDeleted, bool asTracking, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        if ( ids is null || ids.Count == 0 ) {
            return [];
        }

        var query = AddDeletedFilter(asTracking ? QueryTracking : QueryNoTracking, includeDeleted);
        query = ApplyIncludes(query, navigations);

        return await query.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<TDto>> GetByIdsAsync<TDto>(List<TKey> ids, bool includeDeleted = false, CancellationToken cancellationToken = default)
        where TDto : class, new() {
        if ( ids is null || ids.Count == 0 ) {
            return [];
        }

        var query = AddDeletedFilter(QueryNoTracking, includeDeleted).Where(x => ids.Contains(x.Id));

        return await query.ProjectTo<TDto>(_mappingProvider).ToListAsync(cancellationToken);
    }
    #endregion

    #region GetOne
    /// <inheritdoc/>
    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetOne(predicate, includeDeleted, false, cancellationToken, _emptyNavigations);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetOneAsTrackingAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetOne(predicate, includeDeleted, true, cancellationToken, _emptyNavigations);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetOne(predicate, includeDeleted, false, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, string navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetOne(predicate, includeDeleted, false, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetOneAsTrackingAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetOne(predicate, includeDeleted, true, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetOneAsTrackingAsync(Expression<Func<TEntity, bool>> predicate, string navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetOne(predicate, includeDeleted, true, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        return await GetOne(predicate, false, false, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] navigations) {
        return await GetOne(predicate, false, false, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetOneAsTrackingAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        return await GetOne(predicate, false, true, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<TEntity> GetOneAsTrackingAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] navigations) {
        return await GetOne(predicate, false, true, cancellationToken, navigations);
    }

    /// <summary>
    /// Helper method to get one entity
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="asTracking"></param>
    /// <param name="navigations"></param>
    /// <returns></returns>
    private async Task<TEntity> GetOne(Expression<Func<TEntity, bool>> predicate, bool includeDeleted, bool asTracking, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        var query = AddDeletedFilter(asTracking ? QueryTracking : QueryNoTracking, includeDeleted).Where(predicate);
        query = ApplyIncludes(query, navigations);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Helper method to get one entity
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="asTracking"></param>
    /// <param name="navigations"></param>
    /// <returns></returns>
    private async Task<TEntity> GetOne(Expression<Func<TEntity, bool>> predicate, bool includeDeleted, bool asTracking, CancellationToken cancellationToken = default, params string[] navigations) {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        var query = AddDeletedFilter(asTracking ? QueryTracking : QueryNoTracking, includeDeleted).Where(predicate);
        query = ApplyIncludes(query, navigations);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TDto> GetOneAsync<TDto>(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, CancellationToken cancellationToken = default)
        where TDto : class, new() {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        var query = AddDeletedFilter(QueryNoTracking, includeDeleted).Where(predicate);

        return await query
            .ProjectTo<TDto>(_mappingProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
    #endregion

    #region GetAll
    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsync(bool includeDeleted = false, bool ignoreCache = false, CancellationToken cancellationToken = default) {
        if ( ignoreCache ) {
            return await AddDeletedFilter(QueryNoTracking, includeDeleted).ToListAsync(cancellationToken);
        }

        return await _cache.GetOrSetAsync(_cacheKeyService.GetEntityListKey<TEntity>(), async () => {
            return await AddDeletedFilter(QueryNoTracking, includeDeleted).ToListAsync(cancellationToken);
        }, GetCacheOptions(), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsTrackingAsync(bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetAll(x => true, includeDeleted, true, cancellationToken, _emptyNavigations);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetAll(predicate, includeDeleted, false, cancellationToken, _emptyNavigations);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsTrackingAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetAll(predicate, includeDeleted, true, cancellationToken, _emptyNavigations);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetAll(predicate, includeDeleted, false, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, string navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetAll(predicate, includeDeleted, false, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsTrackingAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetAll(predicate, includeDeleted, true, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsTrackingAsync(Expression<Func<TEntity, bool>> predicate, string navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetAll(predicate, includeDeleted, true, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetAll(x => true, includeDeleted, false, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsync(string navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetAll(x => true, includeDeleted, false, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsTrackingAsync(Expression<Func<TEntity, object>> navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetAll(x => true, includeDeleted, true, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsTrackingAsync(string navigation, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await GetAll(x => true, includeDeleted, true, cancellationToken, navigation);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        return await GetAll(predicate, false, false, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] navigations) {
        return await GetAll(predicate, false, false, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsTrackingAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        return await GetAll(predicate, false, true, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsTrackingAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] navigations) {
        return await GetAll(predicate, false, true, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        return await GetAll(x => true, false, false, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, params string[] navigations) {
        return await GetAll(x => true, false, false, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsTrackingAsync(CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        return await GetAll(x => true, false, true, cancellationToken, navigations);
    }

    /// <inheritdoc/>
    public async Task<List<TEntity>> GetAllAsTrackingAsync(CancellationToken cancellationToken = default, params string[] navigations) {
        return await GetAll(x => true, false, true, cancellationToken, navigations);
    }

    /// <summary>
    /// Helper method to get all entities
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="asTracking"></param>
    /// <param name="navigations"></param>
    /// <returns></returns>
    private async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, bool includeDeleted, bool asTracking, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigations) {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        var query = AddDeletedFilter(asTracking ? QueryTracking : QueryNoTracking, includeDeleted).Where(predicate);
        query = ApplyIncludes(query, navigations);

        return await query.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Helper method to get all entities
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="asTracking"></param>
    /// <param name="navigations"></param>
    /// <returns></returns>
    private async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, bool includeDeleted, bool asTracking, CancellationToken cancellationToken = default, params string[] navigations) {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        var query = AddDeletedFilter(asTracking ? QueryTracking : QueryNoTracking, includeDeleted).Where(predicate);
        query = ApplyIncludes(query, navigations);

        return await query.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<TDto>> GetAllAsync<TDto>(bool includeDeleted = false, bool ignoreCache = false, CancellationToken cancellationToken = default)
        where TDto : class, new() {
        if ( ignoreCache ) {
            return await AddDeletedFilter(QueryNoTracking, includeDeleted)
                .ProjectTo<TDto>(_mappingProvider)
                .ToListAsync(cancellationToken);
        }

        return await _cache.GetOrSetAsync(_cacheKeyService.GetDtoListKey<TEntity, TDto>(), async () => {
            return await AddDeletedFilter(QueryNoTracking, includeDeleted)
                .ProjectTo<TDto>(_mappingProvider)
                .ToListAsync(cancellationToken);
        }, GetCacheOptions(), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, CancellationToken cancellationToken = default)
        where TDto : class, new() {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        var query = AddDeletedFilter(QueryNoTracking, includeDeleted).Where(predicate);

        return await query.ProjectTo<TDto>(_mappingProvider).ToListAsync(cancellationToken);
    }
    #endregion

    #region SearchAll
    public async Task<List<TDto>> SearchAllAsync<TDto>(List<SearchFilter> searchFilters, Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, CancellationToken cancellationToken = default)
    where TDto : class, new() {
        var query = AddDeletedFilter(QueryNoTracking, includeDeleted);
        if ( searchFilters?.Count > 0 ) {
            query = query.ApplySearchFilters<TEntity, TKey>(searchFilters);
        }

        if ( predicate is not null ) {
            query = query.Where(predicate);
        }

        return await query.ProjectTo<TDto>(_mappingProvider).ToListAsync(cancellationToken);
    }

    public async Task<List<TDto>> SearchAllAsync<TDto>(List<SearchFilter> searchFilters, bool includeDeleted = false, CancellationToken cancellationToken = default)
       where TDto : class, new() {
        return await SearchAllAsync<TDto>(searchFilters, null, includeDeleted, cancellationToken);
    }
    #endregion

    #region GetAllPaged
    /// <inheritdoc/>
    public async Task<IPagedList<TEntity>> GetAllPagedAsync(
        int pageIndex,
        int pageSize,
        string sortExpression = "",
        Dictionary<string, string> propertyMappings = default,
        IEnumerable<string> ignoredProperties = default,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default) {

        if ( pageIndex <= 0 ) {
            pageIndex = 1;
        }

        return await AddDeletedFilter(QueryNoTracking, includeDeleted)
            .ToPagedListAsync<TEntity, TKey>(pageIndex, pageSize, sortExpression, propertyMappings, ignoredProperties, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IPagedList<TDto>> SearchPagedAsync<TDto>(
        List<SearchFilter> searchFilters,
        Expression<Func<TEntity, bool>> predicate,
        int pageIndex,
        int pageSize,
        string sortExpression = "",
        Dictionary<string, string> propertyMappings = null,
        IEnumerable<string> ignoredProperties = null,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default)
        where TDto : class, new() {
        if ( pageIndex <= 0 ) {
            pageIndex = 1;
        }

        var query = AddDeletedFilter(QueryNoTracking, includeDeleted);

        if ( searchFilters is not null && searchFilters.Count > 0 ) {
            query = query.ApplySearchFilters<TEntity, TKey>(searchFilters);
        }

        if ( predicate is not null ) {
            query = query.Where(predicate);
        }

        return await query.ToPagedListAsync<TEntity, TKey, TDto>(_mappingProvider, pageIndex, pageSize, sortExpression, propertyMappings, ignoredProperties, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IPagedList<TDto>> SearchPagedAsync<TDto>(
        List<SearchFilter> searchFilters,
        int pageIndex,
        int pageSize,
        string sortExpression = "",
        Dictionary<string, string> propertyMappings = null,
        IEnumerable<string> ignoredProperties = null,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default)
        where TDto : class, new() {
        return await SearchPagedAsync<TDto>(searchFilters, null, pageIndex, pageSize, sortExpression, propertyMappings, ignoredProperties, includeDeleted, cancellationToken);
    }

    public async Task<IPagedList<TDto>> SearchPagedAsync<TDto>(
        BaseSearchQuery searchQuery,
        CancellationToken cancellationToken = default)
        where TDto : class, new() {
        return await SearchPagedAsync<TDto>(
            searchQuery.SearchFilters,
            null,
            searchQuery.PageIndex,
            searchQuery.PageSize,
            searchQuery.SortBy,
            propertyMappings: null,
            ignoredProperties: null,
            includeDeleted: false,
            cancellationToken
        );
    }

    public async Task<IPagedList<TDto>> SearchPagedAsync<TDto>(
        BaseSearchQuery searchQuery,
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
       where TDto : class, new() {
        return await SearchPagedAsync<TDto>(
            searchQuery.SearchFilters,
            predicate,
            searchQuery.PageIndex,
            searchQuery.PageSize,
            searchQuery.SortBy,
            propertyMappings: null,
            ignoredProperties: null,
            includeDeleted: false,
            cancellationToken
        );
    }

    /// <inheritdoc/>
    public async Task<IPagedList<TDto>> GetAllPagedAsync<TDto>(
        int pageIndex,
        int pageSize,
        string sortExpression = "",
        Dictionary<string, string> propertyMappings = null,
        IEnumerable<string> ignoredProperties = null,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default)
        where TDto : class, new() {
        return await SearchPagedAsync<TDto>(null, null, pageIndex, pageSize, sortExpression, propertyMappings, ignoredProperties, includeDeleted, cancellationToken);
    }
    #endregion

    #region Exists
    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await AddDeletedFilter(QueryNoTracking, includeDeleted).Where(predicate).AnyAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsWithIdAsync(TKey id, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        return await AddDeletedFilter(QueryNoTracking, includeDeleted).Where(x => x.Id.Equals(id)).AnyAsync(cancellationToken);
    }
    #endregion

    #region Count
    /// <inheritdoc/>
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        if ( predicate is null ) {
            return await AddDeletedFilter(QueryNoTracking, includeDeleted).CountAsync(cancellationToken);
        }

        return await AddDeletedFilter(QueryNoTracking, includeDeleted).Where(predicate).CountAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<long> CountLongAsync(Expression<Func<TEntity, bool>> predicate = null, bool includeDeleted = false, CancellationToken cancellationToken = default) {
        if ( predicate is null ) {
            return await AddDeletedFilter(QueryNoTracking, includeDeleted).LongCountAsync(cancellationToken);
        }

        return await AddDeletedFilter(QueryNoTracking, includeDeleted).Where(predicate).LongCountAsync(cancellationToken);
    }
    #endregion

    #region Insert
    /// <inheritdoc/>
    public async Task InsertAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        await DbSet.AddAsync(entity, cancellationToken);
        await AutoSaveAsync(autoSave, cancellationToken);

        InvalidateCache(entity);
    }

    /// <inheritdoc/>
    public async Task InsertAsync(List<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        if ( entities.Count > 0 ) {

            await DbSet.AddRangeAsync(entities, cancellationToken);
            await AutoSaveAsync(autoSave, cancellationToken);
            InvalidateCache(entities);

        }
    }
    #endregion

    #region Bulk Insert
    public async Task BulkInsertAsync(List<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default) {
        await InsertAsync(entities, autoSave: false, cancellationToken: cancellationToken);

        if ( autoSave ) {
            await BulkSaveAsync(cancellationToken);
        }
    }
    #endregion

    #region Update
    /// <inheritdoc/>
    public async Task UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        _dbContext.Entry(entity).CurrentValues.SetValues(entity);
        await AutoSaveAsync(autoSave, cancellationToken);

        InvalidateCache(entity);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(List<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        if ( entities.Count > 0 ) {
            DbSet.UpdateRange(entities);
            await AutoSaveAsync(autoSave, cancellationToken);

            // If entities count is large, calculating and clearing cache entries will be time-consuming. Instead clear entire cache
            if ( entities.Count > 3000 ) {
                ClearCache();
            } else {
                InvalidateCache(entities);
            }
        }
    }
    #endregion

    #region Bulk Update
    public async Task BulkUpdateAsync(List<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default) {
        await UpdateAsync(entities, autoSave: false, cancellationToken: cancellationToken);

        if ( autoSave ) {
            await BulkSaveAsync(cancellationToken);
        }
    }
    #endregion

    #region Delete
    /// <inheritdoc/>
    public async Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        if ( entity is ISoftDeletedEntity softDeletedEntity ) {
            softDeletedEntity.SoftDelete(await _currentUserService.GetUserIdAsync(), await _currentUserService.GetDelegatedUserIdAsync());
            await UpdateAsync(entity, autoSave: false, cancellationToken: cancellationToken);
        } else if ( IsSoftDeletedType() ) {
            typeof(TEntity).GetMethod(nameof(ISoftDeletedEntity.SoftDelete)).Invoke(entity, [await _currentUserService.GetUserIdAsync(), await _currentUserService.GetDelegatedUserIdAsync()]);
            await UpdateAsync(entity, autoSave: false, cancellationToken: cancellationToken);
        } else {
            DbSet.Remove(entity);
            InvalidateCache(entity);
        }

        await AutoSaveAsync(autoSave, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(List<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        foreach ( var entity in entities ) {
            await DeleteAsync(entity, autoSave: false, cancellationToken: cancellationToken);
        }

        await AutoSaveAsync(autoSave, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(TKey id, bool autoSave = true, CancellationToken cancellationToken = default) {
        var entity = await GetByIdAsTrackingAsync(id, includeDeleted: false, cancellationToken: cancellationToken);

        if ( entity is not null ) {
            await DeleteAsync(entity, autoSave, cancellationToken);
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(List<TKey> ids, bool autoSave = true, CancellationToken cancellationToken = default) {
        var entities = await GetByIdsAsTrackingAsync(ids, includeDeleted: false, cancellationToken: cancellationToken);

        if ( entities.Count > 0 ) {
            await DeleteAsync(entities, autoSave, cancellationToken);
        }
    }

    /// <inheritdoc/>
    public async Task HardDeleteAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        DbSet.Remove(entity);
        InvalidateCache(entity);

        await AutoSaveAsync(autoSave, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task HardDeleteAsync(List<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        foreach ( var entity in entities ) {
            await HardDeleteAsync(entity, autoSave: false, cancellationToken: cancellationToken);
        }

        await AutoSaveAsync(autoSave, cancellationToken);
    }
    #endregion

    #region Save
    public int Save() {
        return _dbContext.SaveChanges();
    }

    /// <inheritdoc/>
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
    #endregion

    #region Bulk Save
    public void BulkSave() {
        _dbContext.BulkSave();
    }

    public async Task BulkSaveAsync(CancellationToken cancellationToken = default) {
        await _dbContext.BulkSaveAsync(cancellationToken: cancellationToken);
    }
    #endregion

    #region Helpers
    /// <summary>
    /// Do auto save
    /// </summary>
    /// <param name="isAutoSaved"></param>
    /// <returns></returns>
    protected async Task<int> AutoSaveAsync(bool isAutoSaved, CancellationToken cancellationToken = default) {
        return isAutoSaved ? await SaveAsync(cancellationToken) : 0;
    }

    /// <summary>
    /// Adds "deleted" filter to query which contains <see cref="ISoftDeletedEntity"/> entries, if needed
    /// </summary>
    /// <param name="includeDeleted">Whether to include deleted items</param>
    protected static IQueryable<TEntity> AddDeletedFilter(IQueryable<TEntity> query, in bool includeDeleted) {
        if ( !includeDeleted || !IsSoftDeletedType() ) {
            return query;
        }

        return query.IgnoreQueryFilters();
    }

    /// <summary>
    /// Check if <typeparamref name="TEntity"/> is a soft deleted entity
    /// </summary>
    /// <returns></returns>
    protected static bool IsSoftDeletedType() {
        return typeof(TEntity)
            .GetInterfaces()
            .Any(i => i == typeof(ISoftDeletedEntity) || (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISoftDeletedEntity<>)));
    }

    /// <summary>
    /// Apply includes for navigation properties
    /// </summary>
    /// <param name="query">Query</param>
    /// <param name="navigations">Navigations</param>
    /// <returns></returns>
    protected static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, Expression<Func<TEntity, object>>[] navigations) {
        if ( navigations is null || navigations.Length == 0 ) {
            return query;
        }

        foreach ( var navigation in navigations ) {
            if ( navigation is null ) {
                continue;
            }

            query = query.Include(navigation);
        }

        return query;
    }

    /// <summary>
    /// Apply includes for navigation properties
    /// </summary>
    /// <param name="query">Query</param>
    /// <param name="navigations">Navigations</param>
    /// <returns></returns>
    protected static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, string[] navigations) {
        if ( navigations is null || navigations.Length == 0 ) {
            return query;
        }

        foreach ( var navigation in navigations ) {
            if ( string.IsNullOrWhiteSpace(navigation) ) {
                continue;
            }

            query = query.Include(navigation);
        }

        return query;
    }

    private List<string> CalculateCacheKeys(IEnumerable<TEntity> entities) {
        var entityType = typeof(TEntity);
        List<string> keys = [_cacheKeyService.GetEntityListKey<TEntity>()];
        if ( entities is not null ) {
            foreach ( var entity in entities ) {
                keys.Add(_cacheKeyService.GetEntityKey<TEntity>(entity.Id));
                keys.Add(_cacheKeyService.GetDtoWildcardKey(entityType, entity.Id));
            }
        }

        keys.Add(_cacheKeyService.GetDtoListWildcardKey(entityType));
        keys.Add(_cacheKeyService.GetCustomWildcardKey(entityType));

        AddRelatedKeys(entityType, keys, true);

        return keys;

        void AddRelatedKeys(Type entityType, List<string> keys, bool firstLevel, List<Type> processedTypes = null) {
            processedTypes ??= [entityType];
            if ( ReflectionCache.EntityTree.TryGetValue(entityType, out var relations) ) {
                foreach ( var relation in relations ) {
                    if ( processedTypes.Contains(relation.Type) ) {
                        continue;
                    }

                    if ( firstLevel ) {
                        keys.Add(_cacheKeyService.GetEntityListKey(relation.Type));
                        keys.Add(_cacheKeyService.GetDtoListWildcardKey(relation.Type));
                        keys.Add(_cacheKeyService.GetCustomWildcardKey(relation.Type));

                        if ( entities is not null ) {
                            foreach ( var entity in entities ) {
                                var foreignKeyValue = relation.ForeignKey.GetValue(entity);
                                keys.Add(_cacheKeyService.GetEntityKey(relation.Type, foreignKeyValue));
                                if ( foreignKeyValue is not null ) {
                                    keys.Add(_cacheKeyService.GetDtoWildcardKey(relation.Type, foreignKeyValue));
                                }
                            }
                        }

                    } else {
                        keys.Add(_cacheKeyService.GetEntityWildcardKey(relation.Type));
                    }

                    processedTypes.Add(relation.Type);

                    if ( ReflectionCache.EntityTree.TryGetValue(relation.Type, out var nestedRelations) ) {
                        AddRelatedKeys(relation.Type, keys, false, processedTypes);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Get list of cache keys that should be invalidated when new entity inserted
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerable<string> GetCacheKeysToInvalidate() {
        return CalculateCacheKeys(entities: null)
            .Concat(GetExtraCacheKeysToInvalidate());
    }

    /// <summary>
    /// Get list of cache keys that should be invalidated when entity updated or deleted
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected virtual IEnumerable<string> GetCacheKeysToInvalidate(TEntity entity) {
        return CalculateCacheKeys([entity])
            .Concat(GetExtraCacheKeysToInvalidate(entity));
    }

    /// <summary>
    /// Get list of cache keys that should be invalidated when list of entities updated
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    protected virtual IEnumerable<string> GetCacheKeysToInvalidate(IEnumerable<TEntity> entities) {
        var keys = CalculateCacheKeys(entities).AsEnumerable();
        foreach ( var entity in entities ) {
            keys = keys.Concat(GetExtraCacheKeysToInvalidate(entity));
        }

        return keys.Distinct();
    }

    protected virtual IEnumerable<string> GetExtraCacheKeysToInvalidate() {
        return [];
    }

    protected virtual IEnumerable<string> GetExtraCacheKeysToInvalidate(TEntity entity) {
        return [];
    }

    /// <summary>
    /// Invalidate cache entries related to entity
    /// </summary>
    /// <param name="entity">Entity instance</param>
    protected void InvalidateCache(TEntity entity) {
        RemoveCacheByKeys(GetCacheKeysToInvalidate(entity));
    }

    /// <summary>
    /// Invalidate cache entries related to entities
    /// </summary>
    /// <param name="entities">Entity instances</param>
    protected void InvalidateCache(IEnumerable<TEntity> entities) {
        RemoveCacheByKeys(GetCacheKeysToInvalidate(entities));
    }

    /// <summary>
    /// Clear all cache entries
    /// </summary>
    protected void ClearCache() {
        _cache.Clear();
    }

    private void RemoveCacheByKeys(IEnumerable<string> keys) {
        var normalKeys = keys.Where(x => !x.Contains('*')).ToArray();
        var wildcardKeys = keys.Where(x => x.Contains('*')).ToArray();

        if ( normalKeys.Length > 0 ) {
            foreach ( var key in normalKeys ) {
                _cache.Remove(key);
            }
        }

        if ( wildcardKeys.Length > 0 ) {
            foreach ( var key in _cache.GetWildcardKeys(wildcardKeys) ) {
                _cache.Remove(key);
            }
        }
    }

    /// <summary>
    /// Get cache options used for current repository instance
    /// </summary>
    /// <returns></returns>
    protected virtual CacheOptions GetCacheOptions() {
        return _cacheOptions;
    }
    #endregion

    #region IDisposable
    /// <summary>
    /// Disposable implementation
    /// </summary>
    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose resources
    /// </summary>
    /// <param name="disposing"></param>
    private void Dispose(bool disposing) {
        if ( disposing && !_disposed ) {
            try {
                _dbContext?.Dispose();
                _disposed = true;
            } catch { }
        }
    }
    #endregion
}

internal class Repository<TEntity>(
    AppDbContext dbContext,
    IMemoryCacheService cache,
    ICacheKeyService<int> cacheKeyService,
    IOptions<CacheOptions> cacheOptions,
    ICurrentUserService currentUserService) : Repository<TEntity, int>(dbContext, cache, cacheKeyService, cacheOptions, currentUserService), IRepository<TEntity> where TEntity : class, IEntity
{
}
