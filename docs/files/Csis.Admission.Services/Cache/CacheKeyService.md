# CacheKeyService.cs

**مسیر**: `Csis.Admission.Services/Cache/CacheKeyService.cs`

## 1. هدف (Purpose)

این سرویس **Generic** برای **تولید کلیدهای استاندارد Cache** استفاده می‌شود تا کلیدهای Cache در سراسر سیستم یکپارچه باشند.

---

## 2. ساختار کلیدها

### فرمت‌های استاندارد:
```
Entity:         {EntityName}_{Id}
DTO:            {EntityName}_{DtoName}_{Id}
Custom:         {EntityName}__CUSTOM__{Key}
```

### مثال:
```
Student_123
Student_StudentDto_123
Student__CUSTOM__TopStudents
```

---

## 3. متدهای اصلی

```csharp
// Entity
GetEntityKey<Student>(123) → "Student_123"

// DTO
GetDtoKey<Student, StudentDto>(123) → "Student_StudentDto_123"

// Custom
GetCustomKey<Student>("Active") → "Student__CUSTOM__Active"

// Wildcard (برای حذف گروهی)
GetDtoWildcardKey(typeof(Student), 123) → "Student_*_123"
```

---

## 4. مثال استفاده

### Cache و Invalidate:
```csharp
// Cache
var key = _cacheKeyService.GetDtoKey<Student, StudentDto>(123);
await _cache.SetAsync(key, dto, TimeSpan.FromHours(1));

// Invalidate بعد از Update
var pattern = _cacheKeyService.GetDtoWildcardKey(typeof(Student), 123);
await _cache.RemoveByPatternAsync(pattern); // حذف تمام DTO ها
```

---

## نتیجه‌گیری

سرویس **پایه‌ای** برای استانداردسازی کلیدهای Cache.

✅ استاندارد و یکپارچه  
✅ پشتیبانی از Wildcard  
✅ Generic و Thread-Safe  
