# مستندات کامل Use Case های سیستم پذیرش

این مستند شامل تمامی Use Case های موجود در سیستم پذیرش می‌باشد که به صورت دسته‌بندی شده و با توضیحات کامل ارائه شده است.

**تاریخ ایجاد:** Mon Dec 22 08:39:18 UTC 2025

---

## فهرست مطالب

- [مدیریت دانشجو (Student Management)](#مدیریت-دانشجو)
- [مدیریت تحت تکفل (Dependent Management)](#مدیریت-تحت-تکفل)
- [مدیریت کاربران و احراز هویت (User & Authentication)](#مدیریت-کاربران-و-احراز-هویت)
- [مدیریت پرونده (Case Management)](#مدیریت-پرونده)
- [خدمات رفاهی (Welfare Services)](#خدمات-رفاهی)
- [اطلاعات تحصیلی (Educational Information)](#اطلاعات-تحصیلی)
- [فعالیت‌های فرهنگی (Cultural Activities)](#فعالیت‌های-فرهنگی)
- [گزارش‌گیری (Reporting)](#گزارش‌گیری)
- [اطلاعات پایه (Basic Data)](#اطلاعات-پایه)
- [اعلان‌ها و درخواست‌ها (Notifications & Requests)](#اعلان‌ها-و-درخواست‌ها)
- [سایر (Others)](#سایر)
- [سرویس‌های سیستم](#سرویس‌های-سیستم)

---

## مدیریت دانشجو (Student Management)

### CompleteStudentInfos

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetCompleteStudentInfoByCodmQuery**
  - توضیح: دریافت اطلاعات کامل طلبه
  - نوع عملیات: Query (خواندن داده)

---

### EmployeeViewStudentLogs

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateEmployeeViewStudentLogCommand**
  - توضیح: لاگ مشاهده اطلاعات طلبه توسط کارمند
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetEmployeeViewStudentLogByPersonnelIdQuery**
  - توضیح: GetEmployeeViewStudentLogByPersonnelIdQuery
  - نوع عملیات: Query (خواندن داده)

- **GetLastEmployeeViewStudentLogByPersonnelIdQuery**
  - توضیح: GetLastEmployeeViewStudentLogByPersonnelIdQuery
  - نوع عملیات: Query (خواندن داده)

---

### NonIranianStudent

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **AssignRelationToNonIranianCommand**
  - توضیح: انتساب نسبت به غیرایرانی ها

  - نوع عملیات: Command (تغییر داده)

- **UpdateNonIranianDependentDivorceCommand**
  - توضیح: UpdateNonIranianDependentDivorceCommand
  - نوع عملیات: Command (تغییر داده)

- **UpdateNonIranianDependentDivorceRequestCommand**
  - توضیح: ثبت درخواست تغییر وضعیت طلاق تکفل غیر ایرانی

  - نوع عملیات: Command (تغییر داده)

- **UpdateNonIranianDependentMarriageCommand**
  - توضیح: UpdateNonIranianDependentMarriageCommand
  - نوع عملیات: Command (تغییر داده)

- **UpdateNonIranianDependentMarriageRequestCommand**
  - توضیح: ثبت درخواست تغییر وضعیت تاهل تکفل غیر ایرانی

  - نوع عملیات: Command (تغییر داده)

- **UpdateNonIranianWifeDivorceCommand**
  - توضیح: UpdateNonIranianWifeDivorceCommand
  - نوع عملیات: Command (تغییر داده)

- **UpdateNonIranianWifeDivorceRequestCommand**
  - توضیح: UpdateNonIranianWifeDivorceRequestCommand
  - نوع عملیات: Command (تغییر داده)

---

### PictureHistories

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetPictureHistoriesByCodmQuery**
  - توضیح: GetPictureHistoriesByCodmQuery

  - نوع عملیات: Query (خواندن داده)

---

### SoldierStudents

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateSoldierStudentCommand**
  - توضیح: CreateSoldierStudentCommand
  - نوع عملیات: Command (تغییر داده)

- **DeleteSoldierStudentCommand**
  - توضیح: DeleteSoldierStudentCommand

  - نوع عملیات: Command (تغییر داده)

- **UpdateSoldierStudentCommand**
  - توضیح: UpdateSoldierStudentCommand
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetSoldierStudentByCodmQuery**
  - توضیح: GetSoldierStudentByCodmQuery
  - نوع عملیات: Query (خواندن داده)

- **GetSoldierStudentByIdQuery**
  - توضیح: GetSoldierStudentByIdQuery
  - نوع عملیات: Query (خواندن داده)

---

### StudentDependents

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateStudentDependentCaseDescriptionCommand**
  - توضیح: تغییر مشخصات پرونده ای تکفل

  - نوع عملیات: Command (تغییر داده)

- **CreateStudentDependentCaseDescriptionRequestCommand**
  - توضیح: تغییر مشخصات پرونده ای تکفل
  - نوع عملیات: Command (تغییر داده)

- **IdentifySpouseFromSabteAhvalCommand**
  - توضیح: فرمان دریافت اطلاعات هویتی از ثبت احوال و ثبت همسر

  - نوع عملیات: Command (تغییر داده)

- **StudentChildRegistryCommand**
  - توضیح: StudentChildRegistryCommand
  - نوع عملیات: Command (تغییر داده)

- **StudentSpouseRegistryCommand**
  - توضیح: ثبت همسر

  - نوع عملیات: Command (تغییر داده)

- **StudentSpouseRegistryRequestCommand**
  - توضیح: درخواست ثبت همسر

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetDependentSpousesQuery**
  - توضیح: نمایش لیست همسران هر مرد

  - نوع عملیات: Query (خواندن داده)

- **GetFamilySinglesByCodmQuery**
  - توضیح: لیست سرپرست و اعضای خانواده

  - نوع عملیات: Query (خواندن داده)

- **GetStudentDependentsQuery**
  - توضیح: دریافت لیست تکفل ها + خود طلبه برای استفاده در نرم افزار جهیزیه

  - نوع عملیات: Query (خواندن داده)

---

### StudentFriends

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateStudentFriendCommand**
  - توضیح: CreateStudentFriendCommand
  - نوع عملیات: Command (تغییر داده)

- **CreateStudentFriendRequestCommand**
  - توضیح: CreateStudentFriendRequestCommand
  - نوع عملیات: Command (تغییر داده)

- **DeleteStudentFriendCommand**
  - توضیح: ویرایش پژوهش

  - نوع عملیات: Command (تغییر داده)

- **DeleteStudentFriendRequestCommand**
  - توضیح: حذف دوست

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetStudentForFriendQuery**
  - توضیح: GetStudentForFriendQuery
  - نوع عملیات: Query (خواندن داده)

- **GetStudentFriendByCodmQuery**
  - توضیح: GetStudentFriendByCodmQuery
  - نوع عملیات: Query (خواندن داده)

---

### StudentMobiles

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **UpdateDependentMobileCommand**
  - توضیح: بروز رسانی موبایل تکفل
  - نوع عملیات: Command (تغییر داده)

- **UpdateDependentMobileRequestCommand**
  - توضیح: بروز رسانی موبایل تکفل
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentMobileForCompleteInfoRegistrationCommand**
  - توضیح: UpdateStudentMobileForCompleteInfoRegistrationCommand
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentPhoneCommand**
  - توضیح: بروز رسانی موبایل طلبه
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentPhoneRequestCommand**
  - توضیح: بروز رسانی موبایل طلبه
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetFamilyMobilesByCodmQuery**
  - توضیح: GetFamilyMobilesByCodmQuery
  - نوع عملیات: Query (خواندن داده)

---

### Students

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateStudentDeathCommand**
  - توضیح: ایجاد فوت طلبه غیرایرانی

  - نوع عملیات: Command (تغییر داده)

- **CreateStudentDeathRequestCommand**
  - توضیح: ایجاد فوت طلبه غیرایرانی

  - نوع عملیات: Command (تغییر داده)

- **ManualStudentExtensionCaseCommand**
  - توضیح: تمدید پرونده دستی

  - نوع عملیات: Command (تغییر داده)

- **ManualStudentExtensionCaseRequestCommand**
  - توضیح: تمدید پرونده دستی

  - نوع عملیات: Command (تغییر داده)

- **StudentExtensionCaseCommand**
  - توضیح: تمدید پرونده

  - نوع عملیات: Command (تغییر داده)

- **StudentNormalEditCaseRequestCommand**
  - توضیح: ویرایش عادی پرونده

  - نوع عملیات: Command (تغییر داده)

- **StudentNormalExtensionCaseCommand**
  - توضیح: ویرایش عادی پرونده

  - نوع عملیات: Command (تغییر داده)

- **SyncDependentBirthCertByIdCommand**
  - توضیح: SyncDependentBirthCertByIdCommand
  - نوع عملیات: Command (تغییر داده)

- **SyncDependentBirthCertCommand**
  - توضیح: بروز رسانی اطلاعات شناسنامه ای براساس ثبت احوال یا المصطفی
  - نوع عملیات: Command (تغییر داده)

- **SyncNonIranianDependentBirthCertByIdCommand**
  - توضیح: SyncNonIranianDependentBirthCertByIdCommand
  - نوع عملیات: Command (تغییر داده)

- **SyncNonIranianStudentBirthCertByCodmCommand**
  - توضیح: SyncNonIranianStudentBirthCertByCodmCommand
  - نوع عملیات: Command (تغییر داده)

- **SyncStudentBirthCertByCodmCommand**
  - توضیح: SyncStudentBirthCertByCodmCommand
  - نوع عملیات: Command (تغییر داده)

- **SyncStudentBirthCertCommand**
  - توضیح: بروز رسانی اطلاعات شناسنامه ای براساس ثبت احوال یا المصطفی
  - نوع عملیات: Command (تغییر داده)

- **UpdateDependentBirthCertCommand**
  - توضیح: بروز رسانی اطلاعات شناسنامه ای
  - نوع عملیات: Command (تغییر داده)

- **UpdateNonIranianDependentBirthCertCommand**
  - توضیح: بروز رسانی اطلاعات شناسنامه ای تکفل
  - نوع عملیات: Command (تغییر داده)

- **UpdateNonIranianDependentCitizenshipCommand**
  - توضیح: بروز رسانی تابعیت براساس ثبت احوال
  - نوع عملیات: Command (تغییر داده)

- **UpdateNonIranianStudentBirthCertCommand**
  - توضیح: بروز رسانی اطلاعات شناسنامه ای
  - نوع عملیات: Command (تغییر داده)

- **UpdateNonIranianStudentCitizenshipCommand**
  - توضیح: بروز رسانی تابعیت براساس ثبت احوال
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentBirthCertCommand**
  - توضیح: بروز رسانی اطلاعات شناسنامه ای
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentProfilePictureCommand**
  - توضیح: UpdateStudentProfilePictureCommand
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentProfilePictureFromCivilRegistryCommand**
  - توضیح: بروزرسانی عکس پروفایل از ثبت احوال (پس از تایید ادمین)

  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentProfilePictureFromCivilRegistryRequestCommand**
  - توضیح: درخواست بروزرسانی عکس پروفایل از ثبت احوال

  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentProfilePictureRequestCommand**
  - توضیح: UpdateStudentProfilePictureRequestCommand
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **CalculateExtensionCaseTimeQuery**
  - توضیح: تمدید پرونده

  - نوع عملیات: Query (خواندن داده)

- **DependentAdvancedSearchQuery**
  - توضیح: جستجوی پیشرفته طلبه
  - نوع عملیات: Query (خواندن داده)

- **GetStudentAddressByCodmQuery**
  - توضیح: Get student by codm

  - نوع عملیات: Query (خواندن داده)

- **GetStudentByCodmQuery**
  - توضیح: GetStudentByCodmQuery
  - نوع عملیات: Query (خواندن داده)

- **GetStudentCardexShahriehQuery**
  - توضیح: کاردکس اطلاعات شهریه طلبه
  - نوع عملیات: Query (خواندن داده)

- **GetStudentCaseByCodmQuery**
  - توضیح: Get student case by codm

  - نوع عملیات: Query (خواندن داده)

- **GetStudentDependentByCodmQuery**
  - توضیح: GetStudentDependentByCodmQuery
  - نوع عملیات: Query (خواندن داده)

- **GetStudentElectronicCardByCodmQuery**
  - توضیح: کارت الکترونیکی طلبه

  - نوع عملیات: Query (خواندن داده)

- **GetStudentHouseHistoryByCodmQuery**
  - توضیح: GetStudentHouseHistoryByCodmQuery
  - نوع عملیات: Query (خواندن داده)

- **GetStudentInfoByCodmQuery**
  - توضیح: GetStudentInfoByCodmQuery

  - نوع عملیات: Query (خواندن داده)

- **GetStudentInfoDependentsByStudentCodmQuery**
  - توضیح: GetStudentInfoDependentsByStudentCodmQuery
  - نوع عملیات: Query (خواندن داده)

- **GetStudentPhoneByCodmQuery**
  - توضیح: Get student phone by codm

  - نوع عملیات: Query (خواندن داده)

- **GetStudentProfileImageByCodmQuery**
  - توضیح: GetStudentProfileImageByCodmQuery
  - نوع عملیات: Query (خواندن داده)

- **GetStudentShahriehInfoByCodmQuery**
  - توضیح: دریافت اطلاعات شهریه طلبه
  - نوع عملیات: Query (خواندن داده)

- **GetStudentSpouseByStudentCodmQuery**
  - توضیح: اطلاعات همسر طلبه

  - نوع عملیات: Query (خواندن داده)

- **GetStudentSummaryCaseByCodmQuery**
  - توضیح: دریافت اطلاعات پرونده ای دانشجو بر اساس کد مرکز (Codm)

  - نوع عملیات: Query (خواندن داده)

- **GetStudentTotalReportByStudentCodmQuery**
  - توضیح: تراز , هدفمندی , معیشت طلبه

  - نوع عملیات: Query (خواندن داده)

- **GetStudentUpdateWizardStepsQuery**
  - توضیح: دریافت فرایندهای نیازمند بروز رسانی طلبه
  - نوع عملیات: Query (خواندن داده)

- **StudentAdvancedSearchQuery**
  - توضیح: جستجوی پیشرفته طلبه
  - نوع عملیات: Query (خواندن داده)

- **StudentInfoNeedUpdateByCodmQuery**
  - توضیح: اطلاعات طلبه که نیاز به بروزرسانی دارند
  - نوع عملیات: Query (خواندن داده)

- **StudentRecordCountByCodmQuery**
  - توضیح: تعداد رکوردهای طلبه در بخش های مختلف
  - نوع عملیات: Query (خواندن داده)

---

## مدیریت تحت تکفل (Dependent Management)

### DependentActiveReasons

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetAllDependentActiveReasonsQuery**
  - توضیح: دریافت همه دلیل رفع انسداد پرونده ها

  - نوع عملیات: Query (خواندن داده)

---

### DependentCaseActive

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **AutomaticOpenDependentCaseCommand**
  - توضیح: باز کردن پرونده تکفل (اجرای واقعی بدون Request)

  - نوع عملیات: Command (تغییر داده)

- **AutomaticOpenDependentCaseRequestCommand**
  - توضیح: درخواست باز شدن پرونده تکفل

  - نوع عملیات: Command (تغییر داده)

- **UpdateDependentCaseActiveEmployeeCommand**
  - توضیح: محاسبه خودکار وضعیت پرونده تکفل
  - نوع عملیات: Command (تغییر داده)

- **UpdateDependentCaseActiveEmployeeRequestCommand**
  - توضیح: درخواست محاسبه و بروزرسانی خودکار وضعیت پرونده تکفل
  - نوع عملیات: Command (تغییر داده)

- **UpdateDependentCaseActiveStatusSeniorCommand**
  - توضیح: ویرایش وضعیت فعال بودن تکفل در پرونده پذیرش

  - نوع عملیات: Command (تغییر داده)

- **UpdateDependentCaseActiveStatusSeniorRequestCommand**
  - توضیح: ویرایش وضعیت غیرفعال بودن تکفل در پرونده پذیرش

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetActiveDeActiveReasonDiffrenceQuery**
  - توضیح: محاسبه علت فعال یا غیر فعال بودن تکفل

  - نوع عملیات: Query (خواندن داده)

---

### DependentDeActiveReasons

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetAllDependentDeActiveReasonsQuery**
  - توضیح: دریافت همه دلیل انسداد پرونده ها

  - نوع عملیات: Query (خواندن داده)

---

### DependentEmployments

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **EmploymentDependentDataImportCommand**
  - توضیح: ارتباط داده ای - اشتغال تکفل

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetDependentEmploymentsByCodmQuery**
  - توضیح: دریافت اشتغال تکفل
  - نوع عملیات: Query (خواندن داده)

---

### Divorce

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **UpdateDependentDivorceCommand**
  - توضیح: UpdateDependentDivorceCommand
  - نوع عملیات: Command (تغییر داده)

- **UpdateDependentDivorceDataImportCommand**
  - توضیح: ارتباط داده ای - طلاق تکفل

  - نوع عملیات: Command (تغییر داده)

- **UpdateDependentDivorceRequestCommand**
  - توضیح: UpdateDependentDivorceRequestCommand
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentSisterDivorceCommand**
  - توضیح: کد مرکز طلبه ی خواهر
    
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentSisterDivorceDataImportCommand**
  - توضیح: ارتباط داده ای - ثبت طلاق سرپرست طلاب خواهر

  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentSisterDivorceRequestCommand**
  - توضیح: کد مرکز طلبه ی خواهر
    
  - نوع عملیات: Command (تغییر داده)

- **UpdateWifeDivorceCommand**
  - توضیح: UpdateWifeDivorceCommand
  - نوع عملیات: Command (تغییر داده)

- **UpdateWifeDivorceRequestCommand**
  - توضیح: کد مرکز خدمات
    
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetDependentSpousesDivorceQuery**
  - توضیح: نمایش لیست همسران هر مرد

  - نوع عملیات: Query (خواندن داده)

---

### Family

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetFamilyByNationalCodeQuery**
  - توضیح: اطلاعات افراد و تکفل براساس شماره ملی

  - نوع عملیات: Query (خواندن داده)

- **GetFamilyByYektaCodeQuery**
  - توضیح: اطلاعات افراد و تکفل براساس کد یکتا

  - نوع عملیات: Query (خواندن داده)

---

### Marriages

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreatePersonMarriageCommand**
  - توضیح: ایجاد موجودیت ازدواج جدید

  - نوع عملیات: Command (تغییر داده)

- **DeletePersonMarriageCommand**
  - توضیح: حذف موجودیت ازدواج با شناسه

  - نوع عملیات: Command (تغییر داده)

- **MarriageDataImportCommand**
  - توضیح: ارتباط داده ای - ثبت ازدواج سرپرست

  - نوع عملیات: Command (تغییر داده)

- **UpdateChildMarriageCommand**
  - توضیح: ثبت ازدواج تکفل

  - نوع عملیات: Command (تغییر داده)

- **UpdateChildMarriageRequestCommand**
  - توضیح: ثبت ازدواج تکفل

  - نوع عملیات: Command (تغییر داده)

- **UpdatePersonMarriageCommand**
  - توضیح: ویرایش موجودیت ازدواج

  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentSisterMarriageCommand**
  - توضیح: ثبت ازدواج طلبه خواهر
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentSisterMarriageRequestCommand**
  - توضیح: ثبت ازدواج طلبه خواهر - درخواست

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetMarriageByIdQuery**
  - توضیح: دریافت موجودیت ازدواج با شناسه

  - نوع عملیات: Query (خواندن داده)

- **SearchMarriagesQuery**
  - توضیح: جستجوی موجودیت ازدواج ها

  - نوع عملیات: Query (خواندن داده)

---

### NonStudentDependants

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateNonStudentDependantCommand**
  - توضیح: ایجاد موجودیت تکفل های غیرطلبه جدید

  - نوع عملیات: Command (تغییر داده)

- **DeleteNonStudentDependantCommand**
  - توضیح: حذف موجودیت تکفل های غیرطلبه با شناسه

  - نوع عملیات: Command (تغییر داده)

- **UpdateNonStudentDependantCommand**
  - توضیح: ویرایش موجودیت تکفل های غیرطلبه

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetNonStudentDependantByIdQuery**
  - توضیح: دریافت موجودیت تکفل های غیرطلبه با شناسه

  - نوع عملیات: Query (خواندن داده)

- **SearchNonStudentDependantsQuery**
  - توضیح: جستجوی موجودیت تکفل های غیرطلبه ها

  - نوع عملیات: Query (خواندن داده)

---

### Pregnancies

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreatePregnancyCommand**
  - توضیح: class
  - نوع عملیات: Command (تغییر داده)

- **CreatePregnancyRequestCommand**
  - توضیح: CreatePregnancyRequestCommand
  - نوع عملیات: Command (تغییر داده)

- **DeletePregnancyCommand**
  - توضیح: DeletePregnancyCommand
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetPregnancyByCodmQuery**
  - توضیح: GetPregnancyByCodmQuery
  - نوع عملیات: Query (خواندن داده)

---

## مدیریت کاربران و احراز هویت (User & Authentication)

### Auth

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **LoginCommand**
  - توضیح: ورود به سامانه

  - نوع عملیات: Command (تغییر داده)

- **LoginStudentCommand**
  - توضیح: ورود طلبه به سامانه

  - نوع عملیات: Command (تغییر داده)

- **RefreshTokenCommand**
  - توضیح: رفرش توکن

  - نوع عملیات: Command (تغییر داده)

---

### People

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **AssignNonIranianParentChildRelationCommand**
  - توضیح: انتساب نسبت خانوادگی به شخص غیر ایران?

  - نوع عملیات: Command (تغییر داده)

- **AssignNonIranianSpousalRelationCommand**
  - توضیح: انتساب همسر برای افراد غیر ایرانی

  - نوع عملیات: Command (تغییر داده)

- **AssignRelationCommand**
  - توضیح: انتساب نسبت خانوادگی به شخص

  - نوع عملیات: Command (تغییر داده)

- **AssignSpousalRelationCommand**
  - توضیح: انتساب همسر

  - نوع عملیات: Command (تغییر داده)

- **CreateIranianPersonByInquiryCommand**
  - توضیح: (ثبت استعلامی فرد ایرانی (با وب سرویس

  - نوع عملیات: Command (تغییر داده)

- **CreateIranianPersonManuallyCommand**
  - توضیح: ثبت دستی فرد ایرانی

  - نوع عملیات: Command (تغییر داده)

- **CreateNonIranianPersonByInquiryCommand**
  - توضیح: (ثبت استعلامی فرد غیر ایرانی (با وب سرویس

  - نوع عملیات: Command (تغییر داده)

- **CreateNonIranianPersonManuallyCommand**
  - توضیح: ثبت دستی فرد غیر ایرانی

  - نوع عملیات: Command (تغییر داده)

- **CreatePersonCommand**
  - توضیح: ایجاد موجودیت شخص جدید

  - نوع عملیات: Command (تغییر داده)

- **DeletePersonCommand**
  - توضیح: حذف موجودیت شخص با شناسه

  - نوع عملیات: Command (تغییر داده)

- **UpdatePersonCommand**
  - توضیح: ویرایش موجودیت شخص

  - نوع عملیات: Command (تغییر داده)

- **UpdatePersonImageCommand**
  - توضیح: بروزرسان? تصو?ر شخص

  - نوع عملیات: Command (تغییر داده)

- **ValidateIranianPersonIdentityCommand**
  - توضیح: Command to validate an Iranian person's identity.

  - نوع عملیات: Command (تغییر داده)

- **ValidateNonIranianPersonIdentityCommand**
  - توضیح: Command to validate a non-Iranian person's identity.

  - نوع عملیات: Command (تغییر داده)

- **ValidatePersonMobileCommand**
  - توضیح: اعتبارسنجی شماره همراه شخص

  - نوع عملیات: Command (تغییر داده)

- **ValidateShebaOwnershipCommand**
  - توضیح: اعتبار سنجی مالکیت شبا

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetPersonByIdQuery**
  - توضیح: دریافت موجودیت شخص با شناسه

  - نوع عملیات: Query (خواندن داده)

- **GetPersonByNationalCodeQuery**
  - توضیح: دریافت موجودیت شخص با کد ملی

  - نوع عملیات: Query (خواندن داده)

- **GetPersonByYektaCodeQuery**
  - توضیح: دریافت موجودیت شخص با کد یکتا

  - نوع عملیات: Query (خواندن داده)

- **SearchPeopleQuery**
  - توضیح: جستجوی موجودیت شخص ها

  - نوع عملیات: Query (خواندن داده)

---

## مدیریت پرونده (Case Management)

### AdmissionAuditLogs

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetDependentAdmissionAuditLogsByCodmQuery**
  - توضیح: سوابق پذیرشی تکفل
  - نوع عملیات: Query (خواندن داده)

- **GetStudentAdmissionAuditLogsByCodmQuery**
  - توضیح: سوابق پذیرشی طلبه
  - نوع عملیات: Query (خواندن داده)

---

### BlockServices

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateDependentBlockServiceCommand**
  - توضیح: ثبت
  - نوع عملیات: Command (تغییر داده)

- **CreateStudentBlockServiceCommand**
  - توضیح: ثبت
  - نوع عملیات: Command (تغییر داده)

- **DeleteDependentBlockServiceCommand**
  - توضیح: حذف
  - نوع عملیات: Command (تغییر داده)

- **DeleteStudentBlockServiceCommand**
  - توضیح: حذف
  - نوع عملیات: Command (تغییر داده)

- **UpdateDependentBlockServiceCommand**
  - توضیح: ویرایش
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentBlockServiceCommand**
  - توضیح: ویرایش
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **DependentBlockedServicesQuery**
  - توضیح: دریافت لیست خدمات مسدود تکفل ها براساس کد مرکز خدمات
  - نوع عملیات: Query (خواندن داده)

- **GetCsisServicesByCodmQuery**
  - توضیح: دریافت لیست خدمات طلبه
  - نوع عملیات: Query (خواندن داده)

- **GetCsisServicesByDependentQuery**
  - توضیح: دریافت لیست خدمات تکفل
  - نوع عملیات: Query (خواندن داده)

- **StudentBlockedServicesByCodmQuery**
  - توضیح: لیست
  - نوع عملیات: Query (خواندن داده)

---

### CaseBlock

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateStudentCaseBlockCommand**
  - توضیح: CreateStudentCaseBlockCommand

  - نوع عملیات: Command (تغییر داده)

- **CreateStudentCaseBlockRequestCommand**
  - توضیح: CreateStudentCaseBlockRequest

  - نوع عملیات: Command (تغییر داده)

- **CreateStudentCaseUnblockCommand**
  - توضیح: CreateStudentCaseUnblockCommand

  - نوع عملیات: Command (تغییر داده)

- **CreateStudentCaseUnblockRequestCommand**
  - توضیح: CreateStudentCaseUnblockRequestCommand

  - نوع عملیات: Command (تغییر داده)

---

### CaseFilings

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateAdmissionCaseStep01InitiateCommand**
  - توضیح: ساخت توکن گام اول
  - نوع عملیات: Command (تغییر داده)

- **CreateAdmissionCaseStep02MobileCommand**
  - توضیح: تایید موبایل گام دوم
  - نوع عملیات: Command (تغییر داده)

- **CreateAdmissionCaseStep03ValidateForRegistrationCommand**
  - توضیح: ساخت توکن گام سوم

  - نوع عملیات: Command (تغییر داده)

- **CreateAdmissionCaseStep04ConfirmIdentityInformationCommand**
  - توضیح: (تایید اطلاعات هویتی گام سوم(تاییدیه

  - نوع عملیات: Command (تغییر داده)

- **CreateAdmissionCaseStep04ValidateIdentityCommand**
  - توضیح: احراز هویت

  - نوع عملیات: Command (تغییر داده)

- **CreateAdmissionCaseStep05ConfirmAddressByPostalCodeCommand**
  - توضیح: تایید آدرس بر اساس کدپستی

  - نوع عملیات: Command (تغییر داده)

- **CreateAdmissionCaseStep06ConfirmStudentProfilePictureRequestCommand**
  - توضیح: ConfirmStudentProfilePictureRequestCommand
  - نوع عملیات: Command (تغییر داده)

- **CreateAdmissionCaseStep07ConfirmBankAccountInformationCommand**
  - توضیح: تایید اطلاعات بانکی

  - نوع عملیات: Command (تغییر داده)

- **CreateAdmissionCaseStep08ConfirmEmploymentCommand**
  - توضیح: درخواست تأیید اشتغال
  - نوع عملیات: Command (تغییر داده)

- **CreateAdmissionCaseStep09CompleteInformationCaseFilingCommand**
  - توضیح: گام چهارم تشکیل پرونده

  - نوع عملیات: Command (تغییر داده)

- **CreateAdmissionCaseStep10CreateUserCommand**
  - توضیح: مرحله آخر ایجاد کاربر 

  - نوع عملیات: Command (تغییر داده)

- **DeleteAllRequestTestCommand**
  - توضیح: DeleteAllRequestTestCommand
  - نوع عملیات: Command (تغییر داده)

- **GenerateCaptchaCommand**
  - توضیح: تولید و دریافت کد کپچا

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **ConfirmStudentCaseStatusQuery**
  - توضیح: تأیید مرحله وضعیت پرونده دانشجویی
  - نوع عملیات: Query (خواندن داده)

- **CreateAdmissionCaseStep05GetAddressByPostalCodeQuery**
  - توضیح: CreateAdmissionCaseStep05GetAddressByPostalCodeQuery
  - نوع عملیات: Query (خواندن داده)

- **GetStudentCaseQuery**
  - توضیح: درخواست تأیید اشتغال
  - نوع عملیات: Query (خواندن داده)

- **GetStudentCaseStatusQuery**
  - توضیح: وضعیت پرونده دانشجویی
  - نوع عملیات: Query (خواندن داده)

---

## خدمات رفاهی (Welfare Services)

### BankAccounts

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **UpdateDependentBankAccountCommand**
  - توضیح: بروز رسانی حساب بانکی
  - نوع عملیات: Command (تغییر داده)

- **UpdateDependentBankAccountRequestCommand**
  - توضیح: درخواست بروز رسانی حساب بانکی
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentBankAccountCommand**
  - توضیح: بروز رسانی حساب بانکی
  - نوع عملیات: Command (تغییر داده)

- **UpdateStudentBankAccountRequestCommand**
  - توضیح: درخواست بروز رسانی حساب بانکی
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetFamilyBankAccountsByCodmQuery**
  - توضیح: GetFamilyBankAccountsByCodmQuery
  - نوع عملیات: Query (خواندن داده)

---

### Houses

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateOrUpdateHouseCommand**
  - توضیح: ایجاد یا ویرایش مسکن طلبه

  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateHouseEmployeeRequestCommand**
  - توضیح: ایجاد درخواست مسکن طلبه

  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateHouseRequestCommand**
  - توضیح: ایجاد درخواست مسکن طلبه

  - نوع عملیات: Command (تغییر داده)

- **DeleteHouseCommand**
  - توضیح: DeleteHouseCommand
  - نوع عملیات: Command (تغییر داده)

- **DeleteHouseRequestCommand**
  - توضیح: DeleteHouseRequestCommand
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetHouseByCodmQuery**
  - توضیح: GetHouseByCodmQuery

  - نوع عملیات: Query (خواندن داده)

---

### HousingAdmissionInfo

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **GetBulkCodmByNationalCodeCommand**
  - توضیح: دریافت اطلاعات کدمرکز طلاب بر اساس لیست کد ملی
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetActiveTenantsQuery**
  - توضیح: دریافت اجاره نامه های فعال

  - نوع عملیات: Query (خواندن داده)

- **GetHousingActivitiesInfoByCodmQuery**
  - توضیح: دریافت اطلاعات فعالیت‌های علمی و فرهنگی برای سامانه مسکن

  - نوع عملیات: Query (خواندن داده)

- **GetHousingBasicInfoByCodmQuery**
  - توضیح: دریافت اطلاعات پایه طلبه برای سامانه مسکن

  - نوع عملیات: Query (خواندن داده)

- **GetHousingStatusInfoByCodmQuery**
  - توضیح: دریافت اطلاعات مسکن و اعتراضات برای سامانه مسکن

  - نوع عملیات: Query (خواندن داده)

---

### Insurances

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetStudentDependentInsurancesByCodmQuery**
  - توضیح: GetStudentDependentInsurancesByCodmQuery
  - نوع عملیات: Query (خواندن داده)

---

## اطلاعات تحصیلی (Educational Information)

### EducationYears

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetEducationYearsQuery**
  - توضیح: دریافت لیست سال های تحصیلی
  - نوع عملیات: Query (خواندن داده)

---

### Educations

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **UpdateEducationCommand**
  - توضیح: کد مرکز
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetEducationByCodmQuery**
  - توضیح: GetEducationByCodmQuery
  - نوع عملیات: Query (خواندن داده)

---

### ExcellentEducationLevels

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetExcellentEducationLevelsQuery**
  - توضیح: دریافت لیست سطوح تحصیلی
  - نوع عملیات: Query (خواندن داده)

---

### ExcellentEducationYears

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetExcellentEducationYearsQuery**
  - توضیح: دریافت لیست سال تحصیلی ممتازین
  - نوع عملیات: Query (خواندن داده)

---

### Excellents

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **DeleteExcellentCommand**
  - توضیح: DeleteExcellentCommand
  - نوع عملیات: Command (تغییر داده)

- **DeleteExcellentRequestCommand**
  - توضیح: DeleteExcellentRequestCommand
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetExcellentsByCodmQuery**
  - توضیح: GetExcellentByCodmQuery

  - نوع عملیات: Query (خواندن داده)

---

### Schools

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetSchoolsQuery**
  - توضیح: دریافت لیست مدارس
  - نوع عملیات: Query (خواندن داده)

---

### UniversityEducations

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateBatchDependentUniversityEducationCommand**
  - توضیح: تحصیلات دانشگاهی
  - نوع عملیات: Command (تغییر داده)

- **CreateBatchStudentUniversityEducationCommand**
  - توضیح: تحصیلات دانشگاهی
  - نوع عملیات: Command (تغییر داده)

- **CreateDependentUniversityEducationCommand**
  - توضیح: ثبت تحصیلات دانشگاهی تکفل
  - نوع عملیات: Command (تغییر داده)

- **CreateDependentUniversityEducationIranianRequestCommand**
  - توضیح: مدل اکشن
  - نوع عملیات: Command (تغییر داده)

- **CreateDependentUniversityEducationNonIranianRequestCommand**
  - توضیح: تحصیلات دانشگاهی
  - نوع عملیات: Command (تغییر داده)

- **CreateStudentUniversityEducationCommand**
  - توضیح: کد مرکز خدمات
  - نوع عملیات: Command (تغییر داده)

- **CreateStudentUniversityEducationIranianRequestCommand**
  - توضیح: CreateStudentUniversityEducationIranianRequestCommand
  - نوع عملیات: Command (تغییر داده)

- **CreateStudentUniversityEducationNonIranianRequestCommand**
  - توضیح: تحصیلات دانشگاهی
  - نوع عملیات: Command (تغییر داده)

- **CreateUniversityEducationCommand**
  - توضیح: ثبت تحصیلات دانشگاهی
  - نوع عملیات: Command (تغییر داده)

- **DeleteDependentUniversityEducationCommand**
  - توضیح: حذف تحصیلات دانشگاهی تکفل

  - نوع عملیات: Command (تغییر داده)

- **DeleteDependentUniversityEducationRequestCommand**
  - توضیح: درخواست حذف تحصیلات دانشگاهی تکفل

  - نوع عملیات: Command (تغییر داده)

- **DeleteStudentUniversityEducationCommand**
  - توضیح: حذف تحصیلات دانشگاهی

  - نوع عملیات: Command (تغییر داده)

- **DeleteStudentUniversityEducationRequestCommand**
  - توضیح: درخواست حذف تحصیلات دانشگاهی

  - نوع عملیات: Command (تغییر داده)

- **UniversityEducationDataImportCommand**
  - توضیح: ارتباطات داده ای - تحصیلات دانشگاهی

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetDependentUniversityEducationsByCodmQuery**
  - توضیح: GetDependentUniversityEducationsByCodmQuery
  - نوع عملیات: Query (خواندن داده)

- **GetStudentUniversityEducationsByCodmQuery**
  - توضیح: GetStudentUniversityEducationsByCodmQuery
  - نوع عملیات: Query (خواندن داده)

---

## فعالیت‌های فرهنگی (Cultural Activities)

### CulturalActivities

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateCulturalActivityCommand**
  - توضیح: CreateCulturalActivityCommand

  - نوع عملیات: Command (تغییر داده)

- **DeleteCulturalActivityCommand**
  - توضیح: DeleteCulturalActivityCommand

  - نوع عملیات: Command (تغییر داده)

- **DeleteCulturalActivityRequestCommand**
  - توضیح: DeleteCulturalActivityCommand

  - نوع عملیات: Command (تغییر داده)

- **UpdateCulturalActivityCommand**
  - توضیح: UpdateCulturalActivityCommand

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetCulturalActivitiesByCodmQuery**
  - توضیح: GetCulturalActivitiesByCodmQuery
  - نوع عملیات: Query (خواندن داده)

- **GetCulturalActivityByIdQuery**
  - توضیح: GetCulturalActivityByIdQuery

  - نوع عملیات: Query (خواندن داده)

---

### CulturalActivityGrades

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **DeleteCulturalActivityGradesByCodmRequestCommand**
  - توضیح: DeleteCulturalActivityGradesByCodmRequestCommand

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **DeleteCulturalActivityGradesByCodmQuery**
  - توضیح: DeleteCulturalActivityGradesByCodmCommand

  - نوع عملیات: Query (خواندن داده)

- **GetCulturalActivityGradesByCodmQuery**
  - توضیح: GetCulturalActivityGradesByCodmQuery

  - نوع عملیات: Query (خواندن داده)

---

### EliteLevels

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetEliteTypesQuery**
  - توضیح: دریافت لیست انواع نخبگانی
  - نوع عملیات: Query (خواندن داده)

---

### EliteTypes

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetEliteTypesQuery**
  - توضیح: دریافت لیست انواع نخبگانی
  - نوع عملیات: Query (خواندن داده)

---

### Elites

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateEliteCommand**
  - توضیح: ثبت نخبگان

  - نوع عملیات: Command (تغییر داده)

- **CreateEliteRequestCommand**
  - توضیح: ثبت نخبگان (درخواست)

  - نوع عملیات: Command (تغییر داده)

- **DeleteEliteCommand**
  - توضیح: DeleteEliteCommand
  - نوع عملیات: Command (تغییر داده)

- **DeleteEliteRequestCommand**
  - توضیح: DeleteEliteRequestCommand
  - نوع عملیات: Command (تغییر داده)

- **UpdateEliteCommand**
  - توضیح: UpdateEliteCommand
  - نوع عملیات: Command (تغییر داده)

- **UpdateEliteRequestCommand**
  - توضیح: بروزرسان? نخبگان (درخواست)

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetElitesByCodmQuery**
  - توضیح: GetElitesByCodmQuery
  - نوع عملیات: Query (خواندن داده)

- **GetElitesByIdQuery**
  - توضیح: GetElitesByIdQuery
  - نوع عملیات: Query (خواندن داده)

---

### Famouses

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateFamousCommand**
  - توضیح: ایجاد مشهور جدید

  - نوع عملیات: Command (تغییر داده)

- **CreateFamousRequestCommand**
  - توضیح: ایجاد مشهور جدید

  - نوع عملیات: Command (تغییر داده)

- **DeleteFamousCommand**
  - توضیح: حذف مشهور با شناسه

  - نوع عملیات: Command (تغییر داده)

- **DeleteFamousRequestCommand**
  - توضیح: حذف مشهور با شناسه

  - نوع عملیات: Command (تغییر داده)

- **UpdateFamousCommand**
  - توضیح: ویرایش مشهور

  - نوع عملیات: Command (تغییر داده)

- **UpdateFamousRequestCommand**
  - توضیح: ویرایش مشهور

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetFamousByCodmQuery**
  - توضیح: دریافت مشهور با شناسه

  - نوع عملیات: Query (خواندن داده)

- **GetFamousByIdQuery**
  - توضیح: دریافت مشهور با شناسه

  - نوع عملیات: Query (خواندن داده)

- **GetFamousesByCodmQuery**
  - توضیح: دریافت مشاهیر ثبت شده برای طلبه
  - نوع عملیات: Query (خواندن داده)

---

### ImamJamaat

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateMosqueWithDetailsCommand**
  - توضیح: دستور ایجاد مسجد با جزئیات

  - نوع عملیات: Command (تغییر داده)

- **CreateMosqueWithDetailsStudentCommand**
  - توضیح: دستور ایجاد مسجد با جزئیات
  - نوع عملیات: Command (تغییر داده)

- **DeleteMosqueCommand**
  - توضیح: DeleteMosqueCommand
  - نوع عملیات: Command (تغییر داده)

- **GetMosqueByAddressCommand**
  - توضیح: شهرستان
  - نوع عملیات: Command (تغییر داده)

- **UpdateMosqueWithDetailsCommand**
  - توضیح: به‌روزرسانی اطلاعات مسجد با جزئیات امام جماعت و فعالیت‌های مسجد  

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetAddressByPostalCodeQuery**
  - توضیح: دریافت آدرس بر اساس کد پستی

  - نوع عملیات: Query (خواندن داده)

- **GetMosqueByIdQuery**
  - توضیح: GetMosqueByIdQuery
  - نوع عملیات: Query (خواندن داده)

- **GetMosqueByIdStudentQuery**
  - توضیح: دریافت اطلاعات مسجد بر اساس شناسه آن برای دانش‌آموزان

  - نوع عملیات: Query (خواندن داده)

- **GetMosqueListQuery**
  - توضیح: GetMosqueListQuery
  - نوع عملیات: Query (خواندن داده)

- **GetMosqueListStudentQuery**
  - توضیح: GetMosqueListStudentQuery
  - نوع عملیات: Query (خواندن داده)

- **GetStudentPossibilityToCreateMosqueQuery**
  - توضیح: (امکان ثبت مسجد توسط طلبه (اطلاعات تلیغ

  - نوع عملیات: Query (خواندن داده)

- **ImamJamaatCanRegisterQuery**
  - توضیح: (استعلام امکان ثبت درخواست امام جماعت(اعتبارسنجی

  - نوع عملیات: Query (خواندن داده)

- **ImamJamaatCodMInquiryQuery**
  - توضیح: استعلام اطلاعات طلبه با کد مرکز

  - نوع عملیات: Query (خواندن داده)

---

### Memorizers

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **DeleteMemorizerCommand**
  - توضیح: DeleteMemorizerCommand
  - نوع عملیات: Command (تغییر داده)

- **DeleteMemorizerRequestCommand**
  - توضیح: DeleteMemorizerRequestCommand
  - نوع عملیات: Command (تغییر داده)

- **MemorizerDataImportCommand**
  - توضیح: ارتباط داده ای - حافظین

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetDependentMemorizerByCodmQuery**
  - توضیح: حافظین
  - نوع عملیات: Query (خواندن داده)

- **GetStudentMemorizerByCodmQuery**
  - توضیح: حافظین
  - نوع عملیات: Query (خواندن داده)

---

### PreachGrades

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreatePreachGradesCommand**
  - توضیح: CreatePreachGradeCommand

  - نوع عملیات: Command (تغییر داده)

- **DeletePreachGradeRequestCommand**
  - توضیح: DeletePreachGradeRequestCommand

  - نوع عملیات: Command (تغییر داده)

- **DeletePreachGradesCommand**
  - توضیح: DeletePreachGradeCommand

  - نوع عملیات: Command (تغییر داده)

- **UpdatePreachGradesCommand**
  - توضیح: UpdatePreachGradeCommand

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetPreachGradesByCodmQuery**
  - توضیح: GetPreachGradesByCodmQuery

  - نوع عملیات: Query (خواندن داده)

- **GetPreachGradesByIdQuery**
  - توضیح: GetPreachGradeByIdQuery

  - نوع عملیات: Query (خواندن داده)

---

### Preaches

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreatePreachCommand**
  - توضیح: ثبت تبلیغ
  - نوع عملیات: Command (تغییر داده)

- **CreatePreachRequestCommand**
  - توضیح: ثبت تبلیغ
  - نوع عملیات: Command (تغییر داده)

- **DataImportPreachCommand**
  - توضیح: ثبت تبلیغ
  - نوع عملیات: Command (تغییر داده)

- **DeletePreachCommand**
  - توضیح: DeletePreachCommand

  - نوع عملیات: Command (تغییر داده)

- **DeletePreachRequestCommand**
  - توضیح: DeletePreachRequestCommand

  - نوع عملیات: Command (تغییر داده)

- **UpdatePreachCommand**
  - توضیح: UpdatePreachCommand

  - نوع عملیات: Command (تغییر داده)

- **UpdatePreachRequestCommand**
  - توضیح: UpdatePreachCommand

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetPreachByIdQuery**
  - توضیح: GetPreachByIdQuery

  - نوع عملیات: Query (خواندن داده)

- **GetPreachesByCodmQuery**
  - توضیح: دریافت لیست تبلیغ با کد مرکز خدمات
  - نوع عملیات: Query (خواندن داده)

---

### ReligiousRoleQuestions

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateOrUpdateReligiousRoleQuestionCommand**
  - توضیح: پرسشنامه نقش آفرینی

  - نوع عملیات: Command (تغییر داده)

- **CreateRequestReligiousRoleQuestionCommand**
  - توضیح: ایجاد درخواست پرسشنامه نقش آفرینی

  - نوع عملیات: Command (تغییر داده)

---

### ResearchGrades

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateResearchGradeCommand**
  - توضیح: CreateResearchGradeCommand

  - نوع عملیات: Command (تغییر داده)

- **DeleteResearchGradeCommand**
  - توضیح: DeleteResearchGradeCommand

  - نوع عملیات: Command (تغییر داده)

- **DeleteResearchGradeRequestCommand**
  - توضیح: DeleteResearchGradeRequestCommand

  - نوع عملیات: Command (تغییر داده)

- **UpdateResearchGradeCommand**
  - توضیح: UpdateResearchGradeCommand

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetResearchGradeByIdQuery**
  - توضیح: GetResearchGradeByIdQuery

  - نوع عملیات: Query (خواندن داده)

- **GetResearchGradesByCodmQuery**
  - توضیح: GetResearchGradesByCodmQuery

  - نوع عملیات: Query (خواندن داده)

---

### Researches

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateResearchCommand**
  - توضیح: ایجاد پژوهش

  - نوع عملیات: Command (تغییر داده)

- **CreateResearchRequestCommand**
  - توضیح: ایجاد پژوهش

  - نوع عملیات: Command (تغییر داده)

- **DeleteResearchCommand**
  - توضیح: حذف پژوهش با شناسه

  - نوع عملیات: Command (تغییر داده)

- **DeleteResearchRequestCommand**
  - توضیح: حذف پژوهش با شناسه

  - نوع عملیات: Command (تغییر داده)

- **UpdateResearchCommand**
  - توضیح: ویرایش پژوهش

  - نوع عملیات: Command (تغییر داده)

- **UpdateResearchRequestCommand**
  - توضیح: ویرایش پژوهش

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetResearchByIdQuery**
  - توضیح: دریافت پژوهش با شناسه

  - نوع عملیات: Query (خواندن داده)

- **GetResearchesByCodmQuery**
  - توضیح: GetResearchesByCodmQuery

  - نوع عملیات: Query (خواندن داده)

---

### TargetedScores

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetSubsistenceTargetedScoresInfoByCodmQuery**
  - توضیح: امتیاز هدفمندی معیشتی
  - نوع عملیات: Query (خواندن داده)

- **GetTargetedScoresInfoByCodmQuery**
  - توضیح: هدفمندی

  - نوع عملیات: Query (خواندن داده)

- **GetTargetingHistoryChangesByCodmQuery**
  - توضیح: دریافت لیست تاریخچه امتیاز هدف مندی
  - نوع عملیات: Query (خواندن داده)

---

### TeachGrades

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateTeachGradesCommand**
  - توضیح: CreateTeachGradeCommand

  - نوع عملیات: Command (تغییر داده)

- **DeleteTeachGradeRequestCommand**
  - توضیح: DeleteTeachGradeRequestCommand

  - نوع عملیات: Command (تغییر داده)

- **DeleteTeachGradesCommand**
  - توضیح: DeleteTeachGradeCommand

  - نوع عملیات: Command (تغییر داده)

- **UpdateTeachGradesCommand**
  - توضیح: UpdateTeachGradeCommand

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetTeachGradesByIdQuery**
  - توضیح: GetTeachGradeByIdQuery

  - نوع عملیات: Query (خواندن داده)

- **GetTeachGradessByCodmQuery**
  - توضیح: GetTeachGradesByCodmQuery

  - نوع عملیات: Query (خواندن داده)

---

### Teaches

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateTeachCommand**
  - توضیح: CreateTeachCommand

  - نوع عملیات: Command (تغییر داده)

- **CreateTeachRequestCommand**
  - توضیح: ایجاد سابقه تدریس برای طلبه

  - نوع عملیات: Command (تغییر داده)

- **DeleteTeachCommand**
  - توضیح: حذف سابقه تدریس برای طلبه

  - نوع عملیات: Command (تغییر داده)

- **DeleteTeachRequestCommand**
  - توضیح: حذف سابقه تدریس برای طلبه

  - نوع عملیات: Command (تغییر داده)

- **TeachDataImportCommand**
  - توضیح: ارتباط داده ای  - تدریس

  - نوع عملیات: Command (تغییر داده)

- **UpdateTeachCommand**
  - توضیح: بروزرسانی تدریس

  - نوع عملیات: Command (تغییر داده)

- **UpdateTeachRequestCommand**
  - توضیح: بروزرسانی تدریس

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetTeachByIdQuery**
  - توضیح: GetTeachByIdQuery

  - نوع عملیات: Query (خواندن داده)

- **GetTeachesByCodmQuery**
  - توضیح: GetTeachesByCodmQuery

  - نوع عملیات: Query (خواندن داده)

---

### Veterans

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateORUpdateVeteranCaptivityDaysCommand**
  - توضیح: ویرایش روز آزادگی ایثارگری

  - نوع عملیات: Command (تغییر داده)

- **CreateORUpdateVeteranPercentCommand**
  - توضیح: ویرایش درصد جانبازی در ایثارگری
  - نوع عملیات: Command (تغییر داده)

- **CreateORUpdateVeteranRelationWithMartyrCommand**
  - توضیح: ویرایش نسبت با شهید ایثارگری

  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateVeteranCommand**
  - توضیح: ثبت یا بروزرسانی اطلاعات ایثارگری

  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateVeteranRequestCommand**
  - توضیح: ثبت یا بروزرسانی اطلاعات ایثارگری (درخواست)

  - نوع عملیات: Command (تغییر داده)

- **DeleteVeteranCommand**
  - توضیح: حذف اطلاعات ایثارگری

  - نوع عملیات: Command (تغییر داده)

- **DeleteVeteranRequestCommand**
  - توضیح: DeleteVeteranRequestCommand
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetVeteranByCodmQuery**
  - توضیح: GetVeteranByCodmQuery

  - نوع عملیات: Query (خواندن داده)

---

## گزارش‌گیری (Reporting)

### ReportBuilders

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateReportBuilderCommand**
  - توضیح: ثبت کزارش گزارش ساز
  - نوع عملیات: Command (تغییر داده)

- **UpdatePreachGradesCommand**
  - توضیح: بروز رسانی
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetReportBuilderByIdQuery**
  - توضیح: دریافت گزارش ساز
  - نوع عملیات: Query (خواندن داده)

- **GetReportBuildersQuery**
  - توضیح: جستجو گزارش ساز
  - نوع عملیات: Query (خواندن داده)

- **ReportBuilderQuery**
  - توضیح: گزارش ساز
  - نوع عملیات: Query (خواندن داده)

- **ReportBuilderTablesQuery**
  - توضیح: جداول گزارش ساز
  - نوع عملیات: Query (خواندن داده)

- **ReportBuilderToExcelQuery**
  - توضیح: گزارش ساز
  - نوع عملیات: Query (خواندن داده)

---

### ReportProfiles

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateReportProfileCommand**
  - توضیح: ایجاد پروفایل گزارش

  - نوع عملیات: Command (تغییر داده)

- **DeleteReportProfileCommand**
  - توضیح: حذف پروفایل گزارش

  - نوع عملیات: Command (تغییر داده)

- **UpdateReportProfileCommand**
  - توضیح: ویرایش پروفایل گزارش

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetProfileStructureByIdQuery**
  - توضیح: فراخوانی ساختار پروفایل گزارش

  - نوع عملیات: Query (خواندن داده)

- **GetReportProfilesListQuery**
  - توضیح: دریافت لیست پروفایل های گزارش ذخیره شده

  - نوع عملیات: Query (خواندن داده)

---

## اطلاعات پایه (Basic Data)

### Addresses

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **ConfirmStudentAddressCommand**
  - توضیح: ConfirmStudentAddressCommand
  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateStudentAddressCommand**
  - توضیح: ثبت/ویرایش آدرس
  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateStudentAddressEmployeeCommand**
  - توضیح: ثبت/ویرایش آدرس
  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateStudentAddressEmployeeRequestCommand**
  - توضیح: ثبت درخواست بروز رسانی آدرس
  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateStudentAddressRequestCommand**
  - توضیح: ثبت درخواست بروز رسانی آدرس
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetAddressByIdQuery**
  - توضیح: GetAddressByIdQuery
  - نوع عملیات: Query (خواندن داده)

- **GetAddressesByCodmQuery**
  - توضیح: GetAddressesByCodmQuery
  - نوع عملیات: Query (خواندن داده)

---

### Branches

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetAgenciesByBranchIdQuery**
  - توضیح: دریافت لیست استان ها
  - نوع عملیات: Query (خواندن داده)

- **GetBranchesQuery**
  - توضیح: دریافت لیست شعب
  - نوع عملیات: Query (خواندن داده)

- **GetProvinceByBranchIdQuery**
  - توضیح: دریافت استان مربوط به شعبه
  - نوع عملیات: Query (خواندن داده)

- **ValidateBranchesByProviceIdQuery**
  - توضیح: دریافت لیست شعب
  - نوع عملیات: Query (خواندن داده)

---

### Cities

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetCitiesQuery**
  - توضیح: دریافت لیست شهرها
  - نوع عملیات: Query (خواندن داده)

---

### CommissionInfos

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetDependentCommissionsInfoByCodmQuery**
  - توضیح: کمسیون تکفل
  - نوع عملیات: Query (خواندن داده)

- **GetStudentCommissionsInfoByCodmQuery**
  - توضیح: کمسیون طلبه
  - نوع عملیات: Query (خواندن داده)

---

### ContinuousInformationTabs

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetContinuousInformationTabQuery**
  - توضیح: نمایش محتوای تب مشخصات مستمری
  - نوع عملیات: Query (خواندن داده)

---

### Countries

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetCountriesQuery**
  - توضیح: دریافت لیست کشورها
  - نوع عملیات: Query (خواندن داده)

---

### CountryDivisions

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateSetPortionCountryDivisionsCommand**
  - توضیح: ایجاد بخش در تقسیمات کشوری

  - نوع عملیات: Command (تغییر داده)

- **CreateSetRuralCountryDivisionsCommand**
  - توضیح: ایجاد دهستان در تقسیمات کشوری

  - نوع عملیات: Command (تغییر داده)

- **CreateSetTownInCountryDivisionsCommand**
  - توضیح: تعریف شهر در تقسیمات کشوری

  - نوع عملیات: Command (تغییر داده)

---

### Documents

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateDocumentCommand**
  - توضیح: CreateDocumentCommand
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **SearchDocumentsQuery**
  - توضیح: SearchDocumentsQuery
  - نوع عملیات: Query (خواندن داده)

---

### Files

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateCaseFillingFileUploadCommand**
  - توضیح: بارگذاری فایل ثبت نام طلبه

  - نوع عملیات: Command (تغییر داده)

- **UploadFileCommand**
  - توضیح: آپلود فایل

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetFileQuery**
  - توضیح: GetFileQuery
  - نوع عملیات: Query (خواندن داده)

---

### Portions

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetPortionsQuery**
  - توضیح: لیست
  - نوع عملیات: Query (خواندن داده)

---

### Provinces

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetProvincesQuery**
  - توضیح: دریافت لیست استان ها
  - نوع عملیات: Query (خواندن داده)

---

### Rurals

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetRuralsQuery**
  - توضیح: دریافت لیست روستاها بر اساس استان

  - نوع عملیات: Query (خواندن داده)

---

### Settings

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateOrUpdateSettingsCommand**
  - توضیح: تغییر نوع ثبت شماره
  - نوع عملیات: Command (تغییر داده)

- **SaveNotificationSettingsCommand**
  - توضیح: ذخیره تنظیمات نوتیفیکیشن

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetNotificationSettingsQuery**
  - توضیح: دریافت تنظیمات نوتیفیکیشن

  - نوع عملیات: Query (خواندن داده)

- **GetSettingsQuery**
  - توضیح: دریافت نوع ثبت استعلام

  - نوع عملیات: Query (خواندن داده)

---

### Towns

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetTownsQuery**
  - توضیح: دریافت لیست شهرها بر اساس استان

  - نوع عملیات: Query (خواندن داده)

---

## اعلان‌ها و درخواست‌ها (Notifications & Requests)

### Employments

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **ConfirmDependentEmploymentCommand**
  - توضیح: تایید وضعیت اشتغال
  - نوع عملیات: Command (تغییر داده)

- **ConfirmStudentEmploymentCommand**
  - توضیح: تایید وضعیت اشتغال
  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateDependentEmploymentCommand**
  - توضیح: CreateOrUpdateDependentEmploymentCommand
  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateDependentEmploymentRequestCommand**
  - توضیح: CreateOrUpdateDependentEmploymentRequestCommand
  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateStudentEmploymentCommand**
  - توضیح: بروز رسانی وضعیت اشتغال
  - نوع عملیات: Command (تغییر داده)

- **CreateOrUpdateStudentEmploymentRequestCommand**
  - توضیح: ثبت درخواست اشتغال طلبه
  - نوع عملیات: Command (تغییر داده)

- **DeleteDependentEmploymentCommand**
  - توضیح: DeleteDependentEmploymentCommand
  - نوع عملیات: Command (تغییر داده)

- **DeleteDependentEmploymentRequestCommand**
  - توضیح: DeleteDependentEmploymentRequestCommand
  - نوع عملیات: Command (تغییر داده)

- **DeleteStudentEmploymentCommand**
  - توضیح: DeleteStudentEmploymentCommand
  - نوع عملیات: Command (تغییر داده)

- **DeleteStudentEmploymentRequestCommand**
  - توضیح: DeleteStudentEmploymentRequestCommand
  - نوع عملیات: Command (تغییر داده)

- **EmployeeDataImportCommand**
  - توضیح: ارتباطات داده ای - اشتغال

  - نوع عملیات: Command (تغییر داده)

- **IdentifyStudentEmploymentCommand**
  - توضیح: شناسایی موردی اشتغال
  - نوع عملیات: Command (تغییر داده)

- **IdentifyStudentEmploymentRequestCommand**
  - توضیح: شناسایی موردی اشتغال
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetDecileByCodmQuery**
  - توضیح: دریافت دهک بر اساس لیست کدمرکز

  - نوع عملیات: Query (خواندن داده)

- **GetIdentifyStudentEmploymentQuery**
  - توضیح: شناسایی موردی اشتغال
  - نوع عملیات: Query (خواندن داده)

- **GetStudentEmploymentByCodmQuery**
  - توضیح: دریافت اشتغال طلبه
  - نوع عملیات: Query (خواندن داده)

---

### Notifications

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **SendNotificationCommand**
  - توضیح: ارسال پیام
  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **SearchNotificationsQuery**
  - توضیح: لیست
  - نوع عملیات: Query (خواندن داده)

---

### Protests

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateProtestByCodmCommand**
  - توضیح: ثبت اعتراض بر اساس کد مرکز

  - نوع عملیات: Command (تغییر داده)

- **CreateProtestByCodmRequestCommand**
  - توضیح: ثبت اعتراض بر اساس کد مرکز

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetProtestByCodmQuery**
  - توضیح: دریافت لیست اعتراضات طلبه
  - نوع عملیات: Query (خواندن داده)

---

## سایر (Others)

### NonStudents

#### Commands (دستورات)

دستورات این بخش عملیات تغییر دهنده (Write Operations) را مدیریت می‌کنند:

- **CreateNonStudentCommand**
  - توضیح: ایجاد موجودیت غیر طلبه جدید

  - نوع عملیات: Command (تغییر داده)

- **DeleteNonStudentCommand**
  - توضیح: حذف موجودیت غیر طلبه با شناسه

  - نوع عملیات: Command (تغییر داده)

- **UpdateNonStudentCommand**
  - توضیح: ویرایش موجودیت غیر طلبه

  - نوع عملیات: Command (تغییر داده)

#### Queries (پرس‌وجوها)

پرس‌وجوهای این بخش عملیات خواندن داده (Read Operations) را مدیریت می‌کنند:

- **GetNonStudentByIdQuery**
  - توضیح: دریافت موجودیت غیر طلبه با شناسه

  - نوع عملیات: Query (خواندن داده)

- **SearchNonStudentsQuery**
  - توضیح: جستجوی موجودیت غیر طلبه ها

  - نوع عملیات: Query (خواندن داده)

---

## سرویس‌های سیستم

این بخش شامل سرویس‌های مختلف سیستم است که وظایف خاصی را بر عهده دارند:

### BirthCertService

**توضیحات:** سرویس اطلاعات کاربر جاری

**متدهای عمومی:**

- `Iranian()`
- `NonIranian()`

---

### CacheKeyService

**توضیحات:** Cache key provider service implementation


**متدهای عمومی:**

- `GetCustomWildcardKey()`
- `GetDtoKey()`
- `GetDtoListKey()`
- `GetDtoListWildcardKey()`
- `GetDtoWildcardKey()`
- `GetEntityKey()`
- `GetEntityKeyPrefix()`
- `GetEntityListKey()`
- `GetEntityWildcardKey()`

---

### CsisHealthInsuranceService

**توضیحات:** CsisHealthInsuranceService

**متدهای عمومی:**

- `CaseState()`

---

### CsisSupInsuranceService

**توضیحات:** CsisSupInsuranceService

**متدهای عمومی:**

- `GetHealthStatus()`
- `GetLifeStatus()`

---

### CsisWsmService

**توضیحات:** LoginRequest

**متدهای عمومی:**

- `LoginApiRequest()`
- `LoginRequest()`
- `ResponseExtra()`
- `Result()`

---

### CurrentUserService

**توضیحات:** سرویس اطلاعات کاربر جاری

**متدهای عمومی:**

- `GetCodmAsync()`
- `GetEmployeeBranchIdAsync()`
- `GetStudentBranchIdAsync()`
- `HasAccessToThisApplicationAsync()`
- `IsAuthorizedAsync()`
- `IsEmployee()`
- `IsEmployeeAsync()`
- `IsSenior()`
- `IsStudent()`
- `IsStudentAsync()`
- `SetCodm()`

---

### DateTimeService

**توضیحات:** Date time service implementation



---

### DistributedCacheService

**توضیحات:** DistributedCacheService

**متدهای عمومی:**

- `ContainsAsync()`
- `RemoveAllAsync()`
- `RemoveAsync()`
- `RemoveByPrefixAsync()`

---

### DistributedPubSubService

**توضیحات:** DistributedPubSubService

**متدهای عمومی:**

- `Publish()`
- `PublishAsync()`
- `Subscribe()`
- `SubscribeAsync()`

---

### EmployeeDataService

**توضیحات:** EmployeeDataService


---

### EmployeeDataServiceBase

**توضیحات:** EmployeeDataServiceBase

**متدهای عمومی:**

- `GetAllJobPositionsAsync()`
- `GetDependantsGroupInfoAsync()`
- `GetEmployeeContactInfoAsync()`
- `GetEmployeeInfoAsync()`
- `GetEmployeesAndDependantsGroupInfoAsync()`
- `GetEmployeesGroupInfoAsync()`
- `GetJobPositionsByPersonnelIdAsync()`

---

### ExcelFileServiceBase

**توضیحات:** ExcelFileServiceBase


---

### IpAddressService

**توضیحات:** IpAddressService

**متدهای عمومی:**

- `GetIpAddress()`

---

### MemoryCacheService

**توضیحات:** MemoryCacheService

**متدهای عمومی:**

- `Clear()`
- `Contains()`
- `GetAllKeys()`
- `GetWildcardKeys()`
- `Remove()`

---

### NotificationService

**توضیحات:** NotificationService

**متدهای عمومی:**

- `GetValidParamNames()`

---

### NotificationServiceBase

**توضیحات:** NotificationServiceBase

**متدهای عمومی:**

- `ProcessTemplate()`
- `SendToEmployeeAsync()`
- `SendToEmployeeTemplateAsync()`
- `SendToStudentAsync()`
- `SendToStudentTemplateAsync()`
- `ValidateTemplate()`

---

### PersonInfoService

**توضیحات:** سرویس دریافت اطلاعات افراد



---

### SendNotificationBackgroundService

**توضیحات:** سرویس ارسال نوتیفیکیشن‌های در صف ارسال



---

### SendService

**توضیحات:** SendService

**متدهای عمومی:**

- `CreateRequest()`

---

### Service

**توضیحات:** Service


---

### Service

**توضیحات:** Service


---

### SettingsService

**توضیحات:** پیاده سازی سرویس تنظیمات



---

### StudentDataService

**توضیحات:** StudentDataService


---

### StudentDataServiceBase

**توضیحات:** StudentDataServiceBase

**متدهای عمومی:**

- `AdvancedSearchAsync()`
- `GetBranchNameAsync()`
- `GetCsisBranchesAsync()`
- `GetDependantsAsync()`
- `GetDependantsGroupInfoAsync()`
- `GetStudentGroupInfoAsync()`
- `GetStudentInfoAsync()`
- `GetStudentWithDependantsAsync()`
- `GetStudentsAndDependantsGroupInfoAsync()`
- `SearchStudentAsync()`

---

### StudentDataServiceHealthCheck

**توضیحات:** StudentDataServiceHealthCheck

**متدهای عمومی:**

- `CheckHealthAsync()`

---

### SyncPermissionsBackgroundService

**توضیحات:** سرویس همگام‌سازی دسترسی‌ها


**متدهای عمومی:**

- `StartAsync()`
- `StartedAsync()`
- `StartingAsync()`
- `StopAsync()`
- `StoppedAsync()`
- `StoppingAsync()`

---

### TrackingCodeService

**توضیحات:** TrackingCodeService

**متدهای عمومی:**

- `GetRandomTrackingCodeAsync()`
- `GetTimeBasedTrackingCodeAsync()`
- `GetTrackingCodeAsync()`

---

