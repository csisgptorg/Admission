/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// Cache key provider service
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface ICacheKeyService<TKey>
{
    /// <summary>
    /// Get cache key for an entity entry
    /// </summary>
    /// <param name="id">Entity record id</param>
    /// <returns></returns>
    string GetEntityKey<TEntity>(TKey id) where TEntity : IEntity<TKey>;

    /// <summary>
    /// Get cache key for an entity entry
    /// </summary>
    /// <param name="entityType">Type of entity</param>
    /// <param name="id">Entity record id</param>
    /// <returns></returns>
    string GetEntityKey(Type entityType, object id);

    /// <summary>
    /// Get wildcard cache key that covers all possible cache keys used for specified entity type
    /// </summary>
    /// <param name="entityType">Type of entity</param>
    /// <returns></returns>
    string GetEntityWildcardKey(Type entityType);

    /// <summary>
    /// Get cache key for list of all entity entries
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    string GetEntityListKey<TEntity>() where TEntity : IEntity<TKey>;

    /// <summary>
    /// Get cache key for list of all entity entries
    /// </summary>
    /// <param name="entityType">Type of entity</param>
    /// <returns></returns>
    string GetEntityListKey(Type entityType);

    /// <summary>
    /// Get cache key for a mapped entity entry to <typeparamref name="TDto"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <param name="id">Entity record id</param>
    /// <returns></returns>
    string GetDtoKey<TEntity, TDto>(TKey id)
        where TEntity : IEntity<TKey>
        where TDto : class;

    /// <summary>
    /// Get cache key for a mapped entity entry
    /// </summary>
    /// <param name="entityType">Type of entity</param>
    /// <param name="dtoType">Type of dto</param>
    /// <param name="id">Entry id</param>
    /// <returns></returns>
    string GetDtoKey(Type entityType, Type dtoType, object id);

    /// <summary>
    /// Get wildcard cache key that covers an entry with all possible DTOs
    /// </summary>
    /// <param name="entityType">Type of entity</param>
    /// <param name="id">Entity record id</param>
    /// <returns></returns>
    string GetDtoWildcardKey(Type entityType, object id);

    /// <summary>
    /// Get cache key for list of all mapped entity entry to <typeparamref name="TDto"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <returns></returns>
    string GetDtoListKey<TEntity, TDto>() where TEntity : IEntity<TKey> where TDto : class;

    /// <summary>
    /// Get cache key for list of all mapped entity entry
    /// </summary>
    /// <param name="entityType">Type of entity</param>
    /// <param name="dtoType">Type of dto</param>
    /// <returns></returns>
    string GetDtoListKey(Type entityType, Type dtoType);

    /// <summary>
    /// Get wildcard cache key that covers all possible list of DTOs
    /// </summary>
    /// <param name="entityType">Type of entity</param>
    /// <returns></returns>
    string GetDtoListWildcardKey(Type entityType);

    /// <summary>
    /// Get the prefix used for all cache keys of entity type <typeparamref name="TEntity"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    string GetEntityKeyPrefix<TEntity>();

    /// <summary>
    /// Get the prefix used for all cache keys of entity
    /// </summary>
    /// <param name="entityType">Type of entity</param>
    /// <returns></returns>
    string GetEntityKeyPrefix(Type entityType);

    /// <summary>
    /// Get custom cache key prefixed with entity type name. Using this method will ensure custom cache is invalidated properly
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="customKey">The custom cache key</param>
    /// <returns></returns>
    string GetCustomKey<TEntity>(string customKey) where TEntity : IEntity<TKey>;

    /// <summary>
    /// Get wildcard cache key that covers all custom keys for specified entity type
    /// </summary>
    /// <param name="entityType">Type of entity</param>
    /// <returns></returns>
    string GetCustomWildcardKey(Type entityType);
}

/// <summary>
/// Cache key provider service
/// </summary>
public interface ICacheKeyService : ICacheKeyService<int> { }
