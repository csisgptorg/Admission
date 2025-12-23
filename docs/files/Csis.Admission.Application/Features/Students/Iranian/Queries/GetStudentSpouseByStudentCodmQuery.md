<div dir="rtl">

# GetStudentSpouseByStudentCodmQuery

## 📄 اطلاعات کلی

**مسیر:** `Features/Students/Iranian/Queries/GetStudentSpouseByStudentCodmQuery.cs`  
**نوع:** Query  
**هدف:** دریافت اطلاعات همسر(همسران) دانشجو

---

## 🎯 هدف

دریافت **لیست همسر(ها)** دانشجو:
- ممکن است چند همسر داشته باشد (طلاق و ازدواج مجدد)
- شامل همسر فعلی و سابق

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record GetStudentSpouseByStudentCodmQuery(int Codm) : IRequest<StudentSpouseDto[]>;
```

**خروجی:** `StudentSpouseDto[]` (آرایه همسران)

---

## 🔄 جریان اجرا

```
1. فراخوانی Repository
   └─> GetSpousesByStudentCodm(Codm)

2. بازگشت آرایه همسران
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: چند همسر
- یک دانشجو ممکن است چند همسر داشته باشد
- همسر فعلی + همسران سابق (طلاق گرفته)

### BR-2: خالی
- اگر بدون همسر: آرایه خالی
- نه null، نه Exception

---

## 💡 نکات

### ⚠️ IMapper غیرضروری
```csharp
private readonly IMapper _mapper;  // ⚠️ استفاده نمی‌شود
```
- تزریق شده اما استفاده نمی‌شود
- باید حذف شود

### Simple Query
- فقط دریافت و بازگشت
- بدون منطق پیچیده

---

## 📚 مستندات مرتبط

- `CreatePersonMarriageCommand`: ثبت ازدواج
- `UpdateDependentDivorceCommand`: ثبت طلاق

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Simplicity** | 10/10 |
| **Dependencies** | 5/10 (IMapper غیرضروری) |

**پیشنهاد:** حذف IMapper

</div>
