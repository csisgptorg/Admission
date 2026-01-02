# GetNonStudentDependantsByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/NonStudentDependants/Queries/GetNonStudentDependantsByCodmQuery.cs`

## هدف
دریافت **افراد تحت تکفل غیردانشجو** (کارمندان).

## ورودی/خروجی
```csharp
public sealed record GetNonStudentDependantsByCodmQuery(int PersonnelId) : IRequest<NonStudentDependantDto[]>;
```

## کاربرد
- لیست افراد تحت تکفل کارمند
- مدیریت تکفل
- بیمه

## الگوها
✅ CQRS ✅ Repository

## نتیجه
Query افراد تحت تکفل کارمندان (غیردانشجو).
