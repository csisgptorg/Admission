using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>مدرسه</summary>
public class School : BaseEntity<short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }
}
