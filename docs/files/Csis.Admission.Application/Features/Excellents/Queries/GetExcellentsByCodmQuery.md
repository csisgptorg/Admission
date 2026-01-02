# GetExcellentsByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Excellents/Queries/GetExcellentsByCodmQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت سوابق برتری تحصیلی** دانشجو استفاده می‌شود.

### کاربرد اصلی:
- اطلاعات برتری‌های تحصیلی (رتبه‌های برتر)
- سطح تحصیلی برتری
- سال تحصیلی کسب برتری

---

## 2. ورودی و خروجی

```csharp
public sealed record GetExcellentsByCodmQuery(int Codm) : IRequest<List<ExcellentDto>>;
```

**خروجی**: لیست برتری‌ها (مرتب شده بر اساس سال تحصیلی)

---

## 3. قوانین کسب‌وکار

### BR-1: مرتب‌سازی
- نتایج بر اساس `EducationYearId` به صورت نزولی (جدیدترین سال)

---

## 4. الگوهای طراحی

1. **CQRS Pattern**
2. **Repository Pattern**
3. **Collection Expressions** (C# 12)

---

## نتیجه‌گیری

Query برای **مدیریت برترین‌های تحصیلی**.

✅ لیست رتبه‌های برتر  
✅ مرتب‌سازی بر اساس سال  
⚠️ استفاده در امتیازدهی
