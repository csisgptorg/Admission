<div dir="rtl">

# SyncDependentBirthCertCommand

## 📄 اطلاعات کلی

**مسیر فایل:**
```
Csis.Admission.Application/Features/Students/Iranian/Commands/SyncDependentBirthCertCommand.cs
```

**Feature:** Students (Dependents)  
**نوع:** Command  
**هدف:** همگام‌سازی اطلاعات شناسنامه‌ای افراد تحت تکفل با وب سرویس‌های خارجی

---

## 🎯 هدف (Purpose)

این Command برای **همگام‌سازی خودکار اطلاعات شناسنامه‌ای افراد تحت تکفل** از منابع معتبر استفاده می‌شود:
- **افراد ایرانی**: از وب سرویس **ثبت احوال**
- **افراد غیرایرانی**: از وب سرویس **المصطفی**

**شباهت با SyncStudentBirthCertCommand:**
- الگوی Two-Step Confirmation یکسان
- منطق کاری مشابه
- تفاوت: برای تحت تکفل (نه خود دانشجو)

---

## 📝 ساختار Command

### ورودی (Request)

```csharp
public sealed record SyncDependentBirthCertCommand : IRequest
{
    public long Id { get; init; }       // شناسه تحت تکفل
    
    [JsonIgnore]
    public bool? Confirmed { get; set; } // تایید کاربر
}
```

### خروجی (Response)

```csharp
void  // یا ConfirmedValidationException
```

---

## 🔄 جریان اجرا (Execution Flow)

### الگوی Two-Step Confirmation:

```
مرحله 1 (Confirmed = null):
1. دریافت اطلاعات تحت تکفل (DependentSummary)
2. دریافت اطلاعات از وب سرویس
3. پرتاب ConfirmedValidationException (نمایش به کاربر)
4. کاربر بررسی می‌کند

مرحله 2 (Confirmed = true):
1. دریافت مجدد اطلاعات از وب سرویس
2. بروزرسانی بر اساس Citizenship:
   ├─> Iranian → SetDependentWithSabteAhvalData
   └─> NonIranian → SetDependentWithAlmostafaData
```

---

## 📦 وابستگی‌ها (Dependencies)

### سرویس‌ها
- `IRepository<DependentSummary, long>`: دسترسی به افراد تحت تکفل
- `IStudentRepository`: عملیات بروزرسانی
- `ICurrentUserService`: اطلاعات کاربر جاری
- `IBirthCertService`: سرویس وب ثبت احوال/المصطفی

### Entities
- `DependentSummary`: خلاصه اطلاعات تحت تکفل

---

## ⚙️ قوانین کسب‌وکار (Business Rules)

### BR-1: الگوی Two-Step Confirmation
- **مرحله 1**: نمایش تغییرات به کاربر
- **مرحله 2**: اعمال تغییرات پس از تایید
- **هدف**: جلوگیری از تغییرات ناخواسته

### BR-2: افراد ایرانی
- استفاده از کد ملی و تاریخ تولد
- دریافت از ثبت احوال
- شامل: نام، نام خانوادگی، نام پدر، شناسنامه، جنسیت، سادات، فوت

### BR-3: افراد غیرایرانی
- استفاده از کد یکتا (YektaCode)
- دریافت از المصطفی
- ⚠️ **همان باگ**: PassportNumber و ResidenceExpireDate هاردکد!

---

## 🚨 مشکلات و نکات (Issues & Notes)

### ⚠️ باگ مشابه با SyncStudentBirthCertCommand

```csharp
// برای غیرایرانیان
var sync = new SetDependentWithAlmostafaDataRepoCommand(
    dependent.Codm, dependent.Id, info.FirstName, info.LastName,
    info.FatherName, 
    PassportNumber: "!!!",  // ⚠️ هاردکد
    info.Nationality, 
    ResidenceExpireDate: 0,  // ⚠️ هاردکد
    ...
);
```

### ⚠️ فراخوانی دوباره وب سرویس
- در هر دو مرحله وب سرویس فراخوانی می‌شود
- پیشنهاد: Cache کردن نتیجه مرحله اول

### ✅ الگوی خوب Two-Step
- UX عالی
- جلوگیری از خطا
- شفافیت برای کاربر

---

## 🔄 مقایسه با Command مشابه

### SyncStudentBirthCertCommand vs SyncDependentBirthCertCommand

| جنبه | SyncStudentBirthCertCommand | SyncDependentBirthCertCommand |
|------|---------------------------|------------------------------|
| **Entity** | Student | Dependent |
| **شناسه** | Codm (int) | Id (long) |
| **Repository** | StudentSummary | DependentSummary |
| **SP (Iranian)** | SetStudentWithSabteAhvalData | SetDependentWithSabteAhvalData |
| **SP (NonIranian)** | SetStudentWithAlmostafaData | SetDependentWithAlmostafaData |
| **منطق** | یکسان | یکسان |
| **باگ** | دارد | دارد (مشابه) |

---

## 💡 پیشنهادات بهبود

### پیشنهاد 1: استخراج کد مشترک
```csharp
// Base class یا Helper method
public abstract class SyncBirthCertCommandBase<TEntity, TId>
{
    protected async Task<IdentityInfo> GetIdentityInfo(TEntity entity, ...)
    {
        // منطق مشترک دریافت از وب سرویس
    }
    
    protected async Task ValidateAndConfirm(bool? confirmed, IdentityInfo info)
    {
        // الگوی Two-Step مشترک
    }
}
```

### پیشنهاد 2: رفع باگ مقادیر هاردکد
- دریافت `PassportNumber` و `ResidenceExpireDate` از وب سرویس
- یا حداقل مقادیر پیش‌فرض منطقی‌تر

### پیشنهاد 3: Cache کردن نتیجه وب سرویس
- ذخیره نتیجه مرحله 1 در Cache
- استفاده در مرحله 2

---

## 📚 مستندات مرتبط

### Commands مرتبط
- `SyncStudentBirthCertCommand`: نسخه دانشجو (بسیار مشابه)
- `UpdateDependentBirthCertCommand`: بروزرسانی دستی
- `SyncDependentBirthCertByIdCommand`: همگام‌سازی با Id مشخص

### Services
- `IBirthCertService`: سرویس وب ثبت احوال/المصطفی

---

## 📊 خلاصه

| جنبه | وضعیت | نمره |
|------|-------|------|
| **عملکرد** | متوسط (فراخوانی دوباره WS) | 6/10 |
| **کیفیت کد** | متوسط (DRY نقض شده، مقادیر هاردکد) | 5/10 |
| **Business Logic** | خوب (الگوی Confirmation) | 8/10 |
| **User Experience** | عالی | 9/10 |

**توصیه کلی**: Command از UX خوبی برخوردار است اما کد مشترک با SyncStudentBirthCertCommand باید Extract شود و باگ‌ها رفع شود.

</div>
