using Csis.Authorization;

namespace Csis.Admission.Application.Enums;

public enum PermissionsEnum : int
{
    [Permission(PermissionCategories.StudentAddress, "مشاهده آدرس طلبه")]
    StudentAddressView = 1,

    [Permission(PermissionCategories.StudentBlockService, "مشاهده خدمات مسدودی طلبه")]
    StudentBlockedServiceView = 2,

    [Permission(PermissionCategories.StudentCommissionInfo, "مشاهده اطلاعات کمیسیون طلبه")]
    StudentCommissionInfoView = 3,

    [Permission(PermissionCategories.StudentCulturalActivity, "مشاهده فعالیت های فرهنگی طلبه")]
    StudentCulturalActivityView = 4,

    [Permission(PermissionCategories.StudentCulturalActivity, "ثبت فعالیت های فرهنگی طلبه")]
    StudentCulturalActivityCreate = 5,

    [Permission(PermissionCategories.StudentCulturalActivity, "ویرایش فعالیت های فرهنگی طلبه")]
    StudentCulturalActivityUpdate = 6,

    [Permission(PermissionCategories.StudentCulturalActivity, "حذف فعالیت های فرهنگی طلبه")]
    StudentCulturalActivityDelete = 7,

    [Permission(PermissionCategories.StudentCulturalActivityGrade, "نمایش رتبه بندی فعالیت های فرهنگی طلبه")]
    StudentCulturalActivityGradeView = 8,

    [Permission(PermissionCategories.StudentEducation, "نمایش تحصیل طلبه")]
    StudentEducationView = 9,

    [Permission(PermissionCategories.StudentElite, "نمایش اطلاعات نخبگی طلبه")]
    StudentEliteView = 10,

    [Permission(PermissionCategories.StudentEmployment, "نمایش اشتغال طلبه")]
    StudentEmploymentView = 11,

    [Permission(PermissionCategories.StudentEmployment, "ثبت اشتغال طلبه")]
    StudentEmploymentRegister = 12,

    [Permission(PermissionCategories.StudentEmployment, "ثبت اشتغال تکفل")]
    DependentEmploymentRegister = 13,

    [Permission(PermissionCategories.StudentExcellent, "نمایش ممتازی طلبه")]
    StudentExcellentView = 14,

    [Permission(PermissionCategories.StudentHouse, "نمایش مسکن طلبه")]
    StudentHouseView = 15,

    [Permission(PermissionCategories.StudentHouse, "ثبت مسکن طلبه")]
    StudentHouseRegister = 16,

    [Permission(PermissionCategories.Marriage, "نمایش ازدواج طلبه")]
    StudentMarriageView = 17,

    [Permission(PermissionCategories.Marriage, "ثبت ازدواج طلبه")]
    StudentMarriageRegister = 18,

    [Permission(PermissionCategories.Marriage, "ثبت ازدواج تکفل")]
    DependentMarriageRegister = 19,

    [Permission(PermissionCategories.Divorce, "ثبت طلاق طلبه")]
    StudentDivorceRegister = 20,

    [Permission(PermissionCategories.StudentMemorizer, "نمایش حافظی طلبه")]
    StudentMemorizerView = 21,

    [Permission(PermissionCategories.StudentPictureHistory, "نمایش سابقه تصاویر طلبه")]
    StudentPictureHistoryView = 22,

    [Permission(PermissionCategories.StudentPreach, "نمایش تبلیغ طلبه")]
    StudentPreachView = 23,

    [Permission(PermissionCategories.StudentPreach, "ثبت تبلیغ طلبه")]
    StudentPreachRegister = 24,

    [Permission(PermissionCategories.StudentPreachGrade, "نمایش رتبه بندی تبلیغ طلبه")]
    StudentPreachGradeView = 25,

    [Permission(PermissionCategories.StudentPreachGrade, "ثبت رتبه بندی تبلیغ طلبه")]
    StudentPreachGradeRegister = 26,

    [Permission(PermissionCategories.StudentPregnancy, "نمایش ایام بارداری طلبه")]
    StudentPregnancyView = 27,

    [Permission(PermissionCategories.StudentPregnancy, "ثبت ایام بارداری طلبه")]
    StudentPregnancyRegister = 28,

