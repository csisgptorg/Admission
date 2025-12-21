using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Domain.Enums;

/// <summary>مدرک تحصیلی</summary>
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public enum StudyLevel : int {
    [Display(Name = "فوق دیپلم")]
    GraduateDiploma = 5,

    [Display(Name = "کارشناسی")]
    BachelorDegree = 6,

    [Display(Name = "کارشناسی ارشد")]
    MasterDegree = 7,

    [Display(Name = "دکتری")]
    DoctoralDegree = 8,
}
