/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Configuration;

/// <summary>
/// Elastic search options
/// </summary>
public sealed class ElasticSearchOptions
{
    /// <summary>
    /// Is logging to elastic search enabled
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Elastic search nodes
    /// </summary>
    public string[] Nodes { get; set; } = [];
}
