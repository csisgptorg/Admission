<div dir="rtl">

# گزارش کشف اولیه پروژه - سیستم پذیرش

## خلاصه اجرایی

این پروژه یک **سیستم جامع مدیریت پذیرش و اطلاعات طلاب** است که با معماری Clean Architecture و الگوی CQRS با استفاده از .NET پیاده‌سازی شده است.

## 1. لیست پروژه‌ها (از Solution)

براساس فایل `Csis.Admission.sln`، پروژه شامل **10 پروژه** به شرح زیر است:

### پروژه‌های Core (هسته)
- **Csis.Admission.Domain**: لایه دامین - شامل Entities، Enums، و قوانین کسب‌وکار پایه
- **Csis.Admission.Application**: لایه اپلیکیشن - شامل Features، Commands، Queries و Business Logic

### پروژه‌های Infrastructure (زیرساخت)
- **Csis.Admission.Persistence**: لایه دیتابیس - شامل EF Core DbContext، Dapper، Configurations و Migrations
- **Csis.Admission.Services**: لایه سرویس‌ها - شامل External Services، Background Services، Cache و Integrations

### پروژه‌های Presentation (ارائه)
- **Csis.Admission.WebApi**: لایه WebApi - شامل Controllers، Middleware و Configuration
- **Csis.Admission**: پروژه اصلی (احتمالاً Console یا Host)

### پروژه‌های Tests (تست)
- **Csis.Admission.UnitTests**: تست‌های واحد
- **Csis.Admission.IntegrationTests**: تست‌های یکپارچه‌سازی
- **Csis.Admission.WebApiTests**: تست‌های WebApi
- **Csis.Admission.NugetTest**: تست‌های NuGet Package

## 2. معماری و لایه‌ها

پروژه از **Clean Architecture** پیروی می‌کند با ساختار زیر:

```
┌─────────────────────────────────────┐
│      Presentation Layer             │
│  (WebApi, Controllers, Middleware)  │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Application Layer              │
│  (Features, CQRS, Handlers,         │
│   Validators, DTOs, Mappings)       │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Domain Layer                   │
│  (Entities, Enums, Value Objects,   │
│   Business Rules)                   │
└─────────────────────────────────────┘
               ▲
               │
┌──────────────┴──────────────────────┐
│   Infrastructure Layer              │
│  ┌────────────┐  ┌────────────┐     │
│  │ Persistence│  │  Services  │     │
│  │ (EF+Dapper)│  │ (External) │     │
│  └────────────┘  └────────────┘     │
└─────────────────────────────────────┘
```

### توضیحات لایه‌ها:

#### Domain Layer
- **Entities**: تعداد تقریبی **108 Entity**
- **Enums**: تعداد تقریبی **84 Enum**
- **Common Interfaces**: `IEntity`, `IAuditable`, `ISoftDeletedEntity`, `ITrackableEntity`, `IFilterable`
- **Base Classes**: `BaseEntity`, `SoftDeletedBaseEntity`

#### Application Layer
- **Features**: **72 Feature Module** (هر feature شامل Commands, Queries, Handlers, Validators, DTOs)
- **الگوی CQRS**: استفاده از MediatR
- **Validation**: استفاده از FluentValidation
- **Mapping**: استفاده از AutoMapper

#### Persistence Layer
- **EF Core DbContext**: `AppDbContext` با Pooling و Bulk Operations
- **Configurations**: **92 Entity Configuration**
- **Migrations**: **3 Migration**
- **Dapper**: `AppDapperContext` برای Stored Procedures
- **Stored Procedures**: **~90 SP** تعریف شده در `ProcedureName` enum
- **Repositories**: Repository Pattern برای Data Access

#### Services Layer
- **External Services**:
  - `EmployeeDataService`: سرویس داده کارکنان
  - `StudentDataService`: سرویس داده دانشجویان
  - `PersonInfoService`: سرویس اطلاعات افراد
  - `NotificationService`: سرویس نوتیفیکیشن
  - `SettingsService`: سرویس تنظیمات
  - `FileManagementService`: مدیریت فایل (احتمالی)
  - `IdentityServerService`: احراز هویت (احتمالی)
  
