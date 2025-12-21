namespace Csis.Admission.Domain.Enums;

/// <inheritdoc/>
public enum ApprovalStatus : short
{
    /// <summary>در انتظار</summary>
    Pending = 1,

    /// <summary>تایید</summary>
    Approved = 2,

    /// <summary>رد</summary>
    Rejected = 3,

    /// <summary>لغو</summary>
    Canceled = 4
}
