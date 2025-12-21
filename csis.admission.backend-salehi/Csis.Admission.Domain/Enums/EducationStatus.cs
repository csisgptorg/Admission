namespace Csis.Admission.Domain.Enums;

/// <summary>وضعیت اشتغال به تحصیل</summary>
public enum EducationStatus : short
{
    /// <summary>محصل</summary>
    Student = 1,

    /// <summary>فارغ التحصیل</summary>
    Graduate = 2,

    /// <summary>انصراف</summary>
    Withdrawal = 3,

    /// <summary>اخراج</summary>
    Expelled = 4,

    /// <summary>عدم اشتغال به تحصیل</summary>
    NotEnrolled = 9
}
