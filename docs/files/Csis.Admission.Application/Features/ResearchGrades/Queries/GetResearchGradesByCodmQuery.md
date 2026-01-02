# GetResearchGradesByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/ResearchGrades/Queries/GetResearchGradesByCodmQuery.cs`

## هدف
دریافت **نمرات/امتیازات پژوهش** دانشجو.

## ورودی/خروجی
```csharp
public sealed record GetResearchGradesByCodmQuery(int Codm) : IRequest<ResearchGradeDto[]>;
```

## کاربرد
- نمایش امتیازات پژوهشی
- محاسبه امتیاز کل
- گزارش‌گیری فعالیت‌های پژوهشی

## الگوها
✅ CQRS ✅ Repository ✅ DTO

## نتیجه
Query برای امتیازدهی فعالیت‌های پژوهشی.  
✅ استفاده در هدفمندی یارانه
