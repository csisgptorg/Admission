<div dir="rtl">

# DeleteTeachCommand

**مسیر**: `Csis.Admission.Application/Features/Teaches/Commands/DeleteTeachCommand.cs`

---

## Purpose (هدف)

حذف سابقه تدریس از سیستم.

---

## Signature

```csharp
public sealed record DeleteTeachCommand(int Codm, int Id) : IRequest<int>;
```

**Record Positional Parameters**: C# 10+

---

## Flow

```
Step 1: teachRepo.DeleteAsync(Id)
  ↓
Step 2: if (!deleted)
  → throw CommandValidationException
  ↓
Step 3: return Id
```

---

## Risks ⚠️

### 1. Cascade Effects
- آیا این تدریس دارای رکوردهای وابسته است؟
- آیا دانشجویانی به این کلاس اضافه شده‌اند؟

**فعلاً**: چک نمی‌شود

### 2. Authorization
- هر کسی می‌تواند حذف کند؟
- آیا صاحب رکورد هست؟

**فعلاً**: چک نمی‌شود

### 3. Soft Delete vs Hard Delete
- Delete واقعی است یا `IsDeleted = true`؟
- **بستگی به پیاده‌سازی Repository دارد**

---

## Best Practices

**پیشنهاد**:
```csharp
// Check ownership
if (teach.Codm != CurrentUser.Codm)
    throw UnauthorizedException();

// Check dependencies
if (await HasStudents(teachId))
    throw new CannotDeleteException("این تدریس دارای دانشجو است");

// Then delete
await teachRepo.DeleteAsync(Id);
```

</div>
