namespace Csis.Admission.Domain.Enums;

/// <summary>نام جداول</summary>
public enum TableName : short
{
    /// <summary>درخواست</summary>
    Request = 1,

    /// <summary>اشتغال طلبه</summary>
    StudentEmployment = 2,

    /// <summary>اشتغال تکفل</summary>
    DependentEmployment = 3,

    /// <summary>آدرس طلبه</summary>
    StudentAddress = 4,

    /// <summary>مسکن طلبه</summary>
    StudentHouse = 5,

    /// <summary>بارداری طلبه</summary>
    StudentPregnancy = 6,
    /// <inheritdoc/>
    StudentMarriage = 7,
    /// <inheritdoc/>
     StudentDivorce = 8,
    /// <inheritdoc/>
    StudentDependentMarriage = 9,
    /// <inheritdoc/>
     StudentDependentDivorce = 10,
}
