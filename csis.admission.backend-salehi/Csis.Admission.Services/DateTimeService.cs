/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using Csis.Utilities;

namespace Csis.Admission.Services;

/// <summary>
/// Date time service implementation
/// </summary>
internal class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;

    public PersianDateTime NowPersian => PersianDateTime.Now;

    public DateTime NowUtc => DateTime.UtcNow;

    public DateOnly Today => DateOnly.FromDateTime(DateTime.Now);

    public DateOnly TodayUtc => DateOnly.FromDateTime(DateTime.UtcNow);
}
