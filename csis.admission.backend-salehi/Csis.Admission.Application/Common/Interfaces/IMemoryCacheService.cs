/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Configuration;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// Memory cache service
/// </summary>
public interface IMemoryCacheService
{
    /// <summary>
    /// Read a value from cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache key</param>
    /// <returns></returns>
    T Get<T>(string key);

    /// <summary>
    /// Check if a value is present in the cache with the specified key
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <returns></returns>
    bool Contains(string key);

    /// <summary>
    /// Write a value to cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="value">The value to cache</param>
    /// <param name="cacheOptions">Cache options</param>
    /// <returns></returns>
    void Set<T>(string key, T value, CacheOptions cacheOptions);

    /// <summary>
    /// Write a value to cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="value">The value to cache</param>
    /// <returns></returns>
    void Set<T>(string key, T value);

    /// <summary>
    /// Read a value from cache if exists. If not write the value to cache then return it
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="factory">The function that return value to cache if no cache found with specified key</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Read a value from cache if exists. If not write the value to cache then return it
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
    /// <returns></returns>
    void Remove(string key);

    /// <summary>
    /// Clear entire cache
    /// </summary>
    void Clear();

    /// <summary>
    /// Get all cache keys present in memory
    /// </summary>
    /// <returns></returns>
    IEnumerable<string> GetAllKeys();

    /// <summary>
    /// Get all cache keys that match a wildcard pattern
    /// </summary>
    /// <param name="wildcardPatterns"></param>
    /// <returns></returns>
    IEnumerable<string> GetWildcardKeys(string[] wildcardPatterns);
}
