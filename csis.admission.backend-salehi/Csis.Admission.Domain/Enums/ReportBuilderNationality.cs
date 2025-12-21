#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Domain.Enums;

/// <summary>
/// ملیت - گزارش ساز
/// </summary>
public enum ReportBuilderNationality : short
{
    [Display(Name = "ایران")]
    Iran = -1,

    [Display(Name = "آرژانتین")]
    Argentina = 0,

    [Display(Name = "آفریقای جنوبی")]
    SouthAfrica = 1,

    [Display(Name = "آلمان")]
    Germany = 3,

    [Display(Name = "آمریکا")]
    Usa = 4,

    [Display(Name = "اتریش")]
    Austria = 5,

    [Display(Name = "اتیوپی")]
    Ethiopia = 6,

    [Display(Name = "ازبکستان")]
    Uzbekistan = 7,

    [Display(Name = "اسپانیا")]
    Spain = 8,

    [Display(Name = "استرالیا")]
    Australia = 9,

    [Display(Name = "افغانستان")]
    Afghanistan = 10,

    [Display(Name = "الجزایر")]
    Algeria = 11,

    [Display(Name = "امارات")]
    Uae = 12,

    [Display(Name = "اندونزی")]
    Indonesia = 13,

    [Display(Name = "انگلستان")]
    England = 14,

    [Display(Name = "اوکراین")]
    Ukraine = 15,

    [Display(Name = "اوگاندا")]
    Uganda = 16,

    [Display(Name = "ایتالیا")]
    Italy = 17,

    [Display(Name = "ایرلند")]
    Ireland = 18,

    [Display(Name = "بحرین")]
    Bahrain = 19,

    [Display(Name = "برزیل")]
    Brazil = 20,

    [Display(Name = "برمه")]
    Burma = 21,

    [Display(Name = "بلژیک")]
    Belgium = 22,

    [Display(Name = "بلغارستان")]
    Bulgaria = 23,

    [Display(Name = "بنگلادش")]
    Bangladesh = 24,

    [Display(Name = "بنین")]
    Benin = 25,

    [Display(Name = "بورکینافاسو")]
    BurkinaFaso = 26,

    [Display(Name = "بوسنی")]
    Bosnia = 27,

    [Display(Name = "پاکستان")]
    Pakistan = 28,

    [Display(Name = "پرو")]
    Peru = 29,

    [Display(Name = "تاجیکستان")]
    Tajikistan = 30,

    [Display(Name = "تانزانیا")]
    Tanzania = 31,

    [Display(Name = "تایلند")]
    Thailand = 32,

    [Display(Name = "ترکمنستان")]
    Turkmenistan = 33,

    [Display(Name = "ترکیه")]
    Turkey = 34,

    [Display(Name = "ترینیداد و توباگو")]
    TrinidadAndTobago = 35,

    [Display(Name = "توگو")]
    Togo = 36,

    [Display(Name = "تونس")]
    Tunisia = 37,

    [Display(Name = "آذربایجان")]
    Azerbaijan = 38,

    [Display(Name = "چاد")]
    Chad = 39,

    [Display(Name = "چین")]
    China = 40,

    [Display(Name = "دانمارک")]
    Denmark = 41,

    [Display(Name = "رواندا")]
    Rwanda = 42,

    [Display(Name = "روسیه")]
    Russia = 43,

    [Display(Name = "زامبیا")]
    Zambia = 44,

    [Display(Name = "زئیر")]
    Zaire = 45,

    [Display(Name = "ژاپن")]
    Japan = 46,

    [Display(Name = "ساحل عاج")]
    IvoryCoast = 47,

    [Display(Name = "سریلانکا")]
    SriLanka = 48,

    [Display(Name = "سنگاپور")]
    Singapore = 49,

    [Display(Name = "سنگال")]
    Senegal = 50,

    [Display(Name = "سوئد")]
    Sweden = 51,

    [Display(Name = "سودان")]
    Sudan = 52,

    [Display(Name = "سوریه")]
    Syria = 53,

    [Display(Name = "سومالی")]
    Somalia = 54,

    [Display(Name = "سیرالئون")]
    SierraLeone = 55,

    [Display(Name = "عراق")]
    Iraq = 56,

    [Display(Name = "عربستان")]
    SaudiArabia = 57,

    [Display(Name = "عمان")]
    Oman = 58,

    [Display(Name = "غنا")]
    Ghana = 59,

    [Display(Name = "فرانسه")]
    France = 60,

    [Display(Name = "فلسطین")]
    Palestine = 61,

    [Display(Name = "فنلاند")]
    Finland = 62,

    [Display(Name = "فیلیپین")]
    Philippines = 63,

    [Display(Name = "قرقیزستان")]
    Kyrgyzstan = 64,

    [Display(Name = "قزاقستان")]
    Kazakhstan = 65,

