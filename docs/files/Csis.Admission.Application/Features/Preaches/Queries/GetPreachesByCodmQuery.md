<div dir="rtl">

# GetPreachesByCodmQuery

**مسیر**: `Csis.Admission.Application/Features/Preaches/Queries/GetPreachesByCodmQuery.cs`

## Purpose
دریافت لیست تمام سوابق تبلیغ یک دانشجو.

## Flow
```
repo.GetAllAsync(x => x.Codm == codm)
  ↓
OrderByDescending(x => x.Id)
  ↓
return List<PreachDto>
```

## Return
لیست تبلیغ‌ها (مرتب از جدید به قدیم)

</div>
