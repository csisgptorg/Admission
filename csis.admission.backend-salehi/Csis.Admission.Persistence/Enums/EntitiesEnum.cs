using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Persistence.Enums;

/// <summary>
/// موجودیت ها
/// </summary>
internal enum EntitiesEnum : byte
{
    [Display(Name = "آدرس")]
    Address = 3,

    [Display(Name = "شغل و درآمد")]
    StudentEmployment = 6,

    [Display(Name = "ممتازين")]
    Excellent = 7,

    [Display(Name = "دوستان")]
    StudentFriend = 8,

    [Display(Name = "ايثارگري")]
    Veteran = 10,

    [Display(Name = "تحصيلات دانشگاهی")]
    UniversityEducation = 13,

    [Display(Name = "تدريس")]
    Teach = 20,

    [Display(Name = "پژوهش")]
    Research = 21,

    [Display(Name = "حافظين")]
    Memorizer = 23,

    [Display(Name = "تبليغ")]
    Preach = 24,

    [Display(Name = "فعاليت هاي فرهنگي")]
    CulturalActivity = 30,

    [Display(Name = "مستاجري")]
    Tenant = 42,

    [Display(Name = "جامعه نخبگاني")]
    Elite = 43,

    [Display(Name = "مسکن")]
    House = 53,

    [Display(Name = "شغل و در آمد تکفل")]
    DependentEmployment = 54,

    [Display(Name = "رتبه پژوهشگر")]
    ResearchGrade = 57,

    [Display(Name = "رتبه تبليغ")]
    PreachGrade = 59,

    [Display(Name = "بارداری")]
    Pregnancy = 60,

    [Display(Name = "سرباز طلبه")]
    SoldierStudent = 61,

    [Display(Name = "اعتراضات")]
    Protest = 62,

    [Display(Name = "رتبه تدريس")]
    TeachGrade = 63,

    [Display(Name = "ثبت رتبه فرهنگي")]
    CulturalActivityGrade = 64,

    [Display(Name = "تحصيل")]
    Education = 69,
}
