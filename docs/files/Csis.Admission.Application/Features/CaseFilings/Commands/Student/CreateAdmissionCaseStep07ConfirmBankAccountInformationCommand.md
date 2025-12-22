<div dir="rtl">

# CreateAdmissionCaseStep07ConfirmBankAccountInformationCommand.cs

**مسیر**: `Csis.Admission.Application/Features/CaseFilings/Commands/Student/CreateAdmissionCaseStep07ConfirmBankAccountInformationCommand.cs`

---

## 1. Purpose (هدف)

**گام هفتم Wizard**: تأیید اطلاعات حساب بانکی دانشجو. برای ایرانی‌ها اعتبارسنجی با سیستم **سیبا** انجام می‌شود.

---

## 2. خلاصه اتفاقات

**جریان اصلی**:
1. بررسی تکراری نبودن شماره حساب در سیستم
2. اعتبارسنجی بر اساس تابعیت:
   - **ایرانی**: سرویس سیبا (کد ملی + شماره حساب)
   - **غیرایرانی**: آپلود مدرک بانکی (الزامی)
3. ذخیره اطلاعات در `Payloads`
4. تغییر `CaseStep` به `BankAccountVerified`

---

## 3. اجزای اصلی

### Command:
```csharp
sealed record CreateBankAccountInformationCommand : IRequest
{
    Guid Token
    string BankAccountNumber     // شماره حساب بانکی
    Guid? FileId                 // فایل مدرک (برای غیرایرانی‌ها)
}
```

### Handler Dependencies:
- `IHttpContextAccessor` - تشخیص Dev Mode
- `IRepository<StudentSummary>` - بررسی تکراری
- `ICsisWsmService` - اعتبارسنجی سیبا
- `IRepository<AdmissionCaseUser, Guid>`

---

## 4. Flow

```
1. بررسی تکراری (فقط Production)
   if (!DevMode && BankAccountNumber موجود)
       └─> CommandValidationException("قبلا ثبت شده")

2. بازیابی AdmissionCaseUser
   └─> GetByIdAsTracking(Token)

3. switch (Citizenship)
   
   case Iranian:
   ├─> csisWsmService.ValidateSibaAccountNumber(NationalCode, BankAccount)
   ├─> اگر نامعتبر → Exception
   └─> Payloads += { BankAccountNumber }
   
   case NonIranian:
   ├─> بررسی FileId (الزامی)
   └─> Payloads += { BankAccountNumber, FileId }

4. به‌روزرسانی
   ├─> CaseStep = BankAccountVerified
   └─> UpdateAsync()
```

---

## 5. Business Rules

### BR-1: Uniqueness (یکتایی)
- شماره حساب در سیستم نباید تکراری باشد
- **استثنا**: در Dev Mode این چک انجام نمی‌شود

### BR-2: اعتبارسنجی بر اساس تابعیت
- **ایرانی**: اعتبارسنجی با سیستم سیبا (بانک مرکزی)
- **غیرایرانی**: آپلود مدرک بانکی الزامی است

### BR-3: سیبا (SIBA)
- شماره حساب باید متعلق به کد ملی وارد شده باشد

---

## 6. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `CommandValidationException` | شماره حساب تکراری | "شماره حساب بانکی وارد شده قبلا در سیستم ثبت شده است." |
| `CommandValidationException` | Token نامعتبر | "شناسه نامعتبر است." |
| `CommandValidationException` | سیبا نامعتبر | "شماره حساب بانکی وارد شده معتبر نمی باشد." |
| `CommandValidationException` | FileId خالی (غیرایرانی) | "آپلود مدرک بانکی برای اتباع غیر ایرانی الزامی می باشد" |
| `CommandValidationException` | تابعیت نامعتبر | "وضعیت تابعیت نامعتبر است." |

---

## 7. Risks & Notes

### امنیت:
- ✅ اعتبارسنجی با سیبا (ایرانی‌ها)
- ✅ بررسی تکراری شماره حساب
- ⚠️ **Bypass در Dev Mode** (تکراری چک نمی‌شود)

### وابستگی خارجی:
- ⚠️ سرویس سیبا (بانک مرکزی)
- **پیشنهاد**: Circuit Breaker + Retry

---

## 8. Use Case های مرتبط

- **UC-030**: تشکیل پرونده (Wizard)
  - **مرحله 7**: تأیید حساب بانکی
  - مرحله قبل: [Step06 - تصویر](./CreateAdmissionCaseStep06ConfirmStudentProfilePictureRequestCommand.md)
  - مرحله بعد: [Step08 - اشتغال](#)

---

## 9. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | گام هفتم Wizard: حساب بانکی |
| **ورودی** | Token + BankAccountNumber + FileId? |
| **اعتبارسنجی** | سیبا (ایرانی) + مدرک (غیرایرانی) |
| **Uniqueness** | ✅ چک تکراری |
| **Dev Mode** | ⚠️ Bypass تکراری |
| **State Transition** | → BankAccountVerified |

</div>
