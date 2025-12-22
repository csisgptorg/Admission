<div dir="rtl">

# UpdateStudentPhoneCommand.cs

**مسیر**: `Csis.Admission.Application/Features/StudentMobiles/Commands/UpdateStudentPhoneCommand.cs`

---

## 1. Purpose (هدف)

**بروزرسانی شماره موبایل و تلفن ثابت** دانشجو با امکان تأیید OTP.

---

## 2. مستندات XML موجود

```csharp
/// <summary>بروز رسانی موبایل طلبه</summary>
```

**کامل**: بروزرسانی موبایل و تلفن با OTP Validation.

---

## 3. خلاصه اتفاقات

```
1. دریافت Codm + Mobile + Tel + OTP
2. ساخت Repo Command
3. Set Audit Params (UserId, PersonnelId)
4. اجرای Repository Update
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record UpdateStudentPhoneCommand(
    int? Codm,           // کد مرکز
    string? Mobile,      // شماره موبایل جدید
    string? PreCodeTel,  // پیش شماره تلفن
    string? Tel,         // شماره تلفن ثابت
    string? Otp          // کد تأیید (برای موبایل)
) : IRequest<long>
```

**یادداشت**: تمام فیلدها `Nullable` هستند.

### Handler Dependencies:
- `IStudentMobileRepository`
- `IHttpContextAccessor` - برای Audit
- `ICsisAuthenticatedUserService` - اطلاعات کاربر

---

## 5. Flow

```
1. ساخت Repo Command
   └─> UpdateStudentPhoneRepoCommand(Codm, Mobile, PreCodeTel, Tel)

2. Set Audit Parameters
   └─> SetLogParam(repoCommand, authenticatedUser, contextAccessor)
       ├─> UserId
       ├─> PersonnelId
       ├─> ApplicationId
       └─> Source

3. اجرای Repository
   └─> repo.Update(repoCommand)

4. بازگشت
   └─> return Id
```

---

## 6. Business Rules

### BR-1: OTP Validation
- پارامتر `Otp` دریافت می‌شود اما در Handler استفاده نمی‌شود!
- **احتمال**: Validation در Validator یا Repository انجام می‌شود

### BR-2: Optional Fields
- تمام فیلدها Nullable → می‌توان فقط Mobile یا فقط Tel را Update کرد

### BR-3: Audit Trail
- `SetLogParam` اطلاعات تغییرات را ثبت می‌کند

---

## 7. Risks & Notes

### Code Quality:
- ⚠️ **OTP Parameter Unused**: `Otp` دریافت می‌شود اما استفاده نمی‌شود در Handler
- **سؤال**: آیا Validation در Repository یا Validator است؟

### Comparison با Wizard:
- **Wizard Step02**: OTP برای تأیید موبایل در ثبت‌نام اولیه
- **این Command**: برای Update موبایل موجود

### امنیت:
- ✅ **Audit Logging**: تغییرات ثبت می‌شود
- ⚠️ **OTP Validation**: نامشخص است کجا انجام می‌شود

---

## 8. Use Case های مرتبط

- **UC-080**: بروزرسانی اطلاعات تماس
- مرتبط با:
  - [CreateAdmissionCaseStep02MobileCommand](../../CaseFilings/Commands/Student/CreateAdmissionCaseStep02MobileCommand.md) - OTP در Wizard

---

## 9. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | بروزرسانی موبایل/تلفن |
| **ورودی** | Codm? + Mobile? + Tel? + Otp? |
| **خروجی** | Id (long) |
| **OTP** | ⚠️ پارامتر موجود اما استفاده نشده |
| **Audit** | ✅ SetLogParam |
| **Nullable** | ✅ تمام فیلدها |
| **Delegation** | ✅ به Repository |

---

**یادداشت**: ساده‌ترین Command برای تماس، اما OTP Logic نامشخص است.

</div>
