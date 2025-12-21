using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// Student phone
/// </summary>
public class StudentPhone : BaseEntity
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
