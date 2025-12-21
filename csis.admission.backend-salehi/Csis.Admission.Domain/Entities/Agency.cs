using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>نمایندگی</summary>
public class Agency : BaseEntity<short>
{
    /// <summary>عنوان</summary>
    public string Title { get; set; }

    /// <summary>شناسه شعبه</summary>
    public int BranchId { get; set; }
}
