namespace Csis.Admission.Domain.Enums;


/// <summary>منبع ثبت کننده</summary>
public enum DataSource : short
{
    /// <summary>طلبه</summary>
    Student = 1,
    
    /// <summary>کارمند</summary>
    Employee = 2,

    /// <summary>وب سرور</summary>
    WebService = 3
}
