using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// ResearchSubject
/// </summary>
public class ResearchSubject : BaseEntity<short>
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }
}

