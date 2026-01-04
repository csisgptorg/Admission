# GetContinuousInformationTabsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/ContinuousInformationTabs/Queries/GetContinuousInformationTabsQuery.cs`

## هدف
دریافت **تب‌های اطلاعاتی مستمر** (UI Configuration).

## ورودی/خروجی
```csharp
public sealed record GetContinuousInformationTabsQuery : IRequest<ContinuousInformationTabDto[]>;
```

## کاربرد
- پیکربندی رابط کاربری
- تب‌های داینامیک
- سفارشی‌سازی UI

## الگوها
✅ CQRS ✅ UI Configuration

## نتیجه
Query پیکربندی تب‌های اطلاعاتی.
