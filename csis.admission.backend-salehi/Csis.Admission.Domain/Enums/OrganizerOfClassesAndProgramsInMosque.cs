namespace Csis.Admission.Domain.Enums;

/// <summary>
/// برگزار کننده کلاس ها و برنامه های مسجد
/// </summary>
public enum OrganizerOfClassesAndProgramsInMosque : short
{
    /// <summary>
    /// امام جماعت
    /// </summary>
    ImamJamaat = 1,

    /// <summary>
    /// بسیج
    /// </summary>
    Basij = 2,

    /// <summary>
    /// کانون فرهنگی مسجد
    /// </summary>
    FarhangiKanoun = 3,

    /// <summary>
    /// هیئت امناء مسجد
    /// </summary>
    HeyatAmana = 4,

    /// <summary>
    /// سایر
    /// </summary>
    Other = 5
}
