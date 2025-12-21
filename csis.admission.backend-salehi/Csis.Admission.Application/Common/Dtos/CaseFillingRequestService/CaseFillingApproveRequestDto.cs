using Csis.Paging;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Common.Dtos.RequestService;


/// <summary>تایید طلبه</summary>
public record ApproveCaseFillingRequestByStudentCommand(long RequestId, ApprovalStatus Status);

/// <summary>تایید کارمند</summary>
public record ApproveCaseFillingRequestByEmployeeCommand(long RequestId, ApprovalStatus Status, bool SkipSmsOnRejected);

/// <inheritdoc/>
public sealed record GetEmployeeCaseFillingRequestsToApproveQuery() : BaseSearchQuery, IRequest<IPagedList<CaseFillingRequestsToApproveDto>> { }
