/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Csis.Admission.Services.Cache;
internal sealed class MemoryCacheService(IMemoryCache cache) : IMemoryCacheService
{
    public bool Contains(string key) {
        return GetAllKeys().Contains(key);
    }

    public T Get<T>(string key) {
        return cache.Get<T>(key);
    }

    public IEnumerable<string> GetAllKeys() {
        return cache.GetKeys();
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, CacheOptions cacheOptions, CancellationToken cancellationToken = default) {
        return await cache.GetOrCreateAsync(key, async entry => {
            return await factory();
        }, cacheOptions);
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default) {
        return await GetOrSetAsync(key, factory, new CacheOptions(), cancellationToken);
    }

    public void Remove(string key) {
        cache.Remove(key);
    }

    public void Clear() {
        if ( cache is MemoryCache memoryCache ) {
            memoryCache.Clear();
            memoryCache.Compact(1.0);
        } else {
            throw new NotImplementedException($"Clearing cache is only implemented for MemoryCache type");
        }
    }

    public void Set<T>(string key, T value, CacheOptions cacheOptions) {
        cache.Set(key, value, new MemoryCacheEntryOptions {
            AbsoluteExpirationRelativeToNow = cacheOptions.AbsoluteExpirationSeconds > 0 ? TimeSpan.FromSeconds(cacheOptions.AbsoluteExpirationSeconds) : null,
            SlidingExpiration = cacheOptions.SlidingExpirationSeconds > 0 ? TimeSpan.FromSeconds(cacheOptions.SlidingExpirationSeconds) : null
        });
    }

    public void Set<T>(string key, T value) {
        Set(key, value, new CacheOptions());
    }

    public IEnumerable<string> GetWildcardKeys(string[] wildcardPatterns) {
        return GetAllKeys().Where(x => IsWildcardMatch(x, ref wildcardPatterns));
    }

    private static bool IsWildcardMatch(string keyToCheck, ref string[] wildcardPatterns) {
        foreach ( var wildcard in wildcardPatterns ) {
            var pattern = $"^{wildcard.Replace(".", "\\.").Replace("*", ".{1,}")}$";
            if ( System.Text.RegularExpressions.Regex.IsMatch(keyToCheck, pattern) ) {
                return true;
            }
        }

        return false;
    }
}
