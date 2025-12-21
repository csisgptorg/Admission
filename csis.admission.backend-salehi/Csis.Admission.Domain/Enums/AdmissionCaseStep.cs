namespace Csis.Admission.Domain.Enums;

/// <summary>
/// مراحل تشکیل پرونده
/// </summary>
public enum AdmissionCaseStep : short
{
    /// <summary> تأیید شماره موبایل </summary>
    MobileVerified = 1,

    /// <summary> تایید وضعیت طلبه برای ثبت نام </summary>
    StudentStatusForRegistrationVerified = 2,

    /// <summary> تأیید اطلاعات هویتی </summary>
    IdentityVerified = 3,

    /// <summary> تأیید آدرس </summary>
    AddressVerified = 4,

    /// <summary> بارگذاری عکس پرسنلی </summary>
    PictureUploaded = 5,

    /// <summary> تأیید اطلاعات بانکی </summary>
    BankAccountVerified = 6,

    /// <summary> تأیید اطلاعات شغلی </summary>
    EmploymentVerified = 7,

    /// <summary> ایجاد کد مرکز </summary>
    CodmCreated = 8,

    /// <summary> تنظیم رمز عبور </summary>
    PasswordSet = 9,

    /// <summary> ارسال کد مرکز از طریق پیامک </summary>
    CodmSendBySms = 10,

    // <summary> تکمیل ثبت نام </summary>
    RegistrationCompleted = 11
}
