# GetEliteLevelsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/EliteLevels/Queries/GetEliteLevelsQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست سطوح نخبگی** (Elite Levels) از دیتابیس استفاده می‌شود.

### کاربرد اصلی:
- Populate کردن Dropdown سطوح نخبگی
- انتخاب سطح نخبگی در ثبت/ویرایش اطلاعات نخبگان
- مثال سطوح: ملی، بین‌المللی، استانی

---

## 2. ورودی (Input)

```csharp
public sealed record GetEliteLevelsQuery : IRequest<EliteLevelDto[]>;
```

### پارامترها:
این Query **هیچ پارامتر ورودی ندارد** و تمام سطوح نخبگی را برمی‌گرداند.

---

## 3. خروجی (Output)

```csharp
EliteLevelDto[]
```

---

## 4. الگوهای طراحی (Design Patterns)

1. **CQRS Pattern**
2. **Repository Pattern**
3. **DTO Pattern**
4. **Collection Expressions** (C# 12): `[.. result]`

---

## 5. مثال استفاده (Usage Example)

### از Controller:
```csharp
[HttpGet("elite-levels")]
public async Task<ActionResult<EliteLevelDto[]>> GetEliteLevels()
{
    var query = new GetEliteLevelsQuery();
    var levels = await _mediator.Send(query);
    return Ok(levels);
}
```

### از Frontend:
```javascript
const response = await fetch('/api/elite-levels');
const levels = await response.json();

// Populate dropdown
const select = document.getElementById('eliteLevelSelect');
levels.forEach(level => {
    select.add(new Option(level.name, level.id));
});
```

---

## 6. Command/Query های مرتبط

- `GetEliteTypesQuery`: دریافت انواع نخبگی
- `CreateEliteCommand`: ثبت نخبه جدید

---

## نتیجه‌گیری

این Query یک **Master Data Query** برای دریافت سطوح نخبگی است.

### نقاط قوت:
✅ کد تمیز و ساده  
✅ Collection Expressions (C# 12)

### پیشنهاد:
⚠️ افزودن Caching (سطوح نادراً تغییر می‌کنند)
