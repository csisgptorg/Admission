# GetPreachGradesByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/PreachGrades/Queries/GetPreachGradesByCodmQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت نمرات و امتیازات فعالیت‌های تبلیغی** دانشجو استفاده می‌شود.

### کاربرد اصلی:
- نمایش امتیازات تبلیغی دانشجو
- محاسبه امتیاز کل برای هدفمندی یارانه
- گزارش‌گیری فعالیت‌های تبلیغی

---

## 2. ورودی (Input)

```csharp
public sealed record GetPreachGradesByCodmQuery(int Codm) : IRequest<PreachGradeDto[]>;
```

### پارامترها:
| پارامتر | نوع | اجباری | توضیحات |
|---------|-----|--------|---------|
| `Codm` | `int` | بله | کد ملی دانشجو |

---

## 3. خروجی (Output)

```csharp
PreachGradeDto[]
```

---

## 4. وابستگی‌ها (Dependencies)

**Dependencies:**
1. **IRepository<PreachGrade>**: دسترسی به جدول نمرات تبلیغ

---

## 5. جریان اجرا (Execution Flow)

```mermaid
graph TD
    A[Query با Codm] --> B[Filter by Codm]
    B --> C[Get PreachGrades]
    C --> D[Convert to DTO Array]
    D --> E[Return Array]
    
    style A fill:#e1f5ff
    style E fill:#c8e6c9
```

---

## 6. قوانین کسب‌وکار (Business Rules)

### BR-1: فیلتر بر اساس دانشجو
- فقط نمرات تبلیغی مرتبط با Codm دانشجو برگردانده می‌شوند

---

## 7. الگوهای طراحی (Design Patterns)

1. **CQRS Pattern**
2. **Repository Pattern**
3. **DTO Pattern**

---

## 8. Use Cases مرتبط

- **UC-TargetedScores**: محاسبه امتیازات هدفمندی یارانه
- نمایش سوابق فعالیت تبلیغی

---

## نتیجه‌گیری

Query برای **امتیازدهی فعالیت‌های تبلیغی**.

### نقاط قوت:
✅ فیلتر بر اساس دانشجو  
✅ استفاده در سیستم هدفمندی یارانه  

### یادآوری:
این امتیازات در محاسبه **GetTargetedScoresInfoByCodmQuery** استفاده می‌شوند.
