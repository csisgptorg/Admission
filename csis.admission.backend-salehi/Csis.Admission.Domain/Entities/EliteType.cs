using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>نوع نخبگی</summary>
public class EliteType : BaseEntity<short>
{
    /// <summary>عنوان</summary>

    public string Title { get; set; }
}
