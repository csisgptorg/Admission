namespace Csis.Admission.Domain.Enums;

/// <summary>
/// نوع ایجاد شخص
/// </summary>
public enum PersonCreateType : short
{
    /// <summary>
    /// ایجاد از طریق وب سرویس
    /// </summary>
    WebService = 1,
    /// <summary>
    /// ایجاد دستی توسط کاربر
    /// </summary>
    ManualByUser = 2
}
