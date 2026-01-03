# GetEducationYearsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/EducationYears/Queries/GetEducationYearsQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست سال‌های تحصیلی** (Education Years) از دیتابیس استفاده می‌شود.

### کاربرد اصلی:
- Populate کردن Dropdown سال‌های تحصیلی
- انتخاب سال تحصیلی در ثبت/ویرایش اطلاعات تحصیلی
- فیلتر داده‌ها بر اساس سال تحصیلی
- گزارش‌گیری

---

## 2. ورودی (Input)

```csharp
public sealed record GetEducationYearsQuery : IRequest<EducationYearDto[]>;
```

### پارامترها:
این Query **هیچ پارامتر ورودی ندارد** و تمام سال‌های تحصیلی را برمی‌گرداند.

---

## 3. خروجی (Output)

```csharp
EducationYearDto[]
```

---

## 4. الگوهای طراحی (Design Patterns)

1. **CQRS Pattern**
2. **Repository Pattern**
3. **DTO Pattern**
4. **Primary Constructor** (C# 12)
5. **Collection Expressions** (C# 12): `[.. result]`

---

## 5. مثال استفاده (Usage Example)

### از Controller:
```csharp
[HttpGet("education-years")]
public async Task<ActionResult<EducationYearDto[]>> GetEducationYears()
{
    var query = new GetEducationYearsQuery();
    var years = await _mediator.Send(query);
    return Ok(years);
}
```

### از Frontend:
```javascript
const response = await fetch('/api/education-years');
const years = await response.json();

// Populate dropdown
const select = document.getElementById('educationYearSelect');
years.forEach(year => {
    select.add(new Option(year.name, year.id));
});
```

---

## 6. Command/Query های مرتبط

- `GetEducationsQuery`: دریافت اطلاعات تحصیلی که شامل سال تحصیلی است

---

## نتیجه‌گیری

این Query یک **Master Data Query** برای دریافت سال‌های تحصیلی است.

### نقاط قوت:
✅ کد تمیز و ساده  
✅ Primary Constructor (C# 12)  
✅ Collection Expressions (C# 12)

### پیشنهاد:
⚠️ افزودن Caching (سال‌های تحصیلی نادراً تغییر می‌کنند)
