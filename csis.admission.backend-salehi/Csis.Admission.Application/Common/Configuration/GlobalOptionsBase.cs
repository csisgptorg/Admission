/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Configuration;

/// <summary>
/// Global Options
/// </summary>
public static partial class GlobalOptions
{
    /// <summary>
    /// Is development mode enabled
    /// </summary>
    public static bool IsDevelopment { get; private set; } = false;

    /// <summary>
    /// Is background services registered
    /// </summary>
    public static bool RunBackgroundServices { get; private set; } = false;

    /// <summary>
    /// Enable file upload action
    /// </summary>
    public static bool AllowFileUpload { get; private set; } = false;

    /// <summary>
    /// Redis key prefix
    /// </summary>
    public static string RedisPrefix { get; private set; }

    /// <summary>
    /// Enable development mode
    /// </summary>
    public static void EnableDevelopmentMode() {
        IsDevelopment = true;
    }

    /// <summary>
    /// Enable background service registration
    /// </summary>
    public static void EnableBackgroundServices() {
        RunBackgroundServices = true;
    }

    /// <summary>
    /// Enable file upload
    /// </summary>
    public static void EnableFileUpload() {
        AllowFileUpload = true;
    }

    /// <summary>
    /// Set redis key prefix
    /// </summary>
    /// <param name="redisPrefix"></param>
    public static void SetRedisPrefix(string redisPrefix) {
        RedisPrefix = redisPrefix;
    }
}
