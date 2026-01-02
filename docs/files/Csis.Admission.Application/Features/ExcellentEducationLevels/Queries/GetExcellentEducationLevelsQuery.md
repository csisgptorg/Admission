# GetExcellentEducationLevelsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/ExcellentEducationLevels/Queries/GetExcellentEducationLevelsQuery.cs`

## هدف
دریافت **لیست سطوح تحصیلی برتری** (دیپلم، کاردانی، کارشناسی، ...).

## ورودی/خروجی
```csharp
public sealed record GetExcellentEducationLevelsQuery : IRequest<ExcellentEducationLevelDto[]>;
```

## کاربرد
- Dropdown سطوح تحصیلی برتری
- ثبت برتری تحصیلی

## الگوها
✅ CQRS ✅ Repository ✅ DTO

## نتیجه
Query ساده Master Data.  
⚠️ پیشنهاد: Caching
