<div dir="rtl">

# CreatePreachRequestCommand

**مسیر**: `Csis.Admission.Application/Features/Preaches/Commands/CreatePreachRequestCommand.cs`

## Purpose
ایجاد درخواست ثبت سابقه تبلیغ (Request-Approval Workflow).

## Flow
```
CreatePreachRequestCommand
  ↓
IRequestService.Create()
  ↓
RequestType: CreatePreach
RequestFlow: DirectRegistration
  ↓
return RequestId (long)
```

## تفاوت با CreatePreachCommand

| CreatePreach | CreatePreachRequest |
|-------------|---------------------|
| مستقیم | درخواست |
| `int` | `long` |
| Repository | RequestService |

</div>
