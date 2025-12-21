using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ImamJamaat.Dtos.Commands;

/// <summary>
/// اطلاعات پایه مسجد
/// </summary>
public sealed record MosqueCommandDto : BaseCommandDto<MosqueCommandDto, Mosque>
{
    public int Codm { get; init; }
    public string OfficialName { get; init; }

    public string LocalNames { get; init; }

    public long? PostalCode { get; init; }

    public bool? MosqueHasNotPostalCode { get; init; }

    public MosqueFormFillerRole MosqueFormRole { get; init; }

    public AnnualActivityType MosqueAnnualActivityStatus { get; init; }

    public ActivityPlaceType PlaceType { get; init; }

    public bool? HasClergyHouse { get; init; }

    public ClergyHouseStatus? ClergyHouseStatus { get; init; }

    public string? ManualMosqueId { get; init; }
}
