<div dir="rtl">

# StudentAdvancedSearchQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Students/Iranian/Queries/StudentAdvancedSearchQuery.cs`

---

## 1. Purpose (هدف)

جستجوی پیشرفته دانشجویان با **فیلترهای دینامیک**، **مرتب‌سازی**، و **صفحه‌بندی**. این Query از `BaseSearchQuery` ارث می‌برد و امکان جستجوی پیچیده را فراهم می‌کند.

---

## 2. مستندات XML موجود

```csharp
/// <summary>جستجوی پیشرفته طلبه</summary>
```

**کامل**: جستجوی پیشرفته با فیلترهای دینامیک و Pagination.

---

## 3. خلاصه اتفاقات

```
1. دریافت فیلترهای جستجو از BaseSearchQuery
2. اجرای SearchPagedAsync روی StudentSummary
3. بازگشت نتایج صفحه‌بندی شده
```

---

## 4. اجزای اصلی

### Query:
```csharp
sealed record StudentAdvancedSearchQuery : BaseSearchQuery, IRequest<IPagedList<StudentAdvancedSearchDto>>
```

**Properties از BaseSearchQuery**:
- `SearchFilters` - فیلترهای دینامیک
- `PageIndex` - شماره صفحه
- `PageSize` - تعداد در صفحه
- `SortBy` - ترتیب مرتب‌سازی

### Handler:
- **Dependency**: `IRepository<StudentSummary>`

---

## 5. Flow

```
1. دریافت پارامترها از Query
   ├─> SearchFilters
   ├─> PageIndex, PageSize
   └─> SortBy

2. اجرای جستجو
   └─> repo.SearchPagedAsync<StudentAdvancedSearchDto>(filters, page, size, sort)

3. بازگشت
   └─> IPagedList<StudentAdvancedSearchDto>
```

---

## 6. Business Rules

### BR-1: Dynamic Filtering
- فیلترها به صورت دینامیک از UI می‌آیند
- می‌تواند شامل: کد ملی، نام، نام خانوادگی، شهر، وضعیت، ...

### BR-2: Pagination
- جلوگیری از بارگذاری تمام رکوردها
- بهینه‌سازی کارایی

### BR-3: Sorting
- قابلیت مرتب‌سازی بر اساس فیلدهای مختلف

---

## 7. Error Handling

- **هیچ Exception صریحی ندارد**
- خطاهای احتمالی از Repository

---

## 8. Risks & Notes

### کارایی:
- ✅ **Pagination**: فقط یک صفحه بارگذاری می‌شود
- ⚠️ **Dynamic Filters**: اگر فیلترها پیچیده باشند، Query کند می‌شود
- **پیشنهاد**: Indexing روی فیلدهای پرجستجو

### امنیت:
- ⚠️ **SQL Injection**: بستگی به پیاده‌سازی `SearchPagedAsync` دارد
- **پیشنهاد**: Parameterization در Repository

### Code Quality:
- ✅ ساده و تمیز
- ✅ استفاده از Generic Paging
- ✅ Separation of Concerns (Query از Data Layer جدا)

---

## 9. Use Case های مرتبط

- **UC-013**: جستجوی دانشجو
- **Actors**: کارمندان (برای پیدا کردن دانشجو)

---

## 10. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | جستجوی پیشرفته دانشجویان |
| **ورودی** | Filters + PageIndex + PageSize + SortBy |
| **خروجی** | IPagedList<StudentAdvancedSearchDto> |
| **Pagination** | ✅ |
| **Dynamic Filters** | ✅ |
| **Sorting** | ✅ |
| **کارایی** | ✅ با Pagination |
| **امنیت** | بستگی به Repository دارد |

---

**یادداشت**: این Query نمونه خوبی از **Flexible Search Pattern** است که می‌تواند فیلترهای مختلف را بپذیرد.

</div>