    [Display(Name = "قطر")]
    Qatar = 66,

    [Display(Name = "کامبوج")]
    Cambodia = 67,

    [Display(Name = "کامرون")]
    Cameroon = 68,

    [Display(Name = "کانادا")]
    Canada = 69,

    [Display(Name = "کلمبیا")]
    Colombia = 70,

    [Display(Name = "کنگو")]
    Congo = 71,

    [Display(Name = "کنیا")]
    Kenya = 72,

    [Display(Name = "کومور")]
    Comoros = 73,

    [Display(Name = "کویت")]
    Kuwait = 74,

    [Display(Name = "گامبیا")]
    Gambia = 75,

    [Display(Name = "گرجستان")]
    Georgia = 76,

    [Display(Name = "گویان")]
    Guyana = 77,

    [Display(Name = "گینه")]
    Guinea = 78,

    [Display(Name = "لبنان")]
    Lebanon = 79,

    [Display(Name = "لیبی")]
    Libya = 80,

    [Display(Name = "ماداگاسکار")]
    Madagascar = 81,

    [Display(Name = "مالاوی")]
    Malawi = 82,

    [Display(Name = "مالزی")]
    Malaysia = 83,

    [Display(Name = "مالی")]
    Mali = 84,

    [Display(Name = "مراکش")]
    Morocco = 85,

    [Display(Name = "مصر")]
    Egypt = 86,

    [Display(Name = "مغرب")]
    Maghreb = 87,

    [Display(Name = "مغولستان")]
    Mongolia = 88,

    [Display(Name = "مقدونیه")]
    Macedonia = 89,

    [Display(Name = "موزامبیک")]
    Mozambique = 90,

    [Display(Name = "میانمار")]
    Myanmar = 91,

    [Display(Name = "نروژ")]
    Norway = 92,

    [Display(Name = "نیجر")]
    Niger = 93,

    [Display(Name = "نیجریه")]
    Nigeria = 94,

    [Display(Name = "ونزوئلا")]
    Venezuela = 95,

    [Display(Name = "هلند")]
    Netherlands = 96,

    [Display(Name = "هند")]
    India = 97,

    [Display(Name = "یمن")]
    Yemen = 98,

    [Display(Name = "یوگسلاوی")]
    Yugoslavia = 99,

    [Display(Name = "مولداوی")]
    Moldova = 100,

    [Display(Name = "شیلی")]
    Chile = 101,

    [Display(Name = "بروندی")]
    Burundi = 102,

    [Display(Name = "نامیبیا")]
    Namibia = 103,

    [Display(Name = "نیوزلند")]
    NewZealand = 104,

    [Display(Name = "آلبانی")]
    Albania = 105,

    [Display(Name = "زیمبابوه")]
    Zimbabwe = 106,

    [Display(Name = "اردن")]
    Jordan = 108,

    [Display(Name = "بولیوی")]
    Bolivia = 109,

    [Display(Name = "موریتانی")]
    Mauritania = 110,

    [Display(Name = "مکزیک")]
    Mexico = 111,

    [Display(Name = "نپال")]
    Nepal = 112,

    [Display(Name = "کاستاریکا")]
    CostaRica = 113,

    [Display(Name = "کره جنوبی")]
    SouthKorea = 114,

    [Display(Name = "کشمیر")]
    Kashmir = 115,

    [Display(Name = "کرواسی")]
    Croatia = 116,

    [Display(Name = "کوبا")]
    Cuba = 117,

    [Display(Name = "کوزوو")]
    Kosovo = 118,

    [Display(Name = "بلاروس")]
    Belarus = 119,

    [Display(Name = "لیبریا")]
    Liberia = 120,

    [Display(Name = "یونان")]
    Greece = 121,

    [Display(Name = "جیبوتی")]
    Djibouti = 122,

    [Display(Name = "صربستان")]
    Serbia = 123,

    [Display(Name = "السالوادور")]
    ElSalvador = 124,

    [Display(Name = "غنا")]
    GhanaAlt = 125,

    [Display(Name = "اوگاندا")]
    UgandaAlt = 126,

    [Display(Name = "موریس")]
    Mauritius = 127,

    [Display(Name = "لهستان")]
    Poland = 128,

    [Display(Name = "آفریقای مرکزی")]
    CentralAfrica = 129,

    [Display(Name = "اکوادور")]
    Ecuador = 130,

    [Display(Name = "زیمبابوه")]
    ZimbabweAlt = 131,

    [Display(Name = "ویتنام")]
    Vietnam = 133,

    [Display(Name = "سوئیس")]
    Switzerland = 134,

    [Display(Name = "اسکاتلند")]
    Scotland = 135,

    [Display(Name = "پرتغال")]
    Portugal = 136,

    [Display(Name = "ارمنستان")]
    Armenia = 137,

    [Display(Name = "دومنیکن")]
    Dominican = 138
}
