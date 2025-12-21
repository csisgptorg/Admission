using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ImamJamaat.Dtos;

/// <summary>
/// اطلاعات پایه مسجد
/// </summary>
public sealed record MosqueDto : BaseDto<MosqueDto, Mosque>
{

    /// <summary> کد مرکز </summary>
    public int Codm { get; init; }
    /// <summary></summary>
    public string OfficialName { get; init; }

    /// <summary></summary>
    public string LocalNames { get; init; }

    /// <summary></summary>
    public string PostalCode { get; init; }

    /// <summary>
    /// آیا مسجد دارای کد پستی است؟
    /// </summary>
    public bool? MosqueHasNotPostalCode { get; init; }

    /// <summary> نقش پرکننده فرم مسجد </summary>
    public MosqueFormFillerRole MosqueFormRole { get; init; }

    /// <summary> وضعیت فعالیت سالانه مسجد </summary>
    public AnnualActivityType MosqueAnnualActivityStatus { get; init; }

    /// <summary></summary>
    public ActivityPlaceType PlaceType { get; init; }

    /// <summary></summary>
    public bool HasClergyHouse { get; init; }

    /// <summary></summary>
    public ClergyHouseStatus? ClergyHouseStatus { get; init; }
    public string? ManualMosqueId { get; init; }
}
