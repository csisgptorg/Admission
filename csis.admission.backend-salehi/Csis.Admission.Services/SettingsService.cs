/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Interfaces.Settings;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Settings;
using Csis.Utilities.Extensions;
using System.Text.Json;

namespace Csis.Admission.Services;

/// <summary>
/// پیاده سازی سرویس تنظیمات
/// </summary>
internal sealed class SettingsService(ISettingRepository settingsRepo, IMemoryCacheService cache) : ISettingsService
{
    private static readonly JsonSerializerOptions _serializerOptions = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        MaxDepth = 32
    };

    public async Task<SettingsModel<TSettings>> GetAsync<TSettings>(string keySuffix) where TSettings : ISettings<TSettings>, new() {
        if ( !keySuffix.HasValue() ) {
            throw new ArgumentException("Key suffix can not be empty or null.", nameof(keySuffix));
        }

        var key = GetDbSettingsKey<TSettings>(keySuffix);

        return await cache.GetOrSetAsync(GetCacheKey(key), async () => {
            var settings = await GetSettingsAsync<TSettings>(key);

            if ( settings is null ) {
                return new SettingsModel<TSettings>(new TSettings().GetDefault(), 1);
            }

            return new SettingsModel<TSettings>(JsonSerializer.Deserialize<TSettings>(settings.Value, _serializerOptions), settings.Version);
        });
    }

    public async Task<SettingsModel<TSettings>> GetAsync<TSettings>() where TSettings : ISettings<TSettings>, new() {
        var key = GetDbSettingsKey<TSettings>(null);

        return await cache.GetOrSetAsync(GetCacheKey(key), async () => {
            var settings = await GetSettingsAsync<TSettings>(key);

            if ( settings is null ) {
                return new SettingsModel<TSettings>(new TSettings().GetDefault(), 1);
            }

            return new SettingsModel<TSettings>(JsonSerializer.Deserialize<TSettings>(settings.Value, _serializerOptions), settings.Version);
        });
    }

    public async Task SaveAsync<TSettings>(string keySuffix, TSettings value) where TSettings : ISettings<TSettings>, new() {
        if ( !keySuffix.HasValue() ) {
            throw new ArgumentException("Key suffix can not be empty or null.", nameof(keySuffix));
        }

        var key = GetDbSettingsKey<TSettings>(keySuffix);
        value ??= new TSettings().GetDefault();

        var currentSettings = await GetSettingsAsync<TSettings>(key, asTracking: true);
        var json = JsonSerializer.Serialize(value, _serializerOptions);

        if ( currentSettings is null ) {
            await settingsRepo.InsertAsync(new Setting {
                Key = key,
                Value = json,
                Version = 1
            });
        } else {
            currentSettings.Value = json;
            currentSettings.Version++;
            await settingsRepo.UpdateAsync(currentSettings);
        }

        cache.Remove(GetCacheKey(key));
    }

    public async Task SaveAsync<TSettings>(TSettings value) where TSettings : ISettings<TSettings>, new() {
        var key = GetDbSettingsKey<TSettings>(null);
        value ??= new TSettings().GetDefault();

        var currentSettings = await GetSettingsAsync<TSettings>(key, asTracking: true);
        var json = JsonSerializer.Serialize(value, _serializerOptions);

        if ( currentSettings is null ) {
            await settingsRepo.InsertAsync(new Setting {
                Key = key,
                Value = json,
                Version = 1
            });
        } else {
            currentSettings.Value = json;
            currentSettings.Version++;
            await settingsRepo.UpdateAsync(currentSettings);
        }

        cache.Remove(GetCacheKey(key));
    }

    private async Task<Setting> GetSettingsAsync<TSettings>(string key, bool asTracking = false) {
        if ( asTracking ) {
            return await settingsRepo.GetByKeyAsTrackingAsync(key);
        } else {
            return await settingsRepo.GetByKeyAsync(key);
        }
    }

    private static string GetDbSettingsKey<TSettings>(string suffix) {
        var key = typeof(TSettings).Name;
        if ( suffix.HasValue() ) {
            key += $"-{suffix.Trim()}";
        }

        if ( key.Length > 100 ) {
            throw new Exception("Settings key is too large. Maximum 100 characters allowed.");
        }

        return key;
    }

    private static string GetCacheKey(string settingsKey) => $"{settingsKey}-settings";
}
