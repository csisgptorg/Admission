# GetNonStudentByIdQuery.cs

**مسیر**: `Csis.Admission.Application/Features/NonStudents/Queries/GetNonStudentByIdQuery.cs`

## هدف
دریافت اطلاعات **غیردانشجو** (کارمند، پرسنل).

## ورودی/خروجی
```csharp
public sealed record GetNonStudentByIdQuery(int Id) : IRequest<NonStudentDto>;
```

## کاربرد
- اطلاعات کارمندان
- پرسنل حوزه
- مدیریت کاربران غیردانشجو

## الگوها
✅ CQRS ✅ Repository

## نتیجه
Query اطلاعات کارمندان/غیردانشجویان.
