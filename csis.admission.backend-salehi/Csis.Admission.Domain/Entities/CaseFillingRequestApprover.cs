using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>درخواست تایید کننده</summary>
public class CaseFillingRequestApprover : SoftDeletedBaseEntity<long>, IFilterable
{
    public CaseFillingRequestApprover() {
    }


    /// <inheritdoc/>
    public CaseFillingRequestApprover(
        ApproverRole approverRole,
        ApprovalStatus status,
        RequestFlow flow,
        string approver = null,
        int? approverPersonnelId = null,
        bool skipSmsOnRejection = false) {
        ApproverRole = approverRole;
        ApproverPersonnelId = approverPersonnelId;
        Approver = approver;
        Status = status;
        ApproveDate = int.Parse(Utilities.PersianDateTime.Now.ToString().Replace("/", ""));
        ApproveTime = DateTime.Now.TimeOfDay;
        Description = flow.ToString();
        SkipSmsOnRejection = skipSmsOnRejection;
    }

    /// <summary>شناسه درخواست</summary>
    public long RequestId { get; set; }

    /// <summary>درخواست</summary>
    public CaseFillingRequest Request { get; set; }

    /// <summary>نقش تأییدکننده</summary>
    public ApproverRole ApproverRole { get; set; }

    /// <summary>شناسه پرسنلی تأییدکننده</summary>
    public int? ApproverPersonnelId { get; set; }

    /// <summary>نام تأییدکننده</summary>
    public string Approver { get; set; }

    /// <summary>تاریخ تأیید</summary>
    public int ApproveDate { get; set; }

    /// <summary>زمان تأیید</summary>
    public TimeSpan ApproveTime { get; set; }

    /// <summary>وضعیت</summary>
    public ApprovalStatus Status { get; set; }

    /// <summary>عدم ارسال پیامک در صورت رد</summary>
    public bool SkipSmsOnRejection { get; set; }


    /// <inheritdoc/>
    public string[] GetFilterableFields() {
        return [nameof(RequestId), nameof(ApproverRole), nameof(ApproverPersonnelId), nameof(Status)];
    }
}
