using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Memorizers.Dtos;
/// <summary>
/// حافظین
/// </summary>
public sealed record StudentMemorizerDto : BaseDto<StudentMemorizerDto, Memorizer>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

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
    public override void CustomMappings(IMappingExpression<Memorizer, StudentMemorizerDto> mapping) {
        mapping.ForMember(dto => dto.CreateDate, config => config.MapFrom(model => model.CreateDate.IntDateToString()));
        mapping.ForMember(dto => dto.ExpireDate, config => config.MapFrom(model => model.ExpireDate.IntDateToString()));
    }
}
