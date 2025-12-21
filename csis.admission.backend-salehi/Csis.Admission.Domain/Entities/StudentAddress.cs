using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// Student address
/// </summary>
public class StudentAddress : BaseEntity
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
