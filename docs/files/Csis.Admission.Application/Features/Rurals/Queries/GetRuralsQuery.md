# GetRuralsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Rurals/Queries/GetRuralsQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست روستاها** (Rurals) از دیتابیس استفاده می‌شود. می‌تواند تمام روستاها یا روستاهای یک حوزه (Portion) خاص را برگرداند.

### کاربرد اصلی:
- Populate کردن Dropdown روستاها در فرم‌های ثبت آدرس
- Cascade Dropdown: Portion → Rural
- آخرین سطح در سلسله مراتب آدرس

---

## 2. ورودی (Input)

```csharp
public sealed record GetRuralsQuery(short? PortionId) : IRequest<RuralDto[]>;
```

### پارامترها:
| پارامتر | نوع | اجباری | توضیحات |
|---------|-----|--------|---------|
| `PortionId` | `short?` | خیر | شناسه حوزه برای فیلتر. اگر null باشد، تمام روستاها برگردانده می‌شوند |

---

## 3. خروجی (Output)

```csharp
RuralDto[]
```

---

## 4. جریان اجرا (Execution Flow)

```mermaid
graph TD
    A[Query Request] --> B{PortionId is null?}
    B -->|Yes| C[Get All Rurals]
    B -->|No| D[Filter by PortionId]
    C --> E[Convert to Array]
    D --> E
    E --> F[Return RuralDto[]]
    
    style A fill:#e1f5ff
    style F fill:#c8e6c9
```

---

## 5. قوانین کسب‌وکار (Business Rules)

### BR-1: فیلتر اختیاری بر اساس حوزه
- **قانون**: اگر PortionId ارسال شود، فقط روستاهای آن حوزه برگردانده می‌شوند

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
// انتخاب حوزه → دریافت روستاها
document.getElementById('portionSelect').addEventListener('change', async (e) => {
    const portionId = e.target.value;
    const response = await fetch(`/api/rurals?portionId=${portionId}`);
    const rurals = await response.json();
    
    const ruralSelect = document.getElementById('ruralSelect');
    ruralSelect.innerHTML = '<option value="">انتخاب روستا</option>';
    rurals.forEach(rural => {
        ruralSelect.add(new Option(rural.name, rural.id));
    });
});
```

---

## 8. Command/Query های مرتبط

- `GetPortionsQuery`: دریافت لیست حوزه‌ها
- `GetTownsQuery`: دریافت لیست بخش‌ها

---

## نتیجه‌گیری

این Query **آخرین سطح** در سلسله مراتب آدرس است: Province → City → Portion → Town → **Rural**

### نقاط قوت:
✅ Cascade Dropdown  
✅ Optional Filter  

### پیشنهاد:
⚠️ افزودن Caching
