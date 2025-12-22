<div dir="rtl">

# CreateAdmissionCaseStep04ConfirmIdentityInformationCommand.cs

**مسیر**: `Csis.Admission.Application/Features/CaseFilings/Commands/Student/CreateAdmissionCaseStep04ConfirmIdentityInformationCommand.cs`

---

## 1. Purpose (هدف)

این فایل **گام چهارم از Wizard تشکیل پرونده** است. پس از دریافت اطلاعات هویتی از ثبت احوال، کاربر باید این اطلاعات را **تأیید کند**. این Command تأییدیه کاربر را ثبت می‌کند.

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// (تایید اطلاعات هویتی گام سوم(تاییدیه
/// </summary>
```

**یادداشت**: عنوان "گام سوم" اشتباه است - این در واقع **گام چهارم** است (بعد از ValidateIdentity).

---

## 3. خلاصه اتفاقات (What Happens)

**جریان اصلی**:
1. دریافت `Token` و `ConfirmIdentityInformation` (boolean)
2. بررسی اینکه کاربر تأیید کرده یا نه
3. اگر تأیید نکرده → Exception
4. بازیابی `AdmissionCaseUser`
5. ثبت تأییدیه: `ConfirmIdentityInformation = true`
6. تغییر `CaseStep` به `IdentityVerified`
7. ذخیره تغییرات

---

## 4. اجزای اصلی

### 4.1. Command

**کلاس**: `ConfirmIdentityInformationCommand`
- **نوع**: `sealed record`
- **Interface**: `IRequest`

**Properties**:
```csharp
Guid Token                              // توکن پرونده
bool ConfirmIdentityInformation         // تأیید اطلاعات هویتی
```

---

### 4.2. Handler

**کلاس**: `ConfirmVerifyingIdentityInformationCommandHandler`

**Injected Dependencies**:
- `IRepository<AdmissionCaseUser, Guid>` - دسترسی به پرونده
- `IMapper` - **استفاده نشده** (اضافی)

---

## 5. Flow داخل فایل

```
1. بررسی تأییدیه کاربر
   if (!ConfirmIdentityInformation)
       └─> CommandValidationException("برای ادامه باید تایید کنید...")

2. بازیابی AdmissionCaseUser
   └─> GetByIdAsTracking(Token)

3. ثبت تأییدیه
   ├─> ConfirmIdentityInformation = true
   ├─> CaseStep = IdentityVerified
   └─> UpdateAsync()
```

---

## 6. Dependencies

| Dependency | Purpose | استفاده |
|-----------|---------|---------|
| `IRepository<AdmissionCaseUser, Guid>` | دسترسی به پرونده | ✅ |
| `IMapper` | Mapping | ❌ استفاده نشده |

---

## 7. Business Rules

### BR-1: اجباری بودن تأیید
- کاربر **باید** اطلاعات هویتی را تأیید کند
- اگر تأیید نکند → نمی‌تواند ادامه دهد

### BR-2: State Transition
- بعد از تأیید → `CaseStep = IdentityVerified`

### BR-3: راه حل برای اطلاعات اشتباه
- اگر اطلاعات اشتباه است، کاربر باید با پشتیبانی تماس بگیرد
- امکان ویرایش در این مرحله وجود ندارد

---

## 8. Data Access

### EF Core:
```csharp
// Query + Update
var admissionCaseUser = await caseUserRepo.GetByIdAsTrackingAsync(Token, ...)
admissionCaseUser.ConfirmIdentityInformation = true
admissionCaseUser.CaseStep = AdmissionCaseStep.IdentityVerified
await caseUserRepo.UpdateAsync(admissionCaseUser, ...)
```

---

## 9. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `CommandValidationException` | تأیید نشده | "برای ادامه باید اطلاعات هویتی را تایید کنید , اگر اطلاعات هویتی شما اشتباه است لطفا با پشتیبانی تماس بگیرید." |
| `CommandValidationException` | Token نامعتبر | "شناسه نامعتبر است." |

---

## 10. Use Case های مرتبط

- **UC-030**: تشکیل پرونده دانشجوی جدید (Wizard)
  - **مرحله 4b**: تأیید اطلاعات هویتی
  - مرحله قبل: [ValidateIdentityCommand](./CreateAdmissionCaseStep04ValidateIdentityCommand.md)
  - مرحله بعد: [Step05 - آدرس](#)

---

## 11. Risks & Notes

### Code Quality:
- ❌ `IMapper` تزریق شده اما استفاده نشده → باید حذف شود
- ❌ عنوان XML Summary اشتباه ("گام سوم" → باید "گام چهارم" باشد)

### UX:
- ⚠️ اگر اطلاعات اشتباه باشد، کاربر باید با پشتیبانی تماس بگیرد
- **پیشنهاد**: امکان ویرایش در سطوح بالاتر (Admin)

---

## 12. Test Ideas

### Happy Path:
- `ConfirmIdentityInformation = true` → `CaseStep = IdentityVerified`

### Edge Cases:
- `ConfirmIdentityInformation = false` → Exception
- Token نامعتبر → Exception

---

## 13. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | گام چهارم Wizard: تأیید اطلاعات هویتی |
| **ورودی** | Token + bool ConfirmIdentityInformation |
| **خروجی** | بدون خروجی (Unit) |
| **اجباری** | تأیید الزامی است |
| **State Transition** | → IdentityVerified |
| **Unused Dependency** | ❌ IMapper |
| **XML Summary** | ❌ عنوان اشتباه |

</div>
