<div dir="rtl">

# DeleteStudentBlockServiceCommand.cs

**مسیر**: `Csis.Admission.Application/Features/BlockServices/Commands/DeleteStudentBlockServiceCommand.cs`

---

## 1. Purpose (هدف)

**رفع مسدودی سرویس** برای دانشجو. این Command مسدودی یک سرویس را حذف کرده و دسترسی دانشجو به آن سرویس را بازگردانی می‌کند.

---

## 2. مستندات XML موجود

```csharp
/// <summary>حذف</summary>
```

**تکمیل شده**: حذف رکورد مسدودی سرویس.

---

## 3. خلاصه اتفاقات

```
1. حذف رکورد StudentBlockService با Id
2. اگر یافت نشد → Exception
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record DeleteStudentBlockServiceCommand(int Id) : IRequest
```

**یادداشت**: فقط Id نیاز است (نه Codm یا ServiceId)

### Handler:
- **Dependency**: `IRepository<StudentBlockService, int>`

---

## 5. Flow

```
1. حذف
   └─> repo.DeleteAsync(Id, saveChanges: true)

2. بررسی نتیجه
   if (!deleted)
       └─> CommandValidationException("رکورد یافت نشد.")
```

---

## 6. Business Rules

### BR-1: Soft Delete یا Hard Delete?
- بستگی به پیاده‌سازی `DeleteAsync` دارد
- احتمالاً Soft Delete (IsDeleted = true)

---

## 7. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `CommandValidationException` | Id یافت نشد | "رکورد یافت نشد." |

---

## 8. Risks & Notes

### امنیت:
- ⚠️ **Authorization**: چک نشده که چه کسی می‌تواند مسدودی را رفع کند
- **پیشنهاد**: افزودن RBAC (مثلاً فقط SeniorPersonnel)

### Audit:
- ⚠️ **عدم لاگ**: رفع مسدودی لاگ نمی‌شود
- **پیشنهاد**: Audit Log برای Compliance

### Code Quality:
- ✅ ساده و واضح
- ❌ **عدم Authorization**

---

## 9. Use Case های مرتبط

- **UC-082**: رفع مسدودی سرویس
- **Precondition**: مسدودی موجود باشد

مرتبط با:
- [CreateStudentBlockServiceCommand.md](./CreateStudentBlockServiceCommand.md) - ایجاد مسدودی

---

## 10. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | رفع مسدودی سرویس |
| **ورودی** | Id |
| **خروجی** | Unit |
| **Delete Type** | بستگی به Repository |
| **Authorization** | ❌ چک نشده |
| **Audit** | ❌ لاگ نمی‌شود |
| **امنیت** | ⚠️ نیاز به RBAC |

---

**Flow کامل مسدودی**:
```
1. Create → مسدود کردن سرویس
2. (سرویس برای دانشجو غیرفعال است)
3. Delete → رفع مسدودی
4. (سرویس دوباره فعال می‌شود)
```

</div>
