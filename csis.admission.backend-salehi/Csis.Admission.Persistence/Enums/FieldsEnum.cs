using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Persistence.Enums;

/// <summary>
/// فیلدهای موجودیت ها
/// </summary>
internal enum FieldsEnum : byte
{
    [Display(Name = " کد مرکز")]
    Codm = 1,

    [Display(Name = "شناسه استان")]
    ProvinceId = 2,

    [Display(Name = "شهرستان")]
    CityId = 3,

    [Display(Name = "بخش")]
    PortionId = 4,

    [Display(Name = "شهر")]
    TownId = 5,

    [Display(Name = "دهستان")]
    RuralId = 6,

    [Display(Name = "شهرک")]
    Township = 7,

    [Display(Name = "روستا")]
    Village = 8,

    [Display(Name = "محله")]
    District = 9,

    [Display(Name = "خیابان اصلی")]
    Avenue = 10,

    [Display(Name = "خیابان فرعی")]
    Street = 11,

    [Display(Name = "کوچه اصلی")]
    Alley = 12,

    [Display(Name = "کوچه فرعی")]
    Lane = 13,

    [Display(Name = "پلاک")]
    Number = 14,

    [Display(Name = "مجتمع")]
    Complex = 15,

    [Display(Name = "بلوک")]
    Block = 16,

    [Display(Name = "واحد")]
    Unit = 17,

    [Display(Name = "طبقه")]
    Floor = 18,

    [Display(Name = "کد پستی")]
    ZipCode = 19,

    [Display(Name = "تاریخ تایید")]
    ConfirmDate = 20,

    [Display(Name = "کد پروژه")]
    ProjectCode = 21,

    [Display(Name = "علامت")]
    Flag = 22,

    [Display(Name = "تعداد جزء حفظ شده")]
    JozCount = 23,

    [Display(Name = "مرجع تایید کنننده حوزوی")]
    ApprovalCenter = 24,

    [Display(Name = "تاریخ انقضاء")]
    ExpireDate = 25,

    [Display(Name = "روزهای آزادگی")]
    CaptivityDays = 26,

    [Display(Name = "درصد جانبازی")]
    VeteranPercent = 27,

    [Display(Name = "نسبت با شهید")]
    RelationWithMartyr = 28,
    
}

