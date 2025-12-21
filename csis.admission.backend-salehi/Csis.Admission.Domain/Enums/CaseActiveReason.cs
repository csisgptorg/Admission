namespace Csis.Admission.Domain.Enums;

/// <summary>علت فعال شدن پرونده</summary>
public enum CaseActiveReason
{
    /// <summary>محصل</summary>
    Student = 12,

    /// <summary>ازکارافتاده</summary>
    Disease = 13,

    /// <summary>فرزند خوانده</summary>
    AdoptedChild = 14,

    /// <summary>طلاق</summary>
    Divorced = 15,

    /// <summary>بیمه جای دیگر</summary>
    InsuredElsewhere = 17,

    /// <summary>دانشجو</summary>
    UniversityStudent = 18,

    /// <summary>بدسرپرست</summary>
    PoorGuardian = 19,

    /// <summary>بدون سرپرست</summary>
    Orphan = 20,

    /// <summary>سرباز</summary>
    Soldier = 24,

    /// <summary>کد مستقل</summary>
    IndependentCode = 25,

    /// <summary>فوت همسر و انتقال</summary>
    SpouseDeathAndTransfer = 29,

    /// <summary>فوت همسر</summary>
    SpouseDeath = 32,

    /// <summary>عدم ازدواج</summary>
    NeverMarried = 33,

    /// <summary>کمیسیون</summary>
    Commission = 36,

    /// <summary>عقد بسته</summary>
    MarriageContract = 38,

    /// <summary>تأمین اجتماعی از طریق طلبه</summary>
    SocialSecurityByClergy = 41,

    /// <summary>تأییدیه المصطفی</summary>
    AlMostafaConfirmation = 44
}

