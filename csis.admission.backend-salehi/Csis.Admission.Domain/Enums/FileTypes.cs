namespace Csis.Admission.Domain.Enums;

/// <summary>
/// انواع فایل‌های مورد استفاده در سامانه
/// </summary>
public enum FileTypes : int
{
    /// <summary>مدرک شناسایی</summary>
    IdentityDocument = 1,
    /// <summary>
    /// مدرک عدم مالکیت خانه
    /// </summary>
    ProofOfNonOwnership = 2,

    /// <summary>
    /// مدرک اجاره  نامه صفحه اول
    /// </summary>
    LeaseCertificateFirstPage = 3,

    /// <summary>
    /// مدرک اجاره  نامه صفحه دوم
    /// </summary>
    LeaseCertificateTwoPage = 4,

    /// <summary>
    ///مستندات اول مربوط به اعتراضات
    /// </summary>

    FirstProtestDocument = 5,

    /// <summary>
    /// مستندات دوم مربوط به اعتراضات
    /// </summary>
    SecondProtestDocument = 6,

    /// <summary>
    /// مدرک مربوط به بانک برای غیر ایرانی ها
    /// </summary>
    BankDocumentForNonIranian = 7

}
