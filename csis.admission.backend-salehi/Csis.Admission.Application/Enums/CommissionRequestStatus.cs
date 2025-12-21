#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Application.Enums;

/// <summary>وضعیت های کمیسیون درخواست</summary>
public enum CommissionRequestStatus
{
    [Display(Name = "عدم تایید مدیر شعبه")]
    BranchManagerRejected = 1,

    [Display(Name = "در انتظار مدیر شعبه")]
    BranchManagerPending = 2,

    [Display(Name = "عدم تایید کارشناس پذیرش ستاد")]
    HqExpertRejected = 3,

    [Display(Name = "در انتظار کارشناس پذیرش ستاد")]
    HqExpertPending = 4,

    [Display(Name = "عدم تایید در جلسه کمیسیون")]
    CommissionRejected = 5,

    [Display(Name = "در انتظار جلسه کمیسیون")]
    CommissionPending = 6,

    [Display(Name = "تایید جلسه کمیسیون")]
    CommissionApproved = 8,

    [Display(Name = "در انتظار اقدام کارشناس شعبه")]
    BranchExpertActionPending = 10,

    [Display(Name = "اقدام توسط کارشناس شعبه")]
    BranchExpertActionDone = 12,

    [Display(Name = "تمدید تاریخ اعتبار کمیسیون")]
    CommissionValidityExtended = 14
}
