# GetCountryDivisionsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/CountryDivisions/Queries/GetCountryDivisionsQuery.cs`

## هدف
دریافت **تقسیمات کشوری** (استان‌های سایر کشورها).

## ورودی/خروجی
```csharp
public sealed record GetCountryDivisionsQuery(short? CountryId) : IRequest<CountryDivisionDto[]>;
```

## کاربرد
- تقسیمات کشورهای خارجی
- آدرس‌دهی غیرایرانیان
- Dropdown استان‌های سایر کشورها

## الگوها
✅ CQRS ✅ Repository ✅ Optional Filter

## نتیجه
Query Master Data برای تقسیمات کشوری.  
⚠️ پیشنهاد: Caching
