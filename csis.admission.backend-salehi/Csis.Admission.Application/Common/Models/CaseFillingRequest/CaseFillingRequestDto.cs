using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Files.Dtos;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public record CaseFillingRequestDto : BaseDto<CaseFillingRequestDto, CaseFillingRequest, long>
{
    /// <inheritdoc/>
    public int Codm { get; init; }

    /// <inheritdoc/>
    public long? DependentId { get; init; }

    /// <inheritdoc/>
    public string Student { get; init; }

    /// <inheritdoc/>
    public RequestType Type { get; init; }

    /// <inheritdoc/>
    public DataSource Source { get; init; }

    /// <inheritdoc/>
    public RequestFlow Flow { get; init; }

    /// <inheritdoc/>
    [JsonIgnore]
    public string JsonPayload { get; set; }

    /// <inheritdoc/>
    public ApprovalStatus? ApprovalStatus { get; init; }

    /// <inheritdoc/>
    public string DateCreated { get; init; }

    /// <inheritdoc/>
    public TimeSpan TimeCreated { get; init; }

    /// <inheritdoc/>
    public int? CreatorPersonnelId { get; init; }

    /// <inheritdoc/>
    public string[] StudentApprovers { get; init; }

    /// <summary>شناسه رکورد</summary>
    public long? RecordId { get; init; }

    /// <inheritdoc/>
    [JsonIgnore]
    public List<CaseFillingRequestDocumentDto> Documents { get; init; }

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
    public override void CustomMappings(IMappingExpression<CaseFillingRequest, CaseFillingRequestDto> mapping) {
        mapping.ForMember(dto => dto.DateCreated, config => config.MapFrom(model => model.DateCreated.IntDateToString()));
        mapping.ForMember(dto => dto.Documents, config => config.MapFrom(model => model.Documents));
        mapping.ForMember(x => x.JsonPayload, opt => opt.MapFrom(x => x.Payload));
        mapping.ForMember(x => x.PayloadModelName, opt => opt.MapFrom(x => x.PayloadModel));
    }
}

