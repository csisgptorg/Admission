<div dir="rtl">

# GetTeachByIdQuery

**مسیر**: `Csis.Admission.Application/Features/Teaches/Queries/GetTeachByIdQuery.cs`

## Purpose
دریافت جزئیات یک سابقه تدریس خاص.

## Flow
```
teachRepo.GetByIdAsync<TeachDto>(Id)
  ↓
if (null)
  → RecordNotFoundException<Teach>
  ↓
return TeachDto
```

## Use Case
نمایش جزئیات برای Edit یا View

</div>
