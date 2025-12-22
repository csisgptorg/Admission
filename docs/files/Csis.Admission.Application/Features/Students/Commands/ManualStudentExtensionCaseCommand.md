<div dir="rtl">

# ManualStudentExtensionCaseCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/Students/Commands/ManualStudentExtensionCaseCommand.cs`  
**نوع:** Command  
**هدف:** تمدید دستی پرونده دانشجو توسط کارمند

---

## 🎯 هدف

تمدید **دستی** پرونده با امکان:
- تعیین دلایل تمدید
- تعیین تاریخ انقضای دلخواه
- فقط توسط کارمند (برخلاف تمدید خودکار)

**تفاوت با StudentExtensionCaseCommand:**
- این Command: دستی، توسط کارمند، تاریخ دلخواه
- StudentExtensionCaseCommand: خودکار، محاسبه‌ای، تاریخ پیش‌فرض

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record ManualStudentExtensionCaseCommand(
    int Codm,
    List<int> CaseValidityReasonId,  // دلایل تمدید
    string CaseValidityDate           // تاریخ انقضا (رشته: 1403/12/29)
) : IRequest<long>;
```

**خروجی:** `long` (شناسه تاریخچه)

---

## 🔄 جریان اجرا

```
1. دریافت اطلاعات کاربر (UserId, PersonnelId)

2. تبدیل دلایل به رشته
   └─> string.Join(",", CaseValidityReasonId)

3. تبدیل تاریخ به Int
   └─> "1403/12/29" → 14031229

4. ایجاد Command مخزن
   └─> ManualStudentExtensionCaseCommandPrc

5. اجرای SP
   └─> ExtensionCaseCommand

6. بازگشت Id تاریخچه
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: فقط کارمند
- `DataSource = Employee` (همیشه)
- نیاز به PersonnelId
- دانشجو نمی‌تواند استفاده کند

### BR-2: دلایل متعدد
```csharp
List<int> CaseValidityReasonId
// مثال: [1, 3, 5] → "1,3,5"
```
- امکان ذکر چند دلیل
- Join با کاما

### BR-3: تاریخ دلخواه
- کارمند تاریخ را خودش تعیین می‌کند
- برخلاف تمدید خودکار که محاسبه می‌شود

---

## 🔒 امنیت

### Authorization
- ⚠️ **هیچ چک صریحی در کد نیست**
- باید Policy برای "ManualExtension" اضافه شود

### Validation
- ⚠️ **فاقد Validator**
- نیاز به FluentValidation:
  - `Codm > 0`
  - `CaseValidityReasonId` نباید خالی باشد
  - `CaseValidityDate` معتبر باشد
  - تاریخ در آینده باشد

---

## 💡 پیشنهادات بهبود

### پیشنهاد 1: Validator
```csharp
public class ManualStudentExtensionCaseCommandValidator 
    : AbstractValidator<ManualStudentExtensionCaseCommand>
{
    public ManualStudentExtensionCaseCommandValidator()
    {
        RuleFor(x => x.Codm)
            .GreaterThan(0);
            
        RuleFor(x => x.CaseValidityReasonId)
            .NotEmpty()
            .WithMessage("حداقل یک دلیل باید انتخاب شود");
            
        RuleFor(x => x.CaseValidityDate)
            .Must(BeValidPersianDate)
            .Must(BeFutureDate);
    }
}
```

### پیشنهاد 2: Authorization
```csharp
[Authorize(Policy = "ManualExtendCase")]
public async Task<long> Handle(...)
```

---

## 🧪 Use Case

### UC-016: تمدید دستی پرونده

**Actor:** کارمند

**Main Flow:**
1. کارمند دانشجو را جستجو می‌کند
2. دلایل تمدید را انتخاب می‌کند
3. تاریخ انقضای جدید را وارد می‌کند
4. سیستم پرونده را تمدید می‌کند
5. تاریخچه ثبت می‌شود

**Postconditions:**
- پرونده تا تاریخ جدید معتبر است
- دلایل تمدید ثبت شده

---

## 🔄 مقایسه با Command مشابه

| جنبه | ManualStudentExtensionCaseCommand | StudentExtensionCaseCommand |
|------|-----------------------------------|----------------------------|
| **Actor** | کارمند | دانشجو/سیستم |
| **تاریخ** | دلخواه | محاسبه شده |
| **دلایل** | چند دلیل | تک دلیل یا محاسبه‌ای |
| **Use Case** | موارد خاص | فرآیند عادی |

---

## 📚 مستندات مرتبط

- `StudentExtensionCaseCommand`: تمدید عادی
- `CalculateExtensionCaseTimeQuery`: محاسبه زمان تمدید

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **سادگی** | 10/10 |
| **Security** | 4/10 (فاقد Authorization) |
| **Validation** | 3/10 (فاقد Validator) |
| **Business Logic** | 8/10 |

**نیاز اصلی:** Validator + Authorization

</div>
