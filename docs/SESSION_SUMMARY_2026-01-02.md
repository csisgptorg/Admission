<div dir="rtl">

# خلاصه مستندسازی - Features و Services

**تاریخ**: 1404/10/12 (2026-01-02)  
**نسخه**: 1.0

---

## 📊 خلاصه آماری

### انجام شده در این Session:

| بخش | تعداد | وضعیت |
|-----|-------|--------|
| **Features جدید** | 3 Query | ✅ |
| **Services اصلی** | 5 سرویس | ✅ |
| **README های راهنما** | 1 فایل | ✅ |
| **مجموع فایل‌های جدید** | 9 فایل | ✅ |

---

## 📁 فایل‌های ایجاد شده

### 1. Features (Queries)

#### ✅ GetProvincesQuery.md
**مسیر**: `docs/files/Csis.Admission.Application/Features/Provinces/Queries/`

**محتوا**:
- دریافت لیست 31 استان ایران
- بدون پارامتر ورودی
- پیشنهاد: افزودن Caching
- استفاده در Cascade Dropdowns

---

#### ✅ GetCitiesQuery.md
**مسیر**: `docs/files/Csis.Admission.Application/Features/Cities/Queries/`

**محتوا**:
- دریافت لیست شهرها
- پارامتر اختیاری: `ProvinceId` (فیلتر بر اساس استان)
- Cascade Dropdown Pattern
- پیشنهاد: Caching با Key بر اساس ProvinceId

---

#### ✅ GetBranchesQuery.md
**مسیر**: `docs/files/Csis.Admission.Application/Features/Branches/Queries/`

