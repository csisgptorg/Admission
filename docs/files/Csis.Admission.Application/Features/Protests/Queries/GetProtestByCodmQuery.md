# GetProtestByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Protests/Queries/GetProtestByCodmQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت اعتراضات دانشجو** استفاده می‌شود.

### کاربرد اصلی:
- لیست اعتراضات ثبت شده
- پیگیری وضعیت اعتراض
- بررسی درخواست‌های اعتراض

---

## 2. ورودی (Input)

```csharp
public sealed record GetProtestByCodmQuery(int Codm) : IRequest<ProtestDto[]>;
```

| پارامتر | نوع | توضیحات |
|---------|-----|---------|
| `Codm` | `int` | کد ملی دانشجو |

---

## 3. خروجی (Output)

```csharp
ProtestDto[]
```

---

## 4. الگوهای طراحی

1. **CQRS Pattern**
2. **Repository Pattern**

---

## نتیجه‌گیری

Query برای **مدیریت اعتراضات**.

✅ لیست اعتراضات  
✅ وضعیت پیگیری
