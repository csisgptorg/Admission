#pragma warning disable CS1591
using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Domain.Entities;
/// <summary>نوع درخواست</summary>
public enum RequestType : short
{
    [Display(Name = "تشکیل پرونده")]
    CreateAdmissionCase = 1,

    [Display(Name = "تمدید پرونده")]
    StudentExtensionCase = 2,

    [Display(Name = "رفع انسداد")]
    Unblock = 3,

    [Display(Name = "ثبت تکفل")]
    RegisterSupport = 4,

    [Display(Name = "ثبت ازدواج")]
    StudentSpouseRegistry = 5,

    [Display(Name = "ثبت طلاق تکفل")]
    UpdateDependentDivorce = 6,

    [Display(Name = "بروزرسانی عکس")]
    UpdateStudentProfilePicture = 7,

    [Display(Name = "تغییر تابعیت")]
    ChangeNationality = 8,

    [Display(Name = "ثبت اشتغال و درآمد")]
    CreateOrUpdateStudentEmployment = 9,

    [Display(Name = "ثبت دوست")]
    RegisterFriend = 10,

    [Display(Name = "بروزرسانی آدرس")]
    CreateOrUpdateStudentAddress = 11,

    [Display(Name = "ثبت تحصیلات دانشگاهی طلبه غیر ایرانی")]
    CreateStudentUniversityEducationNonIranian = 12,

    [Display(Name = "ثبت تحصیلات دانشگاهی طلبه ایرانی")]
    CreateStudentUniversityEducationIranian = 13,


    [Display(Name = "بروزرسانی تلفن همراه")]
    UpdateStudentMobile = 14,

    [Display(Name = "ثبت اعتراض")]
    RegisterStudentBankAccount = 15,

    [Display(Name = "بروزرسانی تلفن همراه تکفل")]
    UpdateDependentMobile = 16,

    [Display(Name = "ثبت درخواست نقش آفرینی")]
    CreateRequestReligiousRoleQuestion = 17,

    [Display(Name = "بروزرسانی شماره حساب")]
    UpdateStudentBankAccount = 18,

    [Display(Name = "تحصیلات")]
    Education = 19,

    [Display(Name = "ثبت درخواست مسکن")]
    CreateOrUpdateHouse = 20,

    [Display(Name = "ثبت درخواست بارداری")]
    CreatePregnancy = 21,

    [Display(Name = "بروزرسانی شغل و درآمد تکفل")]
    CreateOrUpdateDependentEmployment = 22,

    [Display(Name = "بروزرسانی شماره حساب تکفل")]
    UpdateDependentBankAccount = 23,

    [Display(Name = "بروزرسانی وضعیت تاهل طلبه خواهر  ")]
    UpdateStudentSisterMarriage = 24,

    [Display(Name = "بروزرسانی وضعیت تاهل تکفل")]
    UpdateChildMarriage = 25,

    [Display(Name = "بروزرسانی وضعیت طلاق طلبه خواهر  ")]
    UpdateStudentSisterDivorce = 26,

    [Display(Name = "بروزرسانی وضعیت طلاق همسر")]
    UpdateWifeDivorce = 27,

    [Display(Name = "ثبت اعتراض با کد مرکز")]
    CreateProtestByCodm = 28,

    [Display(Name = "بروزرسانی وضعیت طلاق تکفل غیر ایرانی")]
    UpdateNonIranianDependentDivorce = 29,

    [Display(Name = "بروزرسانی وضعیت طلاق همسر غیر ایرانی")]
    UpdateNonIranianWifeDivorce = 30,


    [Display(Name = "فرم جیم")]
    ProtestFormJim = 31,

    [Display(Name = "سابقه مالکیت در سازمان ثبت اسناد")]
    ProtestOwnershipHistory = 32,

    [Display(Name = "سابقه دریافت تسهیلات از بانک مسکن")]
    ProtestHousingLoanHistory = 33,

    [Display(Name = "سابقه موجر بودن")]
    ProtestBeingLandlord = 34,

    [Display(Name = "سابقه خرید و فروش مسکن")]
    ProtestHousingBuySellHistory = 35,

    [Display(Name = "سابقه مسکن شخصی در پذیرش")]
    ProtestPersonalHousingHistory = 36,

    [Display(Name = "شناسایی اشتغال از طریق سامانه‌ها")]
    ProtestEmploymentIdentificationSystems = 37,

    [Display(Name = "دهک معیشتی")]
    ProtestLivelihoodDecile = 38,

    [Display(Name = "اشتغال تکفل")]
    ProtestEmploymentSupport = 39,

    [Display(Name = "بروزرسانی وضعیت ازدواج تکفل غیر ایرانی")]
    UpdateNonIranianDependentMarriage = 40,

    [Display(Name = "بروزرسانی وضعیت ازدواج همسر غیر ایرانی")]
    UpdateNonIranianWifeMarriage = 41,

    [Display(Name = "تکمیل اطلاعات تشکیل پرونده")]
    CompleteInformationCaseFiling = 42,

    [Display(Name = " بروزرسانی تلفن همراه طلبه ایرانی در تشکیل پرونده")]
    UpdateStudentMobileRepo = 43,



