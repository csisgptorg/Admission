# مستندات لایه Services

**مسیر**: `Csis.Admission.Services/`

## 📋 فهرست

این پوشه شامل مستندات **لایه سرویس‌ها** (Services Layer) سیستم پذیرش است.

---

## 🏗️ معماری

لایه Services شامل سرویس‌هایی است که:
- منطق کسب‌وکار مشترک را پیاده‌سازی می‌کنند
- با سرویس‌های خارجی ارتباط برقرار می‌کنند
- Utilities و Helper Functions فراهم می‌کنند
- Cross-Cutting Concerns مانند Caching، Logging، Authentication را مدیریت می‌کنند

---

## 📁 دسته‌بندی سرویس‌ها

### 1. سرویس‌های اصلی (Core Services)

#### ✅ BirthCertService
**مسیر**: [`BirthCertService.md`](BirthCertService.md)

**هدف**: دریافت اطلاعات شناسنامه‌ای از ثبت احوال و المصطفی

**متدها**:
- `Iranian(nationalCode, birthDate)` - اطلاعات ایرانیان
- `NonIranian(yektaCode)` - اطلاعات غیرایرانیان

**Use Cases**:
- UC-011: بروزرسانی شناسنامه‌ای
- UC-012: سینک با ثبت احوال
- UC-030-Step04: تشکیل پرونده

---

#### ✅ CurrentUserService
**مسیر**: [`CurrentUserService.md`](CurrentUserService.md)

**هدف**: دسترسی به اطلاعات کاربر جاری (لاگین شده)

**متدها**:
- `GetUserIdAsync()` - دریافت شناسه کاربر
- `Codm()` - دریافت کد ملی دانشجو
- `PersonnelId()` - دریافت شناسه پرسنلی
- `IsEmployee()` / `IsStudent()` - بررسی نوع کاربر
- `IsAuthorizedAsync(permission)` - بررسی دسترسی
- `GetEmployeeBranchIdAsync()` - دریافت شعبه

**Use Cases**: تقریباً همه Use Case ها (Authorization، Audit)

---

### 2. سرویس‌های Utility

#### ✅ DateTimeService
**مسیر**: [`DateTimeService.md`](DateTimeService.md)

**هدف**: دسترسی به تاریخ و زمان جاری (Mock-able)

**Properties**:
- `Now` - تاریخ/زمان جاری
- `NowPersian` - تاریخ شمسی
- `NowUtc` - UTC
- `Today` / `TodayUtc` - تاریخ امروز

**چرا نه DateTime.Now؟** قابل Mock در Unit Tests

---

#### ✅ TrackingCodeService
**مسیر**: [`TrackingCodeService.md`](TrackingCodeService.md)

**هدف**: تولید کد رهگیری یکتا

**متدها**:
- `GetRandomTrackingCodeAsync(length)` - کد تصادفی عددی
- `GetTimeBasedTrackingCodeAsync()` - کد بر اساس تاریخ/زمان
- `GetTrackingCodeAsync(generator)` - کد با Generator سفارشی

**ویژگی‌ها**:
- بررسی یکتا بودن در DB
- Retry تا 15 بار
- Random Delay برای کاهش Race Condition

---

### 3. سرویس‌های Cache

#### ✅ CacheKeyService
**مسیر**: [`Cache/CacheKeyService.md`](Cache/CacheKeyService.md)

**هدف**: تولید کلیدهای استاندارد Cache

**متدها**:
- `GetEntityKey<TEntity>(id)` → `"Entity_123"`
- `GetDtoKey<TEntity, TDto>(id)` → `"Entity_DtoName_123"`
- `GetCustomKey<TEntity>(key)` → `"Entity__CUSTOM__key"`
- `GetDtoWildcardKey(type, id)` → `"Entity_*_123"` (برای Invalidation)

**چرا؟** استانداردسازی و یکپارچگی کلیدهای Cache

---

#### 🚧 DistributedCacheService
**وضعیت**: هنوز مستندسازی نشده

**هدف**: Wrapper برای Redis Distributed Cache

---

#### 🚧 MemoryCacheService
**وضعیت**: هنوز مستندسازی نشده

**هدف**: Wrapper برای In-Memory Cache

---

### 4. سرویس‌های External Integration (مستندسازی نشده)

- **NotificationService**: ارسال اعلان‌ها
- **PersonInfoService**: اطلاعات افراد
- **StudentDataService**: داده‌های دانشجو
- **EmployeeDataService**: داده‌های کارمند
- **CsisWsmService**: ارتباط با Web Services
- **CsisHealthInsuranceService**: بیمه سلامت
- **CsisSupInsuranceService**: بیمه تکمیلی