    [Permission(PermissionCategories.StudentProtest, "نمایش اعتراضات طلبه")]
    StudentProtestView = 29,

    [Permission(PermissionCategories.StudentResearch, "نمایش پژوهش طلبه")]
    StudentResearchView = 30,

    [Permission(PermissionCategories.StudentResearchGrade, "نمایش رتبه پژوهش طلبه")]
    StudentResearchGradeView = 31,

    [Permission(PermissionCategories.StudentResearchGrade, "ثبت رتبه پژوهش طلبه")]
    StudentResearchGradeRegister = 32,

    [Permission(PermissionCategories.StudentSoldier, "نمایش سرباز طلبه")]
    StudentSoldierView = 33,

    [Permission(PermissionCategories.StudentSoldier, "ثبت سرباز طلبه")]
    StudentSoldierRegister = 34,

    [Permission(PermissionCategories.StudentFriend, "نمایش دوست طلبه")]
    StudentFriendView = 35,

    [Permission(PermissionCategories.StudentFriend, "ثبت دوست طلبه")]
    StudentFriendRegister = 36,

    [Permission(PermissionCategories.Student, "نمایش طلبه")]
    StudentView = 37,

    [Permission(PermissionCategories.Student, "نمایش تلفن طلبه")]
    StudentPhoneView = 38,

    [Permission(PermissionCategories.Student, "نمایش تصویر پروفایل طلبه")]
    StudentProfileImageView = 39,

    [Permission(PermissionCategories.Student, "نمایش تلفن تکفل")]
    DependentPhoneView = 40,

    [Permission(PermissionCategories.Student, "ثبت تصویر پروفایل طلبه")]
    StudentProfileImageRegister = 41,

    [Permission(PermissionCategories.StudentTargetedScore, "نمایش هدفمندی طلبه")]
    StudentTargetedScoreView = 42,

    [Permission(PermissionCategories.StudentTeach, "نمایش تدریس طلبه")]
    StudentTeachView = 43,

    [Permission(PermissionCategories.StudentTeach, "ثبت تدریس طلبه")]
    StudentTeachRegister = 44,

    [Permission(PermissionCategories.StudentTeachGrade, "نمایش رتبه بندی تدریس طلبه")]
    StudentTeachGradeView = 45,

    [Permission(PermissionCategories.StudentTeachGrade, "ثبت رتبه بندی طلبه")]
    StudentTeachGradeRegister = 46,

    [Permission(PermissionCategories.StudentUniversityEducation, "نمایش تحصیلات دانشگاهی طلبه")]
    StudentUniversityEducationView = 47,

    [Permission(PermissionCategories.StudentUniversityEducation, "ثبت تحصیلات دانشگاهی طلبه")]
    StudentUniversityEducationRegister = 48,

    [Permission(PermissionCategories.StudentVeteran, "نمایش ایثارگری طلبه")]
    StudentVeteranView = 49,

    [Permission(PermissionCategories.Personnel, "کاربر ارشد")]
    SeniorPersonnel = 50,

    [Permission(PermissionCategories.FemaleInfo, "نمایش تصویر پروفایل بانوان")]
    FemaleInfoProfilePicture = 51,

    [Permission(PermissionCategories.FemaleInfo, "نمایش اطلاعات فردی و هویتی بانوان")]
    FemaleInfoIdentity = 52,

    [Permission(PermissionCategories.StudentInsurance, "نمایش بیمه طلبه")]
    StudentInsuranceView = 53,

    [Permission(PermissionCategories.StudentBankAccount, "نمایش حساب بانکی تکفل و طلبه")]
    StudentDependetnBankAccountView = 54,

    [Permission(PermissionCategories.StudentBankAccount, "ثبت حساب بانکی طلبه")]
    StudentBankAccountRegister = 55,

    [Permission(PermissionCategories.StudentBankAccount, "ثبت حساب بانکی تکفل")]
    DependentBankAccountRegister = 56,

    [Permission(PermissionCategories.StudentRequest, "نمایش درخواست طلبه")]
    StudentRequestView = 57,

