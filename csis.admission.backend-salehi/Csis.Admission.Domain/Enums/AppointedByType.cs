namespace Csis.Admission.Domain.Enums;

/// <summary>
/// نهاد صادرکننده حکم امام جماعت
/// </summary>
public enum AppointedByType :short
{
    /// <summary>
    /// سازمان تبلیغات
    /// </summary>
    PropagationOrganization = 1,

    /// <summary>
    /// سازمان اوقاف
    /// </summary>
    EndowmentsOrganization = 2,

    /// <summary>
    /// سازمان امور مساجد
    /// </summary>
    MosqueAffairsOrganization = 3,

    /// <summary>
    /// دفتر امام جمعه 
    /// </summary>
    OfficeOfTheImamJamaat = 4,

    /// <summary>
    /// مرکز مدیریت حوزه
    /// </summary>
    SeminaryManagementCenter = 5,

    /// <summary>
    /// سایر نهادها
    /// </summary>
    Other = 6,

    /// <summary>
    /// فاقد حکم
    /// </summary>
    WithoutWarrant = 7
}
