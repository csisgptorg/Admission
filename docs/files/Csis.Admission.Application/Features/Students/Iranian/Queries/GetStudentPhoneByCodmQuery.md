<div dir="rtl">

# GetStudentPhoneByCodmQuery

## 📄 اطلاعات کلی

**مسیر:** `Features/Students/Iranian/Queries/GetStudentPhoneByCodmQuery.cs`  
**نوع:** Query  
**هدف:** دریافت شماره تلفن دانشجو

---

## 🎯 هدف

دریافت **اطلاعات تماس** دانشجو (شماره موبایل، تلفن ثابت).

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record GetStudentPhoneByCodmQuery(int Codm) : IRequest<StudentPhoneDto>;
```

**خروجی:**
```csharp
StudentPhoneDto  // شامل: Mobile, Phone, etc.
```

---

## 🔄 جریان اجرا

```
1. فراخوانی Repository
   └─> GetPhoneByCodm(Codm)

2. بررسی نتیجه
   ├─> اگر null → CommandValidationException
   └─> اگر موجود → بازگشت StudentPhoneDto
```

---

## ⚙️ نکات

### Exception Type
```csharp
throw new CommandValidationException("شماره تلفن طلبه یافت نشد.");
```
- استفاده از `CommandValidationException` (نه `RecordNotFoundException`)
- ناسازگاری با سایر Queries

### پیشنهاد
```csharp
throw new RecordNotFoundException<StudentPhoneDto>(request.Codm);
```

---

## 📚 مستندات مرتبط

- `UpdateStudentPhoneCommand`: بروزرسانی شماره

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **سادگی** | 10/10 |
| **Exception Handling** | 6/10 (ناسازگاری) |

</div>
