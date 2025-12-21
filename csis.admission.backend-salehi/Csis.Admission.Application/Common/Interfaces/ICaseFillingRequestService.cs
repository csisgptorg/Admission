using Csis.Admission.Application.Common.Dtos.RequestService;
using Csis.Admission.Application.Common.Models;
using Csis.Paging;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>سرویس درخواست</summary>
public partial interface ICaseFillingRequestService
{
    /// <summary>ثبت درخواست</summary>
    Task<long> Create(CreateCaseFillingRequestCommand command, CancellationToken cancellationToken);

    /// <summary>تایید کارمند</summary>
    Task ApproveRequestByEmployee(ApproveCaseFillingRequestByEmployeeCommand dto, CancellationToken cancellationToken);

    /// <summary>دریافت</summary>
    Task<CaseFillingRequestDto> GetById(long id, CancellationToken cancellationToken);

    /// <summary>دریافت درخواست های نیازمند تایید کارمند</summary>
    Task<IPagedList<SearchPersonnelCaseFillingRequestsToApproveResult>> GetEmployeeRequestsToApprove
        (GetEmployeeCaseFillingRequestsToApproveQuery query, CancellationToken cancellationToken);

    /// <summary>دریافت همه درخواست های نیازمند تایید کارمند</summary>
    Task<IPagedList<SearchPersonnelCaseFillingRequestsToApproveResult>> GetAllCaseFillingRequest
        (GetEmployeeCaseFillingRequestsToApproveQuery query, CancellationToken cancellationToken);
}
