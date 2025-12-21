/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Configuration;

/// <summary>
/// Redis cache options
/// </summary>
public sealed class RedisOptions
{
    /// <summary>
    /// Redis host address without protocol. example: 127.0.0.1
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Redis port to connect. Default is 6379
    /// </summary>
    public int Port { get; set; } = 6379;

    /// <summary>
    /// Username used for authenticating to redis server
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Password used for authenticating to redis server
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Connection timeout in seconds
    /// </summary>
    public int TimeOutInSeconds { get; set; } = 3;

    /// <summary>
    /// Connect retry times
    /// </summary>
    public int ConnectRetry { get; set; } = 3;

    /// <summary>
    /// Keep alive
    /// </summary>
    public int KeepAliveInSeconds { get; set; } = 60;
}
