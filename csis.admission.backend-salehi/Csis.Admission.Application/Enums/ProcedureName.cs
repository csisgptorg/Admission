namespace Csis.Admission.Application.Enums;

/// <inheritdoc/>
public enum ProcedureName
{
    /// <inheritdoc/>
    GetStudentInfoV4,

    /// <summary>اطلاعات پرونده ای طلبه</summary>
    GetStudentCaseInfoV4,

    /// <inheritdoc/>
    GetStudentPictureV4,

    /// <inheritdoc/>
    GetAddressV4,

    /// <inheritdoc/>
    GetPhoneV4,

    /// <summary>تکفل</summary>
    GetDependentInfoV4,

    /// <summary>کمسیون طلبه</summary>
    GetStudentCommission,
    /// <summary>کمسیون تکفل</summary>
    GetDependentCommission,

    /// <summary>سوابق اطلاعات پذیریش طلبه</summary>
    GetStudentAuditLog,
    /// <summary>سوابق اطلاعات پذیریش تکفل</summary>
    GetDependentAuditLog,


    /// <summary>امتیاز هدفمندی</summary>
    GetTargetedScoreInfoV4,
    /// <summary>امتیاز هدفمندی معیشتی</summary>
    GetSubsistenceTargetedScoreInfoV4,

    /// <summary>خدمات مسدود</summary>
    GetStudentBlockedService,
    /// <summary>خدمات مسدود</summary>
    GetDependentBlockedService,

    /// <summary>سابقه تصاویر پرسنلی</summary>
    GetStudentPictureHistoryV4,

    /// <summary>مشخصات شناسنامه ای فرد اصلی</summary>
    GetStudentBirthCertInfoV4,

    /// <summary>تمدید پرونده</summary>
    SetStudentCaseValidityDateAuto,

    /// <summary>بروزرسانی تصویر پروفایل</summary>
    SetStudentPictureV4,

    /// <summary>دریافت شماره موبایل اعضای خانواده</summary>
    GetFamilyMobileV4,

    /// <summary>ثبت تلفن همراه طلبه</summary>
    SetStudentMobileV4,

    /// <summary>ثبت تلفن همراه تکفل</summary>
    SetDependentMobileV4,

    /// <summary>دریافت شماره حساب بانکی اعضای خانواده</summary>
    GetFamilyBankAccountNumberV4,

    /// <summary>ثبت شماره حساب بانکی طلبه</summary>
    SetStudentBankAccountNumberV4,

    /// <summary>ثبت شماره حساب بانکی اعضای خانواده</summary>
    SetDependentBankAccountNumberV4,

    /// <summary>ثبت تکفل</summary>
    SetNewDependent,

    /// <summary>ثبت تصویر موقت مروبط به پروفایل</summary>
    SetStudentTmpPictureV4,

    /// <summary>دریافت تصویر موقت مروبط به پروفایل</summary>
    GetStudentTmpPictureV4,

    /// <summary>بررسی نیاز به تایید دو طلبه برای آدرس</summary>
    CheckAddressApproveV4,

    /// <inheritdoc/>
    GenerateOtpCode,

    /// <summary>
    /// تاریخ ازدواج سرپرست - فقط طلاب خواهر
    /// </summary>
    SetStudentSisterMarriage,

    /// <summary>
    /// ثبت طلاق سرپرست - فقط طلاب خواهر
    /// </summary>
    SetStudentDivorceV4,


    /// <summary>
    /// ثبت ازدواج افراد تحت تکفل - فرزندان و در حکم فرزند- پرونده اگر باز باشد بسته می شود
    /// </summary>
    SetDependentChildMarriage,



    /// <summary>
    /// ثبت ازدواج افراد تحت تکفل - همسران بیوه- فقط  پرونده بسته می شود
    /// </summary>
    SetDependentSpouseMarriage,


    /// <summary>
    /// ثبت تاریخ طلاق برای  افراد تحت تکفل - همسر - پرونده اش به علت طلاق در افراد تحت تکفل غیر فعال می شود.
    /// </summary>
    SetDependentSpouseDivorce,


    /// <summary>
    /// ثبت تاریخ طلاق برای افراد تحت تکفل - فرزندان - تغییر در وضعیت پرونده صورت نمی گیرد
    /// </summary>
    SetDependentChildDivorce,

    /// <summary>آماده سازی دیتا برای انجام تست آزمایشی</summary>
    PrepareTestData,

    /// <summary>بررسی تکراری نبودن شماره حساب</summary>
    CheckDuplicateBankAccountNumberV4,

    /// <summary>ثبت یا ویرایش شماره حساب</summary>
    StudentBankAccountNumberRepository,

    /// <summary>استعلام از مرکز حوزوی</summary>
    ValidateStudentStatusForRegisterationV4,

    /// <summary>خدمات کارت</summary>
    GetFamilyKhadamatCardV4,

    /// <summary>تامین اجتماعی</summary>
    GetTaminInsuranceInfoV4,

    /// <summary>ستعلام کدهای مرکز در نقش معرف در نقش آفرینی</summary>
    ValidateReligiousRoleV4,

    /// <summary>ثبت نام طلبه - تشکیل پرونده</summary>
    SetNewStudent,

    /// <summary>دریافت اطلاعات مستمری</summary>
    GetPensionStatusV4,



    /// <summary>
    /// غیرفعال کردن تکفل
    /// </summary>
    DeActiveDependentV4,

