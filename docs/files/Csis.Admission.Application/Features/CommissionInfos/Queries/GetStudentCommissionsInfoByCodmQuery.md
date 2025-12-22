<div dir="rtl">

# GetStudentCommissionsInfoByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/CommissionInfos/Queries/GetStudentCommissionsInfoByCodmQuery.cs`

---

## 1. Purpose (هدف)

**دریافت اطلاعات کمیسیون‌های دانشجو** بر اساس کد مرکز (Codm).

---

## 2. مستندات XML موجود

```csharp
/// <summary>کمسیون طلبه</summary>
```

**کامل**: دریافت لیست کمیسیون‌های مرتبط با دانشجو.

---

## 3. خلاصه اتفاقات

```
1. دریافت Codm
2. فراخوانی Repository
3. بازگشت Array از StudentCommissionInfoDto
4. اگر نباشد → RecordNotFoundException
```

---

## 4. اجزای اصلی

### Query:
```csharp
sealed record GetStudentCommissionsInfoByCodmQuery(int Codm) : IRequest<StudentCommissionInfoDto[]>
```

**خروجی**: Array از `StudentCommissionInfoDto`

### Handler Dependencies:
- `IStudentRepository`

---

## 5. Flow

```
1. دریافت Codm
   └─> request.Codm

2. فراخوانی Repository
   └─> _repo.GetStudentCommissionRequestByCodm(Codm)

3. بررسی نتیجه
   if (null)
       └─> throw RecordNotFoundException
   else
       └─> return StudentCommissionInfoDto[]
```

---

## 6. Business Rules

### BR-1: Commission Info
- **کمیسیون**: مجموعه‌ای از درخواست‌ها یا تصمیمات مربوط به دانشجو
- احتمالاً شامل:
  - تاریخ کمیسیون
  - نوع کمیسیون
  - تصمیم
  - وضعیت

### BR-2: Null Handling
- اگر دانشجو کمیسیونی نداشته باشد → Exception
- **سؤال**: آیا بهتر نیست Empty Array بازگردد؟

---

## 7. Error Handling

```csharp
throw new RecordNotFoundException<StudentCommissionInfoDto>(Codm)
```

**یادداشت**: Exception برای عدم وجود، نه Empty Array.

---

## 8. Risks & Notes

### Code Quality:
- ✅ **ساده و واضح**
- ⚠️ **Null vs Empty**: بازگشت null به جای `[]` می‌تواند مشکل‌ساز باشد

### Business Logic:
- **Commission**: نقش در سیستم نامشخص است (نیاز به بررسی DTO)

---

## 9. Use Case های مرتبط

- **UC-090**: مشاهده کمیسیون‌های دانشجو
- **Dashboard/Reports**: نمایش تاریخچه تصمیمات

---

## 10. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | دریافت کمیسیون‌ها |
| **ورودی** | Codm (int) |
| **خروجی** | StudentCommissionInfoDto[] |
| **Null** | → RecordNotFoundException |
| **Empty Array** | ❌ بازگشت داده نمی‌شود |
| **Delegation** | ✅ به Repository |

---

**یادداشت**: ساده‌ترین Query - فقط یک Repository Call.

</div>
