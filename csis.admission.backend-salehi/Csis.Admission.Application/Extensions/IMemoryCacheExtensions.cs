/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Extensions;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;

namespace Csis.Admission.Application.Extensions;

/// <summary>
/// IMemoryCache extension methods
/// </summary>
public static class IMemoryCacheExtensions
{
    /// <summary>
    /// Get cache entry or create if not exists
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="cache"></param>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <param name="options"></param>
    /// <param name="priority"></param>
    /// <returns></returns>
    public static async Task<TItem> GetOrCreateAsync<TItem>(this IMemoryCache cache, string key,
        Func<ICacheEntry, Task<TItem>> factory, CacheOptions options, CacheItemPriority priority = CacheItemPriority.Normal) {
        return await cache.GetOrCreateAsync(key, async entry => {
            entry.SetSlidingExpiration(TimeSpan.FromSeconds(options.SlidingExpirationSeconds));
            entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(options.AbsoluteExpirationSeconds));
            entry.SetPriority(priority);

            return await factory(entry);
        });
    }

    #region Microsoft.Extensions.Caching.Memory_6_OR_OLDER

    private static readonly Lazy<Func<MemoryCache, object>> _getEntriesNet6 =
        new(() => (Func<MemoryCache, object>) Delegate.CreateDelegate(
            typeof(Func<MemoryCache, object>),
            typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance).GetGetMethod(true),
            throwOnBindFailure: true));

    #endregion

    #region Microsoft.Extensions.Caching.Memory_7_OR_NEWER

    private static readonly Lazy<Func<MemoryCache, object>> _getCoherentState =
        new(() =>
            CreateGetter<MemoryCache, object>(typeof(MemoryCache)
                .GetField("_coherentState", BindingFlags.NonPublic | BindingFlags.Instance)));

    private static readonly Lazy<Func<object, ConcurrentDictionary<string, object>>> _getStringEntriesNet7 = new(() =>
        CreateGetter<object, ConcurrentDictionary<string, object>>(typeof(MemoryCache)
            .GetNestedType("CoherentState", BindingFlags.NonPublic)
            .GetField("_stringEntries", BindingFlags.NonPublic | BindingFlags.Instance)));

    private static Func<TParam, TReturn> CreateGetter<TParam, TReturn>(FieldInfo field) {
        var methodName = $"{field.ReflectedType.FullName}.get_{field.Name}";
        var method = new DynamicMethod(methodName, typeof(TReturn), [typeof(TParam)], typeof(TParam), true);
        var ilGen = method.GetILGenerator();
        ilGen.Emit(OpCodes.Ldarg_0);
        ilGen.Emit(OpCodes.Ldfld, field);
        ilGen.Emit(OpCodes.Ret);
        return (Func<TParam, TReturn>) method.CreateDelegate(typeof(Func<TParam, TReturn>));
    }

    #endregion

    private static readonly Func<MemoryCache, IDictionary> _getEntries;

    static IMemoryCacheExtensions() {
        _getEntries = Assembly.GetAssembly(typeof(MemoryCache)).GetName().Version.Major < 7
            ? (cache => (IDictionary) _getEntriesNet6.Value(cache))
            : cache => _getStringEntriesNet7.Value(_getCoherentState.Value(cache));
    }

    /// <summary>
    /// Get all cache keys
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <returns></returns>
    private static ICollection GetAllKeys(this IMemoryCache memoryCache) =>
        _getEntries((MemoryCache) memoryCache).Keys;

    /// <summary>
    /// Get all cache keys of type string
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetKeys(this IMemoryCache memoryCache) =>
        memoryCache.GetAllKeys().OfType<string>();
}
