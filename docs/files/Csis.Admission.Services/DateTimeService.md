# DateTimeService.cs

**مسیر**: `Csis.Admission.Services/DateTimeService.cs`

## 1. هدف (Purpose)

این سرویس برای **دسترسی به تاریخ و زمان جاری** استفاده می‌شود. این سرویس یک Abstraction برای `DateTime` است که امکان Mock کردن در تست‌ها را فراهم می‌کند.

### کاربرد اصلی:
- دسترسی به تاریخ و زمان جاری سیستم
- دسترسی به تاریخ شمسی (Persian)
- دسترسی به UTC DateTime
- استفاده در Audit Logging (CreatedAt, UpdatedAt)
- تست‌پذیری (امکان Mock کردن زمان)

---

## 2. Interface

```csharp
public interface IDateTimeService
{
    DateTime Now { get; }
    PersianDateTime NowPersian { get; }
    DateTime NowUtc { get; }
    DateOnly Today { get; }
    DateOnly TodayUtc { get; }
}
```

---

## 3. Properties (خصوصیات)

### 3.1. Now

**هدف**: دریافت تاریخ و زمان جاری سیستم (Local Time)

#### خروجی:
```csharp
DateTime // تاریخ و زمان جاری (Local)
```

#### کاربرد:
```csharp
var createdAt = _dateTimeService.Now; // 2025-01-02 14:30:45
```

---

### 3.2. NowPersian

**هدف**: دریافت تاریخ و زمان شمسی جاری

#### خروجی:
```csharp
PersianDateTime // تاریخ و زمان شمسی
```

#### کاربرد:
```csharp
var persianNow = _dateTimeService.NowPersian; // 1403/10/12 14:30:45
```

---

### 3.3. NowUtc

**هدف**: دریافت تاریخ و زمان جاری به UTC

#### خروجی:
```csharp
DateTime // تاریخ و زمان UTC
```

#### کاربرد:
```csharp
// برای ذخیره در دیتابیس - بهتر است UTC باشد
var createdAtUtc = _dateTimeService.NowUtc;
```

---

### 3.4. Today

**هدف**: دریافت تاریخ امروز بدون زمان (Local)

#### خروجی:
```csharp
DateOnly // تاریخ امروز
```

#### کاربرد:
```csharp
var today = _dateTimeService.Today; // 2025-01-02
```

---

### 3.5. TodayUtc

**هدف**: دریافت تاریخ امروز بدون زمان (UTC)

#### خروجی:
```csharp
DateOnly // تاریخ امروز UTC
```

---

## 4. چرا این سرویس؟

### مشکل استفاده مستقیم از DateTime.Now:
```csharp
// ❌ مشکل: در Unit Test نمی‌توان Mock کرد
public class MyService
{
    public void DoSomething()
    {
        var now = DateTime.Now; // غیرقابل Mock
    }
}
```

### راه‌حل با IDateTimeService:
```csharp
// ✅ قابل Test و Mock
public class MyService
{
    private readonly IDateTimeService _dateTime;
    
    public MyService(IDateTimeService dateTime)
    {
        _dateTime = dateTime;
    }
    
    public void DoSomething()
    {
        var now = _dateTime.Now; // قابل Mock در Test
    }
}
```

---

## 5. الگوهای طراحی (Design Patterns)

### الگوهای استفاده شده:
1. **Adapter Pattern**: Wrapper برای `DateTime` و `PersianDateTime`
2. **Service Layer Pattern**: سرویس برای دسترسی به زمان
3. **Dependency Injection**: تزریق شدن در سایر سرویس‌ها
4. **Testability Pattern**: امکان Mock کردن در تست‌ها

---

## 6. تست‌پذیری (Testability)

### نمونه Unit Test با Mock:
```csharp
[Fact]
public void DoSomething_ShouldUseSpecificDateTime()
{
    // Arrange
    var fixedDateTime = new DateTime(2025, 1, 1, 12, 0, 0);
    
    var dateTimeServiceMock = new Mock<IDateTimeService>();
    dateTimeServiceMock.Setup(x => x.Now).Returns(fixedDateTime);
    
    var service = new MyService(dateTimeServiceMock.Object);
    
    // Act
    service.DoSomething();
    
    // Assert
    // تست با تاریخ ثابت
}
```

