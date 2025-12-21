using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Files.Dtos;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Common.Models;

/// <summary>درخواست </summary>
public record SearchPersonnelRequestsToApproveResult : BaseDto<SearchPersonnelRequestsToApproveResult, Request, long>
{
    /// <summary>شناسه درخواست</summary>
    public long RequestId { get; init; }

    /// <summary>مرکز خدمات </summary>
    public int Codm { get; set; }

    /// <summary>طلبه</summary>
    public string Student { get; set; }

    /// <summary>شناسه تکفل</summary>
    public long? DependentId { get; set; }

    /// <summary>تکفل</summary>
    public string Dependent { get; set; }

    /// <summary>پی لود</summary>
    [JsonIgnore]
    public string Payload { get; set; }

    /// <summary>نوع</summary>
    public RequestType Type { get; set; }

    /// <summary>وضعیت تایید</summary>
    public ApprovalStatus ApprovalStatus { get; set; }

    /// <summary>پرونده فعال است</summary>
    public bool CaseIsActive { get; set; }

    /// <summary>تاریخ ثبت</summary>
    public string DateCreated { get; set; }

    /// <inheritdoc/>
    [JsonIgnore]
    public List<RequestDocumentDto> Documents { get; init; }

    /// <summary>
    /// لیستی از مشخصات فایل های مدارک
    /// </summary>
    public List<FileModelDto> FilesInfo { get; init; } = [];

    /// <summary>نام مدل پی لود</summary>
    [JsonIgnore]
    public string PayloadModelName { get; init; }

    /// <summary>
    /// مدل پی لود به صورت آبجکت
    /// </summary>
    public object PayloadModelObject { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<Request, SearchPersonnelRequestsToApproveResult> mapping) {
        mapping.ForMember(dto => dto.DateCreated, config => config.MapFrom(model => model.DateCreated.IntDateToString()));
        mapping.ForMember(dto => dto.Codm, config => config.MapFrom(model => model.Codm));
        mapping.ForMember(dto => dto.Student, config => config.MapFrom(model => model.Student));
        mapping.ForMember(dto => dto.DependentId, config => config.MapFrom(model => model.DependentId));
        mapping.ForMember(dto => dto.Dependent, config => config.MapFrom(model => model.Dependent));
        mapping.ForMember(dto => dto.Type, config => config.MapFrom(model => model.Type));
        mapping.ForMember(dto => dto.DateCreated, config => config.MapFrom(model => model.DateCreated.IntDateToString()));
        mapping.ForMember(x => x.Payload, config => config.MapFrom(model => model.Payload));
        mapping.ForMember(dto => dto.Documents, config => config.MapFrom(model => model.Documents));
        mapping.ForMember(x => x.PayloadModelName, config => config.MapFrom(model => model.PayloadModel));
    }
}

