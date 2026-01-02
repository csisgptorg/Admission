# GetAllDependentDeActiveReasonsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/DependentDeActiveReasons/Queries/GetAllDependentDeActiveReasonsQuery.cs`

## هدف
دریافت **لیست دلایل غیرفعال‌سازی تکفل** (طلاق، فوت، اشتغال، ...).

## ورودی/خروجی
```csharp
public sealed record GetAllDependentDeActiveReasonsQuery : IRequest<DependentDeActiveReasonDto[]>;
```

## کاربرد
- Dropdown دلایل غیرفعال‌سازی
- فرآیند غیرفعال‌سازی پرونده تکفل

## الگوها
✅ CQRS ✅ Repository ✅ DTO

## نتیجه
Query Master Data برای مدیریت تکفل.  
⚠️ پیشنهاد: Caching
