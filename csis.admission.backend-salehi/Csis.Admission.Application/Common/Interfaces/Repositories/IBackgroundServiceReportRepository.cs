/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Interfaces.Repositories;

/// <summary>
/// مخزن گزارش اجرای سرویس پس زمینه
/// </summary>
public interface IBackgroundServiceReportRepository
{
    /// <summary>
    /// دریافت تاریخ آخرین اجرای موفق سرویس
    /// </summary>
    /// <param name="serviceTitle">عنوان سرویس</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<DateTime?> GetLastFinishedDateAsync(string serviceTitle, CancellationToken cancellationToken);

    /// <summary>
    /// آیا سرویس درحال اجرا است
    /// </summary>
    /// <param name="serviceTitle">نام سرویس</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsRunningAsync(string serviceTitle, CancellationToken cancellationToken);

    /// <summary>
    /// اجرای سرویس
    /// </summary>
    /// <param name="serviceTitle">عنوان سرویس</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<BackgroundServiceReport> StartAsync(string serviceTitle, CancellationToken cancellationToken);

    /// <summary>
    /// پایان اجرای سرویس
    /// </summary>
    /// <param name="reportId">شناسه گزارش</param>
    /// <param name="report">شرح گزارش</param>
    /// <param name="elapsedMilliseconds">زمان اجرا به میلی ثانیه</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task FinishAsync(int reportId, string report, long elapsedMilliseconds, CancellationToken cancellationToken);

    /// <summary>
    /// پایان اجرای سرویس با خطا
    /// </summary>
    /// <param name="reportId">شناسه گزارش</param>
    /// <param name="error">شرح خطا</param>
    /// <param name="elapsedMilliseconds">زمان اجرا به میلی ثانیه</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task FinishWithErrorAsync(int reportId, string error, long elapsedMilliseconds, CancellationToken cancellationToken);

    /// <summary>
    /// توقف اجباری همه سرویس‌هایی که مدت طولانی درحال اجرا هستند یا به هر دلیلی در وضعیت درحال اجرا باقی مانده اند
    /// </summary>
    /// <param name="longRunningThreshold">مدت زمانی که یک سرویس به عنوان اجرای طولانی در نظر گرفته میشود</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ForceStopLongRunningServicesAsync(TimeSpan longRunningThreshold, CancellationToken cancellationToken);

    /// <summary>
    /// توقف اجباری سرویس‌هایی که مدت طولانی درحال اجرا هستند یا به هر دلیلی در وضعیت درحال اجرا باقی مانده اند
    /// </summary>
    /// <param name="longRunningThreshold">مدت زمانی که یک سرویس به عنوان اجرای طولانی در نظر گرفته میشود</param>
    /// <param name="serviceTitle">عنوان سرویس</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ForceStopLongRunningServicesAsync(TimeSpan longRunningThreshold, string serviceTitle, CancellationToken cancellationToken);
}
