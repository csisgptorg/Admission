<div dir="rtl">

# DeletePreachCommand

**مسیر**: `Csis.Admission.Application/Features/Preaches/Commands/DeletePreachCommand.cs`

## Purpose
حذف سابقه تبلیغ.

## Signature
```csharp
public sealed record DeletePreachCommand(int Codm, int Id) : IRequest<long>;
```

## Return Type
`long` (نه `int` مثل Teaches)

**Inconsistency**: باید یکپارچه شود.

</div>
