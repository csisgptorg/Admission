using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>
/// Student phone
/// </summary>
public record StudentPhoneDto : BaseDto<StudentPhoneDto, StudentPhone>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// Mobile
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    /// PreCodeTel
    /// </summary>
    public string PreCodeTel { get; set; }

    /// <summary>
    /// Tel
    /// </summary>
    public string Tel { get; set; }
}