    [Display(Name = "ایجاد سابقه تبلیغ برای طلبه")]
    CreatePreach = 44,
    [Display(Name = "حذف سابقه تبلیغ برای طلبه")]
    DeletePreach = 45,
    [Display(Name = "بروزرسانی سابقه تبلیغ برای طلبه")]
    UpdatePreach = 46,

    [Display(Name = "ایجاد سابقه تدریس برای طلبه")]
    CreateTeach = 47,
    [Display(Name = "حذف سابقه تدریس برای طلبه")]
    DeleteTeach = 48,
    [Display(Name = "بروزرسانی سابقه تدریس برای طلبه")]
    UpdateTeach = 49,

    [Display(Name = "ایجاد سابقه پژوهش برای طلبه")]
    CreateResearch = 50,
    [Display(Name = "حذف سابقه پژوهش برای طلبه")]
    DeleteResearch = 51,
    [Display(Name = "بروزرسانی سابقه پژوهش برای طلبه")]
    UpdateResearch = 52,

    [Display(Name = "ایجاد سابقه اشتهار برای طلبه")]
    CreateFamous = 53,
    [Display(Name = "حذف سابقه اشتهار برای طلبه")]
    DeleteFamous = 54,
    [Display(Name = "بروزرسانی سابقه اشتهار برای طلبه")]
    UpdateFamous = 55,
    [Display(Name = "حذف فعالیت فرهنگی طلبه")]
    DeleteCulturalActivity = 56,
    [Display(Name = "حذف رتبه فعالیت فرهنگی")]
    DeleteCulturalActivityGrade = 57,
    [Display(Name = "حذف رتبه تبلیغی")]
    DeletePreachGrade = 58,
    [Display(Name = "حذف رتبه پژوهشی")]
    DeleteResearchGrade = 59,
    [Display(Name = "حذف رتبه تدریسی")]
    DeleteTeachGrade = 60,
    [Display(Name = "ایجاد دوست طلبه")]
    CreateStudentFriend = 61,
    [Display(Name = "حذف دوست طلبه")]
    DeleteStudentFriend = 62,
    [Display(Name = "حذف اشتغال و درآمد طلبه")]
    DeleteStudentEmployment = 63,
    [Display(Name = "حذف اشتغال و درآمد تکفل")]
    DeleteDependentEmployment = 64,
    [Display(Name = "ثبت یا بروزرسانی ایثارگری")]
    CreateOrUpdateVeteran = 65,
    [Display(Name = "حذف ایثارگری")]
    DeleteVeteran = 66,
    [Display(Name = "حذف مسکن")]
    DeleteHouse = 67,
    [Display(Name = "حذف ممتازی")]
    DeleteExcellent = 68,
    [Display(Name = "حذف حافظ")]
    DeleteMemorizer = 69,
    [Display(Name = "ثبت یا بروزرسانی نخبگان")]
    CreateOrUpdateElite = 70,
    [Display(Name = "حذف نخبگان")]
    DeleteElite = 71,
    [Display(Name = "انسداد پرونده طلبه")]
    CreateStudentCaseBlock = 72,
    [Display(Name = "رفع انسداد پرونده طلبه")]
    CreateStudentCaseUnblock = 73,
    [Display(Name = "شناسایی موردی اشتغال")]
    IdentifyStudentEmployment = 74,
    [Display(Name = "بروزرسانی تلفن طلبه")]
    UpdateStudentPhone = 75,
    [Display(Name = "ثبت فوت طلبه غیر ایرانی")]
    CreateStudentDeath = 76,
    [Display(Name = "ایجاد توضیحات پرونده تکفل")]
    CreateStudentDependentCaseDescription = 77,
    [Display(Name = "بروزرسانی وضعیت فعال بودن تکفل در پرونده پذیرش")]
    UpdateDependentCaseActiveSenior = 78,
    [Display(Name = "بروزرسانی وضعیت غیرفعال بودن تکفل در پرونده پذیرش")]
    UpdateDependentCaseDeActiveSenior = 79,
    [Display(Name = "بروزرسانی وضعیت فعال بودن تکفل در پرونده پذیرش توسط کارمند")]
    UpdateDependentCaseActiveEmployee = 80,

    [Display(Name = "ثبت تحصیلات دانشگاهی تکفل غیر ایرانی")]
    CreateDependentUniversityEducationNonIranian = 81,

    [Display(Name = "ثبت تحصیلات دانشگاهی تکفل ایرانی")]
    CreateDependentUniversityEducationIranian = 82,
    [Display(Name = "حذف تحصیلات دانشگاهی طلبه")]
    DeleteStudentUniversityEducation = 83,
    [Display(Name = "حذف تحصیلات دانشگاهی تکفل")]
    DeleteDependentUniversityEducation = 84,
    [Display(Name = "تمدید پرونده دستی")]
    ManualStudentExtensionCase = 85,
    [Display(Name = "ویرایش عادی پرونده")]
    StudentNormalEditCase = 86,
    [Display(Name = "ثبت/ویرایش آدرس توسط کارمند")]
    CreateOrUpdateStudentAddressEmployee = 87,
    [Display(Name = "درخواست باز شدن پرونده تکفل")]
    AutomaticOpenDependentCase = 88,

    [Display(Name = "بروزرسانی عکس از ثبت احوال")]
    UpdateStudentProfilePictureFromCivilRegistry = 89,
}
