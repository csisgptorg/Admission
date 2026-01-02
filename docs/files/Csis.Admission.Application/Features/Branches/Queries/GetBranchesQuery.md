# GetBranchesQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Branches/Queries/GetBranchesQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست شعب حوزه علمیه** از دیتابیس استفاده می‌شود. می‌تواند تمام شعب یا فقط شعبی که دارای نمایندگی هستند را برگرداند.

### کاربرد اصلی:
- Populate کردن Dropdown شعب در فرم‌های ثبت‌نام
- انتخاب شعبه در پروفایل دانشجو
- فیلتر شعب بر اساس داشتن نمایندگی
- لیست شعب برای گزارش‌گیری

---

## 2. ورودی (Input)

```csharp
public sealed record GetBranchesQuery(bool? HasAgency) : IRequest<BranchDto[]>;
```

### پارامترها:
| پارامتر | نوع | اجباری | توضیحات |
|---------|-----|--------|---------|
| `HasAgency` | `bool?` | خیر | فیلتر شعب بر اساس داشتن نمایندگی:<br>- `true`: فقط شعبهایی که نمایندگی دارند<br>- `false`: فقط شعبهایی که نمایندگی ندارند<br>- `null`: تمام شعب |

### نمونه‌های استفاده:
```csharp
// دریافت تمام شعب
var query1 = new GetBranchesQuery(null);

// دریافت فقط شعب دارای نمایندگی
var query2 = new GetBranchesQuery(true);

// دریافت شعب بدون نمایندگی
var query3 = new GetBranchesQuery(false);
```

---

## 3. خروجی (Output)

```csharp
BranchDto[]
```

### نمونه خروجی:
```json
[
  { 
    "id": 1, 
    "name": "حوزه علمیه قم", 
    "hasAgency": true,
    "provinceId": 25
  },
  { 
    "id": 2, 
    "name": "حوزه علمیه مشهد", 
    "hasAgency": true,
    "provinceId": 9
  }
]
```

---

## 4. وابستگی‌ها (Dependencies)

**Dependencies:**
1. **IRepository<Branch,short>**: برای خواندن از جدول Branches

---

## 5. جریان اجرا (Execution Flow)

```mermaid
graph TD
    A[Query Request] --> B{HasAgency is null?}
    B -->|Yes| C[Get All Branches]
    B -->|No| D{HasAgency == true?}
    D -->|Yes| E[Get Branches with Agency]
    D -->|No| F[Get Branches without Agency]
    C --> G[Convert to Array]
    E --> G
    F --> G
    G --> H[Return BranchDto[]]
    
    style A fill:#e1f5ff
    style H fill:#c8e6c9
```

### مراحل:
1. **بررسی HasAgency**:
   - اگر null: تمام شعب
   - اگر true: فقط شعب دارای نمایندگی
   - اگر false: فقط شعب بدون نمایندگی
2. **دریافت داده** از جدول Branches با فیلتر مناسب
3. **تبدیل به آرایه** و Return

---

## 6. قوانین کسب‌وکار (Business Rules)

### BR-1: فیلتر اختیاری بر اساس نمایندگی
- **قانون**: شعب می‌توانند دارای نمایندگی (Agency) باشند یا نباشند
- **کاربرد**: برخی فرم‌ها فقط نیاز به شعب دارای نمایندگی دارند

### BR-2: دریافت همه شعب
- **قانون**: اگر HasAgency = null، تمام شعب بدون فیلتر برگردانده می‌شوند

---

## 7. عملکرد و بهینه‌سازی (Performance)

