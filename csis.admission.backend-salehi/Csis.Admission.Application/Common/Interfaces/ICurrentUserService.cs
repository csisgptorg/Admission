namespace Csis.Admission.Application.Common.Interfaces;

public partial interface ICurrentUserService
{
    /// <summary>طلبه</summary>
    Task<bool> IsStudent();

    /// <summary>کد مرکز خدمات</summary>
    Task<int?> Codm();

    /// <summary>پر کردن کد مرکز خدمات</summary>
    Task SetCodm(object obj);

    /// <summary>کارمند</summary>
    Task<bool> IsEmployee();

    /// <summary>کاربر ارشد</summary>
    Task<bool> IsSenior();

    /// <summary>کد پرسنلی</summary>
    Task<int?> PersonnelId();
}
