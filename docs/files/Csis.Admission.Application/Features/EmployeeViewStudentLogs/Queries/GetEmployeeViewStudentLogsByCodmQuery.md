# GetEmployeeViewStudentLogsByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/EmployeeViewStudentLogs/Queries/GetEmployeeViewStudentLogsByCodmQuery.cs`

## هدف
دریافت **لاگ‌های مشاهده اطلاعات دانشجو توسط کارمندان**.

## ورودی/خروجی
```csharp
public sealed record GetEmployeeViewStudentLogsByCodmQuery(int Codm) : IRequest<EmployeeViewLogDto[]>;
```

## کاربرد
- Audit Trail - چه کسی اطلاعات را دیده
- امنیت و Privacy
- ردیابی دسترسی‌ها

## الگوها
✅ CQRS ✅ Audit Trail ✅ Privacy

## نتیجه
Query لاگ‌های مشاهده برای Compliance.  
✅ مهم برای امنیت و Privacy
