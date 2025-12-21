using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>بروزرسانی وضعیت درخواست کمیسیون</summary>
public class SetCommissionRequestRepoCommand : RepoCommandLogParam
{
    /// <summary>شناسه</summary>
    public int CommissionRequestId { get; set; }

    /// <summary>وضعیت</summary>
    public CommissionRequestStatus Status { get; set; }

    /// <summary>توضیحات</summary>
    public string Description { get; set; }
}
