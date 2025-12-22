<div dir="rtl">

# مستندات جامع سیستم پذیرش

## 📖 معرفی

این مجموعه شامل **مستندات کامل و جامع** سیستم پذیرش است که با رویکرد **فایل به فایل** تهیه شده است.

### هدف

هدف این مستندات:
- 📚 ارائه اطلاعات کامل درباره معماری، طراحی و پیاده‌سازی سیستم
- 🔍 تسهیل درک سیستم برای توسعه‌دهندگان جدید
- 📝 مرجع کامل برای نگهداری و توسعه آینده
- 🎯 استخراج Use Case ها و Business Rules
- 🗺️ نقشه‌برداری وابستگی‌ها و جریان داده

---

## 📊 خلاصه آماری پروژه

| متریک | مقدار |
|------|------|
| **تعداد کل فایل‌های C#** | ~1512 |
| **تعداد پروژه‌ها** | 10 |
| **تعداد Features** | 72 |
| **تعداد Use Cases** | 132+ |
| **تعداد Entities** | 108 |
| **تعداد Enums** | 84 |
| **تعداد Controllers** | 112 |
| **تعداد Stored Procedures** | 90 |
| **تعداد EF Configurations** | 92 |
| **تعداد Migrations** | 3 |

---

## 🗂️ ساختار مستندات

```
/docs
├── README.md (این فایل)
├── /index
│   ├── ProjectDiscovery.md      # گزارش کشف اولیه پروژه
│   ├── Features.md              # کاتالوگ 72 Feature
│   ├── UseCases.md              # کاتالوگ 132 Use Case
│   ├── DataAccess.md            # EF Core + Dapper + 90 SP
│   ├── ConfigMap.md             # نقشه کامل تنظیمات
│   ├── DependencyGraph.md       # نمودارهای وابستگی (Mermaid)
│   └── RequestFlows.md          # جریان درخواست‌ها (TODO)
├── /use-cases
│   ├── README.md                # مستندات جامع Use Case ها (تشریحی)
│   └── DETAILED_USE_CASES.md   # مستندات تفصیلی تمام Use Case ها
└── /files
    └── ... (مستندات فایل به فایل - در حال تکمیل)
```

---

## 🚀 شروع سریع

### برای توسعه‌دهندگان جدید:

1. **مرحله 1**: خواندن [گزارش کشف اولیه](/docs/index/ProjectDiscovery.md)
   - درک معماری کلی
   - لایه‌ها و پروژه‌ها
   - تکنولوژی‌های استفاده شده

2. **مرحله 2**: مطالعه [کاتالوگ Features](/docs/index/Features.md)
   - آشنایی با 72 Feature
   - الگوی CQRS
   - دسته‌بندی Features

3. **مرحله 3**: بررسی [Use Cases](/docs/index/UseCases.md)
   - درک جریان‌های کسب‌وکار
   - قوانین کسب‌وکار
   - نمودارهای Sequence

4. **مرحله 4**: مطالعه [Data Access](/docs/index/DataAccess.md)
   - EF Core Configurations
   - Dapper و Stored Procedures
   - Performance Considerations

5. **مرحله 5**: آشنایی با [تنظیمات](/docs/index/ConfigMap.md)
   - appsettings.json
   - تنظیمات بحرانی
   - Checklist Production

---

## 📚 فهرست اسناد Index

### [1. Project Discovery](/docs/index/ProjectDiscovery.md)

**محتوا**:
- لیست پروژه‌ها و معماری
- لایه‌های Core, Infrastructure, Presentation, Tests
- Entry Points (Controllers, Middleware, Background Services)
- نقاط Data Access (EF Core, Dapper)
- تخمین Features و Use Cases
- تکنولوژی‌ها و Packages
- نکات امنیتی و Performance

**حجم**: ~15 هزار کلمه

**کاربرد**: نقطه شروع برای درک کلی سیستم

---

### [2. Features Catalog](/docs/index/Features.md)

**محتوا**:
- جدول جامع 72 Feature
- دسته‌بندی بر اساس دامنه کاری
- آمار Commands, Queries, DTOs, Validators
- الگوهای مشترک Commands و Queries
- قوانین کسب‌وکار Cross-Cutting
- نمودار وابستگی Feature-Level

**کاربرد**: راهنمای جامع Features و الگوهای کدنویسی

---

### [3. Use Cases Catalog](/docs/index/UseCases.md)

**محتوا**:
- 132+ Use Case با فرمت استاندارد
- دسته‌بندی بر اساس Actor و دامنه
- State Machine وضعیت پرونده
- Sequence Diagram برای UC کلیدی (تشکیل پرونده)
- جداول Use Case های بحرانی
- Business Rules و Exception Flows

**کاربرد**: درک جریان‌های کسب‌وکار و نیازمندی‌های سیستم

