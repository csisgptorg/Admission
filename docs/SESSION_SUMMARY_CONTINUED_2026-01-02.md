<div dir="rtl">

# خلاصه Session مستندسازی - ادامه دوم

**تاریخ**: 1404/10/12 (2026-01-02)  
**Session**: ادامه دوم  
**کامیت‌ها**: 192df01, 81c408d

---

## 📊 خلاصه آماری

### فایل‌های ایجاد شده در این Session (9 فایل):

#### Session قبل (192df01):
1. GetPortionsQuery.md
2. GetTownsQuery.md
3. GetRuralsQuery.md
4. GetSchoolsQuery.md
5. PersonInfoService.md

#### این Session (81c408d):
6. GetEliteLevelsQuery.md
7. GetEliteTypesQuery.md
8. GetEducationYearsQuery.md
9. GetFamilyByNationalCodeQuery.md
10. SettingsService.md

---

## 📁 دسته‌بندی مستندات جدید

### 1. سلسله مراتب آدرس (کامل ✅)

تمام سطوح آدرس ایران مستندسازی شدند:

```
استان (Province) → شهر (City) → حوزه (Portion) → بخش (Town) → روستا (Rural)
      ✅               ✅              ✅               ✅             ✅
```

**ویژگی**: تمامی این Queries از الگوی **Cascade Dropdown** پشتیبانی می‌کنند.

---

### 2. Lookup/Master Data (کامل ✅)

تمام Queries مربوط به داده‌های پایه و ثابت:

| Query | کاربرد |
|-------|---------|
| GetCountriesQuery | کشورها |
| GetProvincesQuery | استان‌های ایران (31 استان) |
| GetCitiesQuery | شهرها (فیلتر بر اساس استان) |
| GetPortionsQuery | حوزه‌ها (فیلتر بر اساس شهر) |
| GetTownsQuery | بخش‌ها (فیلتر بر اساس حوزه) |
| GetRuralsQuery | روستاها (فیلتر بر اساس حوزه) |
| GetBranchesQuery | شعب حوزه علمیه |
| GetSchoolsQuery | مدارس |
| GetEliteLevelsQuery | سطوح نخبگی (ملی، بین‌المللی، ...) |
| GetEliteTypesQuery | انواع نخبگی |
| GetEducationYearsQuery | سال‌های تحصیلی |

**جمع**: 11 Query ✅

---

### 3. Family/Insurance Queries

| Query | کاربرد |
|-------|---------|
| GetFamilyByNationalCodeQuery | دریافت اطلاعات خانواده (دانشجو + افراد تحت تکفل) برای بیمه سلامت |

**ویژگی**: ترکیب دانشجو و افراد تحت تکفل در یک لیست با مرتب‌سازی منطقی

---

### 4. Services

| Service | کاربرد |
|---------|---------|
| BirthCertService | ارتباط با ثبت احوال و المصطفی |
| CurrentUserService | اطلاعات کاربر جاری |
| DateTimeService | تاریخ و زمان (Mock-able) |
| TrackingCodeService | تولید کد رهگیری یکتا |
| CacheKeyService | تولید کلیدهای Cache |
| PersonInfoService | تکمیل اطلاعات کارمندان در DTO ها |
| SettingsService | مدیریت تنظیمات با JSON Storage و Caching |

**جمع**: 7 Service ✅

---

## 🎯 ویژگی‌های برجسته

### 1. SettingsService

سرویس جامع مدیریت تنظیمات با قابلیت‌های:
- **Generic**: پشتیبانی از انواع مختلف Settings
- **JSON Storage**: ذخیره تنظیمات به صورت JSON در DB
- **Caching**: Cache خودکار با Invalidation
- **Versioning**: نسخه‌گذاری برای Optimistic Concurrency
- **Key Suffix**: پشتیبانی از تنظیمات چند نمونه‌ای (مثل تنظیمات کاربر)
- **Default Values**: مقادیر پیش‌فرض خودکار

---

### 2. GetFamilyByNationalCodeQuery

