/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common;

/// <summary>
/// A wrapper class representing categorized application constants
/// </summary>
public static partial class Constants
{
    /// <summary>
    /// Database related constants
    /// </summary>
    public static partial class Db
    {
        /// <summary>
        /// Migrations history table name
        /// </summary>
        public const string MigrationsTableName = "__MigrationsHistory";
    }

    /// <summary>
    /// Folders list
    /// </summary>
    public static partial class Folders
    {
        /// <summary>
        /// Protected files folder name
        /// </summary>
        public const string ProtectedFiles = "ProtectedFiles";
    }
}
