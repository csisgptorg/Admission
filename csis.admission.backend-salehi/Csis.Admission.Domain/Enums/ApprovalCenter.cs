namespace Csis.Admission.Domain.Enums;

/// <summary>
/// مرجع تایید کنننده حوزوی
/// </summary>
public enum ApprovalCenter : short
{
    /// <summary>فاقد مرجع</summary>
    None = 0,

    /// <summary>
    /// مرکز مدیریت حوزه های علمیه برادران سراسر کشور
    /// </summary>
    CenterForManagementOfIslamicSeminariesOfBrothersNationwide = 1,

    /// <summary>
    /// مرکز مدیریت حوزه های علمیه خواهران سراسر کشور
    /// </summary>
    CenterForManagementOfIslamicSeminariesOfSistersNationwide = 2,

    /// <summary>
    /// مرکز مدیریت حوزه های علمیه خراسان
    /// </summary>
    ManagementCenterOfIslamicSeminariesOfKhorasan = 3,

    /// <summary>
    /// مرکز مدیریت حوزه های علمیه اصفهان
    /// </summary>
    ManagementCenterOfIslamicSeminariesOfIsfahan = 4,

    /// <summary>
    /// جامعه الزهرا س
    /// </summary>
    JameatAlZahra = 5,

    /// <summary>
    /// جامعه المصطفی العالمیه 
    /// </summary>
    AlMustafaInternationalUniversity = 6,

    /// <summary>
    /// دبیرخانه شورای برنامه ریزی اهل سنت
    /// </summary>
    SecretariatOfSunniPlanningCouncil = 7,

    /// <summary>
    /// کمیسیون
    /// </summary>
    Commission = 8
}
