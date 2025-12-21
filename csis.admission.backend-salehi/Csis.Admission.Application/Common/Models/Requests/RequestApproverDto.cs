using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public record RequestApproverDto : BaseDto<RequestApproverDto, RequestApprover, long>
{
    /// <inheritdoc/>
    public long RequestId { get;init;}

    /// <inheritdoc/>
    public ApproverRole ApproverRole { get;init;}

    /// <inheritdoc/>
    public int? ApproverCodm { get;init;}

    /// <inheritdoc/>
    public int ApproveDate { get;init;}

    /// <inheritdoc/>
    public TimeSpan ApproveTime { get;init;}

    /// <inheritdoc/>
    public int? ApproverPersonnelId { get;init;}

    /// <inheritdoc/>
    public ApprovalStatus Status { get;init;}
}
