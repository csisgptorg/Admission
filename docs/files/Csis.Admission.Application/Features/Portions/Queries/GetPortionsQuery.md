# GetPortionsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Portions/Queries/GetPortionsQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست حوزه‌ها/بخش‌ها** (Portions) از دیتابیس استفاده می‌شود. می‌تواند تمام حوزه‌ها یا حوزه‌های یک شهر خاص را برگرداند.

### کاربرد اصلی:
- Populate کردن Dropdown حوزه‌ها در فرم‌های ثبت آدرس
- Cascade Dropdown: City → Portion
- بخشی از سلسله مراتب آدرس ایران

---

## 2. ورودی (Input)

```csharp
public sealed record GetPortionsQuery(short? CityId) : IRequest<PortionDto[]>;
```

### پارامترها:
| پارامتر | نوع | اجباری | توضیحات |
|---------|-----|--------|---------|
| `CityId` | `short?` | خیر | شناسه شهر برای فیلتر. اگر null باشد، تمام حوزه‌ها برگردانده می‌شوند |

---

## 3. خروجی (Output)

```csharp
PortionDto[]
```

---

## 4. جریان اجرا (Execution Flow)

```mermaid
graph TD
    A[Query Request] --> B{CityId is null?}
    B -->|Yes| C[Get All Portions]
    B -->|No| D[Filter by CityId]
    C --> E[Convert to Array]
    D --> E
    E --> F[Return PortionDto[]]
    
    style A fill:#e1f5ff
    style F fill:#c8e6c9
```

---

## 5. قوانین کسب‌وکار (Business Rules)

### BR-1: فیلتر اختیاری بر اساس شهر
- **قانون**: اگر CityId ارسال شود، فقط حوزه‌های آن شهر برگردانده می‌شوند
- **کاربرد**: Cascade Dropdown سلسله مراتبی آدرس

---

## 6. الگوهای طراحی (Design Patterns)

1. **CQRS Pattern**
2. **Repository Pattern**
3. **DTO Pattern**
4. **Optional Filter Pattern**

---

## 7. مثال استفاده (Usage Example)

### Cascade Dropdown:
```javascript
// انتخاب شهر → دریافت حوزه‌ها
document.getElementById('citySelect').addEventListener('change', async (e) => {
    const cityId = e.target.value;
    const response = await fetch(`/api/portions?cityId=${cityId}`);
    const portions = await response.json();
    
    const portionSelect = document.getElementById('portionSelect');
    portionSelect.innerHTML = '<option value="">انتخاب حوزه</option>';
    portions.forEach(portion => {
        portionSelect.add(new Option(portion.name, portion.id));
    });
});
```

---

## 8. Command/Query های مرتبط

- `GetCitiesQuery`: دریافت لیست شهرها
- `GetTownsQuery`: دریافت لیست بخش‌ها (بر اساس حوزه)
- `GetRuralsQuery`: دریافت لیست روستاها (بر اساس حوزه)

---

## نتیجه‌گیری

این Query بخشی از **سلسله مراتب آدرس** است: Province → City → **Portion** → Town → Rural

### نقاط قوت:
✅ Cascade Dropdown  
✅ Optional Filter  

### پیشنهاد:
⚠️ افزودن Caching
