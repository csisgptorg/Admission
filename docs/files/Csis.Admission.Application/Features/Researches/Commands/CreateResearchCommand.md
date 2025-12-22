<div dir="rtl">

# CreateResearchCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Researches/Commands/CreateResearchCommand.cs`

---

## 1. Purpose (هدف)

این Command برای **ثبت پژوهش‌های دانشجو** (کتاب، مقاله، پروژه) استفاده می‌شود.

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// ایجاد پژوهش
/// </summary>
```

**وضعیت**: خوب - توضیح فارسی کوتاه اما واضح.

---

## 3. خلاصه اتفاقات (What Happens)

1. دریافت اطلاعات پژوهش
2. تبدیل Command به Entity (`ToEntity()`)
3. Insert در Repository
4. بازگشت Id

---

## 4. اجزای اصلی

### Record
```csharp
public sealed record CreateResearchCommand : BaseCommandDto<CreateResearchCommand, Research>, 
    IRequest<int>
{
    public int Codm { get; init; }
    public ResearchType? Type { get; init; }
    public short? SubjectId { get; init; }
    public string Title { get; init; }
    public short? Year { get; init; }
    public ResearchLanguage? Language { get; init; }
    public string BookPublisher { get; init; }
    public string BookShabak { get; init; }
    public string ArticlePublication { get; init; }
    public string ProjectEmployer { get; init; }
}
```

### Handler
```csharp
internal sealed class CreateResearchCommandHandler(IRepository<Research> researchRepo)
    : IRequestHandler<CreateResearchCommand, int>
```

---

## 5. Flow داخل فایل

```
1. Handle()
   ↓
2. request.ToEntity() → Research entity
   ↓
3. InsertAsync(research)
   ↓
4. Return research.Id
```

---

## 6. Dependencies

- `IRepository<Research>` - Generic Repository

---

## 7. Business Rules

### BR-1: انواع پژوهش
```csharp
enum ResearchType {
    Book,      // کتاب
    Article,   // مقاله
    Project    // پروژه
}
```

### BR-2: فیلدهای شرطی
- **کتاب**: BookPublisher + BookShabak
- **مقاله**: ArticlePublication
- **پروژه**: ProjectEmployer

### BR-3: زبان پژوهش
```csharp
enum ResearchLanguage {
    Persian,   // فارسی
    Arabic,    // عربی
    English,   // انگلیسی
    Other      // سایر
}
```

---

## 8. Data Access

```csharp
await researchRepo.InsertAsync(research, cancellationToken)
```

**EF Core**: Direct Insert

---

## 9. Error Handling

❌ بدون Exception Handling

---

## 10. Observability

❌ بدون Logging

---

## 11. Use Cases مرتبط

- **UC-xxx**: ثبت پژوهش دانشجو
- **استفاده**: محاسبه امتیاز علمی، CV دانشجو

---

## 12. Risks & Notes

### Security
⚠️ **عدم Authorization**: چه کسی می‌تواند پژوهش ثبت کند؟

### Validation
⚠️ **Conditional Fields**: فیلدهای شرطی چک نمی‌شوند  
⚠️ **Duplicates**: چک تکراری نیست

### Code Quality
✅ **استفاده از Primary Constructor** (C# 12)  
✅ **BaseCommandDto** برای AutoMapper

---

## 13. Test Ideas

- ثبت کتاب با ناشر و شابک
- ثبت مقاله با نشریه
- ثبت پروژه با کارفرما
- فیلدهای Nullable خالی

---

## 14. خلاصه نکات کلیدی

| جنبه | وضعیت |
|------|-------|
| **Types** | کتاب، مقاله، پروژه |
| **Languages** | فارسی، عربی، انگلیسی، سایر |
| **Pattern** | Generic Repository |
| **AutoMapper** | از BaseCommandDto |
| **Authorization** | ❌ |
| **Validation** | ⚠️ Conditional Fields |

</div>
