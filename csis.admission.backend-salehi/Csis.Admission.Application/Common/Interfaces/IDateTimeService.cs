/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Utilities;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// Date time service used for writing testable date related code
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer, expressed as the local time.
    /// </summary>
    DateTime Now { get; }

    /// <summary>
    /// Gets a <see cref="PersianDateTime"/> object that is set to the current date and time on this computer, expressed as the local time.
    /// </summary>
    PersianDateTime NowPersian { get; }

    /// <summary>
    /// Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).
    /// </summary>
    DateTime NowUtc { get; }

    /// <summary>
    /// Gets a <see cref="DateOnly"/> object that is set to the current date on this computer, expressed as the Coordinated Universal Time (UTC).
    /// </summary>
    DateOnly Today { get; }

    /// <summary>
    /// Gets a <see cref="DateOnly"/> object that is set to the current date on this computer, expressed as the Coordinated Universal Time (UTC).
    /// </summary>
    DateOnly TodayUtc { get; }
}
