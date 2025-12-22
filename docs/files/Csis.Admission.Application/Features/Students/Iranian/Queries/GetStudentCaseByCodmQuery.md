<div dir="rtl">

# GetStudentCaseByCodmQuery

## 📄 اطلاعات کلی

**مسیر:** `Csis.Admission.Application/Features/Students/Iranian/Queries/GetStudentCaseByCodmQuery.cs`  
**نوع:** Query  
**هدف:** دریافت اطلاعات پرونده دانشجو

---

## 🎯 هدف

دریافت **اطلاعات کامل پرونده** دانشجو شامل:
- وضعیت پرونده (CaseStatus)
- تاریخ انقضا (ExpireDate)
- اطلاعات شعبه و مرکز
- تاریخچه تمدیدها

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record GetStudentCaseByCodmQuery(int Codm) : IRequest<StudentCaseDto>;
```

**خروجی:**
```csharp
StudentCaseDto  // اطلاعات کامل پرونده
```

---

## ⚙️ نکات کلیدی

- ✅ استفاده از `IStudentRepository.GetCaseByCodm()` (احتمالاً SP)
- ✅ خطاپردازی با `RecordNotFoundException`
- 📊 Use Case: UC-015 - مشاهده وضعیت پرونده

---

## 📚 مستندات مرتبط

- `GetStudentSummaryCaseByCodmQuery`: خلاصه پرونده (سبک‌تر)
- `GetStudentInfoByCodmQuery`: اطلاعات کامل دانشجو

</div>
