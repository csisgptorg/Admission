# DeleteCulturalActivityCommand.cs

**مسیر**: `Csis.Admission.Application/Features/CulturalActivities/Commands/DeleteCulturalActivityCommand.cs`

## هدف
**حذف فعالیت فرهنگی**.

## ورودی/خروجی
```csharp
public sealed record DeleteCulturalActivityCommand(int Id) : IRequest;
```

## کاربرد
- حذف فعالیت
- Soft/Hard Delete
- مدیریت رکوردها

## الگوها
✅ CQRS ✅ Command

## نتیجه
Command حذف فعالیت فرهنگی.
