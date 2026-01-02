# CreateCulturalActivityCommand.cs

**مسیر**: `Csis.Admission.Application/Features/CulturalActivities/Commands/CreateCulturalActivityCommand.cs`

## هدف
**ثبت فعالیت فرهنگی جدید**.

## ورودی/خروجی
```csharp
public sealed record CreateCulturalActivityCommand(...) : IRequest<int>;
```

## کاربرد
- ثبت فعالیت فرهنگی
- امتیازدهی
- مستندسازی فعالیت‌ها

## الگوها
✅ CQRS ✅ Command

## نتیجه
Command ثبت فعالیت فرهنگی.
