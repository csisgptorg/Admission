<div dir="rtl">

# CreateTeachCommand

**مسیر**: `Csis.Admission.Application/Features/Teaches/Commands/CreateTeachCommand.cs`

---

## 1. Purpose (هدف)

ثبت **مستقیم** سابقه تدریس برای طلبه بدون نیاز به تأیید مدیر.

این Command برای ثبت سریع فعالیت‌های تدریسی توسط کاربران دارای مجوز (مدیران/کارمندان) استفاده می‌شود.

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// CreateTeachCommand
/// </summary>
```

**وضعیت**: ناقص - فقط نام Command

**تکمیل استنباطی**: ثبت مستقیم سابقه تدریس برای طلبه

---

## 3. خلاصه اتفاقات (What Happens)

1. دریافت اطلاعات تدریس از کاربر
2. تبدیل Command به Entity با `ToEntity()` (AutoMapper)
3. ثبت در دیتابیس با `teachRepo.InsertAsync()`
4. بازگشت `Id` رکورد ثبت شده

**نکته**: این Direct Command است - بدون Request-Approval Flow

---

## 4. اجزای اصلی

### Command Properties:
```csharp
- Codm (int): کد مرکز دانشجو
- ProvinceId (short?): استان محل تدریس
- CityId (short?): شهر محل تدریس
- EducationYearId (short?): سال تحصیلی
- EducationSemester (enum?): ترم تحصیلی
- EducationLevel (TeachEducationLevel?): مقطع تحصیلی
- Lesson (string): نام درس
- SchoolId (short?): مدرسه/مرکز آموزشی
- WeekSession (short?): تعداد جلسات هفتگی
- ApprovalCenter (enum?): مرکز حوزوی صادرکننده
- RecordIdInApprovalCenter (string): شناسه در سیستم مرکز
```

### Handler:
```csharp
CreateTeachCommandHandler(IRepository<Teach> teachRepo)
```

### Return Type:
```csharp
IRequest<int> → Id رکورد جدید
```

---

## 5. Flow داخل فایل

```
Step 1: دریافت CreateTeachCommand
   ↓
Step 2: command.ToEntity()
   → استفاده از AutoMapper
   → تبدیل DTO به Entity
   ↓
Step 3: teachRepo.InsertAsync(teach)
   → ثبت در Table Teaches
   ↓
Step 4: return teach.Id
   → بازگشت شناسه رکورد جدید
```

---

## 6. Dependencies

### Injected:
- `IRepository<Teach>`: دسترسی به Table Teaches

### Base Class:
- `BaseCommandDto<CreateTeachCommand, Teach>`:
  - فراهم‌سازی `ToEntity()` با AutoMapper
  - امکان Custom Mappings

### کلاس‌های مرتبط:
- [Teach Entity](/docs/index/Domain_Entities.md#teach)
- [TeachEducationLevel Enum](/docs/index/Enums.md)
- [EducationSemester Enum](/docs/index/Enums.md)
- [ApprovalCenter Enum](/docs/index/Enums.md)

---

## 7. Business Rules

### BR-1: Direct Registration
- این Command برای ثبت مستقیم است
- **نیاز به Authorization**: باید مدیر/کارمند باشد
- بدون Workflow تأیید

### BR-2: Teaching Information
- حداقل: Codm + Lesson
- سایر فیلدها Optional
- مکان (Province/City) برای محاسبه امتیاز

### BR-3: Approval Center Integration
- `RecordIdInApprovalCenter`: شناسه در سیستم مرکز حوزوی
- امکان ردیابی Cross-System

---

## 8. Data Access

### EF Core:
```csharp
await teachRepo.InsertAsync(teach, cancellationToken: cancellationToken);
```

**نکته**: 
- Insert ساده بدون Transaction
- SaveChanges خودکار توسط Repository

### Table:
- `Teaches`

---

## 9. Error Handling

**Potential Errors**:
1. Validation Errors (FluentValidation)
2. Database Constraints Violation
3. Foreign Key Errors (SchoolId, CityId, ...)

**Exception Types**:
- `ValidationException` (از Validator)
- `DbUpdateException` (EF Core)

---

## 10. Observability

### Logging:
- ❌ **عدم Logging** در Handler
- Logging باید در Infrastructure Layer باشد

### Audit:
- ✅ رکورد ثبت می‌شود (Id برمی‌گردد)
- ❌ UserId/PersonnelId ثبت نمی‌شود

---

## 11. Use Cases مرتبط

### UC-Teach-001: ثبت سابقه تدریس (مستقیم)
```
Actor: مدیر/کارمند
Preconditions: دارای مجوز ثبت مستقیم
Flow:
  1. ورود اطلاعات تدریس
  2. اجرای CreateTeachCommand
  3. ثبت بلافاصله بدون تأیید
Postconditions: سابقه تدریس ثبت شد
```

### Related:
- [UC-Teach-002: درخواست ثبت تدریس](/docs/files/Csis.Admission.Application/Features/Teaches/Commands/CreateTeachRequestCommand.md)
- [UC-Teach-003: بروزرسانی تدریس](/docs/files/Csis.Admission.Application/Features/Teaches/Commands/UpdateTeachCommand.md)

---

## 12. Risks & Notes

### Security:
- ⚠️ **عدم Authorization Check**: Command چک نمی‌کند کاربر مجاز است یا نه
- ⚠️ **عدم Ownership Check**: می‌توان برای هر Codm ثبت کرد

### Performance:
- ✅ Insert ساده - سریع
- ✅ بدون Join/Include

### Code Quality:
- ⚠️ **XML Comments ناقص**: WeekSession بدون توضیح
- ✅ Primary Constructor استفاده شده
- ✅ BaseCommandDto pattern

### Concurrency:
- ✅ مشکل Concurrency ندارد (Insert جدید)

---

## 13. Test Ideas

### Happy Path:
```csharp
[Fact]
public async Task CreateTeach_ValidData_ReturnsId()
{
    // Arrange
    var command = new CreateTeachCommand 
    {
        Codm = 123,
        Lesson = "فقه",
        ProvinceId = 1
    };
    
    // Act
    var id = await handler.Handle(command, CancellationToken.None);
    
    // Assert
    id.Should().BeGreaterThan(0);
}
```

### Edge Cases:
```csharp
// تمام فیلدها Null (به جز Codm)
// Lesson خیلی طولانی
// SchoolId نامعتبر
```

### Security:
```csharp
// ثبت برای Codm دیگری (باید Fail شود)
// کاربر بدون مجوز (نیاز به [Authorize])
```

---

## 14. مقایسه با CreateTeachRequestCommand

| ویژگی | CreateTeachCommand | CreateTeachRequestCommand |
|-------|-------------------|---------------------------|
| **جریان** | مستقیم | Request-Approval |
| **کاربران** | مدیر/کارمند | دانشجو |
| **تأیید** | ندارد | دارد |
| **Return** | `int` (Id) | `long` (RequestId) |
| **Service** | Repository | RequestService |

---

## 15. خلاصه نکات کلیدی

| بخش | توضیح |
|-----|-------|
| **الگو** | Direct Command (بدون Approval) |
| **Base Class** | BaseCommandDto |
| **Mapping** | AutoMapper (ToEntity) |
| **Authorization** | ❌ ندارد (نیاز به اضافه شدن) |
| **Audit** | ⚠️ ناقص (فقط Insert) |
| **Validation** | FluentValidator (جدا) |
| **Complexity** | Low |
| **Performance** | High (Insert ساده) |

</div>
