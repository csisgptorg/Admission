using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>لاگ مشاهده اطلاعات طلبه توسط کارمند</summary>
public class EmployeeViewStudentLog : SoftDeletedBaseEntity<long>, IFilterable
{
    /// <summary>کد مرکز</summary>
    public int Codm { get; set; }

    /// <summary>کد پرسنلی</summary>
    public int PersonnelId { get; set; }

    /// <summary>تاریخ</summary>
    public int Date { get; set; }

    /// <summary>زمان</summary>
    public TimeSpan Time { get; set; }

    /// <summary>فیلترها</summary>
    public string[] GetFilterableFields() {
        return [nameof(Codm)];
    }
}
