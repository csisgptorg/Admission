# GetReportBuildersQuery.cs

**مسیر**: `Csis.Admission.Application/Features/ReportBuilders/Queries/GetReportBuildersQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست سازنده‌های گزارش** (Report Builders) استفاده می‌شود.

### کاربرد اصلی:
- لیست گزارش‌های تعریف شده
- مدیریت گزارش‌ساز
- انتخاب گزارش برای اجرا

---

## 2. ورودی و خروجی

```csharp
public sealed record GetReportBuildersQuery : IRequest<ReportBuilderDto[]>;
```

**خروجی**: آرایه گزارش‌سازها

---

## 3. الگوهای طراحی

1. **CQRS Pattern**
2. **Repository Pattern**
3. **Report Builder Pattern**

---

## نتیجه‌گیری

Query برای **سیستم گزارش‌ساز پویا**.

✅ لیست گزارش‌های تعریف شده  
✅ Report Builder Pattern
