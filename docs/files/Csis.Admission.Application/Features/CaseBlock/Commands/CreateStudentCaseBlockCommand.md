<div dir="rtl">

# CreateStudentCaseBlockCommand.cs

**مسیر**: `Csis.Admission.Application/Features/CaseBlock/Commands/CreateStudentCaseBlockCommand.cs`

---

## 1. Purpose (هدف)

این Command برای **مسدود کردن کل پرونده دانشجو** استفاده می‌شود (برخلاف BlockServices که فقط سرویس‌های خاص را مسدود می‌کند).

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// CreateStudentCaseBlockCommand
/// </summary>
/// <param name="Codm"></param>
/// <param name="CaseBlockReasonId"></param>
```

**وضعیت**: ناقص - فقط نام پارامترها ذکر شده، توضیح عملکرد وجود ندارد.

---

## 3. خلاصه اتفاقات (What Happens)

1. دریافت `Codm` و لیست `CaseBlockReason`
2. ساخت `SetStudentBlockedRepoCommand`:
   - تبدیل لیست دلایل به رشته CSV
   - اضافه کردن UserId و PersonnelId کاربر فعلی
   - ApplicationId = 66 (hardcoded)
3. فراخوانی `SetStudentBlocked` از Repository
4. بازگشت Id

---

## 4. اجزای اصلی

### Record
```csharp
public sealed record CreateStudentCaseBlockCommand(
    int Codm,
    List<CaseBlockReason> CaseBlockReasonId
) : IRequest<long>
```

### Handler
```csharp
internal sealed class CreateStudentCaseBlockCommandHandler 
    : IRequestHandler<CreateStudentCaseBlockCommand, long>
```

### Dependencies
- `IStudentRepository` - انجام عملیات مسدودی
- `ICurrentUserService` - دریافت اطلاعات کاربر فعلی

---

## 5. Flow داخل فایل (Step-by-Step)

```
1. Handler.Handle()
   ↓
2. ساخت SetStudentBlockedRepoCommand
   - Codm
   - BlockReasons = Join دلایل با ","
   - UserId از CurrentUser
   - PersonnelId از CurrentUser
   - ApplicationId = 66
   - DataSource = Employee
   ↓
3. SetStudentBlocked(command)
   ↓
4. Return result.Id
```

---

## 6. Dependencies (وابستگی‌ها)

### Injected Dependencies
- `IStudentRepository` → Repository لایه
- `ICurrentUserService` → سرویس احراز هویت

### External Calls
- `_repo.SetStudentBlocked()` - Stored Procedure احتمالی

---

## 7. Business Rules (قوانین کسب‌وکار)

### BR-1: دلایل چندگانه
- یک پرونده می‌تواند به دلایل مختلف مسدود شود
- دلایل به صورت CSV ذخیره می‌شوند

### BR-2: Audit Trail
- UserId و PersonnelId کاربری که مسدود کرده ثبت می‌شود
- برای Accountability

### BR-3: ApplicationId ثابت
- همیشه 66 (احتمالاً شماره سیستم پذیرش)

### BR-4: DataSource
- همیشه `Employee` - فقط کارمند می‌تواند مسدود کند

---

## 8. Data Access

### Repository Method
```csharp
Task<IdResult> SetStudentBlocked(SetStudentBlockedRepoCommand command)
```

**احتمال**: Stored Procedure برای:
- Insert در جدول CaseBlock
- Update وضعیت Student
- ثبت Audit Log

---

## 9. Error Handling

- ❌ **هیچ Exception Handling وجود ندارد**
- اگر Codm نامعتبر باشد → Exception از Repository
- اگر CaseBlockReason خالی باشد → چک نمی‌شود

---

## 10. Observability (Logging/Audit)

- ❌ **هیچ Logging صریح وجود ندارد**
- Audit احتمالاً در Stored Procedure انجام می‌شود:
  - UserId
  - PersonnelId
  - زمان مسدودی

---

## 11. Use Cases مرتبط

- **UC-xxx**: مسدودی کل پرونده دانشجو
- **Scenario**: کارمند → دلایل → مسدودی → عدم دسترسی دانشجو به تمام سرویس‌ها

**تفاوت با BlockServices**:
- CaseBlock: کل پرونده مسدود می‌شود
- BlockServices: فقط سرویس‌های خاص

---

## 12. Risks & Notes

### Security Risks
⚠️ **عدم Authorization**: چک نمی‌شود چه کسی می‌تواند مسدود کند  
⚠️ **عدم Validation**: CaseBlockReason خالی چک نمی‌شود

### Performance
✅ Single Repository Call

### Code Quality Issues
1. ❌ **Hardcoded ApplicationId**: باید از Configuration
2. ⚠️ **CSV Storage**: دلایل به صورت CSV - بهتر است Table جدا باشد
3. ⚠️ **عدم Check Duplicate**: اگر قبلاً مسدود بود چه؟

---

## 13. Test Ideas

### Happy Path
- مسدودی با یک دلیل
- مسدودی با چند دلیل

### Edge Cases
- لیست دلایل خالی
- Codm نامعتبر
- مسدودی مجدد (Duplicate)

### Security
- کاربر بدون Permission تلاش برای مسدودی
- تغییر Codm در Request

---

## 14. خلاصه نکات کلیدی

| جنبه | وضعیت | توضیح |
|------|-------|-------|
| **Scope** | ⭐⭐⭐ | مسدودی کل پرونده (بحرانی‌تر از Service Block) |
| **Authorization** | ❌ | چک نمی‌شود |
| **Validation** | ❌ | دلایل خالی چک نمی‌شود |
| **Audit** | ✅ | UserId + PersonnelId ثبت می‌شود |
| **Hardcoded Values** | ⚠️ | ApplicationId = 66 |
| **Reversal** | ✅ | CreateStudentCaseUnblockCommand |
| **Storage** | ⚠️ | دلایل CSV (بهتر Table جدا) |

**الگو**: Simple Delegation با Audit Trail

**بهبودهای پیشنهادی**:
1. افزودن Authorization Attribute
2. Validation لیست دلایل
3. Check Duplicate Block
4. انتقال ApplicationId به Configuration
5. Table جدا برای CaseBlockReasons

</div>
