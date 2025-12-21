using Csis.Admission.Application.Common.Models;
using Csis.Paging;

namespace Csis.Admission.Application.Common.Dtos.RequestService;


/// <summary>تایید طلبه</summary>
public record ApproveRequestByStudentCommand(long RequestId, ApprovalStatus Status);

/// <summary>تایید کارمند</summary>
public record ApproveRequestByEmployeeCommand(long RequestId, ApprovalStatus Status, bool SkipSmsOnRejected);

/// <inheritdoc/>
public sealed record GetEmployeeRequestsToApproveQuery(int? Codm) : BaseSearchQuery, IRequest<IPagedList<RequestsToApproveDto>> { }
/// <summary>همه درخواست ها</summary>
public sealed record AllRequestQuery() : BaseSearchQuery, IRequest<IPagedList<RequestDto>> { }
