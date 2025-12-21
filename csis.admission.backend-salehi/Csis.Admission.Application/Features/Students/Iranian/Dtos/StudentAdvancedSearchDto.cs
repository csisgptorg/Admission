using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>جستجوی پیشرفته طلبه</summary>
public record StudentAdvancedSearchDto : BaseDto<StudentAdvancedSearchDto, StudentSummary>
{
    /// <summary>کد مرکز</summary>
    public int Codm { get; init; }
    /// <summary>ملیت</summary>
    public Citizenship? Citizenship { get; set; }
    /// <summary>کد ملی</summary>
    public string NationalCode { get; init; }
    /// <summary>کد فیدا</summary>
    public string FidaCode { get; init; }
    /// <summary>کد یکتا</summary>
    public string YektaCode { get; init; }
    /// <summary>نام</summary>
    public string FirstName { get; init; }
    /// <summary>نام خانوادگی</summary>
    public string LastName { get; init; }
    /// <summary>نام پدر</summary>
    public string FatherName { get; init; }
    /// <summary>شماره شناسنامه</summary>
    public string BirthCertNumber { get; init; }
}
