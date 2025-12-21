/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Interfaces.Repositories;

/// <summary>
/// مخزن فایل
/// </summary>
public partial interface IFileRepository : IRepository<UploadedFile>
{
    /// <summary>
    /// بررسی معتبر بودن شناسه فایل
    /// </summary>
    /// <param name="identifier">شناسه فایل</param>
    /// <param name="type">نوع فایل مورد انتظار</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsValidAsync(Guid identifier, FileTypes type, CancellationToken cancellationToken = default);
}
