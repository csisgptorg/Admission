# SearchDocumentsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Documents/Queries/SearchDocumentsQuery.cs`

## هدف
جستجو و دریافت **لیست اسناد و مدارک** با Paging.

## ورودی/خروجی
```csharp
public sealed record SearchDocumentsQuery : BaseSearchQuery, IRequest<IPagedList<DocumentDto>>;
```
- BaseSearchQuery: SearchFilters, PageIndex, PageSize, SortBy
- خروجی: لیست صفحه‌بندی شده اسناد

## کاربرد
- مدیریت اسناد و مدارک دانشجویان
- جستجوی پیشرفته در اسناد
- آرشیو مدارک

## الگوها
✅ CQRS ✅ Paging ✅ Search/Filter

## نتیجه
Query جستجوی اسناد با قابلیت فیلتر و مرتب‌سازی.
