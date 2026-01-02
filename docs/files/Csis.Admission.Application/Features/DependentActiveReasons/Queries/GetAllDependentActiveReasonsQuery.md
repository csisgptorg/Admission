# GetAllDependentActiveReasonsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/DependentActiveReasons/Queries/GetAllDependentActiveReasonsQuery.cs`

## هدف
دریافت **لیست دلایل فعال‌سازی تکفل** (ازدواج، تولد فرزند، ...).

## ورودی/خروجی
```csharp
public sealed record GetAllDependentActiveReasonsQuery : IRequest<DependentActiveReasonDto[]>;
```

## کاربرد
- Dropdown دلایل فعال‌سازی
- فرآیند فعال‌سازی پرونده تکفل

## الگوها
✅ CQRS ✅ Repository ✅ DTO

## نتیجه
Query Master Data برای مدیریت تکفل.  
⚠️ پیشنهاد: Caching
