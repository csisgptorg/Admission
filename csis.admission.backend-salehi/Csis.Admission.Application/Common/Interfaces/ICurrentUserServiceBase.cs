/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Enums;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// سرویس اطلاعات کاربر جاری
/// </summary>
public partial interface ICurrentUserService
{
    /// <summary>
    /// شناسه کاربر
    /// </summary>
    Task<int?> GetUserIdAsync();

    /// <summary>
    /// شناسه کاربر تفویض دهنده درصورتی که کاربر با تفویض وارد سامانه شده باشد
    /// </summary>
    Task<int?> GetDelegatedUserIdAsync();

    /// <summary>
    /// آیا کاربر جاری دسترسی مورد بررسی را دارد
    /// </summary>
    /// <param name="permission">دسترسی مورد بررسی</param>
    /// <returns></returns>
    Task<bool> IsAuthorizedAsync(PermissionsEnum permission);

    /// <summary>
    /// دریافت کد شعبه کاربر جاری - کارمند
    /// </summary>
    /// <returns></returns>
    Task<int> GetEmployeeBranchIdAsync();

    /// <summary>
    /// دریافت کد شعبه کاربر جاری - طلبه
    /// </summary>
    /// <returns></returns>
    Task<int> GetStudentBranchIdAsync();

    /// <summary>
    /// دریافت کد پرسنلی کارمند
    /// </summary>
    /// <returns></returns>
    Task<int?> GetPersonnelIdAsync();

    /// <summary>
    /// دریافت کد مرکز طلبه
    /// </summary>
    /// <returns></returns>
    Task<string> GetCodmAsync();

    /// <summary>
    /// آیا کاربر وارد شده کارمند است
    /// </summary>
    /// <returns></returns>
    Task<bool> IsEmployeeAsync();

    /// <summary>
    /// آیا کاربر وارد شده طلبه است
    /// </summary>
    /// <returns></returns>
    Task<bool> IsStudentAsync();

    /// <summary>
    /// بررسی مجاز بودن سامانه برای کاربر
    /// </summary>
    /// <returns></returns>
    Task<bool> HasAccessToThisApplicationAsync();
}
