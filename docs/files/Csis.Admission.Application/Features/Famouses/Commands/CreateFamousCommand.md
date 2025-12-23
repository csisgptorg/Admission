<div dir="rtl">

# CreateFamousCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/Famouses/Commands/CreateFamousCommand.cs`  
**نوع:** Command  
**هدف:** ثبت اطلاعات فرد مشهور (شخصیت برجسته)

---

## 🎯 هدف

ثبت **شخصیت برجسته** برای دانشجو جهت:
- اعطای امتیاز
- ثبت نقش (روحانی، فرهنگی، اجتماعی، ...)
- ثبت محدوده (محلی، ملی، بین‌المللی)

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record CreateFamousCommand : IRequest<int>
{
    public int Codm { get; init; }
    public AreaEnum Area { get; init; }    // محلی/ملی/بین‌المللی
    public RoleEnum? Role { get; init; }   // نقش
    public TypeEnum Type { get; init; }    // نوع
}
```

**خروجی:** `int` (شناسه Famous)

---

## 🔄 جریان اجرا

```
1. بررسی وجود Famous قبلی
   └─> GetOneAsTrackingAsync(Codm)

2. اگر وجود ندارد
   ├─> ToEntity()
   └─> InsertAsync

3. اگر وجود دارد
   ├─> ToEntity(existingFamous)  // بروزرسانی
   └─> UpdateAsync

4. Log
5. بازگشت Id
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: Upsert Pattern
```csharp
if (existingFamous == null) {
    Insert
} else {
    Update
}
```
- یک دانشجو فقط یک رکورد Famous دارد
- Command هم Create و هم Update را انجام می‌دهد

### BR-2: Enums
**AreaEnum:** محلی، ملی، بین‌المللی  
**RoleEnum:** روحانی، فرهنگی، اجتماعی، ...  
**TypeEnum:** نوع شخصیت

---

## 💡 نکات

### ✅ الگوی خوب: Upsert
- یک API برای Create و Update
- کاربرپسند
- کد تمیز

### ✅ Logging
```csharp
logger.LogDebug("Famous created with id {id}", newFamous.Id);
```
- استفاده صحیح از ILogger
- Structured Logging

### ⚠️ نام گمراه‌کننده
- نام: CreateFamousCommand
- عملکرد: Create **OR** Update
- نام بهتر: `UpsertFamousCommand` یا `CreateOrUpdateFamousCommand`

---

## 📚 مستندات مرتبط

- `DeleteFamousCommand`: حذف
- `UpdateFamousCommand`: بروزرسانی صریح
- `CreateFamousRequestCommand`: درخواست

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Business Logic** | 9/10 (Upsert عالی) |
| **Logging** | 9/10 |
| **Naming** | 5/10 (گمراه‌کننده) |

**نکته برجسته:** الگوی Upsert تمیز و کاربردی 👍

</div>
