<div dir="rtl">

# UpdateDependentDivorceCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Divorce/Commands/UpdateDependentDivorceCommand.cs`

---

## 1. Purpose (هدف)

**ثبت طلاق افراد تحت تکفل** (فرزند یا همسر) با **بازگشایی خودکار پرونده تکفل**.

---

## 2. مستندات XML موجود

```csharp
/// <inheritdoc/>
```

**ناقص**: تمام XML Comments فقط `<inheritdoc/>` است - بدون توضیح واقعی.

---

## 3. خلاصه اتفاقات

```
1. دریافت اطلاعات طلاق
2. ثبت تاریخ طلاق
3. ⭐ بازگشایی خودکار پرونده تکفل (Cascade Effect)
4. بازگشت Id
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record UpdateDependentDivorceCommand : IRequest<long>
{
    int Codm                        // کد مرکز دانشجو
    string DivorceDate              // تاریخ طلاق (شمسی string)
    long? DependentId               // شناسه تکفل
    string DependentNationalCode    // کد ملی تکفل
    string DependentBirthDate       // تاریخ تولد تکفل
}
```

**یادداشت**: NationalCode و BirthDate دریافت می‌شوند اما در Handler استفاده نمی‌شوند!

### Handler Dependencies:
- `IMediator` - برای ارسال Command بازگشایی پرونده
- `IStudentDependentRepository`

**یادداشت**: Handler Name نادرست - `CreateDependentDivorceCommandHandler` اما Command نام `Update...` است!

---

## 5. Flow

```
1. ساخت Model
   └─> SetDependentDivorceModel
       ├─> Codm
       ├─> DependentId
       └─> DivorceDate (StringDateToInt)

2. ثبت طلاق
   └─> studentDependentRepository.UpdateDependentChildDivorceAsync()

3. بازگشایی خودکار پرونده ⭐
   └─> mediator.Send(AutomaticOpenDependentCaseRequestCommand)
       ├─> Codm
       └─> DependentId

4. بازگشت
   └─> return Id
```

---

## 6. Business Rules

### BR-1: Cascade Effect - Automatic Case Reopening ⭐
```
طلاق → بازگشایی خودکار پرونده تکفل
```

**دلیل**: با طلاق، شرایط تکفل تغییر می‌کند → نیاز به بررسی مجدد

### BR-2: String to Int Date Conversion
- تاریخ شمسی به صورت string دریافت می‌شود
- تبدیل به Integer (PersianInteger)
- احتمالاً فرمت: YYYYMMDD (مثلاً 14030515)

### BR-3: Unused Parameters
- `DependentNationalCode` و `DependentBirthDate` دریافت می‌شوند اما استفاده نمی‌شوند
- **احتمال**: برای Validation در FluentValidator

---

## 7. Error Handling

- **هیچ Exception صریحی ندارد**
- خطاهای احتمالی از Repository

---

## 8. Risks & Notes

### Code Quality:
- ❌ **Handler Name Wrong**: `CreateDependentDivorceCommandHandler` → باید `UpdateDependentDivorceCommandHandler`
- ❌ **XML Comments**: فقط `<inheritdoc/>` - بدون توضیح
- ⚠️ **Unused Parameters**: NationalCode, BirthDate

### Business Logic:
- ✅ **Cascade Effect**: بازگشایی خودکار پرونده (منطقی)
- ⚠️ **Dependent Type**: Command می‌گوید "Child" اما ممکن است Spouse هم باشد

### Comparison:
- **UpdateDependentDivorceDataImportCommand**: احتمالاً برای Import داده‌های انبوه
- **UpdateWifeDivorceCommand**: مخصوص همسر
- **UpdateStudentSisterDivorceCommand**: مخصوص خواهر

---

## 9. Dependencies مرتبط

```
UpdateDependentDivorceCommand
  └─> AutomaticOpenDependentCaseRequestCommand
      └─> DependentCaseActive Feature
```

---

## 10. Use Case های مرتبط

- **UC-095**: ثبت طلاق تکفل
- **UC-096**: بازگشایی خودکار پرونده
- مرتبط با:
  - AutomaticOpenDependentCaseRequestCommand

---

## 11. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | ثبت طلاق تکفل |
| **ورودی** | Codm + DivorceDate + DependentId |
| **خروجی** | Id (long) |
| **Cascade** | ⭐ Auto Case Reopening |
| **Handler Name** | ❌ نادرست (Create vs Update) |
| **XML Comments** | ❌ فقط inheritdoc |
| **Unused Params** | ⚠️ 2 پارامتر |

---

**یادداشت بحرانی**: این Command یک Cascade Effect مهم دارد - طلاق باعث بازگشایی خودکار پرونده تکفل می‌شود.

</div>