---

## 7. مثال استفاده (Usage Example)

### در Command Handler برای Audit:
```csharp
internal class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
{
    private readonly IDateTimeService _dateTime;
    private readonly IRepository<Student> _repo;
    
    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            Name = request.Name,
            CreatedAt = _dateTime.Now, // ثبت زمان ایجاد
            // یا بهتر:
            CreatedAtUtc = _dateTime.NowUtc // UTC برای دیتابیس
        };
        
        await _repo.AddAsync(student);
        
        return student.Id;
    }
}
```

### در Query برای فیلتر بر اساس تاریخ:
```csharp
internal class GetTodayRequestsQueryHandler : IRequestHandler<GetTodayRequestsQuery, RequestDto[]>
{
    private readonly IDateTimeService _dateTime;
    private readonly IRepository<Request> _repo;
    
    public async Task<RequestDto[]> Handle(GetTodayRequestsQuery request, CancellationToken cancellationToken)
    {
        var today = _dateTime.Today;
        
        var requests = await _repo.GetAllAsync<RequestDto>(
            x => DateOnly.FromDateTime(x.CreatedAt) == today
        );
        
        return requests.ToArray();
    }
}
```

### در Service برای تولید TrackingCode:
```csharp
public async Task<string> GenerateTrackingCode()
{
    var now = _dateTime.NowPersian;
    
    // کد رهگیری با تاریخ شمسی
    return $"{now.Year}{now.Month:00}{now.Day:00}{now.Hour:00}{now.Minute:00}{now.Second:00}";
}
```

---

## 8. Use Cases مرتبط

این سرویس در **تقریباً همه Use Case ها** استفاده می‌شود:

- **Audit Logging**: ثبت CreatedAt و UpdatedAt
- **تولید کد رهگیری**: استفاده از تاریخ و زمان
- **فیلتر بر اساس تاریخ**: امروز، هفته جاری، ماه جاری
- **محاسبه مهلت**: تاریخ انقضا، سررسید
- **گزارش‌گیری**: فیلتر گزارش‌ها بر اساس بازه زمانی

---

## 9. نکات مهم

### ⚠️ **توجه به TimeZone:**
```csharp
// ❌ استفاده از Now برای ذخیره در دیتابیس مشکل ساز است
entity.CreatedAt = _dateTime.Now; // Local Time - مشکل در TimeZone های مختلف

// ✅ بهتر است UTC استفاده شود
entity.CreatedAtUtc = _dateTime.NowUtc; // UTC - استاندارد جهانی
```

### ✅ **مزایا:**
1. **Testability**: قابل Mock در Unit Tests
2. **Consistency**: تمرکز دسترسی به زمان در یک سرویس
3. **Persian Support**: پشتیبانی از تاریخ شمسی
4. **Simplicity**: ساده و قابل استفاده

---

## 10. Registration در DI Container

```csharp
// در DependencyInjection.cs
services.AddScoped<IDateTimeService, DateTimeService>();
```

---

## نتیجه‌گیری

این سرویس یک **Utility Service پایه‌ای** است که در تمام سیستم استفاده می‌شود و امکان **Mock کردن زمان در تست‌ها** را فراهم می‌کند.

### نقاط قوت:
✅ ساده و کاربردی  
✅ قابل Mock در تست‌ها  
✅ پشتیبانی از تاریخ شمسی  
✅ پشتیبانی از UTC  
✅ استفاده از DateOnly (C# 10+)  

### نقاط ضعف:
⚠️ هیچ! این یک سرویس استاندارد و مناسب است  

### توصیه:
- **همیشه از UTC استفاده کنید** برای ذخیره در دیتابیس
- در نمایش به کاربر، UTC را به Local Time تبدیل کنید
