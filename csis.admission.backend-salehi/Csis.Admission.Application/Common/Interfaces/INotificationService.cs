/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// سرویس نوتیفیکیشن
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// پردازش متن قالب نوتیفیکیشن
    /// </summary>
    /// <param name="template">قالب نوتیفیکیشن</param>
    /// <param name="parameters">پارامترها</param>
    /// <returns>نوتیفیکیشن پردازش شده</returns>
    string ProcessTemplate(string template, Dictionary<string, string> parameters);

    /// <summary>
    /// دریافت لیست پارامترهای مجاز برای قالب نوتیفیکیشن
    /// </summary>
    /// <returns></returns>
    List<string> GetValidParamNames();

    /// <summary>
    /// اعتبارسنجی پارامترهای قالب نوتیفیکیشن
    /// </summary>
    /// <param name="template">قالب نوتیفیکیشن</param>
    /// <returns></returns>
    bool ValidateTemplate(string template);

    /// <summary>
    /// ارسال نوتیفیکیشن به طلبه
    /// </summary>
    /// <param name="codm">کد مرکز</param>
    /// <param name="message">متن پیام</param>
    /// <param name="deliveryChannels">کانال‌های ارسال</param>
    /// <param name="type">نوع نوتیفیکیشن</param>
    /// <param name="priority">اولویت</param>
    /// <param name="scheduleDate">تاریخ زمانبدی ارسال</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SendToStudentAsync(int codm, string message, List<int> deliveryChannels, NotificationType type, NotificationPriority priority = NotificationPriority.Normal, DateTime? scheduleDate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// ارسال نوتیفیکیشن به طلبه با استفاده از قالب نوتیفیکیشن
    /// </summary>
    /// <param name="codm">کد مرکز</param>
    /// <param name="template">قالب نوتیفیکیشن</param>
    /// <param name="deliveryChannels">کانال‌های ارسال</param>
    /// <param name="type">نوع نوتیفیکیشن</param>
    /// <param name="parameters">لیست پارامترها جهت جایگذاری در قالب نوتیفیکیشن</param>
    /// <param name="priority">اولویت</param>
    /// <param name="scheduleDate">تاریخ زمانبدی ارسال</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SendToStudentTemplateAsync(int codm, string template, List<int> deliveryChannels, NotificationType type, Dictionary<string, string> parameters, NotificationPriority priority = NotificationPriority.Normal, DateTime? scheduleDate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// ارسال نوتیفیکیشن به کارمند
    /// </summary>
    /// <param name="personnelId">کد پرسنلی</param>
    /// <param name="message">متن پیام</param>
    /// <param name="deliveryChannels">کانال‌های ارسال</param>
    /// <param name="type">نوع نوتیفیکیشن</param>
    /// <param name="priority">اولویت</param>
    /// <param name="scheduleDate">تاریخ زمانبدی ارسال</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SendToEmployeeAsync(int personnelId, string message, List<int> deliveryChannels, NotificationType type, NotificationPriority priority = NotificationPriority.Normal, DateTime? scheduleDate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// ارسال نوتیفیکیشن به کارمند با استفاده از قالب نوتیفیکیشن
    /// </summary>
    /// <param name="personnelId">کد پرسنلی</param>
    /// <param name="template">قالب نوتیفیکیشن</param>
    /// <param name="deliveryChannels">کانال‌های ارسال</param>
    /// <param name="type">نوع نوتیفیکیشن</param>
    /// <param name="parameters">لیست پارامترها جهت جایگذاری در قالب نوتیفیکیشن</param>
    /// <param name="priority">اولویت</param>
    /// <param name="scheduleDate">تاریخ زمانبدی ارسال</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SendToEmployeeTemplateAsync(int personnelId, string template, List<int> deliveryChannels, NotificationType type, Dictionary<string, string> parameters, NotificationPriority priority = NotificationPriority.Normal, DateTime? scheduleDate = null, CancellationToken cancellationToken = default);
}
