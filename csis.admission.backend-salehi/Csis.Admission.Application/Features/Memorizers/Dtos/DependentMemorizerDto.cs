using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Memorizers.Dtos;
/// <summary>
/// حافظین
/// </summary>
public sealed record DependentMemorizerDto : BaseDto<DependentMemorizerDto, Memorizer>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// DependentId
    /// </summary>
    public long? DependentId { get; set; }

    /// <summary>تکفل</summary>
    public string FullName { get; set; }

    /// <summary>نسبت</summary>
    public DependentRelation? Relation { get; set; }

    /// <summary>
    /// Kind
    /// </summary>
    public MemorizationType? Kind { get; set; }

    /// <summary>
    /// JozCount
    /// </summary>
    public int? JozCount { get; set; }

    /// <summary>
    /// ApprovalCenter
    /// </summary>
    public ApprovalCenter? ApprovalCenter { get; set; }

    /// <summary>
    /// CreateDate
    /// </summary>
    public string CreateDate { get; set; }

    /// <summary>
    /// ExpireDate
    /// </summary>
    public string ExpireDate { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<Memorizer, DependentMemorizerDto> mapping) {
        mapping.ForMember(dto => dto.CreateDate, config => config.MapFrom(model => model.CreateDate.IntDateToString()));
        mapping.ForMember(dto => dto.ExpireDate, config => config.MapFrom(model => model.ExpireDate.IntDateToString()));
        mapping.ForMember(dto => dto.FullName, config => config.MapFrom(model => model.Dependent.FullName));
        mapping.ForMember(dto => dto.Relation, config => config.MapFrom(model => model.Dependent.Relation));
    }
}
