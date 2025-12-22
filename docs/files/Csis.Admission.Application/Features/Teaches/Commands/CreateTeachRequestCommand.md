<div dir="rtl">

# CreateTeachRequestCommand

**مسیر**: `Csis.Admission.Application/Features/Teaches/Commands/CreateTeachRequestCommand.cs`

---

## Purpose (هدف)

ایجاد **درخواست** ثبت سابقه تدریس که نیاز به تأیید مدیر دارد.

این Command از الگوی **Request-Approval Workflow** استفاده می‌کند.

---

## Flow

```
CreateTeachRequestCommand
  ↓
IRequestService.Create()
  ↓
RequestType: CreateTeach
RequestFlow: DirectRegistration
  ↓
Status: Pending
  ↓
بررسی توسط مدیر
  ↓
Approved → CreateTeachCommand (ثبت نهایی)
```

---

## تفاوت با CreateTeachCommand

| ویژگی | CreateTeach | CreateTeachRequest |
|-------|-------------|-------------------|
| **استفاده** | ثبت مستقیم | درخواست + تأیید |
| **Return** | `int` (TeachId) | `long` (RequestId) |
| **Service** | Repository | RequestService |
| **Status** | ثبت شده | Pending |

---

## Business Rules

### BR-1: Request-Approval Pattern
- دانشجو درخواست ثبت می‌کند
- درخواست در Table Requests ثبت می‌شود
- مدیر بررسی و تأیید/رد می‌کند
- در صورت تأیید، CreateTeachCommand اجرا می‌شود

### BR-2: RequestFlow Types
```csharp
DirectRegistration: ثبت بدون تأیید اضافه
ApprovalRequired: نیاز به تأیید چند مرحله‌ای
AutoApproved: تأیید خودکار
```

---

## الگوی Request-Approval Workflow ⭐⭐⭐

این الگو در Features زیر استفاده می‌شود:
- Teaches
- Preaches
- Researches
- Elites
- Excellents

**مزایا**:
- ✅ کنترل کیفیت
- ✅ Audit Trail کامل
- ✅ امکان Reject
- ✅ Notification به کاربر

</div>
