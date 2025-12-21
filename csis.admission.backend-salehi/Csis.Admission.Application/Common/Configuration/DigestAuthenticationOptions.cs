/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Configuration;

/// <summary>
/// Digest authentication options
/// </summary>
public sealed class DigestAuthenticationOptions
{
    /// <summary>
    /// Users allowed to access protected resources with digest authentication
    /// </summary>
    public DigestAuthenticationUser[] Users { get; set; } = [];

    /// <summary>
    /// Realm
    /// </summary>
    public string Realm { get; set; } = "csis.ir";
}

/// <summary>
/// Digest authentication user
/// </summary>
public sealed class DigestAuthenticationUser
{
    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Role
    /// </summary>
    public string Role { get; set; }
}
