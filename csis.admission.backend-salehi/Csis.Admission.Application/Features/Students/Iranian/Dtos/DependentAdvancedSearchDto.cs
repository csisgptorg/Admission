using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>جستجوی پیشرفته تکفل</summary>
public record DependentAdvancedSearchDto : BaseDto<DependentAdvancedSearchDto, DependentSummary,long>
{
    /// <summary>کد مرکز</summary>
    public int Codm { get; init; }

    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; init; }

    /// <summary>نسبت</summary>
    public DependentRelation? Relation { get; init; }

    /// <summary>عناوین نسبت - متفاوت از اینام و شخصی سازی شده</summary>
    public string RelationTitle { get; set; }

    /// <summary>ملیت</summary>
    public Citizenship? Citizenship { get; init; }

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
