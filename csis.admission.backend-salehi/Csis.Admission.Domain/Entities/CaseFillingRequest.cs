using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.Domain.Entities;

/// <summary>درخواست</summary>
public class CaseFillingRequest : SoftDeletedBaseEntity<long>, IFilterable
{
    /// <summary>کد مرکز</summary>
    public int Codm { get; set; }
    /// <summary>نوع</summary>
    public RequestType Type { get; set; }

    /// <summary>منبع</summary>
    public DataSource Source { get; set; }

    /// <summary>فلو</summary>
    public RequestFlow Flow { get; set; }

    /// <summary>تایید کننده بعدی در فرایند</summary>
    public RequestApprovalFlow NextFlowApprover { get; set; }

    /// <summary>وضعیت</summary>
    public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;

    /// <summary>تاریخ ایجاد</summary>
    public int DateCreated { get; set; }

    /// <summary>زمان ایجاد</summary>
    public TimeSpan TimeCreated { get; set; }

    /// <summary>شناسه پرسنل ایجاد کننده</summary>
    public int? CreatorPersonnelId { get; set; }

    /// <summary>پی لود</summary>
    public string Payload { get; set; }

    /// <summary>نام مدل پی لود</summary>
    public string PayloadModel { get; set; }

    /// <summary>شناسه رکورد</summary>
    public long? RecordId { get; set; }

    /// <summary>شناسه پرسنل</summary>
    public int? PersonnelId { get; set; }

    /// <summary>کارمند</summary>
    public string Employee { get; set; }

    /// <summary>شناسه کاربر</summary>
    public int? UserId { get; set; }

    /// <summary>تاییدهای درخواست</summary>
    public ICollection<CaseFillingRequestApprover> Approvers { get; set; }

    /// <summary>مستندات درخواست</summary>
    public ICollection<CaseFillingRequestDocument> Documents { get; set; }

    /// <summary>تایید مستقیم</summary>
    public void DirectApproved(ApproverRole role) {
        var approver = new CaseFillingRequestApprover {
            Status = ApprovalStatus.Approved,
            ApproveDate = int.Parse(Utilities.PersianDateTime.Now.ToString().Replace("/", "")),
            ApproveTime = DateTime.Now.TimeOfDay
        };

        if ( PersonnelId.HasValue ) {
            approver.ApproverRole = role;
            approver.ApproverPersonnelId = PersonnelId;
            approver.Approver = Employee;

        } else {
            approver.ApproverRole = role;
        }

        Approvers ??= [];
        Approvers.Add(approver);
    }

    /// <summary>تایید کارمند</summary>
    public void EmployeeApprover(int personnelId, string employee, ApprovalStatus status, ApproverRole role = ApproverRole.Employee, bool skipSmsOnRejection = false) {

        var approver = new CaseFillingRequestApprover {
            Status = status,
            ApproveDate = int.Parse(Utilities.PersianDateTime.Now.ToString().Replace("/", "")),
            ApproveTime = DateTime.Now.TimeOfDay,
            ApproverRole = role,
            ApproverPersonnelId = personnelId,
            Approver = employee,
            SkipSmsOnRejection = skipSmsOnRejection
        };
        Approvers.Add(approver);
    }


    public string[] GetFilterableFields() => [nameof(Payload), nameof(DateCreated)];
}
