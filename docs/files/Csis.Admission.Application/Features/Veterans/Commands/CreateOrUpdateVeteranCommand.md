<div dir="rtl">

# CreateOrUpdateVeteranCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/Veterans/Commands/CreateOrUpdateVeteranCommand.cs`  
**نوع:** Command  
**هدف:** ثبت یا بروزرسانی اطلاعات ایثارگری

---

## 🎯 هدف

ثبت **اطلاعات ایثارگری** دانشجو شامل:
- تعداد روزهای دفاع مقدس، حرم، اسارت
- درصد جانبازی
- نسبت با شهید
- نوع شهادت

**اهمیت:** محاسبه امتیاز و مزایای ویژه ایثارگران

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record CreateOrUpdateVeteranCommand : IRequest<int>
{
    public int Codm { get; init; }
    public int? HaramDefenceDays { get; init; }      // دفاع از حرم
    public int? HolyDefenseDays { get; init; }       // دفاع مقدس
    public int? CaptivityDays { get; init; }         // اسارت
    public int? JailDays { get; init; }              // زندان (قبل انقلاب)
    public int? ExileDays { get; init; }             // تبعید (قبل انقلاب)
    public short? VeteranPercent { get; init; }      // درصد جانبازی
    public DependentRelation? RelationWithMartyr { get; init; }  // نسبت با شهید
    public MartyrType? MartyrType { get; init; }     // نوع شهادت
    public long? RequestId { get; init; }
}
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: Upsert Pattern
- یک دانشجو یک رکورد Veteran
- Create OR Update در یک Command

### BR-2: انواع خدمت
**دفاع مقدس:** جنگ تحمیلی  
**دفاع از حرم:** سوریه/عراق  
**اسارت:** اسیر جنگ

---

## 💡 نکات

### ✅ نام صحیح
- نام دقیقاً بیانگر عملکرد است

### ✅ Nullable Fields
- انعطاف‌پذیری بالا

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Business Logic** | 9/10 |
| **Naming** | 10/10 |

</div>
