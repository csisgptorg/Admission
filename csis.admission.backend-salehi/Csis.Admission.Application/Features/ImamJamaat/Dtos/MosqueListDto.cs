using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ImamJamaat.Dtos;
public sealed record MosqueListDto : BaseDto<MosqueListDto, Domain.Entities.ImamJamaat>
{
    public string FullName { get; init; }
    public int MosqueId { get; init; }
    public int CodM { get; init; }
    public string MosqueOfficialName { get; init; }
    public string NationalCode { get; init; }
    public DateTime MosqueCreatedOn { get; init; }
}
