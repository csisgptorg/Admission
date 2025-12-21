/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Configuration;

/// <summary>
/// Different connection strings
/// </summary>
public sealed partial class ConnectionStrings
{
    /// <summary>
    /// Connection string used to connect to sql server
    /// </summary>
    public string SqlServer { get; set; } = "";

    /// <summary>
    /// Connection string used to connect to sql server for running integration tests
    /// </summary>
    public string SqlServerTest { get; set; } = "";
}
