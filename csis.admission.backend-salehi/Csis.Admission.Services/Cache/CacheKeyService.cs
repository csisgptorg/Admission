/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Domain.Common;

namespace Csis.Admission.Services.Cache;

/// <summary>
/// Cache key provider service implementation
/// </summary>
/// <typeparam name="TKey">Entity key type</typeparam>
internal class CacheKeyService<TKey> : ICacheKeyService<TKey>
{
    public string GetEntityKeyPrefix<TEntity>() {
        return typeof(TEntity).Name;
    }

    public string GetEntityKeyPrefix(Type entityType) {
        return entityType.Name;
    }

    public string GetDtoKey<TEntity, TDto>(TKey id)
        where TEntity : IEntity<TKey>
        where TDto : class {
        return $"{GetEntityKeyPrefix<TEntity>()}_{typeof(TDto).Name}_{id}";
    }

    public string GetDtoKey(Type entityType, Type dtoType, object id) {
        ArgumentNullException.ThrowIfNull(id);
        return $"{GetEntityKeyPrefix(entityType)}_{dtoType.Name}_{id}";
    }

    public string GetDtoWildcardKey(Type entityType, object id) {
        ArgumentNullException.ThrowIfNull(id);
        return $"{GetEntityKeyPrefix(entityType)}_*_{id}";
    }

    public string GetDtoListKey<TEntity, TDto>()
        where TEntity : IEntity<TKey>
        where TDto : class {
        return $"{GetEntityKeyPrefix<TEntity>()}_{typeof(TDto).Name}_List";
    }

    public string GetDtoListKey(Type entityType, Type dtoType) {
        return $"{GetEntityKeyPrefix(entityType)}_{dtoType.Name}_List";
    }

    public string GetDtoListWildcardKey(Type entityType) {
        return $"{GetEntityKeyPrefix(entityType)}_*_List";
    }

    public string GetEntityKey<TEntity>(TKey id) where TEntity : IEntity<TKey> {
        return $"{GetEntityKeyPrefix<TEntity>()}_{id}";
    }

    public string GetEntityKey(Type entityType, object id) {
        return $"{GetEntityKeyPrefix(entityType)}_{id}";
    }

    public string GetEntityWildcardKey(Type entityType) {
        return $"{GetEntityKeyPrefix(entityType)}_*";
    }

    public string GetEntityListKey<TEntity>() where TEntity : IEntity<TKey> {
        return $"{GetEntityKeyPrefix<TEntity>()}_List";
    }

    public string GetEntityListKey(Type entityType) {
        return $"{GetEntityKeyPrefix(entityType)}_List";
    }

    public string GetCustomKey<TEntity>(string customKey) where TEntity : IEntity<TKey> {
        if ( string.IsNullOrWhiteSpace(customKey) ) {
            throw new ArgumentException($"'{nameof(customKey)}' cannot be null or whitespace.", nameof(customKey));
        }

        return $"{GetEntityKeyPrefix<TEntity>()}__CUSTOM__{customKey}";
    }

    public string GetCustomWildcardKey(Type entityType) {
        return $"{GetEntityKeyPrefix(entityType)}__CUSTOM__*";
    }
}

/// <summary>
/// Cache key provider service implementation
/// </summary>
internal sealed class CacheKeyService : CacheKeyService<int>, ICacheKeyService { }
