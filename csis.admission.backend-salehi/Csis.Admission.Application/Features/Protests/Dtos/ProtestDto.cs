using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Protests.Dtos;

/// <summary>اعتراض</summary>
public sealed record ProtestDto : BaseDto<ProtestDto, Protest, long>
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }
    /// <summary>آیدی فیلد مورد اعتراض</summary>
    public ProtestFormTitle FieldId { get; init; }

    /// <summary>فیلد مورد اعتراض</summary>
    public string Field { get; init; }

    /// <summary>مقدار فیلد مورد اعتراض</summary>
    public string FieldValue { get; init; }

    /// <summary>میتواند اعترض کند</summary>
    public bool ProtestPossibility { get; init; }

    /// <summary>توضیحات</summary>
    public string Description { get; init; }

    /// <summary>شناسه درخواست</summary>
    public long? RequestId { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<Protest, ProtestDto> mapping) {
        mapping.ForMember(dto => dto.Field, config => config.MapFrom(model => model.FieldTitle));
    }
}
