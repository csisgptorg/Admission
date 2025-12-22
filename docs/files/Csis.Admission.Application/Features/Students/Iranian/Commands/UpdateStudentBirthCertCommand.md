<div dir="rtl">

# UpdateStudentBirthCertCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Students/Iranian/Commands/UpdateStudentBirthCertCommand.cs`

---

## 1. Purpose (هدف)

بروزرسانی اطلاعات شناسنامه‌ای دانشجو شامل کد ملی، تاریخ تولد، و مذهب. این Command با **محدودیت‌های امنیتی** برای جلوگیری از تغییرات غیرمجاز طراحی شده است.

---

## 2. مستندات XML موجود

```csharp
/// <summary>بروز رسانی اطلاعات شناسنامه ای</summary>
```

**کامل**: بروزرسانی اطلاعات حساس شناسنامه با اعتبارسنجی ثبت احوال و کنترل سطح دسترسی.

---

## 3. خلاصه اتفاقات

```
1. بررسی تکراری نبودن کد ملی جدید
2. بررسی مجوز کاربر (SeniorPersonnel برای تغییر کد ملی/تاریخ تولد)
3. اعتبارسنجی با ثبت احوال
4. بروزرسانی اطلاعات در دیتابیس
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record UpdateStudentBirthCertCommand : IRequest
{
    int Codm                  // کد مرکز خدمات
    string NationalCode       // کد ملی جدید
    string BirthDate          // تاریخ تولد (رشته)
    Religion Religion         // مذهب
    string Description        // توضیحات
}
```

### Handler Dependencies:
- `IStudentRepository` - بروزرسانی اطلاعات
- `IRepository<StudentSummary>` - بررسی تکراری
- `IRepository<DependentSummary, long>` - (تزریق شده اما استفاده نشده)
- `ICsisAuthenticatedUserService` - بررسی مجوز
- `ICsisWsmService` - اعتبارسنجی ثبت احوال

---

## 5. Flow

```
1. بازیابی دانشجو(های) با Codm یا NationalCode
   if (count > 1)
       └─> CommandValidationException("کد ملی تکراری")

2. بررسی مجوز
   isSenior = IsAuthorizedTo(SeniorPersonnel)
   if (!isSenior && (NationalCode یا BirthDate تغییر کرده))
       └─> CommandValidationException("مجوز ندارید")

3. اعتبارسنجی ثبت احوال
   └─> wsmService.GetIdentityInfoByNationalCode(NationalCode, BirthDate)
   └─> if (Nin خالی) → Exception

4. بروزرسانی
   └─> studentRepo.UpdateStudentBirthCertInfo(...)
```

---

## 6. Business Rules

### BR-1: Uniqueness
- کد ملی جدید نباید در سیستم موجود باشد (به جز برای همین دانشجو)

### BR-2: Authorization
- **کاربران عادی**: فقط مذهب و توضیحات
- **SeniorPersonnel**: کد ملی + تاریخ تولد + مذهب + توضیحات

### BR-3: Civil Registry Validation
- کد ملی + تاریخ تولد باید در ثبت احوال معتبر باشد

### BR-4: Immutable Fields
- `IsSadat` از رکورد قبلی حفظ می‌شود (تغییر نمی‌کند)

---

## 7. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `CommandValidationException` | کد ملی تکراری | "این کد ملی قبلاً در سامانه ثبت شده است." |
| `CommandValidationException` | عدم مجوز | "شما مجوز لازم برای تغییر کد ملی و تاریخ تولد را ندارید." |
| `CommandValidationException` | ثبت احوال نامعتبر | "کد ملی یا تاریخ تولد وارد شده در ثبت احوال یافت نشد." |

---

## 8. Risks & Notes

### امنیت:
- ✅ **Role-Based Access Control** (SeniorPersonnel)
- ✅ اعتبارسنجی با ثبت احوال
- ✅ بررسی تکراری کد ملی
- ⚠️ **Critical Data**: تغییر کد ملی می‌تواند مشکلات جدی ایجاد کند

### کارایی:
- ⚠️ Query با `GetAllAsync` - اگر تعداد زیاد باشد، کند است
- **پیشنهاد**: استفاده از `CountAsync` برای بررسی تکراری

### Code Quality:
- ❌ `IRepository<DependentSummary, long>` تزریق شده اما استفاده نشده
- ⚠️ در خط 36: `students.Count > 1` - اگر دانشجو یافت نشد چه؟
  - باید `if (students.Count == 0)` هم چک شود

---

## 9. Use Case های مرتبط

- **UC-011**: بروزرسانی شناسنامه‌ای دانشجو
- مرتبط با:
  - [SyncStudentBirthCertByCodmCommand.md](./SyncStudentBirthCertByCodmCommand.md)
  - UpdateDependentBirthCertCommand

---

## 10. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | بروزرسانی اطلاعات شناسنامه‌ای |
| **ورودی** | Codm + NationalCode + BirthDate + Religion |
| **خروجی** | Unit |
| **Authorization** | SeniorPersonnel برای کد ملی/تاریخ تولد |
| **Validation** | ثبت احوال + Uniqueness |
| **امنیت** | ✅ RBAC + Civil Registry |
| **ریسک** | ⚠️ تغییر کد ملی Critical است |
| **Unused Dependency** | ❌ IRepository<DependentSummary> |

</div>
