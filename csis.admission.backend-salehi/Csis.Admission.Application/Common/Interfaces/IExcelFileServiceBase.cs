/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// سرویس فایل اکسل
/// </summary>
public partial interface IExcelFileService
{
    /// <summary>
    /// ساخت فایل اکسل
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">داده‌ها</param>
    /// <param name="sheetName">نام شیت اکسل</param>
    /// <returns></returns>
    Task<byte[]> ExportToExcelAsync<T>(List<T> data, string sheetName);

    /// <summary>
    /// ساخت فایل اکسل
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="perSheetData">داده‌های هر شیت</param>
    /// <returns></returns>
    Task<byte[]> ExportToExcelAsync<T>(List<(List<T> data, string sheetName)> perSheetData);

    /// <summary>
    /// پیش پردازش هدر ستون‌های فایل اکسل
    /// </summary>
    /// <param name="fileBytes">فایل اکسل ورودی</param>
    /// <param name="headerCellProcessor">پردازشگر هدر - مقدار اصلی هدر به عنوان ورودی داده می‌شود و مقدار پردازش شده را باید برگرداند</param>
    /// <param name="headerMappings">مپینگ نام هدرها - با استفاده از این دیکشنری نام هدرها پس از اجرای پردازشگر مپ می‌شود</param>
    /// <param name="cancellationToken"></param>
    /// <returns>قایل اکسل با هدرهای پردازش شده</returns>
    Task<byte[]> PreprocessExcelHeaderAsync(byte[] fileBytes, Func<string, string> headerCellProcessor, Dictionary<string, string> headerMappings = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// خواندن فایل
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    List<T> ReadFile<T>(Stream stream) where T : class;

    /// <summary>
    /// خواندن فایل
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bytes"></param>
    /// <returns></returns>
    List<T> ReadFile<T>(byte[] bytes) where T : class;
}
