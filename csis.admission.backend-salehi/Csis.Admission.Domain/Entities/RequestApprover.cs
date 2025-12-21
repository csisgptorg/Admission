using Csis.Admission.Domain.Enums;
using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>درخواست تایید کننده</summary>
public class RequestApprover : SoftDeletedBaseEntity<long>, IFilterable
{
    public RequestApprover() {
    }


    /// <inheritdoc/>
    public RequestApprover(
        ApproverRole approverRole,
        ApprovalStatus status,
        RequestFlow flow,
        string approver = null,
        int? approverCodm = null,
        int? approverPersonnelId = null,
        bool skipSmsOnRejection = false) {
        ApproverRole = approverRole;
        ApproverCodm = approverCodm > 0 ? approverCodm : null;
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
    public Request Request { get; set; }

    /// <summary>نقش تأییدکننده</summary>
    public ApproverRole ApproverRole { get; set; }

    /// <summary>کد مرکز خدمات تأییدکننده</summary>
    public int? ApproverCodm { get; set; }

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
        return [nameof(RequestId), nameof(ApproverRole), nameof(ApproverCodm), nameof(ApproverPersonnelId), nameof(Status)];
    }
}
