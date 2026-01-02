# UploadFileCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Files/Commands/UploadFileCommand.cs`

## هدف
**آپلود فایل** به سیستم Storage.

## ورودی/خروجی
```csharp
public sealed record UploadFileCommand(IFormFile File, ...) : IRequest<Guid>;
```
- File: فایل آپلودی
- خروجی: شناسه فایل ذخیره شده

## کاربرد
- آپلود تصاویر
- آپلود اسناد
- ذخیره فایل‌ها

## الگوها
✅ CQRS ✅ File Upload ✅ Storage

## نتیجه
Command آپلود فایل به Storage.  
⚠️ اعتبارسنجی نوع و حجم فایل
