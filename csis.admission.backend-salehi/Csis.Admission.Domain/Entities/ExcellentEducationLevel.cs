using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>مقطع ممتازین</summary>
public class ExcellentEducationLevel : BaseEntity<short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
