# GetEliteTypesQuery.cs

**مسیر**: `Csis.Admission.Application/Features/EliteTypes/Queries/GetEliteTypesQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست انواع نخبگی** (Elite Types) از دیتابیس استفاده می‌شود.

### کاربرد اصلی:
- Populate کردن Dropdown انواع نخبگی
- انتخاب نوع نخبگی در ثبت/ویرایش اطلاعات نخبگان
- دسته‌بندی نخبگان بر اساس نوع

---

## 2. ورودی (Input)

```csharp
public sealed record GetEliteTypesQuery : IRequest<EliteTypeDto[]>;
```

### پارامترها:
این Query **هیچ پارامتر ورودی ندارد** و تمام انواع نخبگی را برمی‌گرداند.

---

## 3. خروجی (Output)

```csharp
EliteTypeDto[]
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
[HttpGet("elite-types")]
public async Task<ActionResult<EliteTypeDto[]>> GetEliteTypes()
{
    var query = new GetEliteTypesQuery();
    var types = await _mediator.Send(query);
    return Ok(types);
}
```

---

## 6. Command/Query های مرتبط

- `GetEliteLevelsQuery`: دریافت سطوح نخبگی
- `CreateEliteCommand`: ثبت نخبه جدید

---

## نتیجه‌گیری

این Query یک **Master Data Query** برای دریافت انواع نخبگی است.

### نقاط قوت:
✅ کد تمیز و ساده  
✅ Collection Expressions (C# 12)

### پیشنهاد:
⚠️ افزودن Caching
