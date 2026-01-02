# GetCitiesQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Cities/Queries/GetCitiesQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست شهرها** از دیتابیس استفاده می‌شود. می‌تواند تمام شهرها یا شهرهای یک استان خاص را برگرداند.

### کاربرد اصلی:
- Populate کردن Dropdown شهرها در فرم‌های ثبت آدرس
- انتخاب شهر در Profile Editing  
- فیلتر شهر بر اساس استان انتخابی (Cascade Dropdown)
- لیست شهرها برای جستجو

---

## 2. ورودی (Input)

```csharp
public sealed record GetCitiesQuery(short? ProvinceId) : IRequest<CityDto[]>;
```

### پارامترها:
| پارامتر | نوع | اجباری | توضیحات |
|---------|-----|--------|---------|
| `ProvinceId` | `short?` | خیر | شناسه استان برای فیلتر شهرها. اگر null باشد، تمام شهرها برگردانده می‌شوند |

### نمونه‌های استفاده:
```csharp
// دریافت تمام شهرها
var query1 = new GetCitiesQuery(null);

// دریافت شهرهای استان تهران
var query2 = new GetCitiesQuery(1);
```

---

## 3. خروجی (Output)

```csharp
CityDto[]
```

### نمونه خروجی:
```json
[
  { "id": 101, "name": "تهران", "provinceId": 1 },
  { "id": 102, "name": "ری", "provinceId": 1 },
  { "id": 103, "name": "شمیرانات", "provinceId": 1 }
]
```

---

## 4. وابستگی‌ها (Dependencies)

**Dependencies:**
1. **IRepository<City,short>**: برای خواندن از جدول Cities

---

## 5. جریان اجرا (Execution Flow)

```mermaid
graph TD
    A[Query Request] --> B{ProvinceId is null?}
    B -->|Yes| C[Get All Cities]
    B -->|No| D[Filter by ProvinceId]
    C --> E[Convert to Array]
    D --> E
    E --> F[Return CityDto[]]
    
    style A fill:#e1f5ff
    style F fill:#c8e6c9
```

### مراحل:
1. **بررسی ProvinceId**: اگر null باشد تمام شهرها، وگرنه فقط شهرهای آن استان
2. **دریافت داده** از جدول Cities با فیلتر مناسب
3. **تبدیل به آرایه** و Return

---

## 6. قوانین کسب‌وکار (Business Rules)

### BR-1: فیلتر اختیاری بر اساس استان
- **قانون**: اگر ProvinceId ارسال شود، فقط شهرهای آن استان برگردانده می‌شوند
- **کاربرد**: Cascade Dropdown - وقتی کاربر استان را انتخاب می‌کند، شهرهای آن استان نمایش داده می‌شوند

### BR-2: دریافت همه شهرها
- **قانون**: اگر ProvinceId = null، تمام شهرهای کشور برگردانده می‌شوند

---

## 7. عملکرد و بهینه‌سازی (Performance)

### ✅ **مزایا:**
1. **Filtered Query**: اگر ProvinceId ارسال شود، فقط شهرهای آن استان خوانده می‌شوند
2. **AutoMapper Projection**: در سطح دیتابیس

### پیشنهاد بهبود: Caching با Key بر اساس ProvinceId
```csharp
var cacheKey = $"cities_{request.ProvinceId?.ToString() ?? "all"}";

return await _cache.GetOrCreateAsync(cacheKey, async entry => 
{
    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
    var result = await _repo.GetAllAsync<CityDto>(
        x => request.ProvinceId == null || x.ProvinceId == request.ProvinceId
    );
    return result.ToArray();
}) ?? Array.Empty<CityDto>();
```

---

## 8. الگوهای طراحی (Design Patterns)

### الگوهای استفاده شده:
1. **CQRS Pattern**: Query جدا از Command
2. **Repository Pattern**: دسترسی به داده از طریق Repository
3. **DTO Pattern**: استفاده از Data Transfer Object
4. **Optional Filter Pattern**: فیلتر اختیاری با nullable parameter

---

## 9. مثال استفاده (Usage Example)

### از Controller:
```csharp
[HttpGet("cities")]
public async Task<ActionResult<CityDto[]>> GetCities([FromQuery] short? provinceId)
{
    var query = new GetCitiesQuery(provinceId);
    var cities = await _mediator.Send(query);
    return Ok(cities);
}
```

### از Frontend (Cascade Dropdown):
```javascript
// رویداد تغییر استان
document.getElementById('provinceSelect').addEventListener('change', async (e) => {
    const provinceId = e.target.value;
    
    // دریافت شهرهای استان انتخابی
    const response = await fetch(`/api/cities?provinceId=${provinceId}`);
    const cities = await response.json();
    
    // پر کردن dropdown شهرها
    const citySelect = document.getElementById('citySelect');
    citySelect.innerHTML = '<option value="">انتخاب شهر</option>';
    
    cities.forEach(city => {
        const option = document.createElement('option');
        option.value = city.id;
        option.text = city.name;
        citySelect.add(option);
    });
});
```

---

## 10. Command/Query های مرتبط

### Queries مشابه:
- `GetProvincesQuery`: دریافت لیست استان‌ها
- `GetTownsQuery`: دریافت لیست بخش‌ها (بر اساس شهر)
- `GetRuralsQuery`: دریافت لیست روستاها

---

## نتیجه‌گیری

این Query یک **Cascade Dropdown Query** کاربردی است که امکان فیلتر شهرها بر اساس استان را فراهم می‌کند.

### نقاط قوت:
✅ فیلتر اختیاری (Optional Filter)  
✅ کد تمیز و قابل فهم  
✅ Projection در DB  

### نقاط ضعف:
⚠️ فقدان Caching (پیشنهاد می‌شود)  
⚠️ فقدان Validation برای ProvinceId
