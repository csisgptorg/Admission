<div dir="rtl">

# CreateCulturalActivityCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/CulturalActivities/Commands/CreateCulturalActivityCommand.cs`  
**نوع:** Command  
**هدف:** ثبت فعالیت‌های فرهنگی دانشجو

---

## 🎯 هدف

ثبت **فعالیت فرهنگی** برای:
- محاسبه امتیاز فعالیت‌های فرهنگی
- ثبت نوع فعالیت
- ثبت سال انجام

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record CreateCulturalActivityCommand : IRequest<int>
{
    public int Codm { get; set; }
    public CulturalKind Kind { get; set; }      // نوع فعالیت
    public string OtherKind { get; set; }       // سایر انواع
    public int Year { get; set; }               // سال
}
```

**خروجی:** `int` (شناسه CulturalActivity)

---

## 🔄 جریان اجرا

```
1. تبدیل به Entity
   └─> ToEntity()

2. Insert
   └─> InsertAsync

3. بازگشت Id
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: نوع فعالیت
**CulturalKind Enum:**
- مسابقات قرآنی
- همایش‌های فرهنگی
- برنامه‌های هنری
- سایر

### BR-2: OtherKind
- اگر Kind = "سایر" → باید OtherKind پر شود
- توضیح نوع فعالیت غیرمتداول

### BR-3: سال
- Year: سال انجام فعالیت
- برای محاسبه امتیاز

---

## 💡 نکات

### ⚠️ Validation ناقص
```csharp
// نیاز به Validator:
if (Kind == CulturalKind.Other && string.IsNullOrEmpty(OtherKind))
    throw new ValidationException("توضیح نوع فعالیت الزامی است");
```

### Simple Command
- فقط Insert (بدون Upsert)
- منطق ساده

---

## 📚 مستندات مرتبط

- `DeleteCulturalActivityCommand`: حذف
- `CulturalActivityGrades`: نمره‌دهی

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Simplicity** | 10/10 |
| **Validation** | 5/10 |

</div>