    [Permission(PermissionCategories.StudentRequest, "تایید درخواست طلبه")]
    StudentRequestApproved = 58,

    [Permission(PermissionCategories.StudentRequest, "رد درخواست طلبه")]
    StudentRequestRejected = 59,

    [Permission(PermissionCategories.Student, "نمایش تکفل طلبه")]
    DependentView = 60,

    [Permission(PermissionCategories.Student, "نمایش تعداد رکوردهای طلبه در منوهای مختلف")]
    StudentRecordCount = 61,

    [Permission(PermissionCategories.Student, "جستجو پیشرفته طلبه")]
    StudentAdvancedSearch = 62,

    [Permission(PermissionCategories.StudentVeteran, "ویرایش نسبت با شهید ایثارگری طلبه")]
    StudentVeteranUpdateRelationWithMartyr = 63,

    [Permission(PermissionCategories.StudentVeteran, "ویرایش درصد جانبازی ایثارگری طلبه")]
    StudentVeteranUpdateVeteranPercent = 64,

    [Permission(PermissionCategories.StudentVeteran, "ویرایش روزهای آزادگی ایثارگری طلبه")]
    StudentVeteranUpdateCaptivityDays = 65,

    [Permission(PermissionCategories.StudentHouse, "نمایش سابقه مسکن طلبه")]
    StudentHouseHistoryView = 66,

    [Permission(PermissionCategories.StudentFamous, "نمایش مشاهیر ثبت شده برای طلبه")]
    StudentFamousView = 67,

    [Permission(PermissionCategories.Student, "جستجو پیشرفته تکفل")]
    DependentAdvancedSearch = 68,

    [Permission(PermissionCategories.StudentTargetedScore, "نمایش لیست تغییرات امتیاز هدفمندی طلبه")]
    TargetedScoreHistoryChangesView = 69,

    [Permission(PermissionCategories.StudentTargetedScore, "نمایش امتیاز هدفمندی معیشتی طلبه")]
    SubsistenceTargetedScoreView = 70,

    [Permission(PermissionCategories.Student, "نمایش اطلاعات شهریه طلبه")]
    StudentShahriehInfoView = 71,

    [Permission(PermissionCategories.Student, "نمایش اطلاعات طلبه در مراکز حوزوی")]
    StudentInfoInMarakezHowzaviView = 72,

    [Permission(PermissionCategories.Student, "تمدید پرونده")]
    StudentExtensionCase = 73,

    [Permission(PermissionCategories.ContinuousInformationTab, "مشاهده مشخصات مستمری")]
    ViewContinuousInformationTab = 74,

    [Permission(PermissionCategories.CaseFiling, "مدیریت تشکیل پرونده")]
    ManagementCaseFiling = 75,

    [Permission(PermissionCategories.CaseFiling, "نمایش پرونده ها")]
    ViewCaseFiling = 76,

    [Permission(PermissionCategories.ExternalApi, "برنامه بیمه سلامت - خدمات درمانی")]
    HealthInsuranceApplication = 77,

    [Permission(PermissionCategories.ExternalApi, "نمایش اطلاعات کامل طلبه")]
    StudentCompleteInfoAppView = 78,

    [Permission(PermissionCategories.ExternalApi, "نمایش تصویر پروفایل طلبه")]
    StudentProfileImageAppView = 79,

    [Permission(PermissionCategories.ExternalApi, "ثبت اطلاعات توسط ارتباط داده ای")]
    DataImport = 80,

    [Permission(PermissionCategories.StudentApp, "کاردکس - اطلاعات شهریه طلبه")]
    StudentCardexShahriehView = 81,

    [Permission(PermissionCategories.LocationManagement, "نمایش شهر")]
    CityView = 82,

    [Permission(PermissionCategories.LocationManagement, "نمایش بخش")]
    PortionView = 83,

    [Permission(PermissionCategories.ReportBuilder, "ایجاد گزارش در گزارش‌ساز")]
    ReportBuilderCreate = 84,

    [Permission(PermissionCategories.ReportBuilder, "ویرایش گزارش در گزارش‌ساز")]
    ReportBuilderUpdate = 85,

    [Permission(PermissionCategories.ReportBuilder, "حذف گزارش در گزارش‌ساز")]
    ReportBuilderDelete = 86,