---

### 5. سرویس‌های Background (مستندسازی نشده)

- **SyncPermissionsBackgroundService**: سینک دسترسی‌ها
- **SendNotificationBackgroundService**: ارسال اعلان‌ها

---

### 6. سرویس‌های دیگر (مستندسازی نشده)

- **SettingsService**: مدیریت تنظیمات
- **IpAddressService**: دریافت IP کاربر
- **DistributedPubSubService**: Pub/Sub توزیع شده
- **Request/Service**: مدیریت درخواست‌ها
- **CaseFillingRequest/Service**: تشکیل پرونده

---

## 📊 آمار مستندسازی

| دسته | تعداد کل | مستند شده | درصد |
|------|----------|------------|------|
| Core Services | 2 | 2 | 100% ✅ |
| Utility Services | 2 | 2 | 100% ✅ |
| Cache Services | 3 | 1 | 33% 🚧 |
| External Services | 7+ | 0 | 0% ❌ |
| Background Services | 2 | 0 | 0% ❌ |
| Other Services | 5+ | 0 | 0% ❌ |
| **مجموع** | **~20** | **5** | **25%** |

---

## 🎯 الگوهای رایج

### 1. Wrapper Pattern
بسیاری از سرویس‌ها Wrapper برای کتابخانه‌های خارجی هستند:
- `DateTimeService` → `DateTime`
- `CurrentUserService` → `ICsisAuthenticatedUserService`
- `CacheServices` → `IDistributedCache`, `IMemoryCache`

**چرا؟**
- Abstraction
- Testability (Mock-able)
- Consistency

---

### 2. Generic Service Pattern
برخی سرویس‌ها Generic هستند:
- `TrackingCodeService<TEntity>`
- `CacheKeyService<TKey>`
- `Repository<TEntity, TKey>`

**چرا؟**
- Reusability
- Type Safety
- Code Reduction

---

### 3. Primary Constructor (C# 12)
تمام سرویس‌ها از Primary Constructor استفاده می‌کنند:
```csharp
internal sealed class MyService(IDependency dependency) : IMyService
{
    // dependency به صورت خودکار field می‌شود
}
```

---

## 🔧 Dependency Injection

تمام سرویس‌ها در `DependencyInjection.cs` ثبت می‌شوند:

```csharp
services.AddScoped<IDateTimeService, DateTimeService>();
services.AddScoped<ICurrentUserService, CurrentUserService>();
services.AddScoped<IBirthCertService, BirthCertService>();
services.AddScoped(typeof(ITrackingCodeService<>), typeof(TrackingCodeService<>));
services.AddScoped<ICacheKeyService, CacheKeyService>();
// ...
```

---

## ⚠️ نکات مهم

### 1. External Services
سرویس‌هایی که با سیستم‌های خارجی ارتباط دارند:
- ⚠️ نیاز به **Error Handling** قوی
- ⚠️ نیاز به **Retry Policy**
- ⚠️ نیاز به **Circuit Breaker**
- ⚠️ نیاز به **Timeout Configuration**

### 2. Caching
- ✅ استفاده از `CacheKeyService` برای تولید کلید
- ✅ استفاده از Distributed Cache برای Scale-Out
- ✅ تنظیم TTL مناسب
- ⚠️ Invalidation بعد از Update/Delete

### 3. Background Services
- ✅ استفاده از `IHostedService`
- ⚠️ مدیریت Exception ها
- ⚠️ Graceful Shutdown

---

## 📝 To-Do (مستندسازی باقیمانده)

### Priority 1 (بحرانی):
- [ ] NotificationService
- [ ] CsisWsmService (External)
- [ ] DistributedCacheService
- [ ] MemoryCacheService

### Priority 2 (مهم):
- [ ] PersonInfoService
- [ ] StudentDataService
- [ ] EmployeeDataService
- [ ] SettingsService

### Priority 3 (متوسط):
- [ ] Background Services
- [ ] Insurance Services
- [ ] Request/Service
- [ ] CaseFillingRequest/Service

---

## 🔗 لینک‌های مرتبط

- [مستندات Features](../Csis.Admission.Application/Features/)
- [مستندات اصلی](../../README.md)
- [مستندات Use Cases](../../use-cases/)

---

**آخرین بروزرسانی**: 2026-01-02  
**نسخه**: 1.0  
**وضعیت**: 25% تکمیل شده
