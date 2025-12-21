using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Domain.Enums;

/// <summary>نسبت تکفل</summary>
public enum DependentRelation : short
{
    /// <summary>همسر</summary>
    [Display(Name = "همسر")]
    Spouse = 1,

    /// <summary>فرزند</summary>
    [Display(Name = "فرزند")]
    Child = 2,

    /// <summary> پدر و مادر</summary>
    [Display(Name = "پدر و مادر")]
    Parent = 3,

    /// <summary>نوه</summary>
    [Display(Name = "نوه")]
    Grandchild = 4,

    /// <summary>فرزند خوانده</summary>
    [Display(Name = "فرزند خوانده")]
    AdoptedChild = 5,

    /// <summary>
    /// خواهر و برادر
    /// </summary>
    [Display(Name = "خواهر برادر")]
    Siblings = 6,

    /// <summary>
    /// عروس
    /// </summary>
    [Display(Name = "عروس")]
    DaughterInLaw = 7,

    /// <summary>
    /// برادرزاده
    /// </summary>
    [Display(Name = "برادر زاده")]
    Nephew = 8
}

/// <inheritdoc/>
public enum DependentChildRelation : short
{
    /// <summary>فرزند</summary>
    [Display(Name = "فرزند")]
    Child = 2,

    /// <summary>نوه</summary>
    [Display(Name = "نوه")]
    Grandchild = 4,

    /// <summary>فرزند خوانده</summary>
    [Display(Name = "فرزند خوانده")]
    AdoptedChild = 5
}

