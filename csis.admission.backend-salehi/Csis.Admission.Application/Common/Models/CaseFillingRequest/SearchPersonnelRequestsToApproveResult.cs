using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Files.Dtos;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Common.Models;

/// <summary>درخواست </summary>
public record SearchPersonnelCaseFillingRequestsToApproveResult : BaseDto<SearchPersonnelCaseFillingRequestsToApproveResult, CaseFillingRequest, long>
{
    /// <summary>شناسه درخواست</summary>
    public long RequestId { get; init; }

    /// <summary>مرکز خدمات </summary>
    public long? Codm { get; set; }

    /// <summary>طلبه</summary>
    public string Student { get; set; }

    /// <summary>پی لود</summary>
    [JsonIgnore]
    public string Payload { get; set; }

    /// <summary>نوع</summary>
    public RequestType Type { get; set; }

    /// <summary>وضعیت تایید</summary>
    public ApprovalStatus ApprovalStatus { get; set; }

    /// <summary>تاریخ ثبت</summary>
    public string DateCreated { get; set; }

    /// <inheritdoc/>
    [JsonIgnore]
    public List<CaseFillingRequestDocumentDto> Documents { get; init; }

    /// <summary>
    /// لیستی از مشخصات فایل های مدارک
    /// </summary>
    public List<CaseFilingFileModelDto> FilesInfo { get; init; } = [];

    /// <summary>نام مدل پی لود</summary>
    [JsonIgnore]
    public string PayloadModelName { get; init; }

    /// <summary>
    /// مدل پی لود به صورت آبجکت
    /// </summary>
    public object PayloadModelObject { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<CaseFillingRequest, SearchPersonnelCaseFillingRequestsToApproveResult> mapping) {
        mapping.ForMember(dto => dto.Codm, config => config.MapFrom(model => model.RecordId));
        mapping.ForMember(dto => dto.Type, config => config.MapFrom(model => model.Type));
        mapping.ForMember(dto => dto.DateCreated, config => config.MapFrom(model => model.DateCreated.IntDateToString()));
        mapping.ForMember(x => x.Payload, config => config.MapFrom(model => model.Payload));
        mapping.ForMember(dto => dto.Documents, config => config.MapFrom(model => model.Documents));
        mapping.ForMember(x=>x.PayloadModelName , config=> config.MapFrom(model=> model.PayloadModel));
    }
}