    [Permission(PermissionCategories.ReportBuilder, "نمایش گزارش گزارش‌ساز")]
    ReportBuilderView = 87,

    [Permission(PermissionCategories.ReportBuilder, "نمایش گزارشات گزارش‌ساز")]
    ReportBuildersView = 88,

    [Permission(PermissionCategories.ReportBuilder, "جداول گزارش‌ساز")]
    ReportBuilderTables = 89,

    [Permission(PermissionCategories.ReportBuilder, "نمایش نتیجه گزارش گزارش‌ساز")]
    ReportBuilderReport = 90,

    [Permission(PermissionCategories.ImamJamaat, "نمایش لیست مساجد ادمین")]
    ImamJamaatMosqueListView = 91,

    [Permission(PermissionCategories.ImamJamaat, "استعلام کد مرکز حوزوی ادمین")]
    ImamJamaatCodMInquiry = 92,

    [Permission(PermissionCategories.ImamJamaat, "ثبت مسجد با جزئیات امام جماعت و فعالیت‌های مسجد ادمین")]
    CreateMosqueWithDetails = 93,

    [Permission(PermissionCategories.ImamJamaat, "به‌روزرسانی اطلاعات مسجد با جزئیات امام جماعت و فعالیت‌های مسجد ادمین")]
    UpdateMosqueWithDetails = 94,

    [Permission(PermissionCategories.ImamJamaat, "حذف مسجد با جزئیات امام جماعت و فعالیت‌های مسجد ادمین")]
    DeleteMosqueWithDetails = 95,

    [Permission(PermissionCategories.ImamJamaat, "نمایش مسجد براساس اطلاعات ثبت شده ادمین")]
    GetMosqueById = 96,

    [Permission(PermissionCategories.ImamJamaat, "نمایش لیست مساجد ادمین")]
    GetMosqueListView = 97,

    [Permission(PermissionCategories.ImamJamaat, "استعلام کد پستی مسجد")]
    GetAddressByPostalCode = 98,

    [Permission(PermissionCategories.ImamJamaat, "نمایش لیست تمامی درخواست‌ها ادمین")]
    GetAllRequestList = 99,

    [Permission(PermissionCategories.ImamJamaat, "نمایش اطلاعات همسر")]
    DependentSpouseInfoView = 100,
    [Permission(PermissionCategories.ImamJamaat, "نمایش طلبه")]
    ImamJamaatView = 163,

    [Permission(PermissionCategories.People, "جستجوی موجودیت شخص")]
    SearchPeople = 101,

    [Permission(PermissionCategories.People, "ایجاد موجودیت شخص جدید")]
    CreatePerson = 102,

    [Permission(PermissionCategories.People, "ویرایش موجودیت شخص")]
    UpdatePerson = 103,

    [Permission(PermissionCategories.People, "حذف موجودیت شخص")]
    DeletePerson = 104,

    [Permission(PermissionCategories.People, "مشاهده موجودیت شخص")]
    ViewPerson = 105,

    [Permission(PermissionCategories.People, "اعتبارسنجی هویت ایرانی (کد ملی) قبل از ایجاد/بروزرسانی")]
    ValidateIranianPersonIdentity = 106,

    [Permission(PermissionCategories.People, "اعتبارسنجی هویت غیر ایرانی (کد یکتا) قبل از ایجاد/بروزرسانی")]
    ValidateNonIranianPersonIdentity = 107,

    [Permission(PermissionCategories.People, "ثبت نسبت شخص با سایر اشخاص")]
    PersonAssignRelation = 108,

    [Permission(PermissionCategories.People, "ثبت نسبت همسر برای شخص")]
    PersonAssignSpouse = 109,

    [Permission(PermissionCategories.People, "دریافت موجودیت شخص با کد ملی")]
    GetPersonByNationalCode = 110,

    [Permission(PermissionCategories.People, "اعتبارسنجی شماره موبایل قبل از ایجاد/بروزرسانی")]
    ValidatePersonMobileNumber = 111,

    [Permission(PermissionCategories.People, "به‌روزرسانی تصویر شخص")]
    UpdatePersonImage = 112,

