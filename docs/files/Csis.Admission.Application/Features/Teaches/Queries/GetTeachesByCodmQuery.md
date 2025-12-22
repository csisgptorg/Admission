<div dir="rtl">

# GetTeachesByCodmQuery

**مسیر**: `Csis.Admission.Application/Features/Teaches/Queries/GetTeachesByCodmQuery.cs`

## Purpose
دریافت لیست تمام سوابق تدریس یک دانشجو.

## Flow
```
teachRepo.GetAllAsync(x => x.Codm == codm)
  ↓
OrderByDescending(x => x.Id) → جدیدترین‌ها اول
  ↓
return List<TeachDto>
```

## Return
```csharp
List<TeachDto>
```

**نکته**: اگر دانشجو هیچ تدریسی نداشته باشد، لیست خالی برمی‌گردد (نه Exception).

</div>
