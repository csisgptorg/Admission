# GetStudentMemorizerByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Memorizers/Queries/GetStudentMemorizerByCodmQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت اطلاعات حفظ قرآن** دانشجو استفاده می‌شود.

### کاربرد اصلی:
- اطلاعات حافظین و حفظ اجزاء قرآن
- سطح حفظ (کل قرآن، نیمی از قرآن، اجزاء مشخص)
- امتیازدهی بر اساس حفظ قرآن

---

## 2. ورودی و خروجی

```csharp
public sealed record GetStudentMemorizerByCodmQuery(int Codm) : IRequest<List<StudentMemorizerDto>>;
```

**خروجی**: لیست اطلاعات حفظ قرآن (مرتب شده بر اساس جدیدترین)

---

## 3. قوانین کسب‌وکار

### BR-1: مرتب‌سازی
- نتایج بر اساس `Id` به صورت نزولی مرتب می‌شوند (جدیدترین ابتدا)

---

## 4. الگوهای طراحی

1. **CQRS Pattern**
2. **Repository Pattern**
3. **Collection Expressions** (C# 12): `[..result.OrderByDescending]`

---

## نتیجه‌گیری

Query برای **مدیریت حافظین قرآن**.

✅ لیست سوابق حفظ  
✅ مرتب‌سازی بر اساس تاریخ  
⚠️ استفاده در محاسبه امتیازات
