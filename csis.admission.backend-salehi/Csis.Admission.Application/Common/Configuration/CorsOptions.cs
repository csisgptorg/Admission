/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Configuration;

/// <summary>
/// CORS Options
/// </summary>
public sealed class CorsOptions
{
    /// <summary>
    /// Policy name for configuring CORS
    /// </summary>
    public const string PolicyName = "default";

    /// <summary>
    /// Is CORS enabled
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Allowed origins
    /// </summary>
    public string[] Origins { get; set; } = [];

    /// <summary>
    /// Allowed methods
    /// </summary>
    public string[] Methods { get; set; } = ["GET"];
}
