<div dir="rtl">

# AutomaticOpenDependentCaseCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/DependentCaseActive/Commands/AutomaticOpenDependentCaseCommand.cs`  
**نوع:** Command  
**هدف:** فعال‌سازی خودکار پرونده تحت تکفل (بعد از طلاق)

---

## 🎯 هدف

**فعال‌سازی خودکار** پرونده بعد از رخدادهای خاص (مثلاً طلاق):
- بدون نیاز به درخواست کاربر
- دلیل پیش‌فرض: طلاق
- ثبت دهک و اطلاعات تجمیعی

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record AutomaticOpenDependentCaseCommand : IRequest<long>
{
    public int Codm { get; set; }
    public long DependentId { get; set; }
    public short? Decile { get; set; }           // دهک تکفل
    public long? RequestId { get; set; }         // شناسه درخواست
    public object? TajmieiSummary { get; set; }  // خلاصه تجمیعی
}
```

**خروجی:** `long` (DependentId)

---

## 🔄 جریان اجرا

```
1. دریافت اطلاعات تحت تکفل
   └─> DependentSummary

2. شناسایی منبع داده
   ├─> PersonnelId موجود → Employee
   └─> PersonnelId null → Student

3. فعال‌سازی پرونده
   ├─> ActiveReason = Divorce (ثابت)
   ├─> اطلاعات کاربر
   └─> SP: UpdateDependentCaseActiveStatus

4. بازگشت DependentId
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: دلیل ثابت = طلاق
```csharp
ActiveReason = DependentActiveReasonEnum.Divorce
```
- همیشه به دلیل طلاق فعال می‌شود
- برای سایر دلایل از Command دیگری استفاده شود

### BR-2: تشخیص خودکار DataSource
```csharp
DataSource = (await currentUserService.PersonnelId()).HasValue 
    ? DataSource.Employee 
    : DataSource.Student
```
- کارمند: Employee
- دانشجو: Student

### BR-3: پارامترهای اختیاری
- `Decile`: دهک (اختیاری) - ⚠️ استفاده نمی‌شود!
- `TajmieiSummary`: خلاصه تجمیعی - ⚠️ استفاده نمی‌شود!

---

## 🚨 مشکلات

### ⚠️ پارامترهای استفاده نشده
```csharp
public short? Decile { get; set; }            // ⚠️
public object? TajmieiSummary { get; set; }   // ⚠️
```
- تعریف شده‌اند اما استفاده نمی‌شوند
- باید حذف یا استفاده شوند

### ⚠️ دلیل ثابت
- فقط برای طلاق کار می‌کند
- انعطاف کم

---

## 💡 پیشنهاد بهبود

### پیشنهاد 1: استفاده از Decile
```csharp
var activateModel = new UpdateStudentDependentCaseActiveStatusPrc {
    ...
    Decile = request.Decile,  // اضافه کردن
    TajmieiSummary = request.TajmieiSummary?.ToString()
};
```

### پیشنهاد 2: پارامتریزه کردن دلیل
```csharp
public DependentActiveReasonEnum ActiveReason { get; set; } = DependentActiveReasonEnum.Divorce;
```

---

## 🧪 Use Case

### UC-043: فعال‌سازی خودکار پرونده بعد از طلاق

**Trigger:** ثبت طلاق

**Main Flow:**
1. سیستم طلاق را ثبت می‌کند
2. **خودکار** این Command را فراخوانی می‌کند
3. پرونده همسر/فرزندان فعال می‌شود
4. دلیل: طلاق

---

## 📚 مستندات مرتبط

- `UpdateDependentCaseActiveEmployeeCommand`: فعال‌سازی دستی
- `CreatePersonMarriageCommand`: منجر به غیرفعال‌سازی
- `UpdateDependentDivorceCommand`: منجر به فعال‌سازی خودکار

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Automation** | 9/10 (عالی) |
| **Flexibility** | 5/10 (دلیل ثابت) |
| **Code Quality** | 6/10 (پارامترهای بلااستفاده) |

**نکته برجسته:** خودکارسازی فرآیند بعد از طلاق 👍

</div>
