

using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>
/// اطلاعات طلبه در وب سرویس حقوق و دستمزد
/// </summary>
public class StudentDataForPayRunResult
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>تاریخ ایجاد پرونده</summary>
    public int? CaseCreationDate { get; set; }
    /// <summary>وضعیت فعال بودن پرونده</summary>
    public bool? IsActive { get; set; }
    /// <summary>وضعیت تاهل</summary>
    public bool? IsMarried { get; set; }
    /// <summary>وضعیت نخبگی</summary>
    public bool? IsElite { get; set; }
    /// <summary>وضعیت عائله مندی</summary>
    public bool? HasFamily { get; set; }
    /// <summary>جنسیت</summary>
    public Gender? Gender { get; set; }
    /// <summary>دهک اقتصادی</summary>
    public short? Decile { get; set; }
    /// <summary>تاریخ تولد</summary>
    public int? BirthDate { get; set; }
    /// <summary>وضعیت نقش آفرینی</summary>
    public ReligiousRoleStatus? ReligiousRoleStatus { get; set; }
    /// <summary>مجموع امتیاز هدفمندی</summary>
    public float? TotalTargetScore { get; set; }
    /// <summary>مجموع امتیاز نیازمندی</summary>
    public float? TotalNeedingScore { get; set; }
}

/// <summary>
/// اطلاعات طلبه در وب سرویس حقوق و دستمزد
/// </summary>
public class StudentDataForPayRunDto : IMappable
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }
    /// <summary>تاریخ ایجاد پرونده</summary>
    public string? CaseCreationDate { get; set; }
    /// <summary>وضعیت فعال بودن پرونده</summary>
    public bool? IsActive { get; set; }
    /// <summary>وضعیت تاهل</summary>
    public bool? IsMarried { get; set; }
    /// <summary>وضعیت نخبگی</summary>
    public bool? IsElite { get; set; }
    /// <summary>وضعیت عائله مندی</summary>
    public bool? HasFamily { get; set; }
    /// <summary>جنسیت</summary>
    public Gender? Gender { get; set; }
    /// <summary>دهک اقتصادی</summary>
    public short? Decile { get; set; }
    /// <summary>تاریخ تولد</summary>
    public string? BirthDate { get; set; }
    /// <summary>وضعیت نقش آفرینی</summary>
    public ReligiousRoleStatus? ReligiousRoleStatus { get; set; }
    /// <summary>مجموع امتیاز هدفمندی</summary>
    public float? TotalTargetScore { get; set; }
    /// <summary>مجموع امتیاز نیازمندی</summary>
    public float? TotalNeedingScore { get; set; }

    /// <inheritdoc/>
    public void CreateMappings(Profile profile) {
        profile.CreateMap<StudentDataForPayRunResult, StudentDataForPayRunDto>()
            .ForMember(dest => dest.CaseCreationDate, opt => opt.MapFrom(src => src.CaseCreationDate.IntDateToString()))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.IntDateToString()));
    }
}
