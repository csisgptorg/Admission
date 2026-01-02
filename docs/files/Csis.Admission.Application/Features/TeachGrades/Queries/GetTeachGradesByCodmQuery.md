# GetTeachGradesByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/TeachGrades/Queries/GetTeachGradessByCodmQuery.cs`

## هدف
دریافت **نمرات/امتیازات تدریس** دانشجو.

## ورودی/خروجی
```csharp
public sealed record GetTeachGradesByCodmQuery(int Codm) : IRequest<TeachGradeDto[]>;
```

## کاربرد
- نمایش امتیازات تدریس
- محاسبه امتیاز کل
- گزارش‌گیری فعالیت‌های آموزشی

## الگوها
✅ CQRS ✅ Repository ✅ DTO

## نتیجه
Query برای امتیازدهی فعالیت‌های تدریس.  
✅ استفاده در هدفمندی یارانه
