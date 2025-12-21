/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Domain.Settings;

/// <summary>
/// تنظیمات نوتیفیکیشن
/// </summary>
public sealed class NotificationSettings : ISettings<NotificationSettings>
{
    /// <summary>
    /// فعال بودن بک گراند سرویس ارسال نوتیفیکیشن
    /// </summary>
    public bool EnableNotificationSenderService { get; set; }

    /// <summary>
    /// تعداد نوتیفیکیشن پردازش شده در هر اجرای بک گراند سرویس
    /// </summary>
    public int NotificationSenderServiceBatchSize { get; set; }

    /// <summary>
    /// فاصله زمانی اجرای بک گراند سرویس ارسال نوتیفیکیشن (بر حسب ثانیه)
    /// </summary>
    public int NotificationSenderServiceIntervalInSeconds { get; set; }

    /// <summary>
    /// حداکثر تعداد تلاش برای ارسال هر نوتیفیکیشن
    /// </summary>
    public int MaxTryPerNotification { get; set; }

    /// <inheritdoc/>
    public NotificationSettings GetDefault() {
        return new() {
            EnableNotificationSenderService = true,
            NotificationSenderServiceBatchSize = 200,
            NotificationSenderServiceIntervalInSeconds = 120,
            MaxTryPerNotification = 10
        };
    }
}
