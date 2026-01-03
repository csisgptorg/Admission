# GetMosqueListQuery.cs

**مسیر**: `Csis.Admission.Application/Features/ImamJamaat/Queries/GetMosqueListQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لیست مساجد** با قابلیت جستجو و Paging استفاده می‌شود.

### کاربرد اصلی:
- لیست مساجد ثبت شده
- جستجو در مساجد
- مدیریت امامت جماعت

---

## 2. ورودی و خروجی

```csharp
public sealed record GetMosqueListQuery : BaseSearchQuery, IRequest<IPagedList<MosqueListDto>>;
```

**ورودی**: BaseSearchQuery شامل:
- `SearchFilters` - فیلترهای جستجو
- `PageIndex`, `PageSize` - Paging
- `SortBy` - مرتب‌سازی

**خروجی**: `IPagedList<MosqueListDto>`

---

## 3. قوانین کسب‌وکار

### BR-1: حذف منطقی
- مساجد حذف شده نمایش داده نمی‌شوند (`!x.Mosque.Deleted`)

---

## 4. الگوهای طراحی

1. **CQRS Pattern**
2. **Repository Pattern**
3. **Paging Pattern**
4. **Search/Filter Pattern**

---

## نتیجه‌گیری

Query برای **مدیریت امامت جماعت**.

✅ جستجوی پیشرفته  
✅ Paging  
✅ Soft Delete
