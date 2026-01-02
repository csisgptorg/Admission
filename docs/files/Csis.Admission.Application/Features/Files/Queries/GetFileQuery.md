# GetFileQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Files/Queries/GetFileQuery.cs`

## هدف
دریافت **محتوای فایل** از سیستم فایل/Storage.

## ورودی/خروجی
```csharp
public sealed record GetFileQuery(Guid FileId) : IRequest<FileModelDto>;
```
- FileId: شناسه یکتای فایل
- خروجی: محتوا، نام فایل، MIME Type

## کاربرد
- دانلود فایل‌های آپلود شده
- نمایش تصاویر
- Download اسناد

## الگوها
✅ CQRS ✅ File Storage

## نتیجه
Query دریافت فایل از Storage.
