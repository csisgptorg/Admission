/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Configuration;

/// <summary>
/// Database options
/// </summary>
public sealed class DatabaseOptions
{
    /// <summary>
    /// Use in-memory database
    /// </summary>
    public bool UseInMemoryDatabase { get; set; } = false;

    /// <summary>
    /// Enable query logging
    /// </summary>
    public bool EnableLogging { get; set; } = false;

    /// <summary>
    /// Include sensitive data in query logs
    /// </summary>
    public bool EnableSensitiveDataLogging { get; set; } = false;

    /// <summary>
    /// Enable db context pooling
    /// </summary>
    public bool EnablePooling { get; set; } = true;

    /// <summary>
    /// Run database seeders when app starts
    /// </summary>
    public bool RunSeeders { get; set; } = false;

    /// <summary>
    /// Max pool size when pooling is enabled
    /// </summary>
    public int? MaxPoolSize { get; set; }

    /// <summary>
    /// Connection strings
    /// </summary>
    public ConnectionStrings ConnectionStrings { get; set; } = new();
}
