# GetPictureHistoriesByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/PictureHistories/Queries/GetPictureHistoriesByCodmQuery.cs`

## هدف
دریافت **تاریخچه تصاویر** دانشجو.

## ورودی/خروجی
```csharp
public sealed record GetPictureHistoriesByCodmQuery(int Codm) : IRequest<PictureHistoryDto[]>;
```

## کاربرد
- مشاهده تاریخچه تغییرات تصویر
- Audit تصاویر
- مدیریت عکس‌های پروفایل

## الگوها
✅ CQRS ✅ History Tracking

## نتیجه
Query تاریخچه تصاویر دانشجو.  
✅ Audit Trail برای تصاویر
