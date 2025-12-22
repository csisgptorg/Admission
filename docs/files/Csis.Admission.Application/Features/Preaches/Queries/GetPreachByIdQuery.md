<div dir="rtl">

# GetPreachByIdQuery

**مسیر**: `Csis.Admission.Application/Features/Preaches/Queries/GetPreachByIdQuery.cs`

## Purpose
دریافت جزئیات یک سابقه تبلیغ خاص.

## Flow
```
preachRepo.GetByIdAsync<PreachDto>(Id)
  ↓
if (null)
  → RecordNotFoundException<Preach>
  ↓
return PreachDto
```

</div>
