# GetInsuranceInfoByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Insurances/Queries/GetInsuranceInfoByCodmQuery.cs`

## هدف
دریافت **اطلاعات بیمه** دانشجو.

## ورودی/خروجی
```csharp
public sealed record GetInsuranceInfoByCodmQuery(int Codm) : IRequest<InsuranceDto>;
```

## کاربرد
- اطلاعات بیمه درمان
- بیمه تکمیلی
- پوشش بیمه‌ای

## الگوها
✅ CQRS ✅ Repository

## نتیجه
Query اطلاعات بیمه دانشجو و خانواده.  
✅ ارتباط با GetFamilyByNationalCodeQuery
