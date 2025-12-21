/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;
using Csis.Paging;
using System.Linq.Expressions;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// Generic repository for entities
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TKey">Primary key type</typeparam>
public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="id">Record identifier</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="ignoreCache">Ignore cache and force fetch from database</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(
        TKey id,
        bool includeDeleted = false,
        bool ignoreCache = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="id">Record identifier</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsTrackingAsync(
        TKey id,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>    
    /// Get entity by id
    /// </summary>
    /// <param name="id">Record identifier</param>
    /// <param name="navigation">Navigation property to include</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(
        TKey id,
        Expression<Func<TEntity, object>> navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>    
    /// Get entity by id
    /// </summary>
    /// <param name="id">Record identifier</param>
    /// <param name="navigation">Navigation property to include as string. Use only for nested list navigations</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(
        TKey id,
        string navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>    
    /// Get entity by id
    /// </summary>
    /// <param name="id">Record identifier</param>
    /// <param name="navigation">Navigation property to include</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsTrackingAsync(
        TKey id,
        Expression<Func<TEntity, object>> navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>    
    /// Get entity by id
    /// </summary>
    /// <param name="id">Record identifier</param>
    /// <param name="navigation">Navigation property to include as string. Use only for nested list navigations</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsTrackingAsync(
        TKey id,
        string navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>    
    /// Get entity by id
    /// </summary>
    /// <param name="id">Record identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] navigations);

    /// <summary>    
    /// Get entity by id
    /// </summary>
    /// <param name="id">Record identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include as string. Use only for nested list navigations</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default,
        params string[] navigations);

    /// <summary>    
    /// Get entity by id
    /// </summary>
    /// <param name="id">Record identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsTrackingAsync(
        TKey id,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] navigations);

    /// <summary>    
    /// Get entity by id
    /// </summary>
    /// <param name="id">Record identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include as string. Use only for nested list navigations</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsTrackingAsync(
        TKey id,
        CancellationToken cancellationToken = default,
        params string[] navigations);

    /// <summary>
    /// Get entity by id projected to <typeparamref name="TDto"/>
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <param name="id">Record identifier</param>
    /// <param name="includeDeleted"></param>
    /// <param name="ignoreCache">Ignore cache and force fetch from database</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TDto> GetByIdAsync<TDto>(
        TKey id,
        bool includeDeleted = false,
        bool ignoreCache = false,
        CancellationToken cancellationToken = default) where TDto : class, new();

    /// <summary>
    /// Get first record that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetOneAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get first record that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetOneAsTrackingAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get first record that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="navigation">Navigation property to include</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetOneAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>> navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get first record that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="navigation">Navigation property to include as string. Use only for nested list navigations</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetOneAsync(
        Expression<Func<TEntity, bool>> predicate,
        string navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get first record that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="navigation">Navigation property to include</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetOneAsTrackingAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>> navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get first record that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="navigation">Navigation property to include as string. Use only for nested list navigations</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TEntity> GetOneAsTrackingAsync(
        Expression<Func<TEntity, bool>> predicate,
        string navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get first record that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include</param>
    /// <returns></returns>
    Task<TEntity> GetOneAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] navigations);

    /// <summary>
    /// Get first record that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include as string. Use only for nested list navigations</param>
    /// <returns></returns>
    Task<TEntity> GetOneAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params string[] navigations);

    /// <summary>
    /// Get first record that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include</param>
    /// <returns></returns>
    Task<TEntity> GetOneAsTrackingAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] navigations);

    /// <summary>
    /// Get first record that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include as string. Use only for nested list navigations</param>
    /// <returns></returns>
    Task<TEntity> GetOneAsTrackingAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params string[] navigations);

    /// <summary>
    /// Get first record that satisfies provided criteria projected to <typeparamref name="TDto"/>
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<TDto> GetOneAsync<TDto>(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default) where TDto : class, new();

    /// <summary>
    /// Get list of entities by their id
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetByIdsAsync(
        List<TKey> ids,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get list of entities by their id
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetByIdsAsTrackingAsync(
        List<TKey> ids,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get list of entities by their id
    /// </summary>
    /// <param name="ids">List of entity ids</param>
    /// /// <param name="navigation">Navigation property to include</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetByIdsAsync(
        List<TKey> ids,
        Expression<Func<TEntity, object>> navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get list of entities by their id
    /// </summary>
    /// <param name="ids">List of entity ids</param>
    /// /// <param name="navigation">Navigation property to include</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetByIdsAsTrackingAsync(
        List<TKey> ids,
        Expression<Func<TEntity, object>> navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get list of entities by their id
    /// </summary>
    /// <param name="ids">List of entity ids</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// /// <param name="navigations">Navigation properties to include</param>
    /// <returns></returns>
    Task<List<TEntity>> GetByIdsAsync(
        List<TKey> ids,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] navigations);

    /// <summary>
    /// Get list of entities by their id
    /// </summary>
    /// <param name="ids">List of entity ids</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// /// <param name="navigations">Navigation properties to include</param>
    /// <returns></returns>
    Task<List<TEntity>> GetByIdsAsTrackingAsync(
        List<TKey> ids,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] navigations);

    /// <summary>
    /// Get list of entities by their id projected to <typeparamref name="TDto"/>
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TDto>> GetByIdsAsync<TDto>(
        List<TKey> ids,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default) where TDto : class, new();

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="ignoreCache">Ignore cache and force fetch from database</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync(
        bool includeDeleted = false,
        bool ignoreCache = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsTrackingAsync(
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="navigation">Navigation property to include</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, object>> navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="navigation">Navigation property to include as string. Use only for nested list navigations</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync(
        string navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="navigation">Navigation property to include</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsTrackingAsync(
        Expression<Func<TEntity, object>> navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="navigation">Navigation property to include as string. Use only for nested list navigations</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsTrackingAsync(
        string navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] navigations);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include as string. Use only for nested list navigations</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default,
        params string[] navigations);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsTrackingAsync(
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] navigations);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include as string. Use only for nested list navigations</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsTrackingAsync(
        CancellationToken cancellationToken = default,
        params string[] navigations);

    /// <summary>
    /// Get all entities projected to <typeparamref name="TDto"/>
    /// </summary>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="ignoreCache">Ignore cache and force fetch from database</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TDto>> GetAllAsync<TDto>(
        bool includeDeleted = false,
        bool ignoreCache = false,
        CancellationToken cancellationToken = default) where TDto : class, new();

    /// <summary>
    /// Get all records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsTrackingAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="navigation">Navigation property to include</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>> navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="navigation">Navigation property to include as string. Use only for nested list navigations</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate,
        string navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="navigation">Navigation property to include</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsTrackingAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>> navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="navigation">Navigation property to include as string. Use only for nested list navigations</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsTrackingAsync(
        Expression<Func<TEntity, bool>> predicate,
        string navigation,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] navigations);

    /// <summary>
    /// Get all records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include as string. Use only for nested list navigations</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params string[] navigations);

    /// <summary>
    /// Get all records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsTrackingAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] navigations);

    /// <summary>
    /// Get all records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="navigations">Navigation properties to include as string. Use only for nested list navigations</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsTrackingAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params string[] navigations);

    /// <summary>
    /// Get all records that satisfies provided criteria projected to <typeparamref name="TDto"/>
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TDto>> GetAllAsync<TDto>(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default) where TDto : class, new();

    /// <summary>
    /// Search dynamically on provided filters projected to <typeparamref name="TDto"/>
    /// </summary>
    /// <param name="searchFilters">Search filters</param>
    /// <param name="predicate">Custom predicate</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TDto>> SearchAllAsync<TDto>(
        List<SearchFilter> searchFilters,
        Expression<Func<TEntity, bool>> predicate,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default)
       where TDto : class, new();

    /// <summary>
    /// Search dynamically on provided filters projected to <typeparamref name="TDto"/>
    /// </summary>
    /// <param name="searchFilters">Search filters</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<TDto>> SearchAllAsync<TDto>(
        List<SearchFilter> searchFilters,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default)
       where TDto : class, new();

    /// <summary>
    /// Get a paged list of entities
    /// </summary>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="sortExpression">Expression used to sort entities</param>
    /// <param name="propertyMappings">Map properties in sort expression to real property names</param>
    /// <param name="ignoredProperties">List of property name that should be excluded from sort expression</param>
    /// <returns></returns>
    Task<IPagedList<TEntity>> GetAllPagedAsync(
        int pageIndex,
        int pageSize,
        string sortExpression = "",
        Dictionary<string, string> propertyMappings = default,
        IEnumerable<string> ignoredProperties = default,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a paged list of entities projected to <typeparamref name="TDto"/>
    /// </summary>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="sortExpression">Expression used to sort entities</param>
    /// <param name="propertyMappings">Map properties in sort expression to real property names</param>
    /// <param name="ignoredProperties">List of property name that should be excluded from sort expression</param>
    /// <returns></returns>
    Task<IPagedList<TDto>> GetAllPagedAsync<TDto>(
        int pageIndex,
        int pageSize,
        string sortExpression = "",
        Dictionary<string, string> propertyMappings = default,
        IEnumerable<string> ignoredProperties = default,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default)
        where TDto : class, new();

    /// <summary>
    /// Search dynamically on provided filters projected to <typeparamref name="TDto"/> as paged list
    /// </summary>
    /// <param name="searchFilters">Search filters</param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="sortExpression">Expression used to sort entities</param>
    /// <param name="propertyMappings">Map properties in sort expression to real property names</param>
    /// <param name="ignoredProperties">List of property name that should be excluded from sort expression</param>
    /// <returns></returns>
    Task<IPagedList<TDto>> SearchPagedAsync<TDto>(
        List<SearchFilter> searchFilters,
        int pageIndex,
        int pageSize,
        string sortExpression = "",
        Dictionary<string, string> propertyMappings = null,
        IEnumerable<string> ignoredProperties = null,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default)
        where TDto : class, new();

    /// <summary>
    /// Search dynamically on provided filters projected to <typeparamref name="TDto"/> as paged list
    /// </summary>
    /// <param name="searchFilters">Search filters</param>
    /// <param name="predicate">Custom predicate</param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="sortExpression">Expression used to sort entities</param>
    /// <param name="propertyMappings">Map properties in sort expression to real property names</param>
    /// <param name="ignoredProperties">List of property name that should be excluded from sort expression</param>
    /// <returns></returns>
    Task<IPagedList<TDto>> SearchPagedAsync<TDto>(
        List<SearchFilter> searchFilters,
        Expression<Func<TEntity, bool>> predicate,
        int pageIndex,
        int pageSize,
        string sortExpression = "",
        Dictionary<string, string> propertyMappings = null,
        IEnumerable<string> ignoredProperties = null,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default)
        where TDto : class, new();

    /// <summary>
    /// Search dynamically on provided filters projected to <typeparamref name="TDto"/> as paged list
    /// </summary>
    /// <param name="searchQuery">Search query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<IPagedList<TDto>> SearchPagedAsync<TDto>(
        BaseSearchQuery searchQuery,
        CancellationToken cancellationToken = default)
        where TDto : class, new();

    /// <summary>
    /// Search dynamically on provided filters projected to <typeparamref name="TDto"/> as paged list
    /// </summary>
    /// <param name="searchQuery">Search query</param>
    /// <param name="predicate">Custom predicate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<IPagedList<TDto>> SearchPagedAsync<TDto>(
        BaseSearchQuery searchQuery,
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        where TDto : class, new();

    /// <summary>
    /// Determines if there is at least one record that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">Predicate defining the criteria</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines if there is a record with specified id
    /// </summary>
    /// <param name="id">Entry id to check</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<bool> ExistsWithIdAsync(
        TKey id,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Count records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Count records that satisfies provided criteria
    /// </summary>
    /// <param name="predicate">predicate defining the criteria</param>
    /// <param name="includeDeleted">Include soft deleted entities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<long> CountLongAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Insert the entity in database
    /// </summary>
    /// <param name="entity">Entity entry</param>
    /// <param name="autoSave">If true, changes will be automatically save to database and no need to call <see cref="SaveAsync"/> explicitly</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task InsertAsync(
        TEntity entity,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Insert entities in database
    /// </summary>
    /// <param name="entities">Entity entries</param>
    /// <param name="autoSave">If true, changes will be automatically save to database and no need to call <see cref="SaveAsync"/> explicitly</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task InsertAsync(
        List<TEntity> entities,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Insert entities using bulk operations
    /// </summary>
    /// <param name="entities">Entity entries</param>
    /// <param name="autoSave">If true, changes will be automatically save to database and no need to call <see cref="BulkSaveAsync"/> explicitly</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task BulkInsertAsync(
        List<TEntity> entities,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update the entity entry
    /// </summary>
    /// <param name="entity">Entity entry</param>
    /// <param name="autoSave">If true, changes will be automatically save to database and no need to call <see cref="SaveAsync"/> explicitly</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task UpdateAsync(
        TEntity entity,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update entity entries
    /// </summary>
    /// <param name="entities">Entity entries</param>
    /// <param name="autoSave">If true, changes will be automatically save to database and no need to call <see cref="SaveAsync"/> explicitly</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task UpdateAsync(
        List<TEntity> entities,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update entities using bulk operations
    /// </summary>
    /// <param name="entities">Entity entries</param>
    /// <param name="autoSave">If true, changes will be automatically save to database and no need to call <see cref="BulkSaveAsync"/> explicitly</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task BulkUpdateAsync(
        List<TEntity> entities,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete entity entry. (Soft delete if entity is marked as <see cref="ISoftDeletedEntity"/>)
    /// </summary>
    /// <param name="entity">Entity entry</param>
    /// <param name="autoSave">If true, changes will be automatically save to database and no need to call <see cref="SaveAsync"/> explicitly</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task DeleteAsync(
        TEntity entity,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Bulk delete entity entry. (Soft delete if entity is marked as <see cref="ISoftDeletedEntity"/>)
    /// </summary>
    /// <param name="entities">Entity entries</param>
    /// <param name="autoSave">If true, changes will be automatically save to database and no need to call <see cref="SaveAsync"/> explicitly</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task DeleteAsync(
        List<TEntity> entities,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Find and delete entity by its id. (Soft delete if entity is marked as <see cref="ISoftDeletedEntity"/>)
    /// </summary>
    /// <param name="id">Entity id</param>
    /// <param name="autoSave">If true, changes will be automatically save to database and no need to call <see cref="SaveAsync"/> explicitly</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A boolean indicates if entity with specified id found and deleted</returns>
    Task<bool> DeleteAsync(
        TKey id,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Find and bulk delete entity entries by their ids. (Soft delete if entity is marked as <see cref="ISoftDeletedEntity"/>)
    /// </summary>
    /// <param name="ids">Id list</param>
    /// <param name="autoSave">If true, changes will be automatically save to database and no need to call <see cref="SaveAsync"/> explicitly</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task DeleteAsync(
        List<TKey> ids,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// !USE WITH PRECAUTION! <br />
    /// Delete entity entry even if it's an <see cref="ISoftDeletedEntity"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task HardDeleteAsync(
        TEntity entity,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// !USE WITH PRECAUTION! <br />
    /// Bulk delete entity entry even if it's an <see cref="ISoftDeletedEntity"/>
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task HardDeleteAsync(
        List<TEntity> entities,
        bool autoSave = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Save all changes to database
    /// </summary>
    /// <returns>Number of state entries written to database</returns>
    int Save();

    /// <summary>
    /// Save all changes to database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Bulk save all changes to database
    /// </summary>
    void BulkSave();

    /// <summary>
    /// Bulk save all changes to database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task BulkSaveAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Generic repository for entities
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity { }
