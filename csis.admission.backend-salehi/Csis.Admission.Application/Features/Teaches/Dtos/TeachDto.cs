using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Teaches.Dtos;

/// <summary>
/// مدل نمایشی موجودیت ازدواج
/// </summary>
public sealed record TeachDto : BaseDto<TeachDto, Teach>
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// Province
    /// </summary>
    public short? ProvinceId { get; set; }

    /// <summary>
    /// Province
    /// </summary>
    public string Province { get; set; }

    /// <summary>
    /// City
    /// </summary>
    public short? CityId { get; set; }

    /// <summary>
    /// City
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// EducationYear
    /// </summary>
    public short? EducationYearId { get; set; }

    /// <summary>
    /// سال تحصیلی
    /// </summary>
    public string EducationYear { get; set; }

    /// <summary>
    /// EducationSemester
    /// </summary>
    public EducationSemester? EducationSemester { get; set; }

    /// <summary>
    /// مقطع تحصیلی که در آن تدریس میشود
    /// </summary>
    public TeachEducationLevel? EducationLevel { get; set; }

    /// <summary>
    /// Lesson
    /// </summary>
    public string Lesson { get; set; }

    /// <summary>
    /// SchoolId
    /// </summary>
    public short? SchoolId { get; set; }

    /// <summary>
    /// School
    /// </summary>
    public string School { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public short? WeekSession { get; set; }

    /// <summary>
    /// مرکز حوزوی
    /// </summary>
    public ApprovalCenter? ApprovalCenter { get; set; }

    /// <summary>
    /// شناسه تبلیغ در مرکز حوزوی
    /// </summary>
    public string RecordIdInApprovalCenter { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<Teach, TeachDto> mapping) {
        mapping.ForMember(dto => dto.EducationYear, config => config.MapFrom(model => model.EducationYear.Title));
        mapping.ForMember(dto => dto.Province, config => config.MapFrom(model => model.Province.Title));
        mapping.ForMember(dto => dto.City, config => config.MapFrom(model => model.City.Title));
        mapping.ForMember(dto => dto.School, config => config.MapFrom(model => model.School.Title));
    }
}
