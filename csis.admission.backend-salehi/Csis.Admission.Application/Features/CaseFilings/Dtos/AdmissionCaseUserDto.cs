using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Files.Dtos;

namespace Csis.Admission.Application.Features.CaseFilings.Dtos;

/// <summary>
/// Data Transfer Object for AdmissionCaseUser.
/// </summary>
public sealed record AdmissionCaseUserDto : BaseDto<AdmissionCaseUserDto, AdmissionCaseUser, Guid>
{
    /// <summary></summary>
    public string? NationalCode { get; init; }

    /// <summary></summary>
    public string? YektaCode { get; init; }

    /// <summary></summary>
    public int? BirthDate { get; init; }

    /// <summary></summary>
    public string BirthDateStr => BirthDate.IntDateToString();

    /// <summary> لیستی از مشخصات فایل های مدارک </summary>
    public List<CaseFilingFileModelDto> FilesInfo { get; init; } = [];

    /// <summary></summary>
    public string? Mobile { get; init; }

    /// <summary></summary>
    public Citizenship? Citizenship { get; init; }

    /// <summary></summary>
    public ApprovalCenter? ApprovalCenter { get; init; }

    /// <summary></summary>
    public int? CaseNumInApprovalCenter { get; init; }

    /// <summary></summary>
    public long? PostalCode { get; init; }

    /// <summary> سریال کارت ملی </summary>
    public string? NationalCardSerial { get; init; }

    /// <summary></summary>
    public bool ConfirmIdentityInformation { get; init; }

    /// <summary></summary>
    public int? Codm { get; init; }

    /// <summary>
    /// مرحله فعلی
    /// </summary>
    public AdmissionCaseStep? CaseStep { get; init; }

    /// <summary> مذهب </summary>
    public Religion? Religion { get; init; }

    /// <summary></summary>
    public List<PayloadHelper.NamedPayload> Payloads { get; init; } = [];

    /// <summary></summary>
    public long? RequestId { get; init; }

    public override void CustomMappings(IMappingExpression<AdmissionCaseUser, AdmissionCaseUserDto> mapping) {
        base.CustomMappings(mapping);
        mapping.ForMember(d => d.Payloads, opt => opt.MapFrom(s => PayloadHelper.GetPayloadFromString(s.Payloads)));
    }
}