---

### [4. Data Access Deep Dive](/docs/index/DataAccess.md)

**محتوا**:
#### بخش 1: Entity Framework Core
- AppDbContext و Configuration
- OnModelCreating و Conventions
- Bulk Operations
- 92 Entity Configuration
- Relationships و Delete Behaviors
- Indexes و Value Converters
- Migrations
- Connection Pooling (Max: 1024)
- Interceptors (Audit Logging)
- Performance: Tracking, Eager Loading, Projections, Split Queries

#### بخش 2: Dapper
- AppDapperContext
- 90 Stored Procedures (Query + Command + Events + Integration + Utility)
- ExecuteQuery, ExecuteProcedureToList, ExecuteProcedureSingleOrDefault
- Mapping و Parameterization
- SQL Injection Prevention
- Transaction Boundary (چالش EF + Dapper)
- Risks و Pitfalls

#### بخش 3: Repository Pattern
- Generic Repository با Cache
- 13 Custom Repository
- نمونه: StudentRepository

**حجم**: ~29 هزار کلمه

**کاربرد**: راهنمای موشکافانه Data Access

---

### [5. Configuration Map](/docs/index/ConfigMap.md)

**محتوا**:
- تمامی کلیدهای appsettings.json
- 13 بخش تنظیمات
- توضیح هر کلید: معنی، مقدار پیش‌فرض، حساسیت، محل مصرف
- تنظیمات بحرانی:
  - DatabaseOptions (Connection String, Pooling)
  - CacheOptions (AbsoluteExpiration, SlidingExpiration)
  - RedisOptions (Host, Password)
  - CorsOptions (Origins)
  - External Services (IdentityServer, FileManagement, etc.)
  - Serilog
  - Swagger
- نمودار وابستگی تنظیمات (Mermaid)
- Checklist Production

**کاربرد**: راهنمای تنظیمات برای Deployment و Configuration Management

---

### [6. Dependency Graph](/docs/index/DependencyGraph.md)

**محتوا**:
- نمودار Solution Level (وابستگی پروژه‌ها)
- نمودار Layer Dependencies (WebApi → Application → Domain → Persistence/Services)
- نمودار Use Case Level (UC-030: تشکیل پرونده)
- نمودار Feature Dependencies (Students Feature)
- نمودار Service Dependencies
- نمودار Data Access Layer (EF + Dapper)
- نمودار External Service Integrations
- نمودار Background Services
- نمودار Cache Strategy
- نمودار Transaction Boundaries
- جداول خلاصه وابستگی‌ها
- نمودار Deployment (احتمالی)

**حجم**: 12 Mermaid Diagram

**کاربرد**: درک بصری از وابستگی‌ها و جریان داده

---

### [7. Use Cases Documentation (جدید)](/docs/use-cases/README.md)

**محتوا**:
- مستندات جامع و تشریحی تمامی Use Case های سیستم
- دسته‌بندی Use Case ها بر اساس دامنه کاری
- شرح تفصیلی منطق هر Use Case
- توضیح Commands و Queries
- شرح سرویس‌های سیستم و وظایف آن‌ها
- معماری CQRS و الگوهای طراحی
- تشریح کامل 250+ Command و Query
- 27 سرویس سیستم با توضیحات کامل

**حجم**: ~450 خط مستندات تشریحی + 2900 خط مستندات تفصیلی

**کاربرد**: 
- درک کامل منطق و عملکرد سیستم
- راهنمای توسعه‌دهندگان برای پیاده‌سازی ویژگی‌های جدید
- مرجع کامل برای تمامی Use Case های موجود
- **توجه**: این مستند شامل جزئیات امنیتی نیست

---

## 🗂️ دسته‌بندی Features

### Core Student Management (مدیریت اصلی دانشجو)
- **Students** (24 Commands, 23 Queries) - بحرانی ⚠️
- **People** (16C, 4Q)
- **StudentMobiles** (5C, 1Q)
- **BankAccounts** (4C, 1Q)
- **Addresses** (5C, 2Q)

### Case Management (مدیریت پرونده)
- **CaseFilings** (16C, 4Q) - بحرانی ⚠️ - Wizard 10 مرحله‌ای
- **CaseBlock** (4C, 0Q)
- **DependentCaseActive** (6C, 1Q)

### Dependents Management (افراد تحت تکفل)
- **StudentDependents** (6C, 3Q)
- **NonStudentDependants** (3C, 2Q)
- **DependentEmployments** (1C, 1Q)

### Family Events (رویدادهای خانوادگی)
- **Marriages** (8C, 2Q)
- **Divorce** (8C, 1Q)
- **Pregnancies** (3C, 1Q)
- **Family** (0C, 2Q)

