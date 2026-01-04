# GetCompleteStudentInfoByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/CompleteStudentInfos/Queries/GetCompleteStudentInfoByCodmQuery.cs`

## هدف
دریافت **اطلاعات کامل دانشجو** (تمام جزئیات).

## ورودی/خروجی
```csharp
public sealed record GetCompleteStudentInfoByCodmQuery(int Codm) : IRequest<CompleteStudentInfoDto>;
```

## کاربرد
- نمایش پروفایل کامل
- گزارش جامع دانشجو
- Dashboard دانشجو

## الگوها
✅ CQRS ✅ Aggregate Pattern

## نتیجه
Query جامع تمام اطلاعات دانشجو.  
⚠️ ممکن است حجیم باشد - بهینه‌سازی Query
