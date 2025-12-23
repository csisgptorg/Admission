<div dir="rtl">

# CalculateExtensionCaseTimeQuery

## 📄 اطلاعات کلی

**مسیر:** `Features/Students/Iranian/Queries/CalculateExtensionCaseTimeQuery.cs`  
**نوع:** Query  
**هدف:** بررسی امکان تمدید پرونده

---

## 🎯 هدف

**بررسی** اینکه آیا پرونده دانشجو نیاز به تمدید دارد یا خیر:
- اگر تاریخ انقضا < 3 ماه آینده → نیاز به تمدید (false)
- اگر تاریخ انقضا >= 3 ماه آینده → نیازی نیست (true)

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record CalculateExtensionCaseTimeQuery(int Codm) : IRequest<bool>;
```

**خروجی:**
```csharp
bool
// true: پرونده هنوز معتبر است (3+ ماه باقی مانده)
// false: نیاز به تمدید دارد (کمتر از 3 ماه)
```

---

## 🔄 جریان اجرا

```
1. دریافت اطلاعات پرونده
   └─> StudentSummaryCaseDto

2. بررسی مسدودی
   ├─> اگر IsBlock == true
   └─> پرتاب Exception

3. محاسبه زمان باقیمانده
   ├─> مقایسه CaseValidityDate با (الان + 3 ماه)
   └─> بازگشت نتیجه
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: پنجره 3 ماهه
```csharp
CaseValidityDate >= (DateTime.Now + 3 Months)
```
- اگر بیش از 3 ماه باقی مانده: نیاز به تمدید ندارد
- اگر کمتر از 3 ماه: باید تمدید شود

### BR-2: پرونده مسدود
```csharp
if (founded.IsBlock)
    throw new CommandValidationException("پرونده مسدود");
```
- پرونده مسدود نمی‌تواند تمدید شود
- باید ابتدا رفع مسدودی شود

---

## 💡 نکات

### ⚠️ نام گمراه‌کننده
```csharp
// نام: CalculateExtensionCaseTimeQuery
// واقعیت: بررسی می‌کند، محاسبه نمی‌کند!
```
**نام بهتر:** `CanExtendCaseQuery` یا `NeedsExtensionQuery`

### ⚠️ Dependency غیرضروری
```csharp
IMediator mediator,  // ⚠️ استفاده نمی‌شود
ICsisAuthenticatedUserService authenticatedUserService  // ⚠️ استفاده نمی‌شود
```

---

## 🧪 Use Case

### UC-017: بررسی نیاز به تمدید

**Actor:** دانشجو/کارمند

**Main Flow:**
1. سیستم این Query را فراخوانی می‌کند
2. اگر true: "پرونده شما تا X تاریخ معتبر است"
3. اگر false: "پرونده شما نیاز به تمدید دارد"

---

## 📚 مستندات مرتبط

- `StudentExtensionCaseCommand`: تمدید پرونده
- `ManualStudentExtensionCaseCommand`: تمدید دستی

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Business Logic** | 9/10 |
| **Naming** | 4/10 (گمراه‌کننده) |
| **Dependencies** | 5/10 (غیرضروری) |

**پیشنهاد:** تغییر نام و حذف dependencies استفاده نشده

</div>
