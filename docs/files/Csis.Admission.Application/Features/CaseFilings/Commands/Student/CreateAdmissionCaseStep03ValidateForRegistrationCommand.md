<div dir="rtl">

# CreateAdmissionCaseStep03ValidateForRegistrationCommand.cs

**مسیر**: `Csis.Admission.Application/Features/CaseFilings/Commands/Student/CreateAdmissionCaseStep03ValidateForRegistrationCommand.cs`

---

## 1. Purpose (هدف)

این فایل **گام سوم از فرآیند 10 مرحله‌ای تشکیل پرونده** را پیاده‌سازی می‌کند. وظیفه این مرحله **اعتبارسنجی وضعیت دانشجو در حوزه علمیه** است که شامل تأیید شماره پرونده حوزه و مرکز تأیید می‌شود.

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// ساخت توکن گام سوم
/// </summary>
```

**تکمیل شده**: این Command برای اعتبارسنجی وضعیت دانشجو در حوزه علمیه از طریق مرکز تأیید (ApprovalCenter) و شماره پرونده استفاده می‌شود.

---

## 3. خلاصه اتفاقات (What Happens)

**جریان اصلی**:
1. دریافت `Token` (caseId) و اطلاعات حوزوی از کاربر
2. بازیابی رکورد `AdmissionCaseUser`
3. اعتبارسنجی وضعیت در حوزه بر اساس تابعیت:
   - **ایرانی**: شماره پرونده حوزه + مرکز تأیید + کد ملی + تاریخ تولد
   - **غیرایرانی**: شماره پرونده حوزه + مرکز تأیید + کد یکتا + تاریخ تولد
4. به‌روزرسانی رکورد با اطلاعات جدید
5. تغییر `CaseStep` به `StudentStatusForRegistrationVerified`

---

## 4. اجزای اصلی

### 4.1. Command (درخواست)

**کلاس**: `CreateAdmissionCaseThirdStepCommand`
- **نوع**: `sealed record`
- **Base Class**: `BaseCommandDto<..., AdmissionCaseUser, Guid>`
- **Interface**: `IRequest`

**Properties**:
```csharp
Guid Token                         // شناسه پرونده
ApprovalCenter ApprovalCenter      // مرکز تأیید (Enum)
int CaseNumInApprovalCenter        // شماره پرونده در حوزه
string NationalCardSerial          // سریال شناسنامه
Religion Religion                  // مذهب
```

---

### 4.2. Handler (پردازش‌گر)

**کلاس**: `CreateAdmissionCaseThirdStepCommandHandler`

**Injected Dependencies**:
- `IRepository<AdmissionCaseUser, Guid>` - دسترسی به پرونده
- `ApprovalCenterValidator` - اعتبارسنجی وضعیت حوزه

---

## 5. Flow داخل فایل

```
1. بازیابی AdmissionCaseUser
   ├─> GetByIdAsTracking(Token)
   └─> اگر null → CommandValidationException

2. اعتبارسنجی بر اساس تابعیت
   if (Citizenship == Iranian)
       └─> approvalCenterValidator.Iranian(CaseNum, ApprovalCenter, NationalCode, BirthDate)
   else
       └─> approvalCenterValidator.NonIranian(CaseNum, ApprovalCenter, YektaCode, BirthDate)

3. به‌روزرسانی Entity
   ├─> command.ToEntity(admissionCaseUser)
   ├─> CaseStep = StudentStatusForRegistrationVerified
   └─> UpdateAsync()
```

---

## 6. Dependencies

| Dependency | Purpose |
|-----------|---------|
| `IRepository<AdmissionCaseUser, Guid>` | دسترسی به پرونده |
| `ApprovalCenterValidator` | اعتبارسنجی با سیستم حوزه |

**لینک**:
- [ApprovalCenterValidator](#) - TODO

---

## 7. Business Rules

### BR-1: اعتبارسنجی وضعیت حوزه
- دانشجو باید در مرکز تأیید مشخص شده دارای پرونده معتبر باشد
- شماره پرونده + کد ملی/یکتا + تاریخ تولد باید تطابق داشته باشد

### BR-2: State Transition
- بعد از تأیید موفق → `CaseStep = StudentStatusForRegistrationVerified`

### BR-3: تابعیت
- **ایرانی**: نیاز به کد ملی
- **غیرایرانی**: نیاز به کد یکتا

---

## 8. Data Access

### EF Core Operations:
```csharp
// Query
GetByIdAsTrackingAsync(Token, false, cancellationToken)

// Update
UpdateAsync(entity, cancellationToken)
```

---

## 9. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `CommandValidationException` | Token نامعتبر | "شناسه موقت (توکن) نامعتبر است." |
| `ValidationException` | اعتبارسنجی حوزه ناموفق | از `ApprovalCenterValidator` |

---

## 10. Use Case های مرتبط

- **UC-030**: تشکیل پرونده دانشجوی جدید (Wizard)
  - **مرحله 3**: اعتبارسنجی وضعیت حوزه
  - مرحله قبل: [Step02 - تأیید موبایل](./CreateAdmissionCaseStep02MobileCommand.md)
  - مرحله بعد: [Step04 - تأیید اطلاعات هویتی](#)

---

## 11. Risks & Notes

### امنیت:
- ✅ اعتبارسنجی خارجی با مرکز تأیید
- ⚠️ نیاز به SSL/TLS برای ارتباط با سرویس خارجی

### کارایی:
- ⚠️ درخواست به سرویس خارجی ممکن است کند باشد
- پیشنهاد: Timeout و Retry Policy

---

## 12. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | گام سوم Wizard: اعتبارسنجی حوزه |
| **ورودی** | Token + شماره پرونده حوزه + مرکز تأیید |
| **خروجی** | بدون خروجی (Unit) |
| **اعتبارسنجی** | ApprovalCenterValidator |
| **State Transition** | → StudentStatusForRegistrationVerified |

</div>
