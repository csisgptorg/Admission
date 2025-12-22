<div dir="rtl">

# UpdateTeachCommand

**مسیر**: `Csis.Admission.Application/Features/Teaches/Commands/UpdateTeachCommand.cs`

---

## Purpose (هدف)

بروزرسانی سابقه تدریس موجود.

---

## Flow

```
Step 1: دریافت Id + اطلاعات جدید
  ↓
Step 2: teachRepo.GetByIdAsTrackingAsync(Id)
  → اگر null باشد: RecordNotFoundException
  ↓
Step 3: request.ToEntity(teach)
  → Update کردن properties
  ↓
Step 4: teachRepo.UpdateAsync(teach, saveChanges: true)
```

---

## نکات مهم

### Tracking:
```csharp
GetByIdAsTrackingAsync()
```
- Entity در EF Context Track می‌شود
- تغییرات به صورت خودکار شناسایی می‌شوند

### SaveChanges:
```csharp
UpdateAsync(teach, saveChanges: true)
```
- Immediately ذخیره می‌شود
- بدون Transaction

---

## Type Mismatch Issue ⚠️

**مشکل**:
```csharp
CreateTeachCommand:
  CityId (short?)

UpdateTeachCommand:
  CityId (int?)
```

این inconsistency می‌تواند باعث مشکل شود.

**توصیه**: یکپارچه‌سازی به `int?`

</div>
