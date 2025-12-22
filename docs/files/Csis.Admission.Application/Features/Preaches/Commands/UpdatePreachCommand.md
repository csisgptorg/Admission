<div dir="rtl">

# UpdatePreachCommand

**مسیر**: `Csis.Admission.Application/Features/Preaches/Commands/UpdatePreachCommand.cs`

## Purpose
بروزرسانی سابقه تبلیغ موجود.

## Flow
```
GetByIdAsTrackingAsync(Id)
  → if null: CommandValidationException
  ↓
ToEntity(preach) → Custom Mappings
  ↓
UpdateAsync(preach, saveChanges: true)
  ↓
return preach.Id
```

## Error Message Issue ⚠️

```csharp
throw new CommandValidationException(
    $"تبلیغ با شناسه {request.Id} یافت نشد."
);
```

**مشکل**: پیام باید "برای بروزرسانی یافت نشد" باشد.

</div>