    /// <summary>
    /// ایجاد شهر در تقسیمات کشوری
    /// </summary>
    SetTown,

    /// <summary>
    /// ایجاد دهستان در تقسیمات کشوری
    /// </summary>
    SetRural,

    /// <summary>
    /// ایجاد بخش در تقسیمات کشوری
    /// </summary>
    SetPortion,

    /// <summary>تاریخچه آخرین مشاهدات کاربر</summary>
    GetLastViewedCodm,

    /// <summary>تعداد رکوردهای هر منو</summary>
    GetTableRecordCountV4,

    /// <summary>سابقه هدفمندی</summary>
    GetHouseHistoryV4,

    /// <summary>اطلاعات شهریه</summary>
    GetShahriehData,

    /// <summary>لیست اعتراضات طلبه</summary>
    GetProtestPossibility,

    /// <summary>اطلاعات طلبه که نیاز به بروزرسانی دارند</summary>
    GetMustUpdate,

    //TODO : موقت باید حذف شود
    /// <summary>دریافت کاردکس اطلاعات شهریه</summary>
    GetShahriehPayments,

    /// <summary> دریافت اطلاعات همسر طلبه بر اساس کد مرکز </summary>
    GetSpouseInfoV4,

    /// <summary> دریافت امتیاز و امتیاز کل برای طرح های معیشتی و طراز </summary>
    GetTarazAndLivelihoodTotalScoreAndTotalScore,

    /// <summary>دریافت کد یکتا برای افراد</summary>
    GenerateUniqueCode,

    /// <summary>بروز رسانی شعبه و نمایندگی</summary>
    UpdateBranchAndAgency,

    /// <summary>گام هایی قابل نمایش در ویزارد بیروز رسانی اطلاعات طلبه</summary>
    GetUpdateWizardStepsVisibilty,

    /// <summary>دریافت اطلاعات کمسیون طلبه</summary>
    GetCommissionForNewStudent,

    /// <summary>بروزرسانی وضعیت کمیسیون</summary>
    SetCommissionStatus,

    /// <summary>دریافت وضعیت تحصیلی طلبه</summary>
    GetStudentEducationStatus,

    /// <summary>بروز رسانی اطلاعات شناسنامه ای طلبه</summary>
    UpdateStudentIdentity,

    /// <summary>بروز رسانی اطلاعات شناسنامه ای تکفل</summary>
    UpdateDependentIdentity,

    /// <summary>بروز رسانی اطلاعات شناسنامه ای طلبه غیر ایرانی</summary>
    UpdateNonIranianStudentIdentity,

    /// <summary>بروز رسانی اطلاعات شناسنامه ای تکفلغیر ایرانی</summary>
    UpdateNonIranianDependentIdentity,

    /// <summary>بروز رسانی تابعیت طلبه</summary>
    UpdateNonIranianStudentCitizenship,

    /// <summary>بروز رسانی تابعیت تکفل</summary>
    UpdateNonIranianDependentCitizenship,

    /// <summary>بروز رسانی اطلاعات شناسنامه ای طلبه</summary>
    SetStudentBirthCertInfo,

    /// <summary>بروز رسانی اطلاعات شناسنامه ای طلبه</summary>
    SetDependentBirthCertInfo,
    /// <summary>بروز رسانی تلفن ثابت طلبه</summary>
    SetStudentTel,
    /// <summary>بروز رسانی مسدودی خدمات طلبه</summary>
    SetStudentBlocked,
    /// <summary>رفع مسدودی خدمات طلبه</summary>
    SetStudentUnBlocked,
    /// <summary>تنظیم تاریخ اعتبار پرونده طلبه</summary>
    SetStudentCaseValidityDate,
    /// <summary>تنظیم توضیحات پرونده طلبه</summary>
    SetStudentCaseDescription,
    /// <summary>ثبت فوت طلبه</summary>
    SetStudentDeath,
    /// <summary>بروز رسانی تلفن طلبه</summary>
    SetPhone,
    /// <summary>تنظیم توضیحات پرونده تکفل</summary>
    SetDependentCaseDescription,
    /// <summary>فعال کردن پرونده تکفل</summary>
    SetDependentActive,
    /// <summary>غیرفعال کردن پرونده تکفل</summary>
    SetDependentDeActive,
    /// <summary>دریافت اطلاعات از کارافتادگی تکفل</summary>
    GetDependentPensionCommission,
    /// <summary>دریافت دیتا برای اجرای پرداخت حقوق و مستمری</summary>
    GetDataForPayRunByCodm,
    /// <summary>دریافت دیتا برای اجرای پرداخت حقوق و مستمری بر اساس لیست کد مرکز</summary>
    GetDataForPayRunByCodmList,

    /// <summary>دریافت دیتا برای اجرای پرداخت حقوق و مستمری بر اساس بازه کد مرکز</summary>
    GetDataForPayRunByStartEnd,

    /// <summary>بروز رسانی اطلاعات شناسنامه ای براساس ثبت احوال</summary>
    SetStudentWithSabtAhvalData,

    /// <summary>بروز رسانی اطلاعات شناسنامه ای براساس ثبت احوال</summary>
    SetDependentWithSabtAhvalData,

    /// <summary>بروز رسانی اطلاعات شناسنامه ای براساس المصطفی</summary>
    SetStudentWithAlmostafaData,

    /// <summary>بروز رسانی اطلاعات شناسنامه ای براساس المصطفی</summary>
    SetDependentWithAlmostafaData
}

