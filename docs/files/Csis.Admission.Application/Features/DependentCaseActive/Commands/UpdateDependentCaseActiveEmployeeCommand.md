<div dir="rtl">

# UpdateDependentCaseActiveEmployeeCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/DependentCaseActive/Commands/UpdateDependentCaseActiveEmployeeCommand.cs`  
**نوع:** Command  
**هدف:** فعال یا غیرفعال کردن پرونده افراد تحت تکفل توسط کارمند

---

## 🎯 هدف

**مدیریت وضعیت پرونده تحت تکفل** با امکان:
- فعال‌سازی (با ذکر دلیل فعال‌سازی)
- غیرفعال‌سازی (با ذکر دلیل غیرفعال‌سازی)
- توسط کارمند (نه دانشجو)

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record UpdateDependentCaseActiveEmployeeCommand(
    int Codm,
    long DependentId,
    DependentDeActiveReasonEnum? DependentDeActiveReason,  // دلیل غیرفعال
    DependentActiveReasonEnum? DependentActiveReason       // دلیل فعال
) : IRequest<long>;
```

**منطق:** یکی از دو پارامتر باید مقدار داشته باشد (نه هر دو)

---

## 🔄 جریان اجرا

```
1. بررسی نوع عملیات
   ├─> DeActiveReason موجود + ActiveReason null → غیرفعال‌سازی
   ├─> ActiveReason موجود + DeActiveReason null → فعال‌سازی
   └─> هر دو موجود یا هر دو null → هیچ عملی انجام نمی‌شود ⚠️

2. غیرفعال‌سازی:
   ├─> ایجاد UpdateStudentDependentCaseDeActiveStatusPrc
   └─> اجرای SP: UpdateDependentCaseDeActiveStatus

3. فعال‌سازی:
   ├─> ایجاد UpdateStudentDependentCaseActiveStatusPrc
   └─> اجرای SP: UpdateDependentCaseActiveStatus

4. بازگشت DependentId
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: Mutual Exclusive
```csharp
if (DeActiveReason != null && ActiveReason == null) { ... }
if (ActiveReason != null && DeActiveReason == null) { ... }
```
- فقط یک عملیات در هر فراخوانی
- نمی‌توان همزمان فعال و غیرفعال کرد

### BR-2: دلایل Enum
**دلایل غیرفعال‌سازی:**
- فوت
- ازدواج (برای فرزندان)
- استقلال مالی
- ...

**دلایل فعال‌سازی:**
- طلاق
- بازگشت به تحت تکفل
- ...

### BR-3: فقط کارمند
- `DataSource = Employee`
- PersonnelId اجباری

---

## 🚨 مشکلات

### ⚠️ مشکل 1: Validation ناقص
```csharp
// اگر هر دو null باشند → هیچ خطایی نمی‌دهد!
// اگر هر دو مقدار داشته باشند → هیچ خطایی نمی‌دهد!
```

**راه حل:**
```csharp
if (DeActiveReason == null && ActiveReason == null)
    throw new CommandValidationException("باید یکی از دلایل مشخص شود");
    
if (DeActiveReason != null && ActiveReason != null)
    throw new CommandValidationException("نمی‌توان همزمان فعال و غیرفعال کرد");
```

### ⚠️ مشکل 2: بازگشت DependentId
```csharp
return request.DependentId;  // همیشه
```
- بازگشت Id عملیات واقعی بهتر است
- فعلاً نمی‌توان فهمید عملیات انجام شد یا نه

---

## 💡 پیشنهاد بهبود

### Validator
```csharp
public class UpdateDependentCaseActiveEmployeeCommandValidator 
    : AbstractValidator<UpdateDependentCaseActiveEmployeeCommand>
{
    public UpdateDependentCaseActiveEmployeeCommandValidator()
    {
        RuleFor(x => x).Must(x => 
            (x.DependentActiveReason != null && x.DependentDeActiveReason == null) ||
            (x.DependentActiveReason == null && x.DependentDeActiveReason != null)
        ).WithMessage("دقیقاً یکی از دلایل باید مشخص شود");
    }
}
```

---

## 🧪 Use Cases

### UC-042: غیرفعال کردن پرونده تحت تکفل

**Actor:** کارمند

**Main Flow:**
1. کارمند دلیل غیرفعال‌سازی را انتخاب می‌کند (مثلاً ازدواج)
2. سیستم پرونده را غیرفعال می‌کند
3. تاریخچه ثبت می‌شود

### UC-041: فعال کردن پرونده تحت تکفل

**Actor:** کارمند

**Main Flow:**
1. کارمند دلیل فعال‌سازی را انتخاب می‌کند (مثلاً طلاق)
2. سیستم پرونده را فعال می‌کند
3. تاریخچه ثبت می‌شود

---

## 📚 مستندات مرتبط

- `AutomaticOpenDependentCaseCommand`: فعال‌سازی خودکار
- `UpdateDependentCaseActiveStatusSeniorCommand`: نسخه Senior

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Business Logic** | 7/10 |
| **Validation** | 3/10 (ناقص) |
| **Code Quality** | 6/10 |

**نیاز فوری:** Validator برای Mutual Exclusive

</div>
