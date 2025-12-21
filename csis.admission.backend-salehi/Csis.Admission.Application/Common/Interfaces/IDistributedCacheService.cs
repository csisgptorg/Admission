/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Configuration;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// Distributed cache service
/// </summary>
public interface IDistributedCacheService
{
    /// <summary>
    /// Read a value from cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if a value is present in the cache with the specified key
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ContainsAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write a value to cache. Maximum entry size is 1.5MB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="value">The value to cache</param>
    /// <param name="cacheOptions">Cache options</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetAsync<T>(string key, T value, CacheOptions cacheOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write a value to cache. Maximum entry size is 1.5MB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="value">The value to cache</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Read a value from cache if exists. If not write the value to cache then return it. Maximum entry size is 1.5MB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="factory">The function that return value to cache if no cache found with specified key</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Read a value from cache if exists. If not write the value to cache then return it. Maximum entry size is 1.5MB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="factory">The function that return value to cache if no cache found with specified key</param>
    /// <param name="cacheOptions">Cache options</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, CacheOptions cacheOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove cache entry by key
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove cache entries matching a prefix
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removed all cache entries related to this project
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveAllAsync(CancellationToken cancellationToken = default);
}
