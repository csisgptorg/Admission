<div dir="rtl">

# CreateOrUpdateStudentEmploymentCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Employments/Commands/CreateOrUpdateStudentEmploymentCommand.cs`

---

## 1. Purpose (هدف)

**ثبت یا بروزرسانی اطلاعات اشتغال** دانشجو شامل وضعیت کار، درآمد، بیمه پایه و تکمیلی، و اشتغال در حوزه.

---

## 2. مستندات XML موجود

```csharp
/// <summary>بروز رسانی وضعیت اشتغال</summary>
```

**کامل**: ثبت/بروزرسانی اطلاعات کامل اشتغال با Upsert Pattern.

---

## 3. خلاصه اتفاقات

```
1. جستجوی Employment موجود
2. Upsert (Insert or Update)
3. (SMS Notification - commented out)
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record CreateOrUpdateStudentEmploymentCommand : BaseCommandDto<..., StudentEmployment>, IRequest<int>
{
    int Codm                           // کد مرکز
    bool? HasIncome                    // دارای درآمد
    bool? IsEmployee                   // کارمند
    string EmployeeName                // نام محل کار
    string EmployeeAddress             // آدرس محل کار
    bool? HasSufficientIncome          // درآمد کافی
    bool? HasAnotherBaseInsurance      // بیمه پایه دیگر
    string InsurancePlaceName          // نام محل بیمه
    string InsurancePlaceAddress       // آدرس محل بیمه
    bool? HasAnotherSupInsurance       // بیمه تکمیلی دیگر
    bool? IsEmployeeInHowze            // اشتغال در حوزه
    EmploymentHowzeType? HowzeTypeId   // نوع اشتغال حوزه
    bool? IsRetried                    // بازنشسته
    short? Decile                      // دهک درآمدی
    short? InsuranceTypeId             // نوع بیمه
    EmploymentReference? Reference     // منبع اطلاعات (JsonIgnore)
    long? RequestId                    // شناسه درخواست
}
```

**یادداشت**: `Reference` با default value `KhodeEzhari` (خود اظهاری)

### Handler Dependencies:
- `IRepository<StudentEmployment>`
- `ICsisNotificationService` - (استفاده نشده)
- `IRepository<UploadedFile>` - (استفاده نشده)
- `IRepository<RequestDocument, long>` - (استفاده نشده)

---

## 5. Flow

```
1. جستجوی Employment
   └─> repo.GetOneAsTrackingAsync(Codm)

2. Upsert
   if (employment == null)
       └─> Insert
   else
       └─> Update

3. (Commented) SMS Notification
   └─> "طلبه گرامی اشتغال شما در {EmployeeName} ثبت شد."
```

---

## 6. Business Rules

### BR-1: Upsert Pattern
- یک دانشجو فقط **یک** رکورد Employment دارد

### BR-2: Conditional Fields
- اگر `IsEmployee = true` → `EmployeeName`, `EmployeeAddress` پر می‌شود
- اگر `HasAnotherBaseInsurance = true` → `InsurancePlaceName`, `InsurancePlaceAddress`
- اگر `IsEmployeeInHowze = true` → `HowzeTypeId`

### BR-3: Reference Source
- `EmploymentReference.KhodeEzhari` (خود اظهاری) - default
- احتمالاً می‌تواند `WebService` باشد (اعتبارسنجی خودکار)

### BR-4: Decile (دهک درآمدی)
- از 1 تا 10
- احتمالاً محاسبه خودکار بر اساس درآمد

---

## 7. Risks & Notes

### Code Quality:
- ❌ **Unused Dependencies**: 3 dependency تزریق شده اما استفاده نشده:
  - `ICsisNotificationService`
  - `IRepository<UploadedFile>`
  - `IRepository<RequestDocument, long>`
- ✅ **SMS Commented Out**: قابلیت SMS آماده اما غیرفعال

### Business Logic:
- ⚠️ **No Validation**: اعتبارسنجی‌ها احتمالاً در Validator
- ⚠️ **Decile**: محاسبه خودکار یا دستی؟

### Comparison:
- **Wizard Step08**: احتمالاً از همین Command استفاده می‌کند

---

## 8. Use Case های مرتبط

- **UC-075**: ثبت/بروزرسانی اشتغال
- مرتبط با:
  - [CreateAdmissionCaseStep08ConfirmEmploymentCommand](../../CaseFilings/Commands/Student/CreateAdmissionCaseStep08ConfirmEmploymentCommand.md)

---

## 9. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | ثبت/بروزرسانی اشتغال |
| **ورودی** | Codm + 14 فیلد اشتغال/بیمه |
| **خروجی** | EmploymentId (int) |
| **Upsert** | ✅ بر اساس Codm |
| **SMS** | ⚠️ Commented Out |
| **Unused Dependencies** | ❌ 3 dependency |
| **Validation** | در Validator Layer |

---

**یادداشت**: این Command یکی از جامع‌ترین Commands است (14 فیلد اطلاعاتی).

</div>
