using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Educations.Dtos;

/// <summary>تحصیلات دانشگاهی</summary>
public sealed record EducationDto : BaseDto<EducationDto, Education>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>سال ورود</summary>
    public int? EnteringYear { get; set; }

    /// <summary>وضعیت</summary>
    public EducationStatus? EducationStatus { get; set; }

    /// <summary>مرکز تایید کننده</summary>
    public ApprovalCenter? ApprovalCenter { get; set; }

    /// <summary>شماره پرونده در مرکز تایید کننده</summary>
    public long? CaseNumInApprovalCenter { get; set; }
}
