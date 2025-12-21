/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Configuration;

/// <summary>
/// Database cache options
/// </summary>
public sealed class CacheOptions
{
    /// <summary>
    /// Absolute expiration time in seconds
    /// </summary>
    public int AbsoluteExpirationSeconds { get; set; } = 1800;

    /// <summary>
    /// Sliding expiration time in seconds
    /// </summary>
    public int SlidingExpirationSeconds { get; set; } = 600;
}
