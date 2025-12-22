<div dir="rtl">

# CreatePersonMarriageCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Marriages/Commands/CreatePersonMarriageCommand.cs`

---

## 1. Purpose (هدف)

**ثبت رکورد ازدواج** جدید در سیستم با اعتبارسنجی وجود شوهر و همسر.

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// ایجاد موجودیت ازدواج جدید
/// </summary>
```

**کامل**: ثبت ازدواج با تاریخ‌های ازدواج، طلاق، و فوت.

---

## 3. خلاصه اتفاقات

```
1. بررسی وجود شوهر (اختیاری)
2. بررسی وجود همسر (اختیاری)
3. ایجاد رکورد Marriage
4. بازگشت Id
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record CreatePersonMarriageCommand : BaseCommandDto<..., Marriage>, IRequest<int>
{
    int? HusbandPersonId     // شناسه شوهر
    int? WifePersonId        // شناسه همسر
    DateOnly? DeathDate      // تاریخ فوت
    DateOnly? DivorceDate    // تاریخ طلاق
    DateOnly? MarriageDate   // تاریخ ازدواج
}
```

**یادداشت**: تمام فیلدها `Nullable` هستند.

### Handler Dependencies:
- `IPersonMarriageRepository` - ذخیره ازدواج
- `IPersonRepository` - بررسی وجود افراد
- `ILogger<...>` - (تزریق شده اما استفاده نشده)

---

## 5. Flow

```
1. if (HusbandPersonId != null)
       └─> بررسی وجود در Person Repository
       └─> if (!exists) → CommandValidationException

2. if (WifePersonId != null)
       └─> بررسی وجود در Person Repository
       └─> if (!exists) → CommandValidationException

3. ایجاد Entity
   └─> marriage = command.ToEntity()

4. ذخیره
   └─> InsertAsync(marriage)

5. بازگشت
   └─> return marriage.Id
```

---

## 6. Business Rules

### BR-1: Optional Persons
- هم شوهر و هم همسر `Nullable` هستند
- **سؤال**: آیا می‌تواند هر دو null باشند؟

### BR-2: Person Existence Validation
- اگر HusbandPersonId ارسال شود، باید موجود باشد
- اگر WifePersonId ارسال شود، باید موجود باشد

### BR-3: Multiple Dates
- `MarriageDate`: تاریخ ازدواج
- `DivorceDate`: تاریخ طلاق (nullable)
- `DeathDate`: تاریخ فوت (nullable)

---

## 7. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `CommandValidationException` | شوهر نامعتبر | "شوهر انتخاب شده نامعتبر است" |
| `CommandValidationException` | همسر نامعتبر | "همسر انتخاب شده نامعتبر است" |

---

## 8. Risks & Notes

### Business Logic:
- ⚠️ **Nullable Persons**: هر دو می‌توانند null باشند؟
- **پیشنهاد**: حداقل یکی باید پر باشد

### Code Quality:
- ❌ **ILogger Unused**: تزریق شده اما استفاده نشده
- ✅ Validation قبل از Insert

### Data Integrity:
- ⚠️ **No Uniqueness Check**: آیا یک نفر می‌تواند چند بار ازدواج کند؟
- **پیشنهاد**: بررسی ازدواج فعال (بدون DivorceDate)

---

## 9. Use Case های مرتبط

- **UC-055**: ثبت ازدواج
- مرتبط با:
  - [DeletePersonMarriageCommand.md](./DeletePersonMarriageCommand.md)
  - [UpdatePersonMarriageCommand](#)

---

## 10. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | ثبت رکورد ازدواج |
| **ورودی** | HusbandPersonId? + WifePersonId? + Dates |
| **خروجی** | Id (int) |
| **Validation** | وجود افراد |
| **Nullable** | ⚠️ همه فیلدها Nullable |
| **Unused** | ❌ ILogger |
| **Uniqueness** | ❌ چک نشده |

</div>
