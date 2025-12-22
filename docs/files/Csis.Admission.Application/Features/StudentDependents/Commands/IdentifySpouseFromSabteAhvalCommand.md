<div dir="rtl">

# IdentifySpouseFromSabteAhvalCommand.cs

**مسیر**: `Csis.Admission.Application/Features/StudentDependents/Commands/IdentifySpouseFromSabteAhvalCommand.cs`

---

## 1. Purpose (هدف)

**شناسایی و اعتبارسنجی همسر** از طریق ثبت احوال قبل از ثبت نهایی. این Command اطلاعات هویتی همسر را دریافت کرده و به کاربر نمایش می‌دهد.

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// فرمان دریافت اطلاعات هویتی از ثبت احوال و ثبت همسر
/// </summary>
```

**یادداشت**: XML Summary گمراه‌کننده است - این Command "ثبت" نمی‌کند، فقط "شناسایی" می‌کند.

---

## 3. خلاصه اتفاقات

```
1. دریافت اطلاعات همسر (کد ملی + تاریخ تولد + تاریخ ازدواج + مذهب)
2. فراخوانی ثبت احوال
3. بازگشت اطلاعات هویتی (نام، نام خانوادگی، پدر، کد ملی، تاریخ تولد)
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record IdentifySpouseFromSabteAhvalCommand : IRequest<SpouseIdentifyDto>
{
    int? Codm                    // کد مرکز دانشجو
    string SpouseNationalCode    // کد ملی همسر
    string SpouseBirthDate       // تاریخ تولد همسر
    string MarriageDate          // تاریخ ازدواج
    Religion Religion            // مذهب همسر
}
```

**یادداشت**: `MarriageDate` و `Religion` دریافت می‌شوند اما استفاده نمی‌شوند!

### Handler:
- **Dependency**: `ICsisWsmService`, `ICsisAuthenticatedUserService`

---

## 5. Flow

```
1. ساخت Request
   └─> GetIdentityInfoByNationalCodeRequest(Codm, SpouseNationalCode, SpouseBirthDate)

2. فراخوانی ثبت احوال
   └─> csisWsmService.GetIdentityInfoByNationalCode(request)

3. بازگشت DTO
   └─> SpouseIdentifyDto(Name, Family, FatherName, Nin, BirthDate)
```

---

## 6. Business Rules

### BR-1: Identification Only
- این Command فقط **شناسایی** می‌کند، **ثبت** نمی‌کند
- ثبت توسط `StudentSpouseRegistryCommand` انجام می‌شود

### BR-2: Two-Step Process
- **Step 1**: این Command → نمایش اطلاعات به کاربر
- **Step 2**: تأیید کاربر → `StudentSpouseRegistryCommand`

---

## 7. Risks & Notes

### امنیت:
- ✅ اعتبارسنجی با ثبت احوال

### Code Quality:
- ❌ **Unused Properties**: `MarriageDate` و `Religion` استفاده نمی‌شوند
- ⚠️ **Misleading XML**: Summary می‌گوید "ثبت" اما فقط "شناسایی" است
- ❌ **Handler Name**: `RegisterSpouseFromSabteAhvalCommandHandler` اما عملکرد Identify است

### کارایی:
- ✅ سبک و سریع (یک API Call)

---

## 8. Use Case های مرتبط

- **UC-050**: ثبت ازدواج
- **Flow**:
  1. کاربر کد ملی و تاریخ تولد همسر را وارد می‌کند
  2. `IdentifySpouseFromSabteAhvalCommand` → دریافت اطلاعات
  3. نمایش اطلاعات به کاربر (نام، نام خانوادگی، ...)
  4. کاربر تأیید می‌کند
  5. `StudentSpouseRegistryCommand` → ثبت نهایی

مرتبط با:
- [StudentSpouseRegistryCommand.md](./StudentSpouseRegistryCommand.md)

---

## 9. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | شناسایی همسر از ثبت احوال |
| **ورودی** | Codm? + SpouseNationalCode + SpouseBirthDate + MarriageDate + Religion |
| **خروجی** | SpouseIdentifyDto |
| **عملکرد** | Identify (نه Register) |
| **Unused** | ❌ MarriageDate, Religion |
| **Handler Name** | ⚠️ گمراه‌کننده (Register vs Identify) |
| **XML Summary** | ⚠️ نادرست |

---

**پیشنهاد بهبود**:
1. تصحیح XML Summary
2. تغییر نام Handler به `IdentifySpouseFromSabteAhvalCommandHandler`
3. حذف Properties استفاده نشده یا استفاده از آن‌ها

</div>
