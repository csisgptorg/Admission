/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace Csis.Admission.Services.Cache;
internal sealed class DistributedCacheService(
    IDistributedCache cache,
    IServiceProvider serviceProvider,
    ILogger<DistributedCacheService> logger) : IDistributedCacheService
{
    private static readonly JsonSerializerOptions _serializerOptions = new() {
        MaxDepth = 32,
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IncludeFields = true,
    };

    public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default) {
        var cachedValue = await cache.GetStringAsync(key, cancellationToken);

        if ( cachedValue is null ) {
            return default;
        }

        return JsonSerializer.Deserialize<T>(cachedValue, _serializerOptions);
    }

    public async Task<bool> ContainsAsync(string key, CancellationToken cancellationToken = default) {
        return await cache.GetStringAsync(key, cancellationToken) is not null;
    }

    public async Task SetAsync<T>(string key, T value, CacheOptions cacheOptions, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(cacheOptions);

        if ( value is null ) {
            return;
        }

        var options = new DistributedCacheEntryOptions {
            AbsoluteExpirationRelativeToNow = cacheOptions.AbsoluteExpirationSeconds > 0 ? TimeSpan.FromSeconds(cacheOptions.AbsoluteExpirationSeconds) : null,
            SlidingExpiration = cacheOptions.SlidingExpirationSeconds > 0 ? TimeSpan.FromSeconds(cacheOptions.SlidingExpirationSeconds) : null
        };

        var serializedValue = JsonSerializer.Serialize(value, _serializerOptions);

        if ( serializedValue.Length < 1_500_000 ) { // 1.5MB max
            await cache.SetStringAsync(key, serializedValue, options, cancellationToken);
        }
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) {
        await SetAsync(key, value, new CacheOptions(), cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default) {
        await cache.RemoveAsync(key, cancellationToken);
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, CacheOptions cacheOptions, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(cacheOptions);

        var cachedValue = await GetAsync<T>(key, cancellationToken);

        if ( cachedValue is not null ) {
            return cachedValue;
        }

        cachedValue = await factory();

        await SetAsync(key, cachedValue, cacheOptions, cancellationToken);
        return cachedValue;
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default) {
        return await GetOrSetAsync(key, factory, new CacheOptions(), cancellationToken);
    }

    public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default) {
        IConnectionMultiplexer redis = null;
        try {
            redis = serviceProvider.GetService<IConnectionMultiplexer>();
        } catch ( Exception ) {
            logger.LogError("Could not get redis connection multiplexer");
            throw;
        }

        var sanitizedPrefix = $"{GlobalOptions.RedisPrefix}{SanitizePrefix(prefix)}";
        var endpoints = redis.GetEndPoints();
        long totalDeleted = 0;

        // The Lua script to remove entries by prefix
        var script = @"
            local cursor = '0'
            local deleted = 0
            repeat
                local result = redis.call('SCAN', cursor, 'MATCH', ARGV[1], 'COUNT', 1000)
                cursor = result[1]
                local keys = result[2]
                if #keys > 0 then
                    redis.call('DEL', unpack(keys))
                    deleted = deleted + #keys
                end
            until cursor == '0'
            return deleted
        ";

        var tasks = endpoints.Select(async endpoint => {
            var server = redis.GetServer(endpoint);

            // Skip if server is not a primary (we only delete from primaries)
            if ( !server.IsConnected || server.IsReplica ) {
                return 0L;
            }

            var db = redis.GetDatabase();
            var deletedCount = (long) await db.ScriptEvaluateAsync(
                script,
                values: [sanitizedPrefix]
            );

            return deletedCount;
        });

        var results = await Task.WhenAll(tasks);
        totalDeleted = results.Sum();

        logger.LogInformation("Deleted {totalKeys} keys from {endpointsCount} endpoints by prefix: {prefix} - sanitized: {sanitizedPrefix}",
            totalDeleted, endpoints.Length, prefix, sanitizedPrefix);
    }

    public async Task RemoveAllAsync(CancellationToken cancellationToken = default) {
        await RemoveByPrefixAsync(null, cancellationToken);
    }

    private static string SanitizePrefix(string prefix) {
        if ( prefix is not null ) {
            // Disallow accidental wildcards
            if ( prefix.Contains('*') || prefix.Contains('?') || prefix.Contains('[') ) {
                throw new ArgumentException("Prefix cannot contain Redis glob wildcards (*, ?, [).");
            }

            // Trim any accidental spaces
            prefix = prefix.Trim();

            return $"{prefix}*";
        }

        return "*";
    }
}
