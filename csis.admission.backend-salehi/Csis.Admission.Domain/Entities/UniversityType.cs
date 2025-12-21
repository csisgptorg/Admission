using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// UniversityType
/// </summary>
public class UniversityType : BaseEntity<short>
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }
}