### Academic & Cultural Activities
- **Researches** (6C, 2Q)
- **Teaches** (7C, 2Q)
- **Preaches** (7C, 2Q)
- **CulturalActivities** (4C, 2Q)
- **Memorizers** (3C, 2Q)
- **ImamJamaat** (12C, 7Q)

### Excellence & Honors
- **Elites** (6C, 2Q)
- **Excellents** (2C, 1Q)
- **Famouses** (6C, 3Q)
- **Veterans** (7C, 1Q)

### Housing & Targeting (مسکن و هدفمندی)
- **Houses** (5C, 1Q)
- **HousingAdmissionInfo** (1C, 4Q)
- **TargetedScores** (0C, 3Q)

### Commission & Approval
- **CommissionInfos** (0C, 2Q)

### Services & Blocking
- **BlockServices** (6C, 4Q) - بحرانی ⚠️

### Authentication
- **Auth** (3C, 0Q) - بحرانی ⚠️

### Reporting & Audit
- **ReportBuilders** (3C, 5Q)
- **ReportProfiles** (3C, 2Q)
- **AdmissionAuditLogs** (0C, 2Q)

### Master Data
- Countries, Provinces, Cities, Towns, Rurals, Portions
- Branches, Schools
- CountryDivisions (3C, 0Q)

### File & Document Management
- **Files** (2C, 1Q)
- **Documents** (1C, 1Q)
- **PictureHistories** (0C, 1Q)

### Notifications
- **Notifications** (1C, 1Q)

### Settings
- **Settings** (2C, 2Q)

### Protests
- **Protests** (2C, 1Q)

### Non-Iranian Students
- **NonIranianStudent** (7C, 0Q)
- **NonStudents** (3C, 2Q)

---

## 🎯 Use Cases کلیدی

### احراز هویت
- UC-001: ورود کارمند
- UC-002: ورود دانشجو
- UC-003: تمدید توکن
- UC-004: تولید OTP

### مدیریت دانشجو
- UC-010: مشاهده اطلاعات کامل
- UC-011: بروزرسانی شناسنامه‌ای (با ثبت احوال)
- UC-012: سینک با ثبت احوال
- UC-013: بروزرسانی تصویر
- UC-014: تمدید پرونده
- UC-015: مسدودی پرونده
- UC-016: رفع مسدودی

### تشکیل پرونده (Wizard)
- UC-030: تشکیل پرونده دانشجوی جدید (10 مرحله)
  - Step01: هویت
  - Step02: موبایل + OTP
  - Step03: اعتبارسنجی حوزه
  - Step04: شناسنامه
  - Step05: آدرس
  - Step06: تصویر
  - Step07: بانک
  - Step08: شغل
  - Step09: تکمیل
  - Step10: ایجاد کاربر

### تکفل
- UC-040: ثبت تکفل جدید
- UC-041: فعال کردن پرونده
- UC-042: غیرفعال کردن پرونده

### رویدادهای خانوادگی
- UC-050-056: ثبت ازدواج/طلاق

### مسکن و هدفمندی
- UC-070: محاسبه امتیاز

### خدمات و مسدودی
- UC-080-082: مسدودی/رفع مسدودی

---

## 🛠️ تکنولوژی‌ها

### Framework
- .NET 6.0+
- ASP.NET Core WebApi

### Data Access
- **Entity Framework Core** (ORM)
- **Dapper** (Micro ORM for SPs)
- **EFCore.BulkExtensions** (Bulk Operations)
- **Microsoft.Data.SqlClient**

### CQRS & Mediator
- **MediatR**

### Validation
- **FluentValidation**

### Mapping
- **AutoMapper**

### Logging
- **Serilog** (Structured Logging)
- **Elastic.Serilog.Sinks** (ElasticSearch)

### Caching
- **Redis** (Distributed Cache)
- **MemoryCache** (In-Memory)

### Authentication
- **Csis.DigestAuthentication**

### Health Checks
- **AspNetCore.HealthChecks**

### API Documentation
- **Swashbuckle** (Swagger/OpenAPI)

---

## 🏗️ معماری

### Clean Architecture

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

---

## 🔒 امنیت

### نکات امنیتی کلیدی:

1. **Password در appsettings**: ⚠️ باید به User Secrets یا Key Vault منتقل شود
2. **CORS**: `"*"` → باید به دامنه‌های خاص محدود شود
3. **Swagger**: غیرفعال یا محافظت شده با Authentication
4. **DigestAuth Users**: خالی → باید پر شود
5. **SQL Injection**: استفاده از Parameterization
6. **Sensitive Data Logging**: غیرفعال در Production
7. **API Keys**: باید در محل امن نگهداری شوند

---

## ⚡ Performance

### نکات Performance:

