<div dir="rtl">

# DeletePersonMarriageCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Marriages/Commands/DeletePersonMarriageCommand.cs`

---

## 1. Purpose (هدف)

**حذف رکورد ازدواج** با شناسه. این Command برای حذف اطلاعات ازدواج (احتمالاً در صورت خطا در ثبت) استفاده می‌شود.

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// حذف موجودیت ازدواج با شناسه
/// </summary>
/// <param name="Id">شناسه موجودیت ازدواج</param>
```

**کامل**: حذف رکورد ازدواج از دیتابیس.

---

## 3. خلاصه اتفاقات

```
1. لاگ شروع حذف (Debug)
2. حذف از دیتابیس
3. اگر موفق نبود → RecordNotFoundException
4. لاگ موفقیت (Debug)
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record DeletePersonMarriageCommand(int Id) : IRequest
```

### Handler:
- **Dependencies**: `IPersonMarriageRepository`, `ILogger`

---

## 5. Flow

```
1. لاگ
   └─> logger.LogDebug("Deleting marriage with id {id}", Id)

2. حذف
   └─> personMarriageRepo.DeleteAsync(Id)

3. بررسی نتیجه
   if (!deleted)
       └─> RecordNotFoundException<Marriage>(Id)

4. لاگ موفقیت
   └─> logger.LogDebug("Marriage with id {id} deleted.")
```

---

## 6. Business Rules

### BR-1: Hard Delete یا Soft Delete?
- بستگی به پیاده‌سازی `DeleteAsync` دارد
- احتمالاً Soft Delete (IsDeleted = true)

### BR-2: No Cascade Check
- چک نمی‌شود که آیا این ازدواج دارای فرزند است یا نه
- **Risk**: ممکن است داده‌های مرتبط orphan شوند

---

## 7. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `RecordNotFoundException<Marriage>` | Id یافت نشد | استاندارد از Csis.Abstractions |

---

## 8. Observability

### Logging:
```csharp
LogDebug("Deleting marriage with id {id}", Id)
LogDebug("Marriage with id {id} deleted.")
```

- سطح: Debug (فقط در Development)
- **پیشنهاد**: افزودن Information Log برای Production

---

## 9. Risks & Notes

### امنیت:
- ⚠️ **Authorization**: چک نشده که چه کسی می‌تواند حذف کند
- **پیشنهاد**: RBAC

### Data Integrity:
- ⚠️ **Cascade Effects**: فرزندان یا سایر اطلاعات مرتبط چه می‌شوند؟
- **پیشنهاد**: Foreign Key Constraints یا Manual Cascade

### Audit:
- ⚠️ **عدم Audit**: حذف ازدواج لاگ نمی‌شود (فقط Debug)
- **پیشنهاد**: Audit Table برای تغییرات مهم

### Code Quality:
- ✅ استفاده از Logging
- ✅ Exception مناسب

---

## 10. Use Case های مرتبط

- **UC-057**: حذف اطلاعات ازدواج (در صورت خطا)
- مرتبط با:
  - [CreatePersonMarriageCommand.md](./CreatePersonMarriageCommand.md)

---

## 11. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | حذف رکورد ازدواج |
| **ورودی** | Id |
| **خروجی** | Unit |
| **Delete Type** | بستگی به Repository |
| **Logging** | ✅ Debug Level |
| **Authorization** | ❌ چک نشده |
| **Cascade** | ❌ بررسی نشده |
| **Audit** | ❌ فقط Debug Log |

---

**یادداشت**: حذف ازدواج ممکن است تأثیرات گسترده‌ای داشته باشد (فرزندان، تکفل‌ها، ...). بررسی Cascade Effects ضروری است.

</div>
