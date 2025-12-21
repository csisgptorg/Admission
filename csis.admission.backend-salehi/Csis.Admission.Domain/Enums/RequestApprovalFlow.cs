namespace Csis.Admission.Domain.Enums;

/// <summary>فلو تایید درخواست</summary>
public enum RequestApprovalFlow : short
{
    /// <summary>طلبه</summary>
    Student = 1,

    /// <summary>کاربر</summary>
    Employee = 2,

    /// <summary>کاربر ارشد</summary>
    SeniorEmployee = 3,

    /// <summary>پایان</summary>
    TheEnd = 4
}
