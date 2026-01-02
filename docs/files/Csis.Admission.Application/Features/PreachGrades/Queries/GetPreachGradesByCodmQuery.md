# GetPreachGradesByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/PreachGrades/Queries/GetPreachGradesByCodmQuery.cs`

## هدف
دریافت **نمرات/امتیازات تبلیغ** دانشجو.

## ورودی/خروجی
```csharp
public sealed record GetPreachGradesByCodmQuery(int Codm) : IRequest<PreachGradeDto[]>;
```

## کاربرد
- نمایش امتیازات تبلیغی
- محاسبه امتیاز کل
- گزارش‌گیری فعالیت‌های تبلیغی

## الگوها
✅ CQRS ✅ Repository ✅ DTO

## نتیجه
Query برای امتیازدهی فعالیت‌های تبلیغی.  
✅ استفاده در هدفمندی یارانه
