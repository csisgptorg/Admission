namespace Csis.Admission.Domain.Enums;

/// <summary>نوع بیمه پایه</summary>
public enum EmploymentInsuranceType:short
{
    /// <summary>بيمه تامين اجتماعي</summary>
    SocialSecurity = 1,

    /// <summary>بيمه نيروهاي مسلح</summary>
    ArmedForces = 2,

    /// <summary>بيمه بنياد شهيد و امور ايثارگران</summary>
    MartyrsFoundation = 4,

    /// <summary>بيمه بانک</summary>
    BankInsurance = 5,

    /// <summary>بيمه کميته امداد</summary>
    ReliefCommittee = 6,

    /// <summary>بيمه بهزيستي</summary>
    WelfareInsurance = 7,

    /// <summary>خويش فرما</summary>
    SelfEmployed = 8,

    /// <summary>نال</summary>
    Null = 9,

    /// <summary>بيمه خدمات درماني</summary>
    HealthServices = 10,

    /// <summary>بيمه روستايي</summary>
    RuralInsurance = 11,

    /// <summary>هيچکدام</summary>
    None = 12
}