- **Background Services**:
  - `SyncPermissionsBackgroundService`: همگام‌سازی دسترسی‌ها
  - `SendNotificationBackgroundService`: ارسال نوتیفیکیشن

- **Cache Services**:
  - `DistributedCacheService`: کش توزیع‌شده (Redis)
  - `MemoryCacheService`: کش حافظه
  - `CacheKeyService`: مدیریت کلیدهای کش

- **Other Services**:
  - `DistributedPubSubService`: Pub/Sub توزیع‌شده
  - `DateTimeService`: سرویس تاریخ/زمان
  - `ExcelFileServiceBase`: سرویس پایه Excel

#### WebApi Layer
- **Controllers**: **112 Controller**
- **Middleware**:
  - `UserIdLogScopeMiddleware`: لاگ UserId
  - `LogRequestMiddleware`: لاگ درخواست‌ها
  - سایر Middleware های Csis.Utilities

## 3. Entry Points (نقاط ورودی)

### Controllers
تعداد: **112 کنترلر**

دسته‌بندی Controllers:
- **ImamJamaat**: `ImamJamaatStudentController`, `ImamJamaatAdminController`
- **NonStudents**: `NonStudentsController`, `NonStudentDependantsController`
- **Management**: `AppManagementController`, `ReportProfilesController`
- **Other**: 
  - `TestController`
  - `TownsController`, `CitiesController`, `RuralsController`
  - `BranchesController`, `AgenciesController`
  - `RequestsController`, `PortionsController`
  - `EliteLevelsController`, `FilesController`
  - و کنترلرهای دیگر...

### Middleware Pipeline
ترتیب Middleware در `Program.cs`:
1. Serilog Request Logging
2. Exception Handling Middleware
3. Digest Authentication Middleware
4. CORS Middleware
5. `UserIdLogScopeMiddleware` - اضافه کردن UserId به Log Scope
6. `LogRequestMiddleware` - لاگ جزئیات Request
7. Authorization & Authentication
8. Routing & Controllers

### Background Services / Hosted Services
- `SyncPermissionsBackgroundService`: همگام‌سازی مجوزها با سرویس خارجی
- `SendNotificationBackgroundService`: ارسال نوتیفیکیشن‌های صف‌شده

### Health Checks
- `StudentDataServiceHealthCheck`: بررسی سلامت سرویس داده دانشجویان
- احتمالاً سایر Health Checks برای Database و Redis

## 4. نقاط Data Access

