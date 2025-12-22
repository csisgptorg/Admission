<div dir="rtl">

# CreateStudentBlockServiceCommand.cs

**مسیر**: `Csis.Admission.Application/Features/BlockServices/Commands/CreateStudentBlockServiceCommand.cs`

---

## 1. Purpose (هدف)

**مسدود کردن یک سرویس خاص** برای دانشجو. این Command امکان محدود کردن دسترسی دانشجو به یک سرویس خاص (مثلاً وام، بیمه تکمیلی، ...) را فراهم می‌کند.

---

## 2. مستندات XML موجود

```csharp
/// <summary>ثبت</summary>
```

**تکمیل شده**: ثبت مسدودی یک سرویس برای دانشجو با ذکر علت و تاریخ.

---

## 3. خلاصه اتفاقات

```
1. بررسی تکراری نبودن (Codm + ServiceId)
2. ایجاد رکورد StudentBlockService
3. بازگشت Id
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record CreateStudentBlockServiceCommand : BaseCommandDto<..., StudentBlockService>, IRequest<int>
{
    int Codm              // کد مرکز دانشجو
    int ServiceId         // شناسه سرویس مسدود شده
    string Reason         // علت مسدودی
    string BlockDate      // تاریخ انسداد (رشته)
}
```

**Custom Mapping**:
```csharp
BlockDate.StringDateToInt() → model.BlockDate
```

### Handler:
- **Dependency**: `IRepository<StudentBlockService>`

---

## 5. Flow

```
1. بررسی تکراری
   if (Exists(Codm && ServiceId))
       └─> CommandValidationException

2. ایجاد Entity
   └─> command.ToEntity()

3. ذخیره
   └─> repo.InsertAsync(studentBlockService)

4. بازگشت
   └─> return Id
```

---

## 6. Business Rules

### BR-1: Uniqueness
- یک دانشجو نمی‌تواند دو بار برای یک سرویس مسدود شود
- ترکیب `(Codm, ServiceId)` یکتا است

### BR-2: TODO
- **یادداشت**: در کد TODO برای "ثبت درخواست" وجود دارد
- احتمالاً نیاز به ثبت Audit یا Request

---

## 7. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `CommandValidationException` | سرویس قبلاً مسدود شده | "این خدمت برای طلبه مسدود شده است." |

---

## 8. Risks & Notes

### امنیت:
- ⚠️ **Authorization**: چک نشده که چه کسی می‌تواند سرویس را مسدود کند
- **پیشنهاد**: افزودن RBAC

### کارایی:
- ✅ بررسی تکراری قبل از Insert

### Code Quality:
- ⚠️ **TODO در کد**: "ثبت درخواست" باید پیاده شود
- ❌ **عدم Authorization Check**

---

## 9. Use Case های مرتبط

- **UC-080**: مسدود کردن سرویس
- **Actor**: کارمند (احتمالاً)
- **Reason**: نقض قوانین، بدهی، ...

مرتبط با:
- [DeleteStudentBlockServiceCommand.md](./DeleteStudentBlockServiceCommand.md) - رفع مسدودی

---

## 10. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | مسدود کردن سرویس برای دانشجو |
| **ورودی** | Codm + ServiceId + Reason + BlockDate |
| **خروجی** | Id (int) |
| **Uniqueness** | ✅ بررسی تکراری |
| **Authorization** | ❌ چک نشده |
| **TODO** | ⚠️ ثبت درخواست |
| **امنیت** | ⚠️ نیاز به RBAC |

</div>
