/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Interfaces.Repositories;

/// <summary>
/// مخزن نوتیفیکیشن
/// </summary>
public interface INotificationRepository : IRepository<Domain.Entities.Notification>
{
    /// <summary>
    /// دریافت نوتیفیکیشن‌های درانتظار ارسال
    /// </summary>
    /// <param name="maxCount"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Domain.Entities.Notification>> GetPendingNotificationsAsync(int maxCount, CancellationToken cancellationToken);

    /// <summary>
    /// بررسی وجود نوتیفیکیشن درانتظار پردازش
    /// </summary>
    /// <returns></returns>
    Task<bool> HasPendingNotificationAsync(CancellationToken cancellationToken);
}
