<div dir="rtl">

# CreateEliteCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/Elites/Commands/CreateEliteCommand.cs`  
**نوع:** Command  
**هدف:** ثبت اطلاعات نخبگان

---

## 🎯 هدف

ثبت **نخبگان** (Elite) برای:
- محاسبه امتیاز ویژه
- ثبت نوع و سطح نخبگی
- مدت زمان نخبگی
- مرجع تایید

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record CreateEliteCommand : IRequest<int>
{
    public int Codm { get; set; }
    public short? EliteTypeId { get; set; }       // نوع نخبگی
    public short? EliteLevelId { get; set; }      // سطح نخبگی
    public string? StartDate { get; set; }        // تاریخ شروع
    public string? EndDate { get; set; }          // تاریخ پایان
    public string ApprovalCenterTitle { get; set; } // مرجع تایید
    public long? RequestId { get; set; }
}
```

**خروجی:** `int` (شناسه Elite)

---

## 🔄 جریان اجرا

```
1. تبدیل تاریخ‌ها
   ├─> StartDate: string → int
   └─> EndDate: string → int

2. تبدیل به Entity
   └─> ToEntity() (AutoMapper)

3. Insert
   └─> InsertAsync

4. بازگشت Id
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: نوع و سطح نخبگی
- **EliteTypeId**: نوع (علمی، فرهنگی، ورزشی، ...)
- **EliteLevelId**: سطح (محلی، کشوری، بین‌المللی)

### BR-2: مدت زمان
- StartDate, EndDate: مدت اعتبار نخبگی
- Nullable: ممکن است دائمی باشد

### BR-3: مرجع تایید
- ApprovalCenterTitle: نام مرجع تاییدکننده
- الزامی (غیر Nullable)

---

## 💡 نکات

### ✅ Custom Mapping
```csharp
mapping.ForMember(dest => dest.StartDate, 
    cfg => cfg.MapFrom(src => src.StartDate.StringDateToInt()));
```
- تبدیل خودکار تاریخ Persian string به int

### ⚠️ فقط Insert
- این Command فقط Insert می‌کند (نه Update)
- برای Update باید `UpdateEliteCommand` استفاده شود
- برخلاف Famous/Veteran که Upsert بودند

---

## 📚 مستندات مرتبط

- `UpdateEliteCommand`: بروزرسانی
- `DeleteEliteCommand`: حذف
- `CreateEliteRequestCommand`: درخواست

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Business Logic** | 8/10 |
| **Simplicity** | 9/10 |

**نکته:** Simple Insert Command با Custom Mapping

</div>
