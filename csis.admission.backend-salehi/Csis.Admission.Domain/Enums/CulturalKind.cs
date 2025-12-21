namespace Csis.Admission.Domain.Enums;

/// <summary>
/// نوع مدیریت موسسات مختلف
/// </summary>
public enum CulturalKind : short
{
    /// <summary>
    /// مدیریت موسسات فرهنگی
    /// </summary>
    CulturalInstitutions = 1,

    /// <summary>
    /// مدیریت موسسات دینی
    /// </summary>
    ReligiousInstitutions = 2,

    /// <summary>
    /// مدیریت هیأت مذهبی
    /// </summary>
    ReligiousCommittees = 3,

    /// <summary>
    /// سایر انواع مدیریت
    /// </summary>
    Other = 4
}
