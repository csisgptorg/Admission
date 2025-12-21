using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>سطح نخبگی </summary>
public class EliteLevel : BaseEntity<short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
