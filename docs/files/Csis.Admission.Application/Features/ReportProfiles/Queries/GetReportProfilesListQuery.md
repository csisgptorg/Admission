# GetReportProfilesListQuery.cs

**مسیر**: `Csis.Admission.Application/Features/ReportProfiles/Queries/GetReportProfilesListQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست پروفایل‌های گزارش** استفاده می‌شود.

### کاربرد اصلی:
- لیست ساختارهای گزارش ذخیره شده
- الگوهای گزارش
- پروفایل‌های از پیش تعریف شده

---

## 2. ورودی و خروجی

```csharp
public sealed record GetReportProfilesListQuery : IRequest<ReportProfileDto[]>;
```

**خروجی**: آرایه پروفایل‌های گزارش

---

## 3. الگوهای طراحی

1. **CQRS Pattern**
2. **Repository Pattern**
3. **Profile Pattern**

---

## نتیجه‌گیری

Query برای **مدیریت پروفایل‌های گزارش**.

✅ الگوهای گزارش ذخیره شده  
✅ قابلیت استفاده مجدد