### ✅ **مزایا:**
1. **Filtered Query**: اگر HasAgency مشخص شود، فیلتر در DB اعمال می‌شود
2. **AutoMapper Projection**: در سطح دیتابیس
3. **Primary Constructor** (C# 12): کد کوتاه‌تر

### پیشنهاد بهبود: Caching
```csharp
private readonly IMemoryCache _cache;

public async Task<BranchDto[]> Handle(GetBranchesQuery query, CancellationToken cancellationToken) 
{
    var cacheKey = $"branches_{query.HasAgency?.ToString() ?? "all"}";
    
    return await _cache.GetOrCreateAsync(cacheKey, async entry => 
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
        var result = await repo.GetAllAsync<BranchDto>(
            x => !query.HasAgency.HasValue || x.HasAgency == query.HasAgency.Value
        );
        return result.ToArray();
    }) ?? Array.Empty<BranchDto>();
}
```

---

## 8. الگوهای طراحی (Design Patterns)

### الگوهای استفاده شده:
1. **CQRS Pattern**: Query جدا از Command
2. **Repository Pattern**: دسترسی به داده از طریق Repository
3. **DTO Pattern**: استفاده از Data Transfer Object
4. **Optional Filter Pattern**: فیلتر اختیاری با nullable parameter
5. **Primary Constructor** (C# 12): تزریق وابستگی در تعریف کلاس

---

## 9. تست‌پذیری (Testability)

### نمونه Unit Tests:
```csharp
[Fact]
public async Task Handle_WithNullHasAgency_ShouldReturnAllBranches()
{
    // Arrange
    var branches = new List<BranchDto>
    {
        new() { Id = 1, Name = "قم", HasAgency = true },
        new() { Id = 2, Name = "تهران", HasAgency = false }
    };
    
    _repoMock.Setup(x => x.GetAllAsync<BranchDto>(It.IsAny<Expression<Func<Branch, bool>>>()))
        .ReturnsAsync(branches);
    
    var query = new GetBranchesQuery(null);
    
    // Act
    var result = await _handler.Handle(query, CancellationToken.None);
    
    // Assert
    Assert.Equal(2, result.Length);
}

[Fact]
public async Task Handle_WithHasAgencyTrue_ShouldReturnOnlyBranchesWithAgency()
{
    // Arrange
    var branches = new List<BranchDto>
    {
        new() { Id = 1, Name = "قم", HasAgency = true }
    };
    
    _repoMock.Setup(x => x.GetAllAsync<BranchDto>(It.IsAny<Expression<Func<Branch, bool>>>()))
        .ReturnsAsync(branches);
    
    var query = new GetBranchesQuery(true);
    
    // Act
    var result = await _handler.Handle(query, CancellationToken.None);
    
    // Assert
    Assert.Single(result);
    Assert.True(result[0].HasAgency);
}
```

---

## 10. مثال استفاده (Usage Example)

### از Controller:
```csharp
[HttpGet("branches")]
public async Task<ActionResult<BranchDto[]>> GetBranches([FromQuery] bool? hasAgency)
{
    var query = new GetBranchesQuery(hasAgency);
    var branches = await _mediator.Send(query);
    return Ok(branches);
}
```

### از Frontend:
```javascript
// دریافت تمام شعب
const response1 = await fetch('/api/branches');
const allBranches = await response1.json();

// دریافت فقط شعب دارای نمایندگی
const response2 = await fetch('/api/branches?hasAgency=true');
const branchesWithAgency = await response2.json();

// پر کردن dropdown
const select = document.getElementById('branchSelect');
branchesWithAgency.forEach(branch => {
    const option = document.createElement('option');
    option.value = branch.id;
    option.text = branch.name;
    select.add(option);
});
```

---

## 11. Command/Query های مرتبط

### Queries مشابه:
- `GetAgenciesByBranchIdQuery`: دریافت نمایندگی‌های یک شعبه
- `GetProvinceByBranchIdQuery`: دریافت استان شعبه
- `ValidateBranchesByProviceIdQuery`: اعتبارسنجی شعبه بر اساس استان

---

## نتیجه‌گیری

این Query یک **Master Data Query** کاربردی است که لیست شعب حوزه علمیه را با قابلیت فیلتر بر اساس داشتن نمایندگی فراهم می‌کند.

### نقاط قوت:
✅ فیلتر اختیاری (Optional Filter)  
✅ استفاده از Primary Constructor (C# 12)  
✅ کد تمیز و کوتاه  
✅ Projection در DB  

### نقاط ضعف:
⚠️ فقدان Caching (پیشنهاد می‌شود)  
⚠️ فقدان Logging  

### Use Cases:
- **UC-030**: تشکیل پرونده - انتخاب شعبه
- **UC-010**: ویرایش اطلاعات دانشجو - تغییر شعبه
- گزارش‌گیری بر اساس شعبه
