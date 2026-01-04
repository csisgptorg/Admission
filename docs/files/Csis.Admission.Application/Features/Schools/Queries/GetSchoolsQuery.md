# GetSchoolsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Schools/Queries/GetSchoolsQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست مدارس** از دیتابیس استفاده می‌شود و تمام مدارس را برمی‌گرداند.

### کاربرد اصلی:
- Populate کردن Dropdown مدارس در فرم‌ها
- انتخاب مدرسه در اطلاعات تحصیلی
- لیست مدارس برای گزارش‌گیری

---

## 2. ورودی (Input)

```csharp
public sealed record GetSchoolsQuery : IRequest<SchoolDto[]>;
```

### پارامترها:
این Query **هیچ پارامتر ورودی ندارد** و تمام مدارس را برمی‌گرداند.

---

## 3. خروجی (Output)

```csharp
SchoolDto[]
```

---

## 4. جریان اجرا (Execution Flow)

```mermaid
graph TD
    A[Query Request] --> B[Get All Schools from DB]
    B --> C[Convert to Array]
    C --> D[Return SchoolDto[]]
    
    style A fill:#e1f5ff
    style D fill:#c8e6c9
```

---

## 5. الگوهای طراحی (Design Patterns)

1. **CQRS Pattern**: Query جدا از Command
2. **Repository Pattern**: دسترسی به داده از طریق Repository
3. **DTO Pattern**: استفاده از Data Transfer Object
4. **Collection Expressions** (C# 12): `[.. result]`

---

## 6. عملکرد و بهینه‌سازی (Performance)

### پیشنهاد بهبود: Caching
```csharp
// مدارس نادراً تغییر می‌کنند - مناسب برای Caching
var cacheKey = "schools_all";
return await _cache.GetOrCreateAsync(cacheKey, async entry => 
{
    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
    var result = await _repo.GetAllAsync<SchoolDto>();
    return [.. result];
}) ?? [];
```

---

## 7. مثال استفاده (Usage Example)

### از Controller:
```csharp
[HttpGet("schools")]
public async Task<ActionResult<SchoolDto[]>> GetSchools()
{
    var query = new GetSchoolsQuery();
    var schools = await _mediator.Send(query);
    return Ok(schools);
}
```

### از Frontend:
```javascript
const response = await fetch('/api/schools');
const schools = await response.json();

// Populate dropdown
const select = document.getElementById('schoolSelect');
schools.forEach(school => {
    select.add(new Option(school.name, school.id));
});
```

---

## 8. Command/Query های مرتبط

- `GetEducationsQuery`: دریافت اطلاعات تحصیلی که شامل مدرسه است

---

## نتیجه‌گیری

این Query یک **Master Data Query** ساده برای دریافت لیست مدارس است.

### نقاط قوت:
✅ کد تمیز و ساده  
✅ استفاده از Collection Expressions (C# 12)  

### پیشنهاد:
⚠️ افزودن Caching (مدارس نادراً تغییر می‌کنند)
