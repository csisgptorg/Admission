# GetTownsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Towns/Queries/GetTownsQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست بخش‌ها** (Towns) از دیتابیس استفاده می‌شود. می‌تواند تمام بخش‌ها یا بخش‌های یک حوزه (Portion) خاص را برگرداند.

### کاربرد اصلی:
- Populate کردن Dropdown بخش‌ها در فرم‌های ثبت آدرس
- Cascade Dropdown: Portion → Town
- فیلتر بخش بر اساس حوزه انتخابی

---

## 2. ورودی (Input)

```csharp
public sealed record GetTownsQuery(short? PortionId) : IRequest<TownDto[]>;
```

### پارامترها:
| پارامتر | نوع | اجباری | توضیحات |
|---------|-----|--------|---------|
| `PortionId` | `short?` | خیر | شناسه حوزه برای فیلتر. اگر null باشد، تمام بخش‌ها برگردانده می‌شوند |

---

## 3. خروجی (Output)

```csharp
TownDto[]
```

---

## 4. جریان اجرا (Execution Flow)

```mermaid
graph TD
    A[Query Request] --> B{PortionId is null?}
    B -->|Yes| C[Get All Towns]
    B -->|No| D[Filter by PortionId]
    C --> E[Convert to Array]
    D --> E
    E --> F[Return TownDto[]]
    
    style A fill:#e1f5ff
    style F fill:#c8e6c9
```

---

## 5. قوانین کسب‌وکار (Business Rules)

### BR-1: فیلتر اختیاری بر اساس حوزه
- **قانون**: اگر PortionId ارسال شود، فقط بخش‌های آن حوزه برگردانده می‌شوند
- **کاربرد**: Cascade Dropdown سلسله مراتبی آدرس

---

## 6. الگوهای طراحی (Design Patterns)

1. **CQRS Pattern**: Query جدا از Command
2. **Repository Pattern**: دسترسی به داده از طریق Repository
3. **DTO Pattern**: استفاده از Data Transfer Object
4. **Optional Filter Pattern**: فیلتر اختیاری با nullable parameter

---

## 7. مثال استفاده (Usage Example)

### از Controller:
```csharp
[HttpGet("towns")]
public async Task<ActionResult<TownDto[]>> GetTowns([FromQuery] short? portionId)
{
    var query = new GetTownsQuery(portionId);
    var towns = await _mediator.Send(query);
    return Ok(towns);
}
```

### Cascade Dropdown:
```javascript
// انتخاب حوزه → دریافت بخش‌ها
document.getElementById('portionSelect').addEventListener('change', async (e) => {
    const portionId = e.target.value;
    const response = await fetch(`/api/towns?portionId=${portionId}`);
    const towns = await response.json();
    
    const townSelect = document.getElementById('townSelect');
    townSelect.innerHTML = '<option value="">انتخاب بخش</option>';
    towns.forEach(town => {
        townSelect.add(new Option(town.name, town.id));
    });
});
```

---

## 8. Command/Query های مرتبط

- `GetPortionsQuery`: دریافت لیست حوزه‌ها
- `GetRuralsQuery`: دریافت لیست روستاها (بر اساس بخش)

---

## نتیجه‌گیری

این Query بخشی از **سلسله مراتب آدرس** است: Province → City → Portion → **Town** → Rural

### نقاط قوت:
✅ Cascade Dropdown  
✅ Optional Filter  
✅ کد تمیز  

### پیشنهاد:
⚠️ افزودن Caching
