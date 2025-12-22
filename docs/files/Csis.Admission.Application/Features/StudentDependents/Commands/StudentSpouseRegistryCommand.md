<div dir="rtl">

# StudentSpouseRegistryCommand.cs

**مسیر**: `Csis.Admission.Application/Features/StudentDependents/Commands/StudentSpouseRegistryCommand.cs`

---

## 1. Purpose (هدف)

**ثبت همسر** برای دانشجو به عنوان فرد تحت تکفل. این Command پس از اعتبارسنجی اطلاعات همسر، آن را در سیستم ثبت می‌کند.

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// ثبت همسر
/// </summary>
```

**کامل**: ثبت همسر دانشجو به عنوان تکفل.

---

## 3. خلاصه اتفاقات

```
1. دریافت Codm و اطلاعات همسر
2. فراخوانی Stored Procedure ثبت
3. بازگشت Id تکفل
```

---

## 4. اجزای اصلی

### Command:
```csharp
record StudentSpouseRegistryCommand : IRequest<long>
{
    int Codm                                                 // کد مرکز دانشجو
    StudentDependentRegistryPrcRequest StudentDependentRegistryPrcRequest  // اطلاعات کامل
}
```

### Handler:
- **Dependency**: `IStudentDependentRepository`

---

## 5. Flow

```
1. فراخوانی Repository
   └─> dependentRepository.Create(StudentDependentRegistryPrcRequest)

2. بازگشت
   └─> return Id
```

---

## 6. Business Rules

### BR-1: Delegation Pattern
- این Command فقط واسط است
- اعتبارسنجی‌ها در Command دیگر انجام شده (احتمالاً `IdentifySpouseFromSabteAhvalCommand`)

### BR-2: Stored Procedure
- استفاده از SP برای ثبت (Performance و Transaction Safety)

---

## 7. Use Case های مرتبط

- **UC-050**: ثبت ازدواج
- **Precondition**: اطلاعات همسر از ثبت احوال اعتبارسنجی شده
- **Flow**:
  1. `IdentifySpouseFromSabteAhvalCommand` → اعتبارسنجی
  2. `StudentSpouseRegistryCommand` → ثبت

مرتبط با:
- [IdentifySpouseFromSabteAhvalCommand.md](./IdentifySpouseFromSabteAhvalCommand.md)
- [StudentChildRegistryCommand.md](./StudentChildRegistryCommand.md)

---

## 8. Risks & Notes

### Code Quality:
- ✅ ساده و واضح
- ✅ Separation of Concerns (اعتبارسنجی جدا، ثبت جدا)

### امنیت:
- ⚠️ **Authorization**: چک نشده
- باید در Controller یا Validator باشد

---

## 9. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | ثبت همسر به عنوان تکفل |
| **ورودی** | Codm + StudentDependentRegistryPrcRequest |
| **خروجی** | Id (long) |
| **Delegation** | ✅ فقط واسط به Repository |
| **Validation** | در Command دیگر |
| **امنیت** | ⚠️ Authorization نامشخص |

</div>