1. **Connection Pooling**: فعال (Max: 1024)
2. **Bulk Operations**: برای Insert/Update دسته‌ای
3. **NoTracking**: برای Query های Read-Only
4. **Eager Loading**: برای کاهش N+1 Problem
5. **Split Queries**: برای Join های بزرگ
6. **Compiled Queries**: برای Query های تکراری
7. **Caching**: Redis + Memory Cache
8. **Dapper**: برای SP های سنگین

---

## 📋 Checklist Production

### Database
- [ ] تغییر Connection String
- [ ] استفاده از Managed Identity یا Key Vault
- [ ] بررسی MaxPoolSize (1024 خیلی زیاد است؟)
- [ ] غیرفعال کردن Sensitive Data Logging
- [ ] فعال کردن Logging (اما نه EnableSensitiveDataLogging)

### Security
- [ ] پر کردن DigestAuthenticationOptions.Users
- [ ] تنظیم CORS Origins به دامنه‌های خاص
- [ ] تنظیم AllowedHosts
- [ ] پر کردن API Keys سرویس‌های خارجی
- [ ] غیرفعال کردن Swagger یا محافظت با Auth

### Configuration
- [ ] تنظیم Redis برای Production
- [ ] فعال کردن GlobalOptions.RunBackgroundServices
- [ ] فعال کردن ElasticSearch
- [ ] بررسی Timeout ها (Command Timeout: 180s خیلی زیاد؟)

### Monitoring
- [ ] تنظیم Serilog برای Centralized Logging
- [ ] فعال کردن Health Checks
- [ ] پیکربندی Application Insights یا ابزار Monitoring

---

## 🧪 تست‌ها

### پروژه‌های تست:
- **Csis.Admission.UnitTests**: تست‌های واحد
- **Csis.Admission.IntegrationTests**: تست‌های یکپارچه‌سازی
- **Csis.Admission.WebApiTests**: تست‌های API

---

## 📞 پشتیبانی

برای سوالات یا مشکلات، به مستندات زیر مراجعه کنید:
- [Project Discovery](/docs/index/ProjectDiscovery.md)
- [Features Catalog](/docs/index/Features.md)
- [Use Cases](/docs/index/UseCases.md)
- [Data Access](/docs/index/DataAccess.md)
- [Config Map](/docs/index/ConfigMap.md)
- [Dependency Graph](/docs/index/DependencyGraph.md)

---

## 📝 وضعیت مستندسازی

### ✅ تکمیل شده:
- [x] گزارش کشف اولیه (ProjectDiscovery.md)
- [x] کاتالوگ Features (Features.md)
- [x] کاتالوگ Use Cases (UseCases.md)
- [x] Data Access (DataAccess.md) - EF Core + Dapper
- [x] Configuration Map (ConfigMap.md)
- [x] Dependency Graph (DependencyGraph.md)
- [x] Use Cases Documentation (use-cases/README.md) - مستندات جامع Use Case ها
- [x] README.md (این فایل)

### 🚧 در حال تکمیل:
- [ ] Request Flows (RequestFlows.md) - جریان درخواست‌ها و State Machines
- [ ] مستندات فایل به فایل (One-Doc-Per-File)
  - [ ] Application/Features (72 feature × 3-5 فایل = ~300 فایل)
  - [ ] Services (20 فایل)
  - [ ] Persistence (Configurations: 92، Repositories: 13)
  - [ ] WebApi (Controllers: 112)
  - [ ] Domain (Entities: 108، Enums: 84)

### تخمین باقیمانده:
- **مستندات Index**: 95% تکمیل
- **مستندات Use Cases**: 100% تکمیل ✅
- **مستندات فایل به فایل**: 0% تکمیل (در اولویت بعدی)

**توجه**: با توجه به حجم عظیم (1512 فایل)، مستندسازی فایل به فایل به صورت تدریجی و بر اساس اولویت انجام خواهد شد.

---

## 🎯 اولویت‌های مستندسازی فایل به فایل

### اولویت 1 (بحرانی ⚠️):
1. Features کلیدی:
   - Students (Iranian/NonIranian)
   - CaseFilings (Wizard 10 مرحله‌ای)
   - Auth
   - BlockServices

2. Services اصلی:
   - CaseFillingRequestService
   - NotificationService
   - External Service Integrations

3. Persistence:
   - AppDbContext
   - AppDapperContext
   - StudentRepository
   - PersonRepository

### اولویت 2 (مهم):
1. Features:
   - StudentDependents
   - Marriages
   - Divorce
   - Houses
   - CommissionInfos

2. Controllers:
   - ImamJamaatController
   - RequestsController

### اولویت 3 (متوسط):
- سایر Features
- سایر Controllers
- Entity Configurations

---

**آخرین بروزرسانی**: 2024-12-22

**نسخه مستندات**: 1.0

</div>
