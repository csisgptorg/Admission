# GetCulturalActivitiesByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/CulturalActivities/Queries/GetCulturalActivitiesByCodmQuery.cs`

## هدف
دریافت **فعالیت‌های فرهنگی** دانشجو.

## ورودی/خروجی
```csharp
public sealed record GetCulturalActivitiesByCodmQuery(int Codm) : IRequest<CulturalActivityDto[]>;
```

## کاربرد
- لیست فعالیت‌های فرهنگی
- امتیازدهی فعالیت‌ها
- گزارش‌گیری

## الگوها
✅ CQRS ✅ Repository

## نتیجه
Query فعالیت‌های فرهنگی دانشجو.
