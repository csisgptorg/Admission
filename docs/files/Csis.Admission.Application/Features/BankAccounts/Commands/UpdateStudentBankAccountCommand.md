<div dir="rtl">

# UpdateStudentBankAccountCommand.cs

**مسیر**: `Csis.Admission.Application/Features/BankAccounts/Commands/UpdateStudentBankAccountCommand.cs`

---

## 1. Purpose (هدف)

**بروزرسانی شماره حساب بانکی** دانشجو. این Command ساده‌ترین Command برای مدیریت اطلاعات بانکی است.

---

## 2. مستندات XML موجود

```csharp
/// <summary>بروز رسانی حساب بانکی</summary>
```

**کامل**: بروزرسانی شماره حساب بانکی دانشجو.

---

## 3. خلاصه اتفاقات

```
1. دریافت Codm + BankAccountNumber
2. فراخوانی Repository با Stored Procedure
3. بازگشت Id
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record UpdateStudentBankAccountCommand : IRequest<long>
{
    int Codm                     // کد مرکز
    string BankAccountNumber     // شماره حساب (16-26 رقمی معمولاً)
}
```

### Handler:
- **Dependency**: `IStudentBankAccountRepository`

---

## 5. Flow

```
1. ساخت Repo Command
   └─> UpdateStudentBankAccountNumberRepoCommand(Codm, BankAccountNumber)

2. اجرای Repository
   └─> bankAccountRepo.Update(repoCommand)

3. بازگشت
   └─> return Id
```

---

## 6. Business Rules

### BR-1: Simple Update
- فقط شماره حساب را به‌روزرسانی می‌کند
- بدون اعتبارسنجی اضافی در Application Layer

### BR-2: Validation در لایه دیگر
- اعتبارسنجی احتمالاً در:
  - Validator (FluentValidation)
  - Repository/Stored Procedure

---

## 7. Risks & Notes

### امنیت:
- ⚠️ **Authorization**: چک نشده
- ⚠️ **Validation**: 
  - آیا شماره حساب معتبر است؟
  - آیا قبلاً استفاده شده؟
  - آیا با سیبا چک می‌شود؟ (مثل Wizard Step07)

### Code Quality:
- ✅ ساده و واضح
- ✅ **Delegation**: کار اصلی در Repository

### Comparison:
- در **Wizard Step07**: اعتبارسنجی با سیبا انجام می‌شود
- در **این Command**: بدون اعتبارسنجی ظاهری
- **سؤال**: آیا این Command فقط برای Update (نه ثبت اولیه) است؟

---

## 8. Use Case های مرتبط

- **UC-070**: بروزرسانی حساب بانکی
- مرتبط با:
  - [CreateAdmissionCaseStep07ConfirmBankAccountInformationCommand](../../CaseFilings/Commands/Student/CreateAdmissionCaseStep07ConfirmBankAccountInformationCommand.md) - ثبت اولیه با سیبا

---

## 9. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | بروزرسانی شماره حساب |
| **ورودی** | Codm + BankAccountNumber |
| **خروجی** | Id (long) |
| **Validation** | ❌ در این Layer خیر |
| **Authorization** | ❌ چک نشده |
| **Delegation** | ✅ به Repository |
| **SIBA** | ❓ اعتبارسنجی نامشخص |

---

**تفاوت با Wizard**:
- **Wizard Step07**: ثبت اولیه + اعتبارسنجی سیبا + چک تکراری
- **این Command**: فقط Update ساده

**پیشنهاد**: افزودن اعتبارسنجی سیبا یا حداقل چک تکراری.

</div>