    [Permission(PermissionCategories.StudentResearch, "ثبت پژوهش طلبه")]
    StudentResearchCreate = 113,

    [Permission(PermissionCategories.StudentResearch, "ویرایش پژوهش طلبه")]
    StudentResearchEdit = 114,

    [Permission(PermissionCategories.StudentResearch, "حذف پژوهش طلبه")]
    StudentResearchDelete = 115,

    [Permission(PermissionCategories.StudentFamous, "ثبت مشاهیر برای طلبه")]
    StudentFamousCreate = 116,

    [Permission(PermissionCategories.StudentFamous, "ویرایش مشاهیر طلبه")]
    StudentFamousEdit = 117,

    [Permission(PermissionCategories.StudentFamous, "حذف مشاهیر طلبه")]
    StudentFamousDelete = 118,

    [Permission(PermissionCategories.StudentMobile, "نمایش موبایل طلبه")]
    StudentMobileView = 119,

    [Permission(PermissionCategories.StudentMobile, "به‌روزرسانی موبایل طلبه")]
    StudentMobileEdit = 120,

    [Permission(PermissionCategories.StudentMobile, "به‌روزرسانی موبایل تکفل")]
    StudentDependentMobileEdit = 121,

    [Permission(PermissionCategories.StudentFriend, "حذف دوست طلبه")]
    StudentFriendDelete = 122,

    [Permission(PermissionCategories.StudentEmployment, "حذف اشتغال طلبه")]
    StudentEmploymentDelete = 123,

    [Permission(PermissionCategories.StudentEmployment, "حذف اشتغال تکفل")]
    DependentEmploymentDelete = 124,

    [Permission(PermissionCategories.StudentVeteran, "ثبت و ویرایش ایثارگری")]
    StudentVeteranCreateOrUpdate = 125,

    [Permission(PermissionCategories.StudentVeteran, "حذف ایثارگری")]
    StudentVeteranDelete = 126,

    [Permission(PermissionCategories.StudentHouse, "حذف مسکن")]
    StudentHouseDelete = 127,

    [Permission(PermissionCategories.StudentExcellent, "حذف ممتازی")]
    StudentExcellentDelete = 128,

    [Permission(PermissionCategories.StudentMemorizer, "حذف حافظ")]
    StudentMemorizerDelete = 129,

    [Permission(PermissionCategories.StudentElite, "ثبت و ویرایش نخبگان")]
    StudentEliteCreateOrUpdate = 130,

    [Permission(PermissionCategories.StudentElite, "حذف نخبگان")]
    StudentEliteDelete = 131,

    [Permission(PermissionCategories.StudentBlockCase, "مسدودی پرونده طلبه")]
    StudentBlockCase = 132,

    [Permission(PermissionCategories.StudentBlockCase, "رفع مسدودی پرونده طلبه")]
    StudentUnblockCase = 133,

    [Permission(PermissionCategories.Student, "ویرایش عادی پرونده طلبه")]
    StudentNormalEditCase = 134,

    [Permission(PermissionCategories.StudentEmployment, "شناسایی موردی اشتغال طلبه")]
    StudentEmploymentIdentify = 135,

    [Permission(PermissionCategories.NonIranian, "ثبت فوت طلبه غیر ایرانی")]
    RegisterNonIranianStudentDeath = 136,

    [Permission(PermissionCategories.StudentDependents, "ثبت توضیحات پرونده تکفل")]
    DependentCaseDescriptionCreate = 137,

    [Permission(PermissionCategories.StudentBlockService, "ثبت")]
    CreateStudentBlockService = 138,

    [Permission(PermissionCategories.StudentBlockService, "ویرایش")]
    UpdateStudentBlockService = 139,

    [Permission(PermissionCategories.StudentBlockService, "حذف")]
    DeleteStudentBlockService = 140,

    [Permission(PermissionCategories.StudentBlockService, "لیست")]
    StudentBlockServiceViews = 143,

    [Permission(PermissionCategories.DependentBlockService, "ثبت")]
    CreateDependentBlockService = 144,

    [Permission(PermissionCategories.DependentBlockService, "ویرایش")]
    UpdateDependentBlockService = 145,

