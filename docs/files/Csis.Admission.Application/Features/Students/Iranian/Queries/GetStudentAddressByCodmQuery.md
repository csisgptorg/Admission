<div dir="rtl">

# GetStudentAddressByCodmQuery

## 📄 اطلاعات کلی

**مسیر:** `Features/Students/Queries/GetStudentAddressByCodmQuery.cs`  
**نوع:** Query  
**هدف:** دریافت آدرس دانشجو

---

## 🎯 هدف

دریافت آدرس **شخصی** دانشجو (نه آدرس مرکز یا مکان‌های دیگر).

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record GetStudentAddressByCodmQuery(int Codm) : IRequest<AddressDto>;
```

**خروجی:** `AddressDto` (یا خالی)

---

## 🔄 جریان اجرا

```
1. جستجو در جدول Address
   └─> Codm == Codm AND ProjectCode == 1

2. اگر یافت شد
   └─> بازگشت AddressDto

3. اگر یافت نشد
   └─> بازگشت new AddressDto() (خالی)
```

---

## ⚙️ نکات کلیدی

### ProjectCode = 1 (Self)
```csharp
var selfProjectCode = 1;
```
- `ProjectCode = 1`: آدرس شخصی
- سایر کدها: پروژه‌ها/مکان‌های دیگر

### بازگشت خالی به جای Exception
```csharp
return result ?? new AddressDto();
```
- برخلاف سایر Queries که Exception می‌دهند
- این Query آدرس خالی برمی‌گرداند
- **دلیل**: آدرس اختیاری است

---

## 💡 نکته ویژه

### ✅ الگوی Null Object
- به جای `null` یا Exception
- یک DTO خالی برمی‌گرداند
- کاربر می‌تواند بررسی کند آدرس موجود است یا نه

### ⚠️ نیاز به Validator
```csharp
RuleFor(x => x.Codm).GreaterThan(0);
```

---

## 📚 مستندات مرتبط

- `CreateOrUpdateStudentAddressCommand`: افزودن/بروزرسانی آدرس
- `AddressDto`: ساختار آدرس

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **سادگی** | 10/10 |
| **Business Logic** | 9/10 (Null Object Pattern) |
| **Validation** | 5/10 |

**نکته برجسته:** استفاده از Null Object Pattern 👍

</div>