### EF Core DbContext
- **DbContext**: `AppDbContext`
- **Connection String**: SQL Server
- **Features**:
  - Database Pooling (MaxPoolSize: 1024)
  - Bulk Operations (EFCore.BulkExtensions)
  - Automatic Auditing (CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
  - Soft Delete Support
  - Schema: `stu` (default schema)
  - Precision: Decimal(18,2)

### Dapper Context
- **Context**: `AppDapperContext`
- **Command Timeout**: 180 seconds
- **Schema**: `stu.*` برای Stored Procedures
- **Methods**:
  - `ExecuteQuery`: اجرای Query مستقیم
  - `ExecuteProcedureToList`: اجرای SP و دریافت لیست
  - `ExecuteProcedureSingleOrDefault`: اجرای SP و دریافت یک نتیجه
  - `ExecuteProcedure`: اجرای SP بدون خروجی

### Repositories
الگوی Repository برای:
- `StudentRepository`: مدیریت اطلاعات دانشجویان
- `PersonRepository`: مدیریت اطلاعات افراد
- `AdmissionAuditLogRepository`: لاگ‌های پذیرش
- `BasicDataRepository`: داده‌های پایه
- `StudentBankAccountRepository`: حساب بانکی دانشجو
- `StudentDependentRepository`: افراد تحت تکفل دانشجو
- `StudentMobileRepository`: شماره موبایل دانشجو
- `QueryBuilderRepository`: ساخت Query های پیچیده

## 5. تخمین Features و مسیرها

### تعداد Feature Modules: **72 Feature**

لیست کامل Features:
1. Addresses - مدیریت آدرس‌ها
2. AdmissionAuditLogs - لاگ‌های ممیزی پذیرش
3. Auth - احراز هویت
4. BankAccounts - حساب‌های بانکی
5. BlockServices - خدمات مسدود
6. Branches - شعب
7. CaseBlock - مسدودی پرونده
8. CaseFilings - تشکیل پرونده
9. Cities - شهرها
10. CommissionInfos - اطلاعات کمیسیون
11. CompleteStudentInfos - اطلاعات کامل دانشجو
12. ContinuousInformationTabs - تب‌های اطلاعات مستمر
13. Countries - کشورها
14. CountryDivisions - تقسیمات کشوری
15. CulturalActivities - فعالیت‌های فرهنگی
16. CulturalActivityGrades - نمرات فعالیت فرهنگی
17. DependentActiveReasons - دلایل فعال‌سازی تکفل
18. DependentCaseActive - فعال‌سازی پرونده تکفل
19. DependentDeActiveReasons - دلایل غیرفعال‌سازی تکفل
20. DependentEmployments - اشتغال افراد تحت تکفل
21. Divorce - طلاق
22. Documents - مدارک
23. EducationYears - سال‌های تحصیلی
24. Educations - تحصیلات
25. EliteLevels - سطوح نخبگان
26. EliteTypes - انواع نخبگان
27. Elites - نخبگان
28. EmployeeViewStudentLogs - لاگ مشاهده دانشجو توسط کارمند
29. Employments - اشتغال
30. ExcellentEducationLevels - سطوح تحصیلی برتر
31. ExcellentEducationYears - سال‌های تحصیلی برتر
32. Excellents - برترین‌ها
33. Family - خانواده
34. Famouses - مشاهیر
35. Files - فایل‌ها
36. Houses - مسکن
37. HousingAdmissionInfo - اطلاعات پذیرش مسکن
38. ImamJamaat - امام جماعت
39. Insurances - بیمه‌ها
40. Marriages - ازدواج
41. Memorizers - حافظان
42. NonIranianStudent - دانشجوی غیر ایرانی
43. NonStudentDependants - افراد تحت تکفل غیر دانشجو
44. NonStudents - غیر دانشجویان
45. Notifications - نوتیفیکیشن‌ها
46. People - افراد
47. PictureHistories - تاریخچه تصاویر
48. Portions - بخش‌ها
49. PreachGrades - نمرات تبلیغ
50. Preaches - تبلیغات
51. Pregnancies - بارداری
52. Protests - اعتراضات
53. Provinces - استان‌ها
54. ReligiousRoleQuestions - سوالات نقش‌آفرینی مذهبی
55. ReportBuilders - سازنده گزارش
56. ReportProfiles - پروفایل گزارش
57. ResearchGrades - نمرات پژوهش
58. Researches - پژوهش‌ها
59. Rurals - روستاها
60. Schools - مدارس
61. Settings - تنظیمات
62. SoldierStudents - دانشجویان سرباز
63. StudentDependents - افراد تحت تکفل دانشجو
64. StudentFriends - دوستان دانشجو
65. StudentMobiles - موبایل‌های دانشجو
66. Students - دانشجویان
67. TargetedScores - امتیازهای هدفمند
68. TeachGrades - نمرات تدریس
69. Teaches - تدریس‌ها
70. Towns - شهرها
71. UniversityEducations - تحصیلات دانشگاهی
72. Veterans - جانبازان/ایثارگران

### ساختار هر Feature (الگوی استاندارد):
```
Feature/
├── Commands/
│   ├── Create/
│   │   ├── Command.cs
│   │   ├── Handler.cs
│   │   └── Validator.cs
│   ├── Update/
│   ├── Delete/
│   └── ...
├── Queries/
│   ├── GetAll/
│   ├── GetById/
│   ├── GetByFilter/
│   └── ...
├── DTOs/
│   ├── RequestDto.cs
│   ├── ResponseDto.cs
│   └── ...
└── Profiles/
    └── MappingProfile.cs
```

## 6. فهرست اولیه Use Cases (استخراج شده از Features و Stored Procedures)

### دسته‌بندی Use Cases بر اساس Actor:

#### UC دانشجو (Student)
- UC-001: ثبت‌نام طلبه جدید
- UC-002: مشاهده اطلاعات پرونده
- UC-003: بروزرسانی اطلاعات شخصی
- UC-004: بروزرسانی آدرس
- UC-005: بروزرسانی شماره تماس
- UC-006: بروزرسانی حساب بانکی
- UC-007: آپلود تصویر پروفایل
- UC-008: مشاهده تاریخچه تصاویر
- UC-009: مشاهده خدمات مسدود
- UC-010: ثبت اعتراض

#### UC افراد تحت تکفل (Dependent)
- UC-011: ثبت تکفل جدید
- UC-012: بروزرسانی اطلاعات تکفل
- UC-013: فعال/غیرفعال کردن پرونده تکفل
- UC-014: ثبت ازدواج/طلاق تکفل
- UC-015: بروزرسانی موبایل تکفل
- UC-016: بروزرسانی حساب بانکی تکفل

#### UC کمیسیون (Commission)
- UC-017: بررسی پرونده دانشجو در کمیسیون
- UC-018: تایید/رد پرونده
- UC-019: مشاهده وضعیت کمیسیون
- UC-020: بروزرسانی وضعیت کمیسیون

#### UC پذیرش مسکن (Housing Admission)
- UC-021: ثبت درخواست پذیرش مسکن
- UC-022: بررسی امتیاز هدفمندی
- UC-023: محاسبه امتیاز معیشتی

#### UC مدیریت پرونده (Case Management)
- UC-024: تشکیل پرونده
- UC-025: تمدید پرونده (دستی/خودکار)
- UC-026: مسدودی پرونده
- UC-027: رفع مسدودی پرونده
- UC-028: بستن پرونده
- UC-029: افزودن توضیحات پرونده

#### UC گزارش‌گیری (Reporting)
- UC-030: تولید گزارش داده پرداخت
- UC-031: مشاهده سوابق ممیزی
- UC-032: تولید گزارش‌های سفارشی (Report Builder)
- UC-033: مشاهده آمار و ارقام

#### UC احراز هویت (Authentication)
- UC-034: ورود به سیستم
- UC-035: تولید کد OTP
- UC-036: تایید کد OTP
- UC-037: خروج از سیستم

#### UC مدیریت اطلاعات پایه (Master Data)
- UC-038: مدیریت شهرها
- UC-039: مدیریت استان‌ها
- UC-040: مدیریت تقسیمات کشوری
- UC-041: مدیریت شعب
- UC-042: مدیریت مدارس
- UC-043: مدیریت تنظیمات سیستم

#### UC یکپارچه‌سازی (Integration)
- UC-044: استعلام از مرکز حوزوی
- UC-045: استعلام از ثبت احوال
- UC-046: استعلام بیمه تامین اجتماعی
- UC-047: استعلام کارت خدمات
- UC-048: بروزرسانی از داده‌های المصطفی

#### UC فعالیت‌های فرهنگی/علمی (Cultural/Academic)
- UC-049: ثبت فعالیت‌های فرهنگی
- UC-050: ثبت پژوهش
- UC-051: ثبت تدریس
- UC-052: ثبت تبلیغ
- UC-053: ثبت حفظ قرآن
- UC-054: ثبت نقش‌آفرینی مذهبی

#### UC نوتیفیکیشن (Notification)
- UC-055: ارسال نوتیفیکیشن به دانشجو
- UC-056: مشاهده نوتیفیکیشن‌های دریافتی

## 7. تکنولوژی‌ها و Packages مهم

### Framework & Runtime
- .NET 6.0 یا بالاتر
- ASP.NET Core WebApi

### ORM & Data Access
- **Entity Framework Core**: ORM اصلی
- **EFCore.BulkExtensions**: عملیات Bulk
- **Dapper**: Micro ORM برای Stored Procedures
- **Microsoft.Data.SqlClient**: اتصال به SQL Server

### CQRS & Mediator
- **MediatR**: پیاده‌سازی CQRS pattern

### Validation
- **FluentValidation**: اعتبارسنجی

### Mapping
- **AutoMapper**: نگاشت اشیاء

### Logging
- **Serilog**: لاگینگ ساختاریافته
- **Serilog.Exceptions**: لاگ استثناها
- **Elastic.Serilog.Sinks**: ارسال لاگ به ElasticSearch

### Caching
- **Redis**: کش توزیع‌شده
- **MemoryCache**: کش حافظه

### Authentication
- **Csis.DigestAuthentication**: احراز هویت Digest

### Health Checks
- **AspNetCore.HealthChecks**: بررسی سلامت سیستم

### Swagger/OpenAPI
- **Swashbuckle**: مستندات API

### Utilities
- **Csis.Utilities**: ابزارهای مشترک داخلی

## 8. Configuration Sections (کلیدهای اصلی appsettings)

1. **DatabaseOptions**: تنظیمات دیتابیس
2. **CacheOptions**: تنظیمات کش
3. **CorsOptions**: تنظیمات CORS
4. **DigestAuthenticationOptions**: احراز هویت
5. **ElasticSearchOptions**: ElasticSearch
6. **EmployeeDataServiceOptions**: سرویس داده کارکنان
7. **FileManagementOptions**: مدیریت فایل
8. **GlobalOptions**: تنظیمات عمومی
9. **IdentityServerOptions**: Identity Server
10. **RedisOptions**: Redis Cache
11. **Serilog**: تنظیمات لاگینگ
12. **StudentDataServiceOptions**: سرویس داده دانشجویان
13. **SwaggerOptions**: تنظیمات Swagger

## 9. نکات و مشاهدات مهم

### نقاط قوت معماری:
- ✅ معماری Clean و جداسازی واضح لایه‌ها
- ✅ استفاده از الگوی CQRS
- ✅ Validation در سطح Application
- ✅ Audit Logging خودکار
- ✅ Soft Delete Support
- ✅ Database Pooling
- ✅ Bulk Operations برای Performance
- ✅ Health Checks
- ✅ Background Services برای وظایف زمان‌بندی شده

### نکات امنیتی:
- 🔒 Digest Authentication
- 🔒 CORS Configuration
- 🔒 Sensitive Data Logging قابل کنترل
- 🔒 API Key برای External Services

### Performance Considerations:
- ⚡ Connection Pooling (Max: 1024)
- ⚡ Distributed Cache (Redis)
- ⚡ Memory Cache
- ⚡ Bulk Operations
- ⚡ Dapper برای Queries سنگین
- ⚡ Command Timeout: 180 seconds

### نکات Data Access:
- 📊 Dual approach: EF Core + Dapper
- 📊 ~90 Stored Procedure
- 📊 Schema: `stu`
- 📊 92 Entity Configuration

## 10. مراحل بعدی مستندسازی

با توجه به این گزارش اولیه، مراحل بعدی مستندسازی به ترتیب زیر انجام خواهد شد:

1. ✅ **ProjectDiscovery.md** - تکمیل شد
2. ⏭️ **Features.md** - کاتالوگ جامع Features
3. ⏭️ **UseCases.md** - مستندات کامل Use Cases
4. ⏭️ **RequestFlows.md** - جریان درخواست‌ها و State Machines
5. ⏭️ **DataAccess.md** - مستندات EF Core و Dapper
6. ⏭️ **ConfigMap.md** - نقشه کامل تنظیمات
7. ⏭️ **DependencyGraph.md** - نمودار وابستگی‌ها
8. ⏭️ **One-Doc-Per-File** - مستندسازی فایل به فایل

## 11. تخمین حجم کار

- **تعداد کل فایل‌های .cs**: ~1512 فایل
- **تعداد Features**: 72 module
- **تعداد Controllers**: 112 کنترلر
- **تعداد Entities**: ~108 entity
- **تعداد Enums**: ~84 enum
- **تعداد Configurations**: ~92 configuration
- **تعداد Stored Procedures**: ~90 SP

**زمان تخمینی کل**: بسته به عمق تحلیل، این پروژه نیاز به مستندسازی جامع و دقیق دارد.

</div>
