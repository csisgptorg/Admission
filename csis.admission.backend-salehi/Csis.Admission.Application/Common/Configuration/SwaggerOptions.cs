/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Configuration;

/// <summary>
/// Swagger options
/// </summary>
public sealed class SwaggerOptions
{
    /// <summary>
    /// Is swagger enabled
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// Indicates if JWT bearer token support should be added to swagger ui
    /// </summary>
    public bool AddJwtSupport { get; set; } = true;

    /// <summary>
    /// Indicates if XML documents should be included in the swagger ui
    /// </summary>
    public bool IncludeXmlDocuments { get; set; } = false;

    /// <summary>
    /// Indicates if authorization info should be persisted
    /// </summary>
    public bool PersistAuthorization { get; set; } = false;

    /// <summary>
    /// Version
    /// </summary>
    public string Version { get; set; } = "1";

    /// <summary>
    /// Route prefix used to load swagger ui
    /// </summary>
    public string RoutePrefix { get; set; } = "swagger";

    /// <summary>
    /// Url prefix used to load swagger ui assets (js, css, ...)
    /// </summary>
    public string AssetsPrefix { get; set; } = "";

    /// <summary>
    /// Swagger ui document title
    /// </summary>
    public string DocumentTitle { get; set; } = "My Api";

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = "Description about this api";

    /// <summary>
    /// Get version string
    /// </summary>
    /// <returns></returns>
    public string GetVersion() {
        return $"v{Version}";
    }
}
