<div dir="rtl">

# CreatePregnancyCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/Pregnancies/Commands/CreatePregnancyCommand.cs`  
**نوع:** Command  
**هدف:** ثبت اطلاعات بارداری همسر دانشجو

---

## 🎯 هدف

ثبت **بارداری** برای همسر دانشجو جهت:
- محاسبه مزایا
- پیگیری وضعیت بارداری
- ثبت تاریخ شروع و پایان

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record CreatePregnancyCommand : IRequest
{
    public int Codm { get; init; }
    public string StartDate { get; init; }  // تاریخ شروع (رشته)
    public string EndDate { get; init; }    // تاریخ پایان (رشته)
    public long? RequestId { get; init; }   // شناسه درخواست
}
```

---

## 🔄 جریان اجرا

```
1. دریافت بارداری موجود (اگر هست)
   └─> GetOneAsTrackingAsync

2. تبدیل تاریخ‌ها
   ├─> StartDate: string → int
   └─> EndDate: string → int

3. تبدیل به Entity
   └─> ToEntity() (AutoMapper)

4. بروزرسانی
   └─> UpdateAsync
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: یک بارداری فعال
- هر دانشجو فقط یک بارداری فعال دارد
- بارداری جدید، قدیمی را جایگزین می‌کند

### BR-2: تاریخ‌ها
- StartDate: تاریخ شروع بارداری
- EndDate: تاریخ تقریبی زایمان (9 ماه بعد)

---

## 🚨 مشکلات

### ⚠️ عدم بررسی تداخل
```csharp
// هیچ چکی برای تداخل تاریخ‌ها نیست
// آیا StartDate < EndDate?
```

### ⚠️ منطق Update
```csharp
pregnancy = await repo.GetOneAsTrackingAsync(...);
pregnancy = command.ToEntity();  // ⚠️ Reference قبلی از بین می‌رود
await repo.UpdateAsync(pregnancy, ...);
```
**مشکل:** `pregnancy` اول دریافت می‌شود اما استفاده نمی‌شود!

**منطق صحیح:**
```csharp
var existing = await repo.GetOneAsTrackingAsync(...);
if (existing != null) {
    // بروزرسانی existing
} else {
    // ایجاد جدید
    await repo.AddAsync(command.ToEntity());
}
```

---

## 💡 پیشنهاد بهبود

### Validator
```csharp
RuleFor(x => x.StartDate)
    .Must(BeValidPersianDate)
    .Must((cmd, start) => start < cmd.EndDate)
    .WithMessage("تاریخ شروع باید قبل از پایان باشد");
```

---

## 📚 مستندات مرتبط

- `DeletePregnancyCommand`: حذف بارداری
- `CreatePregnancyRequestCommand`: درخواست ثبت

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Business Logic** | 6/10 |
| **Code Quality** | 4/10 (منطق Update نادرست) |
| **Validation** | 3/10 |

**نیاز فوری:** رفع منطق Update و افزودن Validation

</div>
