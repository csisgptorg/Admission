/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// سرویس کد رهگیری
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface ITrackingCodeService<TEntity> where TEntity : class, IEntity, ITrackableEntity
{
    /// <summary>
    /// تولید کد رهگیری با فرمول دلخواه
    /// </summary>
    /// <param name="generator">تولید کننده کد رهگیری - طول خروجی باید حداقل 6 و حداکثر 30 کاراکتر باشد</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GetTrackingCodeAsync(Func<string> generator, CancellationToken cancellationToken = default);

    /// <summary>
    /// تولید کد رهگیری به صورت عدد تصادفی با طول مشخص
    /// </summary>
    /// <param name="codeLength">طول کد رهگیری - مقادیر 6 تا 30 مجاز می‌باشد</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GetRandomTrackingCodeAsync(int codeLength = 8, CancellationToken cancellationToken = default);

    /// <summary>
    /// تولید کد رهگیری بر اساس زمان با طول 16 کاراکتر
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GetTimeBasedTrackingCodeAsync(CancellationToken cancellationToken = default);
}
