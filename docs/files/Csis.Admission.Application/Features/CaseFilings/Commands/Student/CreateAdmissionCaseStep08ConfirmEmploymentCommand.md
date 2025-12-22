<div dir="rtl">

# CreateAdmissionCaseStep08ConfirmEmploymentCommand.cs

**مسیر**: `Csis.Admission.Application/Features/CaseFilings/Commands/Student/CreateAdmissionCaseStep08ConfirmEmploymentCommand.cs`

---

## 1. Purpose (هدف)

**گام هشتم Wizard**: دریافت و ذخیره اطلاعات شغلی و بیمه‌ای دانشجو. این اطلاعات برای محاسبه امتیاز پذیرش و مشخص کردن نوع بیمه استفاده می‌شود.

---

## 2. خلاصه اتفاقات

**جریان اصلی**:
1. دریافت اطلاعات جامع اشتغال (13 فیلد)
2. ذخیره کل Command در `Payloads` (بدون اعتبارسنجی پیچیده)
3. تغییر `CaseStep` به `EmploymentVerified`

---

## 3. اجزای اصلی

### Command:
```csharp
sealed record ConfirmEmploymentCommand : IRequest
{
    Guid Token
    bool? HasIncome                         // آیا درآمد دارد؟
    bool IsEmployee                         // آیا کارمند است؟
    string? EmployeeName                    // نام محل کار
    string? EmployeeAddress                 // آدرس محل کار
    bool? HasSufficientIncome               // آیا درآمد کافی دارد؟
    bool? HasAnotherBaseInsurance           // بیمه پایه دیگر
    string? InsurancePlaceName              // نام محل بیمه
    string? InsurancePlaceAddress           // آدرس محل بیمه
    bool? HasAnotherSupInsurance            // بیمه تکمیلی دیگر
    bool? IsEmployeeInHowze                 // اشتغال در حوزه
    EmploymentHowzeType? HowzeTypeId        // نوع اشتغال حوزه
    bool? IsRetried                         // بازنشسته؟
    EmploymentInsuranceType? InsuranceTypeId // نوع بیمه
    EmploymentReference? Reference          // مرجع اشتغال
    Guid? FileId                            // فایل پیوست
}
```

### Handler:
- **ساده‌ترین Handler در Wizard**
- فقط ذخیره `request` کامل در `Payloads`

---

## 4. Flow

```
1. بازیابی AdmissionCaseUser
   └─> GetByIdAsTracking(Token)

2. ذخیره اطلاعات
   ├─> CaseStep = EmploymentVerified
   ├─> Payloads += request (کل Command)
   └─> UpdateAsync()
```

---

## 5. Business Rules

### BR-1: بدون اعتبارسنجی پیچیده
- این مرحله **هیچ اعتبارسنجی خارجی** ندارد
- فقط ذخیره اطلاعات

### BR-2: Nullable Fields
- اکثر فیلدها `nullable` هستند (اختیاری)

### BR-3: کاربرد اطلاعات
- **محاسبه امتیاز**: درآمد، اشتغال در حوزه
- **تعیین نوع بیمه**: بیمه پایه، تکمیلی
- **ارزیابی نیاز**: درآمد کافی، بازنشستگی

---

## 6. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `CommandValidationException` | Token نامعتبر | "شناسه نامعتبر است." |

**یادداشت**: اعتبارسنجی فیلدها توسط `ConfirmEmploymentValidator` انجام می‌شود (FluentValidation).

---

## 7. Risks & Notes

### Code Quality:
- ✅ ساده و واضح
- ✅ تمام اطلاعات را ذخیره می‌کند

### Validation:
- اعتبارسنجی‌های کسب‌وکاری در Validator جدا است
- **پیشنهاد**: بررسی Validator برای قوانین پیچیده

---

## 8. Use Case های مرتبط

- **UC-030**: تشکیل پرونده (Wizard)
  - **مرحله 8**: اطلاعات اشتغال و بیمه
  - مرحله قبل: [Step07 - حساب بانکی](./CreateAdmissionCaseStep07ConfirmBankAccountInformationCommand.md)
  - مرحله بعد: [Step09 - تکمیل اطلاعات](#)

---

## 9. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | گام هشتم Wizard: اشتغال و بیمه |
| **ورودی** | Token + 13 فیلد اطلاعات شغلی |
| **خروجی** | Unit |
| **اعتبارسنجی** | فقط Validator (بدون سرویس خارجی) |
| **Payload** | کل Command ذخیره می‌شود |
| **State Transition** | → EmploymentVerified |
| **پیچیدگی** | ⭐ ساده‌ترین Step |

---

**یادداشت**: این ساده‌ترین Step از Wizard است - فقط ذخیره اطلاعات بدون اعتبارسنجی خارجی.

</div>
