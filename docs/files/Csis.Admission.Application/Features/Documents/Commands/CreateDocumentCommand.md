# CreateDocumentCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Documents/Commands/CreateDocumentCommand.cs`

## هدف
**ایجاد سند/مدرک جدید** در سیستم.

## ورودی/خروجی
```csharp
public sealed record CreateDocumentCommand(...) : IRequest<int>;
```
- خروجی: شناسه سند ایجاد شده

## کاربرد
- ثبت مدارک جدید
- آپلود فایل‌های ضمیمه
- مدیریت اسناد

## الگوها
✅ CQRS ✅ Command Pattern

## نتیجه
Command ثبت سند جدید.
