# GetHousingBasicInfoByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/HousingAdmissionInfo/Queries/GetHousingBasicInfoByCodmQuery.cs`

## 1. هدف (Purpose)

این Query برای **دریافت اطلاعات پایه مسکن** دانشجو استفاده می‌شود.

### کاربرد اصلی:
- اطلاعات واحد مسکونی
- وضعیت اجاره/مالکیت
- آدرس و مشخصات مسکن

---

## 2. ورودی (Input)

```csharp
public sealed record GetHousingBasicInfoByCodmQuery(int Codm) : IRequest<HousingBasicInfoDto>;
```

| پارامتر | نوع | توضیحات |
|---------|-----|---------|
| `Codm` | `int` | کد ملی دانشجو |

---

## 3. خروجی (Output)

```csharp
HousingBasicInfoDto
```

---

## 4. الگوهای طراحی

1. **CQRS Pattern**
2. **Repository Pattern**

---

## نتیجه‌گیری

Query برای **اطلاعات پایه مسکن**.

✅ اطلاعات واحد مسکونی  
✅ وضعیت مالکیت
