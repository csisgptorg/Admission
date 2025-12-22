<div dir="rtl">

# CreateStudentCaseUnblockCommand.cs

**مسیر**: `Csis.Admission.Application/Features/CaseBlock/Commands/CreateStudentCaseUnblockCommand.cs`

---

## 1. Purpose (هدف)

این Command برای **رفع مسدودی کل پرونده دانشجو** استفاده می‌شود.

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// CreateStudentCaseUnblockCommand
/// </summary>
/// <param name="Codm"></param>
```

**وضعیت**: ناقص - فقط نام پارامتر، بدون توضیح عملکرد.

---

## 3. خلاصه اتفاقات (What Happens)

1. دریافت `Codm`
2. ساخت `SetStudentUnBlockedRepoCommand`:
   - Codm
   - UserId و PersonnelId کاربر فعلی
   - ApplicationId = 66
   - DataSource = Employee
3. فراخوانی `SetStudentUnblocked`
4. بازگشت Id

---

## 4. اجزای اصلی

### Record
```csharp
public sealed record CreateStudentCaseUnblockCommand(int Codm) : IRequest<long>
```

### Handler
```csharp
internal sealed class CreateStudentCaseUnblockCommandHandler 
    : IRequestHandler<CreateStudentCaseUnblockCommand, long>
```

---

## 5. Flow داخل فایل

```
1. Handle(Codm)
   ↓
2. ساخت SetStudentUnBlockedRepoCommand
   ↓
3. SetStudentUnblocked()
   ↓
4. Return Id
```

---

## 6. Dependencies

- `IStudentRepository`
- `ICurrentUserService`

---

## 7. Business Rules

### BR-1: فقط کارمند
- DataSource = Employee
- دانشجو نمی‌تواند خودش را رفع مسدودی کند

### BR-2: Audit
- UserId و PersonnelId ثبت می‌شود

---

## 8. Data Access

```csharp
Task<IdResult> SetStudentUnblocked(SetStudentUnBlockedRepoCommand command)
```

---

## 9. Error Handling

❌ هیچ Exception Handling نیست

---

## 10. Observability

❌ بدون Logging صریح

---

## 11. Use Cases مرتبط

- **UC-xxx**: رفع مسدودی پرونده
- **Prerequisite**: پرونده باید قبلاً مسدود شده باشد

---

## 12. Risks & Notes

### Security
⚠️ **عدم Authorization**: چه کسی می‌تواند رفع مسدودی کند؟  
⚠️ **عدم Validation**: چک نمی‌شود Codm مسدود هست یا نه

### Code Quality
❌ **Hardcoded ApplicationId**

---

## 13. Test Ideas

- رفع مسدودی پرونده مسدود شده
- رفع مسدودی پرونده غیرمسدود (چه اتفاقی می‌افتد؟)
- Codm نامعتبر

---

## 14. خلاصه نکات کلیدی

| جنبه | وضعیت |
|------|-------|
| **Reversal of** | CreateStudentCaseBlockCommand |
| **Authorization** | ❌ |
| **Validation** | ❌ |
| **Audit** | ✅ |

</div>
