# GetStudentAdmissionAuditLogsByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/AdmissionAuditLogs/Queries/GetStudentAdmissionAuditLogsByCodmQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت لاگ‌های Audit** تغییرات دانشجو استفاده می‌شود.

### کاربرد اصلی:
- ردیابی تغییرات اطلاعات دانشجو
- Audit Trail برای Compliance
- بررسی تاریخچه تغییرات

---

## 2. ورودی (Input)

```csharp
public sealed record GetStudentAdmissionAuditLogsByCodmQuery(int Codm) : IRequest<AuditLogDto[]>;
```

| پارامتر | نوع | توضیحات |
|---------|-----|---------|
| `Codm` | `int` | کد ملی دانشجو |

---

## 3. خروجی (Output)

```csharp
AuditLogDto[] // لیست لاگ‌های تغییرات
```

### ساختار DTO:
- `Timestamp` - زمان تغییر
- `UserId` - کاربر ایجادکننده تغییر
- `Action` - نوع عملیات (Create, Update, Delete)
- `OldValue` - مقدار قبلی
- `NewValue` - مقدار جدید
- `FieldName` - نام فیلد تغییر یافته

---

## 4. الگوهای طراحی

1. **CQRS Pattern**
2. **Audit Trail Pattern**
3. **Repository Pattern**

---

## نتیجه‌گیری

Query مهم برای **Audit و Compliance**.

✅ ردیابی تغییرات  
✅ Timestamp دقیق