Query ترکیبی برای بیمه سلامت:
- دریافت دانشجو + افراد تحت تکفل
- مرتب‌سازی بر اساس نسبت (Relation)
- وضعیت فعال/غیرفعال
- استفاده از Collection Expressions (C# 12)

---

### 3. Cascade Dropdown Pattern

تمام Queries مربوط به آدرس از این الگو پشتیبانی می‌کنند:
```javascript
// انتخاب استان → دریافت شهرها
provinceSelect.onChange → GetCitiesQuery(provinceId)

// انتخاب شهر → دریافت حوزه‌ها
citySelect.onChange → GetPortionsQuery(cityId)

// انتخاب حوزه → دریافت بخش‌ها
portionSelect.onChange → GetTownsQuery(portionId)
```

---

## 📈 پیشرفت کلی

### قبل از این Session:
- Features: 125/394 (31.7%)
- Services: 6/20 (30%)

### بعد از این Session:
- **Features: 134/394 (34%)** ✅
- **Services: 7/20 (35%)** ✅

### پیشرفت Session:
- ➕ 9 فایل جدید
- **Features**: +9 Query
- **Services**: +2 Service (PersonInfoService در session قبل، SettingsService در این session)

---

## 🔄 باقیمانده

### Features (260 فایل):
**Priority 1**:
- People (Queries/Commands) - 15+ فایل
- Marriages - 10 فایل
- Divorce - 9 فایل
- Pregnancies - 4 فایل
- Documents - 3 فایل

**Priority 2**:
- Educations - 8 فایل
- Teaches, Preaches, Researches - 20+ فایل
- Elites, Excellents, Veterans - 15+ فایل
- ImamJamaat - 19 فایل

### Services (13 سرویس):
**Priority 1**:
- NotificationService
- StudentDataService
- EmployeeDataService
- DistributedCacheService
- MemoryCacheService

**Priority 2**:
- Background Services (2 سرویس)
- External Services (WSM, Insurance)
- IpAddressService
- HttpRequest/SendService

---

## 💡 نکات کلیدی

### 1. تکمیل Lookup Queries
تمام Queries مربوط به داده‌های پایه و ثابت مستندسازی شدند:
- ✅ داده‌های جغرافیایی (آدرس)
- ✅ داده‌های آموزشی (مدارس، سال‌های تحصیلی)
- ✅ داده‌های نخبگی (سطوح و انواع)
- ✅ شعب حوزه

---

### 2. الگوهای مشترک

**Cascade Dropdown**:
- GetCitiesQuery(provinceId)
- GetPortionsQuery(cityId)
- GetTownsQuery(portionId)
- GetRuralsQuery(portionId)

**Collection Expressions (C# 12)**:
```csharp
return [.. result];
return [.. studentDto, .. dependentDto];
```

**Primary Constructor (C# 12)**:
```csharp
internal sealed class Handler(IRepository repo) : IRequestHandler<Query, Dto[]>
```

---

### 3. پیشنهادات بهبود مشترک

برای تمام Lookup Queries:
⚠️ **افزودن Caching**: این داده‌ها نادراً تغییر می‌کنند

```csharp
var cacheKey = "lookup_countries";
return await _cache.GetOrCreateAsync(cacheKey, async entry => 
{
    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
    var result = await _repo.GetAllAsync<CountryDto>();
    return [.. result];
});
```

---

## 📊 آمار نهایی Session

| آیتم | تعداد |
|------|-------|
| فایل‌های جدید | 9 |
| کامیت‌ها | 2 |
| Features مستند شده | 134/394 (34%) |
| Services مستند شده | 7/20 (35%) |
| Lookup Queries کامل | 11/11 (100%) ✅ |
| سلسله مراتب آدرس کامل | 5/5 (100%) ✅ |

---

## 📞 لینک‌های مرتبط

- [مستندات Features](../files/Csis.Admission.Application/Features/)
- [مستندات Services](../files/Csis.Admission.Services/)
- [README اصلی](../README.md)

---

**آخرین بروزرسانی**: 1404/10/12  
**وضعیت**: در حال پیشرفت  
**پیشرفت کلی**: 34% ✅

</div>
