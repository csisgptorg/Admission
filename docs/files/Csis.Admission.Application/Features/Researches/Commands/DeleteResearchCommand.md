<div dir="rtl">

# DeleteResearchCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Researches/Commands/DeleteResearchCommand.cs`

---

## 1. Purpose (هدف)

Command **حذف** پژوهش طلبه. این Command برای حذف رکورد پژوهش، مقاله، کتاب یا پروژه تحقیقاتی از سیستم استفاده می‌شود.

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// حذف پژوهش با شناسه
/// </summary>
/// <param name="Id">شناسه پژوهش</param>
```

**کامل**: توضیح واضح با پارامترها

---

## 3. خلاصه اتفاقات

**جریان اصلی**:
```
1. دریافت Codm و Id
2. حذف رکورد از Repository
3. اگر حذف موفق نبود → خطا
4. Log و برگشت Id
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record DeleteResearchCommand(int Codm, int Id) : IRequest<int>
{
    int Codm    // کد مرکز خدمات
    int Id      // شناسه Research
}
```

### Handler Dependencies:
- **IRepository<Research>**: دسترسی به داده‌های پژوهش
- **ILogger**: ثبت لاگ

---

## 5. Flow

```mermaid
graph TD
    A[دریافت Command: Codm, Id] --> B[repo.DeleteAsync: Id]
    B --> C{حذف موفق بود?}
    C -->|خیر| D[❌ خطا: حذف ناموفق]
    C -->|بله| E[logger.LogDebug]
    E --> F[✅ برگشت Id]
```

---

## 6. Business Rules

### BR-1: حذف فیزیکی
- رکورد به طور **کامل** از دیتابیس حذف می‌شود

### BR-2: Validation در Delete
- بررسی می‌شود که رکورد وجود داشته باشد
- اگر وجود نداشت → Exception واضح

### BR-3: Logging
- حذف موفق در سطح Debug لاگ می‌شود

---

## 7. Dependencies

### Internal:
- `IRepository<Research>`: عملیات حذف
- `ILogger<DeleteResearchCommandHandler>`: لاگ

---

## 8. Input/Output

### Input:
```csharp
int Codm    // کد مرکز خدمات (استفاده نمی‌شود)
int Id      // شناسه Research
```

### Output:
```csharp
int Id      // شناسه رکورد حذف شده
```

### Exceptions:
- **CommandValidationException**: "حذف پژوهش با شناسه {Id} ناموفق بود."

---

## 9. Side Effects

1. **حذف کامل**: رکورد Research حذف می‌شود
2. **Logging**: ثبت در لاگ

---

## 10. الگوهای استفاده شده

### ✅ Delete with Validation (مشابه DeleteFamousCommand)
```csharp
if (!await repo.DeleteAsync(id)) {
    throw new CommandValidationException("حذف ناموفق بود");
}
```

### ✅ Proper Logging
```csharp
logger.LogDebug("Research with id {id} deleted.", id);
```

---

## 11. Performance

- **Database Operations**: 1 DELETE
- عملیات بسیار ساده و سریع

---

## 12. Security

- ⚠️ **Codm Validation**: `Codm` در Command وجود دارد اما استفاده نمی‌شود
- ✅ **Logger استفاده می‌شود**: خوب است
- ✅ **Validation**: بررسی وجود رکورد قبل از حذف

---

## 13. نکات مهم

### 💡 الگوی مشابه DeleteFamousCommand
این Command از همان الگوی خوب DeleteFamousCommand استفاده می‌کند:
1. ✅ بررسی موفقیت Delete
2. ✅ Exception واضح
3. ✅ Logging مناسب

### ⚠️ اما Codm استفاده نمی‌شود
```csharp
// مشکل متداول:
await repo.DeleteAsync(request.Id)  // Codm بررسی نمی‌شود
```

**پیشنهاد**:
```csharp
var research = await repo.GetByIdAsync(request.Id);
if (research == null)
    throw new CommandValidationException($"پژوهش با شناسه {request.Id} یافت نشد");
if (research.Codm != request.Codm)
    throw new UnauthorizedException();
await repo.DeleteAsync(request.Id);
```

### 🎯 Research Types
حذف می‌تواند شامل:
- مقاله منتشر شده
- کتاب تألیف شده
- پروژه تحقیقاتی

---

## 14. مثال استفاده

```csharp
var cmd = new DeleteResearchCommand(
    Codm: 12345,
    Id: 789
);
var deletedId = await mediator.Send(cmd);
// Output: 789
// Log: "Research with id 789 deleted."
```

---

## 15. Related Commands

- **CreateResearchCommand**: ایجاد پژوهش
- **UpdateResearchCommand**: بروزرسانی پژوهش
- **DeleteResearchRequestCommand**: حذف از طریق Request System

---

## 16. تغییرات پیشنهادی

### 1. افزودن Codm Validation
```csharp
public async Task<int> Handle(DeleteResearchCommand request, CancellationToken cancellationToken)
{
    var research = await researchRepo.GetByIdAsync(request.Id, cancellationToken);
    
    if (research == null)
        throw new CommandValidationException($"پژوهش با شناسه {request.Id} یافت نشد");
    
    // بررسی Ownership
    if (research.Codm != request.Codm)
        throw new UnauthorizedException("شما مجاز به حذف این پژوهش نیستید");
    
    await researchRepo.DeleteAsync(request.Id, cancellationToken);
    
    logger.LogDebug("Research with id {id} deleted for Codm {Codm}", request.Id, request.Codm);
    
    return request.Id;
}
```

### 2. بهبود Log Level
```csharp
// بجای Debug
logger.LogInformation("Research {Id} deleted for Codm {Codm}", request.Id, request.Codm);
```

### 3. بهبود Exception Message
```csharp
// بجای "حذف ناموفق بود"
throw new RecordNotFoundException<Research>(request.Id);
```

---

</div>