    [Permission(PermissionCategories.DependentBlockService, "حذف")]
    DeleteDependentBlockService = 146,

    [Permission(PermissionCategories.DependentBlockService, "لیست")]
    DependentBlockServiceViews = 147,

    [Permission(PermissionCategories.StudentBlockService, "لیست خدمات طلبه")]
    StudentServiceViews = 150,

    [Permission(PermissionCategories.DependentBlockService, "لیست خدمات تکفل")]
    DependentServiceViews = 151,

    [Permission(PermissionCategories.StudentDependents, "به‌روزرسانی وضعیت فعال یا غیر فعال تکفل")]
    DependentCaseActiveUpdate = 148,
    [Permission(PermissionCategories.StudentDependents, "به‌روزرسانی خودکار وضعیت فعال یا غیر فعال تکفل توسط کارمند")]
    DependentCaseActiveEmployeeUpdate = 149,
    [Permission(PermissionCategories.StudentEducation, "ویرایش تحصیل طلبه")]
    StudentEducationEdit = 153,
    [Permission(PermissionCategories.StudentUniversityEducation, "حذف تحصیلات دانشگاهی طلبه")]
    StudentUniversityEducationDelete = 154,
    [Permission(PermissionCategories.StudentUniversityEducation, "حذف تحصیلات دانشگاهی تکفل")]
    DependentUniversityEducationDelete = 155,
    [Permission(PermissionCategories.People, "تنظیمات پنل افراد")]
    SetPeoplePanelSettings = 156,
    [Permission(PermissionCategories.People, "اعتبارسنجی مالکیت شبا")]
    ValidateShebaOwnership = 157,
    [Permission(PermissionCategories.People, "ثبت مالکیت شبا")]
    CreateShebaOwnership = 158,
    [Permission(PermissionCategories.NonIranian, "ثبت نسبت خانوادگی برای فرد غیر ایرانی - همچنین ثبت ازدواج طلبه غیر ایرانی با همسر غیر ایرانی نیز از این طریق انجام می‌شود")]
    AssignRelationToNonIranian = 159,
    [Permission(PermissionCategories.NonIranian, "ثبت طلاق تکفل برای فرد غیر ایرانی")]
    MarkDependentAsDivorced = 160,
    [Permission(PermissionCategories.NonIranian, "ثبت طلاق همسر برای فرد غیر ایرانی")]
    MarkWifeAsDivorced = 161,
    [Permission(PermissionCategories.NonIranian, "ثبت ازدواج تکفل برای فرد غیر ایرانی")]
    MarkDependentAsMarried = 162,
    // * 163 استفاده شده در بالا برای ImamJamaatView
    [Permission(PermissionCategories.ExternalApi, "دریافت داده‌های مورد نیاز برای پردازش حقوق و دستمزد طلبه")]
    GetDataForPayRun = 164  ,

    #region Report Profiles
    [Permission(PermissionCategories.ReportProfiles, "دسترسی به گزارشات ذخیره شده")]
    AccessReportProfiles = int.MaxValue - 200,

    [Permission(PermissionCategories.ReportProfiles, "ایجاد پروفایل گزارش عمومی")]
    CreatePublicReportProfile = int.MaxValue - 199,

    [Permission(PermissionCategories.ReportProfiles, "ایجاد پروفایل گزارش خصوصی")]
    CreatePrivateReportProfile = int.MaxValue - 198,

    [Permission(PermissionCategories.ReportProfiles, "مشاهده پروفایل گزارش عمومی")]
    ViewPublicReportProfiles = int.MaxValue - 197,

    [Permission(PermissionCategories.ReportProfiles, "ویرایش پروفایل‌های گزارش ساخته شده توسط دیگر کاربران")]
    EditAllPublicProfiles = int.MaxValue - 196,
    #endregion

    #region AppManagement
    [Permission(PermissionCategories.AppManagement, "ریست کش ردیس")]
    ResetRedisCache = int.MaxValue - 100,

    [Permission(PermissionCategories.AppManagement, "مدیریت تنظیمات نوتیفیکیشن")]
    ManageNotificationSettings = int.MaxValue - 99,
    #endregion
}
