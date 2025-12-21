using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Common.Models;

/// <summary>درخواست های نیازمند تایید</summary>
public record RequestsToApproveDto : BaseDto<RequestsToApproveDto, Request, long>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>طلبه</summary>
    public string Student { get; set; }

    /// <summary>شناسه تکفل</summary>
    public long? DependentId { get; set; }

    /// <summary>تکفل</summary>
    public string Dependent { get; set; }

    /// <summary>نوع</summary>
    public RequestType Type { get; set; }

    /// <summary>وضعیت تایید</summary>
    public ApprovalStatus ApprovalStatus { get; set; }
}
