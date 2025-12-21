/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Shared.Kernel.Public.Models;
using Csis.Shared.Kernel.Public.Models.BaseInformation;
using Csis.Shared.Kernel.Public.Models.Students;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// سرویس دریافت اطلاعات طلاب
/// </summary>
public partial interface IStudentDataService
{
    /// <summary>
    /// سرویس دریافت اطلاعات طلبه ها بصورت لیستی بر اساس لیستی از کدهای مرکز
    /// </summary>
    /// <param name="codmList">لیست کد مرکز</param>
    /// <param name="chunkSize">تعداد افرادی که اطلاعات آن‌ها در هر درخواست دریافت میشود</param>
    /// <returns></returns>
    Task<List<PersonInfoExtended>> GetStudentGroupInfoAsync(IEnumerable<string> codmList, int chunkSize = 1000);

    /// <summary>
    ///  سرویس دریافت اطلاعات تکفل‌ها بصورت لیستی بر اساس لیستی از تکفل آی دی ها
    /// </summary>
    /// <param name="takafolIds">لیست شناسه تکفل</param>
    /// /// <param name="chunkSize">تعداد افرادی که اطلاعات آن‌ها در هر درخواست دریافت میشود</param>
    /// <returns></returns>
    Task<List<PersonInfoExtended>> GetDependantsGroupInfoAsync(IEnumerable<int> takafolIds, int chunkSize = 1000);

    /// <summary>
    ///  سرویس دریافت اطلاعات سرپرست‌ها و تکفل‌ها بصورت لیستی بر اساس لیستی از کد مرکز تکفل آی دی
    /// </summary>
    /// /// <param name="codmList">لیست کد مرکز</param>
    /// <param name="takafolIds">لیست شناسه تکفل</param>
    /// /// <param name="chunkSize">تعداد افرادی که اطلاعات آن‌ها در هر درخواست دریافت میشود</param>
    /// <returns></returns>
    Task<List<PersonInfoExtended>> GetStudentsAndDependantsGroupInfoAsync(IEnumerable<string> codmList, IEnumerable<int> takafolIds, int chunkSize = 1000);

    /// <summary>
    /// سرویس دریافت شعبه مرکز خدمات
    /// </summary>
    /// <returns></returns>
    Task<List<CsisBranch>> GetCsisBranchesAsync();

    /// <summary>
    /// دریافت نام شعبه
    /// </summary>
    /// <param name="branchId">کد شعبه</param>
    /// <returns></returns>
    Task<string> GetBranchNameAsync(int branchId);

    /// <summary>
    /// سرویس دریافت اطلاعات پذیرشی طلبه
    /// </summary>
    /// <param name="codm">کد مرکز</param>
    /// <returns></returns>
    Task<PersonInfoExtended> GetStudentInfoAsync(string codm);

    /// <summary>
    /// سرویس دریافت اطلاعات پذیرشی طلبه با ورودی کد های مختلف
    /// </summary>
    /// <param name="searchTerm">عبارت جستجو شامل کد مرکز، کد ملی یا کد فیدا یا یکتا</param>
    /// <returns></returns>
    Task<PersonInfoExtended> SearchStudentAsync(string searchTerm);

    /// <summary>
    /// سرویس دریافت لیست تکفل های طلبه
    /// </summary>
    /// <param name="codm">کد مرکز</param>
    /// <param name="onlyActiveDependants">فقط تکفل‌های فعال دریافت شود</param>
    /// <returns></returns>
    Task<List<PersonInfoExtended>> GetDependantsAsync(string codm, bool onlyActiveDependants = false);

    /// <summary>
    /// سرویس دریافت اطلاعات طلبه به همراه تکفل‌ها 
    /// </summary>
    /// <param name="codm">کد مرکز</param>
    /// <param name="onlyActiveDependants">فقط تکفل‌های فعال دریافت شود</param>
    /// <returns></returns>
    Task<PersonInfoExtended> GetStudentWithDependantsAsync(string codm, bool onlyActiveDependants = false);

    /// <summary>
    /// جستجوی پیشرفته طلاب
    /// </summary>
    /// <param name="searchParams">پارامترهای جستجو</param>
    /// <returns></returns>
    Task<List<StudentSearchResult>> AdvancedSearchAsync(StudentSearchParam searchParams);
}
