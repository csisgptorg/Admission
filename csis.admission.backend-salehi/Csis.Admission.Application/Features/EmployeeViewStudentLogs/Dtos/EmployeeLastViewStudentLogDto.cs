namespace Csis.Admission.Application.Features.ViewLogs.Dtos;

/// <summary>تاریخچه آخرین مشاهدات کاربر</summary>
public sealed record EmployeeLastViewStudentLogDto
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }

    /// <summary>اطلاعات طلبه</summary>
    public string FullName { get; init; }
}
