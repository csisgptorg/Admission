# UpdateNonStudentDependantCommand.cs

**مسیر**: `Csis.Admission.Application/Features/NonStudentDependants/Commands/UpdateNonStudentDependantCommand.cs`

## هدف
**بروزرسانی اطلاعات فرد تحت تکفل غیردانشجو**.

## ورودی/خروجی
```csharp
public sealed record UpdateNonStudentDependantCommand(...) : IRequest;
```

## کاربرد
- ویرایش اطلاعات تکفل
- بروزرسانی وضعیت
- مدیریت افراد تحت تکفل

## الگوها
✅ CQRS ✅ Command

## نتیجه
Command بروزرسانی افراد تحت تکفل کارمندان.
