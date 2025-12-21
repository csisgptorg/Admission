using System.Text.Json;
using System.ComponentModel;

namespace Csis.Admission.Domain.Entities;

/// <summary>تاریخچه امتیاز هدفمندی</summary> 
public class TargetedScoreHistory: BaseEntity
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>دیتای هدفمندی به صورت جی سون</summary>
    public string TargetedScoreJson { get; set; }

    /// <summary>امتیاز هدفمندی</summary>
    public TargetedScoreModel TargetedScore=>
        !string.IsNullOrWhiteSpace(TargetedScoreJson)?JsonSerializer.Deserialize<TargetedScoreModel>(TargetedScoreJson):null;

    /// <summary>تاریخ</summary>
    public int Date { get; set; }

    /// <summary>زمان</summary>
    public TimeSpan? Time { get; set; }

    /// <summary>ورژن</summary>
    public byte? Version { get; set; }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable VSSpell001 // Spell Check
#pragma warning disable IDE0049 // Simplify Names
    /// <summary>امتیاز هدفمندی</summary>
    public class TargetedScoreModel
    {
        [DisplayName("کد مرکز")]
        public int? Codm { get; set; }

        //[Browsable(false)]
        //public bool? Masdod { get; set; }

        [DisplayName("مسدود")]
        public string MasdodStr { get; set; }

        //[Browsable(false)]
        //public string VazP { get; set; }

        [DisplayName("وضعیت پرونده")]
        public string VazPStr { get; set; }

        //[Browsable(false)]
        //public bool? Fot { get; set; }

        [DisplayName("مرحوم")]
        public string FotStr { get; set; }

        //[Browsable(false)]
        //public short? Sex { get; set; }

        [DisplayName("جنسیت")]
        public string SexStr { get; set; }

        //[Browsable(false)]
        //public short? Mazhab { get; set; }

        [DisplayName("مذهب")]
        public string MazhabStr { get; set; }

        //[Browsable(false)]
        //public short? Tabee { get; set; }

        [DisplayName("تابعیت")]
        public string TabeeStr { get; set; }

        //[Browsable(false)]
        //public int? CodSh { get; set; }

        [DisplayName("شعبه")]
        public string CodShStr { get; set; }

        //[Browsable(false)]
        //public int? MarajeT { get; set; }

        [DisplayName("مرجع تایید کننده حوزوی")]
        public string MarajeTStr { get; set; }

        [DisplayName("شماره پرونده")]
        public long? No_Parvande { get; set; }

        [DisplayName("کد ملی")]
        public long? Codmel { get; set; }

        [DisplayName("تراز")]
        public int? Taraz { get; set; }

        //[Browsable(false)]
        //public bool? Shaghel { get; set; }

        [DisplayName("شاغل")]
        public string ShaghelStr { get; set; }

        [DisplayName("دهک")]
        public short? Dahak { get; set; }

        [DisplayName("صادر")]
        public string Sader { get; set; }

        [DisplayName("نسبت با شهید")]
        public short? NesbatBaShahid { get; set; }

        [DisplayName("درصد جانبازی")]
        public short? JanbaziDarsad { get; set; }

        [DisplayName("تعداد روز آزادگی")]
        public int? AzadegiTotalDay { get; set; }

        [DisplayName("تعداد روز دفاع مقدس")]
        public int? DefaMoqadasTotalDay { get; set; }

        [DisplayName("تعداد روز دفاع از حرم")]
        public int? ModafeHaramTotalDay { get; set; }

        //[Browsable(false)]
        //public int? VPoor { get; set; }

        [DisplayName("مناطق محروم")]
        public string VPoorStr { get; set; }

        //[Browsable(false)]
        //public int? VAghaleyat { get; set; }

        [DisplayName("مناطق محروم اقلیت شیعه")]
        public string VAghaleyatStr { get; set; }

        //[Browsable(false)]
        //public int? VazT { get; set; }

        [DisplayName("وضعیت تاهل")]
        public string VazTStr { get; set; }

        [DisplayName("تعداد فرزند نوه فرزندخوانده")]
        public int? FarzandNaveFarzandKhandeCount { get; set; }

        [DisplayName("تعداد همسر")]
        public int? HamsarCount { get; set; }

        //[Browsable(false)]
        //public int? Town { get; set; }

        [DisplayName("شهر")]
        public string TownStr { get; set; }

        [DisplayName("بالاترین سطح نخبگی")]
        public short? NokhbeMaxLevel { get; set; }

        [DisplayName("سطوح نخبگی")]
        public string NokhbeLevels { get; set; }

        [DisplayName("وضعیت فعال بودن در مرکز حوزوی")]
        public bool? MarakezIsActive { get; set; }

        [DisplayName("وضعیت تحصیل در مرکز حوزوی (قدیم)")]
        public bool? MarakezTahsil { get; set; }

        [DisplayName("وضعیت تحصیل در مرکز حوزوی (جدید)")]
        public string MarakezTahsilStatusStr { get; set; }

        [DisplayName("وضعیت پرونده در مرکز حوزوی")]
        public string MarakezStatus { get; set; }

        [DisplayName("سال ورود به حوزه")]
        public short? MarakezMinSaleVorood { get; set; }

        [DisplayName("رشد تحصیلی در سه سال")]
        public Single RoshdTahsiliDar3Sal { get; set; }

        //[Browsable(false)]
        //public short? TadrisCurrnetYearMaxMaghta { get; set; }

        [DisplayName("بالاترین سطح تدریس در سال جاری")]
        public string TadrisCurrnetYearMaxMaghtaStr { get; set; }

        [DisplayName("تعداد سال های سابقه تدریس")]
        public short? TadrisHistoryYearCount { get; set; }

        [DisplayName("بالاترین رتبه تدریس در سال جاری")]
        public short? MaxTadrisGrade { get; set; }

        [DisplayName("تعداد روز تبلیغ در سال جاری")]
        public short? TablighCurrnetYearMobaleghDays { get; set; }

        [DisplayName("امام جمعه در سال جاری")]
        public bool? TablighCurrentYearEmamJomeeNormal { get; set; }

        [DisplayName("امام جماعت در سال جاری")]
        public bool? TablighCurrentYearEmamJamaat { get; set; }

        [DisplayName("تبلیغ روحانی مستقر در سال جاری")]
        public bool TablighCurrnetYearRohaniMostaghar { get; set; }

        [DisplayName("تبلیغ طرح هجرت در سال جاری")]
        public bool? TablighCurrnetYearTarhHejrat { get; set; }

        [DisplayName("تبلیغ مجازی(دفتر تبلیغات) در سال جاری")]
        public bool? TablighCurrnetYearMajaziDaftar { get; set; }

        [DisplayName("تبلیغ مجازی(سایر) در سال جاری")]
        public bool? TablighCurrnetYearMajaziOther { get; set; }

        [DisplayName("تبلیغ سفیران هدایت در سال جاری")]
        public bool? TablighCurrnetYearSafiran { get; set; }

        [DisplayName("تبلیغ مستمر ساعتی در سال جاری")]
        public bool? TablighCurrnetYearMostamerSaati { get; set; }

        [DisplayName("تبلیغ مدارس امین در سال جاری")]
        public bool? TablighCurrnetYearMadaresAmin { get; set; }

        [DisplayName("تعداد سال سابقه تبلیغ")]
        public short? TablighHistoryYearCount { get; set; }

        [DisplayName("بالاترین ربتبه تبلیغ")]
        public short? MaxTablighGrade { get; set; }

        [DisplayName("پژوهش در سال جاری")]
        public short? ResearchCurrentYear { get; set; }

        [DisplayName("تعداد سابقه پژوهش")]
        public short? ResearchHistoryCount { get; set; }

        [DisplayName("بالاترین رتبه پژوهش")]
        public short? MaxResearchGrade { get; set; }

        [DisplayName("فعالیت فرهنگی در سال جاری")]
        public bool? FarhangiCurrentYear { get; set; }

        [DisplayName("بالاترین رتبه فرهنگی")]
        public short? MaxFarhangiGrade { get; set; }

        //[Browsable(false)]
        //public short? BeforHozeClassicMaxDegree { get; set; }

        [DisplayName("بالاترین مقطع دانشگاهی قبل از ورود به حوزه")]
        public string BeforHozeClassicMaxDegreeStr { get; set; }

        [DisplayName("مشهور")]
        public short? IsMashHoor { get; set; }

        [DisplayName("حفظ جز قرآن")]
        public short? QuranJoz { get; set; }

        [DisplayName("بیماری خاص فرد اصلی")]
        public bool? BimariKhasAsli { get; set; }

        [DisplayName("تعداد بیماری خاص تکفل")]
        public short? BimariKhasTakaffolCount { get; set; }

        [DisplayName("امتیاز تحصیل")]
        public Single EmtiazTahsil { get; set; }

        [DisplayName("امتیاز تدریس جاری")]
        public Single EmtiazTadrisCurrent { get; set; }

        [DisplayName("امتیاز سابقه تدریس")]
        public Single EmtiazTadrisHistory { get; set; }

        [DisplayName("امتیاز پژوهش جاری")]
        public Single EmtiazResearchCurrent { get; set; }

        [DisplayName("امتیاز سابقه پژوهش")]
        public Single EmtiazResearchHistory { get; set; }

        [DisplayName("امتیاز تبلیغ جاری")]
        public Single EmtiazTablighCurrent { get; set; }

        [DisplayName("امتیاز سابقه تبلیغ")]
        public Single EmtiazTablighHistory { get; set; }

        [DisplayName("امتیاز حافظین")]
        public Single EmtiazHafezin { get; set; }

        [DisplayName("امتیاز ایثارگری")]
        public Single EmtiazEsargari { get; set; }

        [DisplayName("امتیاز تحصیلات دانشگاهی")]
        public Single EmtiazClassic { get; set; }

        [DisplayName("جمع امتیاز")]
        public Single EmtiazSum { get; set; }

        [DisplayName("ضریب تکفل")]
        public Single ZaribTakaffol { get; set; }

        [DisplayName("ضریب دهک")]
        public Single ZaribDahak { get; set; }

        [DisplayName("ضریب منطقه")]
        public Single ZaribMantaghe { get; set; }

        [DisplayName("ضریب آدرس")]
        public Single ZaribAddress { get; set; }

        [DisplayName("ضریب تحصیل")]
        public Single ZaribTahsil { get; set; }

        [DisplayName("ضریب نخبگی")]
        public Single ZaribNokhbe { get; set; }

        [DisplayName("ضریب بیماری خاص فرد اصلی")]
        public Single ZaribBimariKhasAsli { get; set; }

        [DisplayName("ضریب بیماری خاص تکفل")]
        public Single ZaribBimariKhasTakaffol { get; set; }

        [DisplayName("امتیاز کل")]
        public Single EmtiazKol { get; set; }
    }
}
