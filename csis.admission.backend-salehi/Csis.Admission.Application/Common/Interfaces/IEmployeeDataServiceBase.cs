/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Models;
using Csis.Shared.Kernel.Public.Models.Employee;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// سرویس دریافت اطلاعات کارمندان
/// </summary>
public partial interface IEmployeeDataService
{
    /// <summary>
    /// دریافت اطلاعات کارمند
    /// </summary>
    /// <param name="personnelId">کد پرسنلی</param>
    /// <param name="activeOnly">فقط کارمند با وضعیت فعال را دریافت کن</param>
    /// <returns></returns>
    Task<EmployeeInfo> GetEmployeeInfoAsync(int personnelId, bool activeOnly = false);

    /// <summary>
    /// دریافت اطلاعات کارمنان به صورت گروهی
    /// </summary>
    /// <param name="personnelIds">لیست کد پرسنلی</param>
    /// <param name="chunkSize">تعداد افرادی که اطلاعات آن‌ها در هر درخواست دریافت میشود</param>
    /// <returns></returns>
    Task<List<EmployeeInfo>> GetEmployeesGroupInfoAsync(IEnumerable<int> personnelIds, int chunkSize = 1000);

    /// <summary>
    /// دریافت اطلاعات کارمنان به صورت گروهی
    /// </summary>
    /// <param name="personnelIds">لیست کد پرسنلی</param>
    /// <param name="takafolIds">لیست شناسه تکفل</param>
    /// <param name="chunkSize">تعداد افرادی که اطلاعات آن‌ها در هر درخواست دریافت میشود</param>
    /// <returns></returns>
    Task<List<EmployeePersonInfo>> GetEmployeesAndDependantsGroupInfoAsync(IEnumerable<int> personnelIds, IEnumerable<int> takafolIds, int chunkSize = 1000);

    /// <summary>
    ///  سرویس دریافت اطلاعات تکفل‌ها بصورت لیستی بر اساس لیستی از تکفل آی دی ها
    /// </summary>
    /// <param name="takafolIds">لیست شناسه تکفل</param>
    /// <param name="chunkSize">تعداد افرادی که اطلاعات آن‌ها در هر درخواست دریافت میشود</param>
    /// <returns></returns>
    Task<List<EmployeePersonInfo>> GetDependantsGroupInfoAsync(IEnumerable<int> takafolIds, int chunkSize = 1000);

    /// <summary>
    /// دریافت اطلاعات تماس کارمندان بر اساس لیست کد پرسنلی
    /// </summary>
    /// <param name="personnelIds">لیست کد پرسنلی</param>
    /// <returns></returns>
    Task<List<EmployeeContactInfo>> GetEmployeeContactInfoAsync(List<int> personnelIds);

    /// <summary>
    /// دریافت لیست همه جایگاه‌های شغلی
    /// </summary>
    /// <returns></returns>
    Task<List<JobPositionModel>> GetAllJobPositionsAsync();

    /// <summary>
    /// دریافت لیست جایگاه‌های شغلی یک نفر براساس کد پرسنلی
    /// </summary>
    /// <param name="personnelId">کد پرسنلی</param>
    /// <returns></returns>
    Task<List<JobPositionModel>> GetJobPositionsByPersonnelIdAsync(int personnelId);
}
