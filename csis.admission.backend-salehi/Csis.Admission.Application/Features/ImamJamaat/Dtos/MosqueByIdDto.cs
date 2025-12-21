namespace Csis.Admission.Application.Features.ImamJamaat.Dtos;

/// <summary>
/// مدل اطلاعات مسجد شامل امام جماعت و فعالیت‌های آن
/// </summary>
public sealed record MosqueByIdDto
{
    /// <summary>
    /// مدل مسجد
    /// </summary>
    public MosqueDto Mosque { get; init; }

    /// <summary>
    /// امام جماعت مسجد
    /// </summary>
    public List<ImamJamaatDto> ImamJamaat { get; init; }

    /// <summary>
    /// فعالیت مسجد
    /// </summary>
    public MosqueActivityDto MosqueActivity { get; init; }

    /// <summary>
    /// آدرس مسجد
    /// </summary>
    public MosqueAddressDto MosqueAddress { get; init; }
}
