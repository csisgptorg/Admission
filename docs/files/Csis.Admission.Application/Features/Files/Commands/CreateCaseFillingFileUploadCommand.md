# CreateCaseFillingFileUploadCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Files/Commands/CreateCaseFillingFileUploadCommand.cs`

## هدف
**آپلود فایل مربوط به تشکیل پرونده**.

## ورودی/خروجی
```csharp
public sealed record CreateCaseFillingFileUploadCommand(...) : IRequest<Guid>;
```

## کاربرد
- آپلود مدارک تشکیل پرونده
- ضمائم فرآیند ثبت‌نام
- مدیریت فایل‌های پرونده

## الگوها
✅ CQRS ✅ File Upload ✅ Case Filing

## نتیجه
Command آپلود فایل تشکیل پرونده.  
✅ استفاده در UC-030: تشکیل پرونده
