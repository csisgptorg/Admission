using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>
/// Student address
/// </summary>
public record StudentAddressDto : BaseDto<StudentAddressDto, StudentAddress>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// FullAddress
    /// </summary>
    public string FullAddress { get; set; }
}
