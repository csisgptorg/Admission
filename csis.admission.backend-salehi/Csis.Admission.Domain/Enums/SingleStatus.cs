namespace Csis.Admission.Domain.Enums;

/// <summary>
/// وضعیت تجرد
/// </summary>
public enum SingleStatus : byte
{
    /// <summary>
    /// عدم ازدواج
    /// </summary>
    Single = 1,

    /// <summary>
    /// فوت همسر
    /// </summary>
    Widowed = 2,

    /// <summary>
    /// طلاق همسر
    /// </summary>
    Divorced = 3
}
