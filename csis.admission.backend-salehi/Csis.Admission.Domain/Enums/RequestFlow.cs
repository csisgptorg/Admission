namespace Csis.Admission.Domain.Enums;

/// <summary>فلو درخواست</summary>
public enum RequestFlow : short
{
    /// <summary> ثبت مستقیم </summary>
    DirectRegistration = 1,

    /// <summary> طلبه - طلبه </summary>
    DualStudents = 2,

    /// <summary> طلبه - کاربر </summary>
    StudentToEmployee = 3,

    /// <summary> طلبه - کاربر - کاربر ارشد </summary>
    StudentToEmployeeToSeniorEmployee = 4,

    /// <summary> کاربر - کاربر ارشد </summary>
    EmployeeToSeniorEmployee = 5
}