**محتوا**:
- دریافت لیست شعب حوزه علمیه
- پارامتر اختیاری: `HasAgency` (فیلتر شعب دارای نمایندگی)
- استفاده از Primary Constructor (C# 12)
- Use Cases: UC-030 (تشکیل پرونده)

---

### 2. Services (لایه سرویس)

#### ✅ BirthCertService.md
**مسیر**: `docs/files/Csis.Admission.Services/`

**محتوا**:
- دریافت اطلاعات شناسنامه‌ای از ثبت احوال (ایرانیان)
- دریافت اطلاعات از سامانه المصطفی (غیرایرانیان)
- متدها:
  - `Iranian(nationalCode, birthDate)`
  - `NonIranian(yektaCode)`
- Use Cases: UC-011، UC-012، UC-030-Step04
- نکات امنیتی و پیشنهادات بهبود (Caching، Retry، Circuit Breaker)

---

#### ✅ CurrentUserService.md
**مسیر**: `docs/files/Csis.Admission.Services/`

**محتوا**:
- Wrapper برای احراز هویت کاربر
- متدهای کلیدی:
  - `GetUserIdAsync()` - شناسه کاربر
  - `Codm()` - کد ملی دانشجو
  - `PersonnelId()` - شناسه پرسنلی
  - `IsEmployee()` / `IsStudent()` - نوع کاربر
  - `IsAuthorizedAsync(permission)` - بررسی دسترسی
- استفاده در همه Use Case ها برای Authorization و Audit
- پیشنهاد: استفاده از فیلدهای Caching موجود

---

#### ✅ DateTimeService.md
**مسیر**: `docs/files/Csis.Admission.Services/`

**محتوا**:
- Abstraction برای `DateTime` (Mock-able)
- Properties:
  - `Now` - تاریخ/زمان جاری
  - `NowPersian` - تاریخ شمسی
  - `NowUtc` - UTC
  - `Today` / `TodayUtc`
- چرا نه `DateTime.Now`؟ قابل Mock در Unit Tests
- استفاده در Audit Logging، تولید TrackingCode، فیلترها

---

#### ✅ TrackingCodeService.md
**مسیر**: `docs/files/Csis.Admission.Services/`

**محتوا**:
- Generic Service برای تولید کد رهگیری یکتا
- 3 نوع تولید:
  - `GetRandomTrackingCodeAsync(length)` - تصادفی عددی
  - `GetTimeBasedTrackingCodeAsync()` - بر اساس تاریخ/زمان شمسی
  - `GetTrackingCodeAsync(generator)` - Generator سفارشی
- بررسی یکتا بودن در DB
- Retry Pattern (تا 15 بار)
- Random Delay برای کاهش Race Condition
- Use Case: UC-030 (تشکیل پرونده)

---

#### ✅ CacheKeyService.md
**مسیر**: `docs/files/Csis.Admission.Services/Cache/`

**محتوا**:
- تولید کلیدهای استاندارد Cache
- فرمت‌ها:
  - Entity: `{Entity}_{Id}`
  - DTO: `{Entity}_{Dto}_{Id}`
  - Custom: `{Entity}__CUSTOM__{Key}`
  - Wildcard: `{Entity}_*_{Id}` (برای Invalidation)
- استفاده در Repository و Query Handlers
- Invalidate بعد از Update/Delete

---

#### ✅ README.md (Services)
**مسیر**: `docs/files/Csis.Admission.Services/`

**محتوا**:
- فهرست کامل تمام سرویس‌ها (~20 سرویس)
- دسته‌بندی:
  - Core Services (2 سرویس) - 100% مستند ✅
  - Utility Services (2 سرویس) - 100% مستند ✅
  - Cache Services (3 سرویس) - 33% مستند 🚧
  - External Services (7+ سرویس) - 0% مستند ❌
  - Background Services (2 سرویس) - 0% مستند ❌
- الگوهای رایج (Wrapper، Generic، Primary Constructor)
- نکات DI و Best Practices

---

## 🎯 ویژگی‌های مستندات

تمام مستندات با ساختار یکسان و جامع:

1. **هدف (Purpose)**: توضیح کاربرد
2. **ورودی/خروجی**: پارامترها و نتیجه
3. **جریان اجرا**: نمودار Mermaid
4. **قوانین کسب‌وکار**: Business Rules
5. **وابستگی‌ها**: Dependencies
6. **الگوهای طراحی**: Design Patterns
7. **نکات امنیتی**: Security Considerations
8. **عملکرد**: Performance و پیشنهادات بهبود
9. **خطاها**: Error Handling
10. **تست‌پذیری**: نمونه Unit Tests
11. **مثال استفاده**: Usage Examples
12. **Use Cases**: ارتباط با UC ها
13. **نتیجه‌گیری**: خلاصه نقاط قوت/ضعف

---

## 📈 پیشرفت کلی پروژه

### قبل از این Session:
- Features: 117/394 (29.7%)
- Services: 0/20 (0%)

### بعد از این Session:
- **Features: 120/394 (30.5%)** ✅
- **Services: 5/20 (25%)** ✅

### پیشرفت:
- ➕ 3 Feature Query
- ➕ 5 Service
- ➕ 1 README
- **مجموع: 9 فایل جدید**

---

## 🔄 ادامه کار

### Priority 1 (فوری):
1. **Features پرکاربرد**:
   - People (Queries/Commands)
   - Marriages
   - Divorce
   - Pregnancies
   - Educations

2. **Services حیاتی**:
   - NotificationService
   - PersonInfoService
   - StudentDataService
   - DistributedCacheService

### Priority 2 (مهم):
3. **Features متوسط**:
   - Towns, Rurals, Schools
   - EliteLevels, EliteTypes
   - ExcellentEducationLevels
   - Settings

4. **Services مکمل**:
   - EmployeeDataService
   - SettingsService
   - MemoryCacheService
   - Background Services

### Priority 3 (نرمال):
5. **باقی Features** (269 فایل)
6. **باقی Services** (10+ سرویس)

---

## 💡 نکات مهم

### برای توسعه‌دهندگان:
1. **الگوی یکسان**: تمام مستندات از ساختار GetCountriesQuery.md پیروی می‌کنند
2. **Mermaid Diagrams**: نمودارهای جریان برای درک بهتر
3. **Best Practices**: پیشنهادات بهبود برای هر بخش
4. **Use Cases**: ارتباط با UC های سیستم

### برای نگهداری:
1. **README.md**: فهرست کامل و دسته‌بندی شده
2. **لینک‌های داخلی**: ارتباط بین مستندات
3. **وضعیت**: ✅ کامل، 🚧 در حال انجام، ❌ نشده

---

## 📞 تماس و پشتیبانی

برای سوالات یا مشکلات:
- مستندات اصلی: `/docs/README.md`
- Use Cases: `/docs/use-cases/`
- Features Index: `/docs/index/Features.md`

---

**تهیه‌کننده**: GitHub Copilot  
**تاریخ**: 1404/10/12  
**وضعیت**: ✅ تکمیل شده

</div>
