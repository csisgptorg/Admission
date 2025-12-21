using Csis.Admission.Application.Common.Dtos.RequestService;
using Csis.Admission.Application.Common.Models;
using Csis.Paging;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>سرویس درخواست</summary>
public partial interface IRequestService
{
    /// <summary>ثبت درخواست</summary>
    Task<long> Create(CreateRequestCommand command, CancellationToken cancellationToken);

    /// <summary>تایید طلبه</summary>
    Task ApproveRequestByStudent(ApproveRequestByStudentCommand dto, CancellationToken cancellationToken);

    /// <summary>تایید کارمند</summary>
    Task ApproveRequestByEmployee(ApproveRequestByEmployeeCommand dto, CancellationToken cancellationToken);

    /// <summary>دریافت</summary>
    Task<RequestDto> GetById(long id, CancellationToken cancellationToken);

    /// <summary>دریافت درخواست های طلبه</summary>
    Task<List<RequestDto>> GetAllByCodmAsync(int? codm, bool? isCompleted, CancellationToken cancellationToken);

    /// <summary>دریافت درخواست های نیازمند تایید طلبه</summary>
    Task<RequestsToApproveDto[]> GetStudentRequestsToApprove(CancellationToken cancellationToken);

    /// <summary>دریافت درخواست های نیازمند تایید کارمند</summary>
    Task<IPagedList<SearchPersonnelRequestsToApproveResult>> GetEmployeeRequestsToApprove
        (GetEmployeeRequestsToApproveQuery query, CancellationToken cancellationToken);

    /// <summary>دریافت درخواست ها با صفحه بندی</summary>
    Task<IPagedList<RequestDto>> GetAllAsync(AllRequestQuery query, CancellationToken cancellationToken);

    /// <summary>
    /// دریافت مقایسه کامل درخواست با داده‌های فعلی
    /// <para>این متد شامل: داده فعلی دیتابیس + تغییرات درخواستی + لیست تفاوت‌ها</para>
    /// </summary>
    /// <param name="requestId">شناسه درخواست</param>
    /// <param name="cancellationToken">توکن لغو</param>
    /// <returns>نتیجه کامل مقایسه شامل داده فعلی، تغییرات درخواستی و لیست تفاوت‌ها</returns>
    Task<RequestComparisonDetailResult> GetRequestComparisonDetailAsync(long requestId, CancellationToken cancellationToken);
}
