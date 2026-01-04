<div dir="rtl">

# UpdateDependentBankAccountRequestCommand.cs

**مسیر**: `Csis.Admission.Application/Features/BankAccounts/Commands/UpdateDependentBankAccountRequestCommand.cs`

---

## 1. Purpose (هدف)

Command **ثبت درخواست بروزرسانی حساب بانکی تکفل** (Dependent) با **Workflow کامل** شامل اعتبارسنجی SIBA، بررسی تکراری بودن، و الزام بارگزاری مدرک برای شهروندان غیرایرانی.

---

## 2. مستندات XML موجود

```csharp
/// <summary>درخواست بروز رسانی حساب بانکی</summary>
```

**کامل**: ثبت درخواست با Validation کامل و Request Flow مناسب بر اساس نوع کاربر و تابعیت.

---

## 3. خلاصه اتفاقات

**جریان اصلی**:
```
1. Set Codm از CurrentUser (اگر خالی باشد)
2. بررسی نوع کاربر (Employee یا Student)
3. دریافت اطلاعات تکفل
4. Validate:
   - الزام بارگزاری مدرک برای غیرایرانی‌ها
   - بررسی تکراری نبودن شماره حساب
   - اعتبارسنجی فرمت SIBA
   - اعتبارسنجی تطبیق با کد ملی (برای ایرانی‌ها)
5. تعیین RequestFlow (DirectRegistration یا StudentToEmployee)
6. ساخت و ثبت Request با مدرک (در صورت وجود)
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record UpdateDependentBankAccountRequestCommand : IRequest
{
    long DependentId           // شناسه تکفل
    int Codm                   // کد مرکز خدمات
    string BankAccountNumber   // شماره حساب بانکی جدید
    Guid? FileId               // شناسه فایل مدرک (اختیاری)
}
```

### Handler Dependencies:
- **ICsisWsmService**: اعتبارسنجی شماره حساب با سرویس SIBA
- **IRequestService**: ثبت و مدیریت Request
- **IRepository<DependentSummary, long>**: دریافت اطلاعات تکفل
- **ICurrentUserService**: دریافت اطلاعات کاربر جاری

---

## 5. Flow

```mermaid
graph TD
    A[دریافت Command] --> B[SetCodm از CurrentUser]
    B --> C{IsEmployee?}
    C --> D[دریافت اطلاعات Dependent]
    D --> E[Validate]
    
    E --> E1{غیرایرانی + بدون فایل + NOT Senior?}
    E1 -->|بله| F1[❌ Exception: بارگزاری مدرک الزامی]
    E1 -->|خیر| E2
    
    E2{شماره حساب تکراری؟}
    E2 -->|بله| F2[❌ Exception: شماره حساب قبلا ثبت شده]
    E2 -->|خیر| E3
    
    E3[ValidateSibaAccountNumber - فرمت]
    E3 --> E4{ایرانی؟}
    E4 -->|بله| E5[ValidateSibaAccountNumber - وب‌سرویس]
    E4 -->|خیر| G
    E5 --> E6{معتبر؟}
    E6 -->|خیر| F3[❌ Exception: شماره حساب مطابقت ندارد]
    E6 -->|بله| G
    
    G[تعیین RequestFlow]
    C -->|Employee| G1[DirectRegistration]
    C -->|Student + ایرانی| G1
    C -->|Student + غیرایرانی| G2[StudentToEmployee]
    
    G1 --> H
    G2 --> H
    
    H[ساخت CreateRequestCommand]
    H --> I{FileId دارد?}
    I -->|بله| J[AddDocument]
    I -->|خیر| K
    J --> K[ثبت Request]
    K --> L[✅ پایان]
```

---

## 6. Business Rules

### BR-1: Request Flow Strategy
```csharp
var flow = isEmployee switch {
    true => RequestFlow.DirectRegistration,           // کارمند → مستقیم
    false => dependent.Citizenship == Citizenship.Iranian 
        ? RequestFlow.DirectRegistration              // دانشجو ایرانی → مستقیم
        : RequestFlow.StudentToEmployee               // دانشجو غیرایرانی → نیاز تایید
};
```

**توضیح**:
- **Employee**: همیشه DirectRegistration (اعمال بدون تایید)
- **Student Iranian**: DirectRegistration (خود اظهاری معتبر)
- **Student Non-Iranian**: StudentToEmployee (نیاز به تایید کارمند)

### BR-2: Document Requirement
```csharp
if ((dependent.Citizenship == Citizenship.NonIranian && !command.FileId.HasValue) 
    && !isPersonnelSenior) {
    throw new CommandValidationException("بارگزاری مدرک برای شهروندان غیر ایرانی الزامی می باشد.");
}
```

**استثنا**: کارمندان Senior می‌توانند بدون مدرک ثبت کنند (override policy)

### BR-3: Duplicate Check
```csharp
if (await dependentRepo.ExistsAsync(
    x => x.Codm != command.Codm && x.BankAccountNumber == command.BankAccountNumber)) {
    throw new CommandValidationException("شماره حساب قبلا ثبت شده است.");
}
```

**دقت**: شماره حساب نباید برای Codm دیگری ثبت شده باشد

### BR-4: SIBA Validation
```csharp
// 1. فرمت (متد Utility)
Common.Utilities.ValidateSibaAccountNumber(command.BankAccountNumber);

// 2. تطبیق با کد ملی (فقط ایرانی‌ها)
if (dependent.Citizenship == Citizenship.Iranian) {
    var sibaAccountNumber = new ValidateSibaAccountNumberRequest(
        dependent.Codm, dependent.NationalCode, command.BankAccountNumber);
    if (!await wsmService.ValidateSibaAccountNumber(sibaAccountNumber)) {
        throw new CommandValidationException("شماره حساب با کد ملی مطابقبت ندارد.");
    }
}
```

---

## 7. Dependencies

### Internal:
- **IRequestService**: مدیریت Request Workflow
- **IRepository<DependentSummary, long>**: دسترسی به اطلاعات تکفل
- **ICurrentUserService**: احراز هویت و Authorization

### External:
- **ICsisWsmService**: اعتبارسنجی SIBA با سامانه بانکی

### Utilities:
- **Common.Utilities.SetCodm**: تنظیم Codm از Token
- **Common.Utilities.ValidateSibaAccountNumber**: اعتبارسنجی فرمت شماره حساب

---

## 8. Input/Output

### Input:
```csharp
{
    DependentId: 123456,              // شناسه تکفل
    Codm: 54321,                      // کد مرکز خدمات (اختیاری - از Token)
    BankAccountNumber: "1234567890123", // شماره حساب SIBA 13 رقمی
    FileId: Guid.Parse("...")         // اختیاری (اجباری برای غیرایرانی‌ها)
}
```

### Output:
```csharp
void (IRequest - بدون خروجی)
```

### Exceptions:
- **CommandValidationException**: 
  - بارگزاری مدرک الزامی (غیرایرانی بدون فایل)
  - شماره حساب تکراری
  - شماره حساب نامعتبر (فرمت)
  - شماره حساب مطابقت ندارد (کد ملی)
- **UnauthorizedException**: دسترسی غیرمجاز
- **RecordNotFoundException**: تکفل یافت نشد

---

## 9. Side Effects

- ✅ **Request Creation**: ثبت یک Request جدید در سیستم
- ✅ **Document Attachment**: پیوست مدرک به Request (در صورت وجود)
- ✅ **Workflow Initiation**: شروع فرآیند تایید (برای StudentToEmployee)
- ⚠️ **External API Call**: فراخوانی SIBA WebService
- ✅ **Audit Log**: احتمالاً در RequestService ثبت می‌شود

---

## 10. الگوهای استفاده شده

### ✅ Strategy Pattern (Request Flow)
```csharp
var flow = isEmployee switch {
    true => RequestFlow.DirectRegistration,
    false => dependent.Citizenship == Citizenship.Iranian 
        ? RequestFlow.DirectRegistration 
        : RequestFlow.StudentToEmployee
};
```

### ✅ Validation Pipeline
```csharp
private async Task Validate(...) {
    // Step 1: Document Validation
    // Step 2: Duplicate Check
    // Step 3: Format Validation
    // Step 4: SIBA WebService Validation
}
```

### ✅ Fluent Builder Pattern
```csharp
var requestCommand = new CreateRequestCommand(command, flow);
if (command.FileId.HasValue) {
    requestCommand.AddDocument(command.FileId.Value);
}
await requestService.Create(requestCommand, cancellationToken);
```

### ✅ Primary Constructor (C# 12)
```csharp
internal sealed class UpdateDependentBankAccountRequestCommandHandler(
    ICsisWsmService wsmService,
    IRequestService requestService,
    IRepository<DependentSummary, long> dependentRepo,
    ICurrentUserService currentUser
)
```

---

## 11. Performance

- **Database Queries**: 2-3 SELECT (Dependent + Duplicate Check)
- **External API Calls**: 1 (SIBA Validation - فقط برای ایرانی‌ها)
- **Request Creation**: 1 INSERT
- ⚠️ **Network Latency**: SIBA WebService ممکن است کند باشد

**بهینه‌سازی**:
- Caching برای نتایج SIBA Validation (موقت)
- Async Validation (اگر ممکن باشد)

---

## 12. Security

### ✅ Authorization
```csharp
_ = await Common.Utilities.SetCodm(command, currentUser);
var isEmployee = await currentUser.IsEmployee();
var isPersonnelSenior = await currentUser.IsSenior();
```
- بررسی نوع کاربر و سطح دسترسی

### ✅ Data Validation
- بررسی تکراری نبودن شماره حساب
- اعتبارسنجی فرمت SIBA
- اعتبارسنجی تطبیق با کد ملی (فقط ایرانی‌ها)

### ✅ Document Security
- الزام بارگزاری مدرک برای غیرایرانی‌ها
- استثنا برای Senior Personnel (override)

### ⚠️ Sensitive Data
- شماره حساب بانکی (باید رمزنگاری شود؟)
- کد ملی در فراخوانی SIBA

---

## 13. نکات مهم

### 💡 تفاوت با UpdateDependentBankAccountCommand

| ویژگی | UpdateDependentBankAccountRequestCommand | UpdateDependentBankAccountCommand |
|-------|------------------------------------------|-----------------------------------|
| **Workflow** | ✅ با Request Flow | ❌ Direct Update |
| **Validation** | ✅ کامل (SIBA, تکراری، مدرک) | ❌ کمتر |
| **Authorization** | ✅ در Handler | ⚠️ در Controller |
| **Document** | ✅ اختیاری/اجباری | ❌ بدون مدرک |
| **SIBA Check** | ✅ وب‌سرویس | ❌ بدون بررسی |
| **Use Case** | درخواست کاربر | عملیات داخلی |

### 🎯 RequestFlow Decision Tree
```
Employee?
├─ YES → DirectRegistration (همیشه)
└─ NO (Student)
    ├─ Iranian? → DirectRegistration (خود اظهاری معتبر)
    └─ Non-Iranian → StudentToEmployee (نیاز تایید)
```

### ⚠️ Typo در کد
```csharp
// خط 64 - غلط املایی
throw new CommandValidationException("شماره حساب با کد ملی مطابقبت ندارد.");
//                                                                    ^^
// باید باشد: "مطابقت"
```

---

## 14. مثال استفاده

### سناریو 1: دانشجو ایرانی (بدون مدرک)
```csharp
var command = new UpdateDependentBankAccountRequestCommand {
    DependentId = 123456,
    Codm = 54321,  // یا null (از Token)
    BankAccountNumber = "1234567890123",
    FileId = null  // ایرانی → مدرک اختیاری
};
await mediator.Send(command);
// → DirectRegistration (اعمال مستقیم)
```

### سناریو 2: دانشجو غیرایرانی (با مدرک)
```csharp
var fileId = await UploadDocument("bank-account-proof.pdf");
var command = new UpdateDependentBankAccountRequestCommand {
    DependentId = 123456,
    Codm = 54321,
    BankAccountNumber = "1234567890123",
    FileId = fileId  // غیرایرانی → مدرک اجباری
};
await mediator.Send(command);
// → StudentToEmployee (نیاز تایید کارمند)
```

### سناریو 3: کارمند (همیشه مستقیم)
```csharp
var command = new UpdateDependentBankAccountRequestCommand {
    DependentId = 123456,
    Codm = 54321,
    BankAccountNumber = "1234567890123",
    FileId = null  // کارمند → بدون محدودیت
};
await mediator.Send(command);
// → DirectRegistration (اعمال مستقیم)
```

---

## 15. Related Commands

- **UpdateDependentBankAccountCommand**: بروزرسانی مستقیم بدون Workflow
  - مسیر: [UpdateDependentBankAccountCommand.md](./UpdateDependentBankAccountCommand.md)
- **UpdateStudentBankAccountRequestCommand**: درخواست بروزرسانی حساب دانشجو
  - مسیر: [UpdateStudentBankAccountRequestCommand.md](./UpdateStudentBankAccountRequestCommand.md)

---

## 16. تغییرات پیشنهادی

### 1. رفع Typo
```csharp
throw new CommandValidationException("شماره حساب با کد ملی مطابقت ندارد.");
```

### 2. افزودن Logging
```csharp
public async Task Handle(...) {
    _logger.LogInformation(
        "Updating dependent bank account request. DependentId={DependentId}, Codm={Codm}",
        command.DependentId, command.Codm);
    
    try {
        await Validate(...);
        // ...
        _logger.LogInformation("Request created successfully. RequestId={RequestId}", requestId);
    } catch (Exception ex) {
        _logger.LogError(ex, "Failed to create bank account update request");
        throw;
    }
}
```

### 3. Extract Validation Rules
```csharp
private class DependentBankAccountValidator {
    public async Task ValidateDocumentRequirement(...) { }
    public async Task ValidateDuplicate(...) { }
    public void ValidateFormat(...) { }
    public async Task ValidateSibaMatch(...) { }
}
```

### 4. افزودن Response DTO
```csharp
public sealed record UpdateDependentBankAccountRequestCommand : IRequest<RequestCreatedResult>;

public record RequestCreatedResult(
    long RequestId, 
    RequestFlow Flow, 
    string Status,
    string Message
);
```

### 5. Caching SIBA Validation
```csharp
private async Task<bool> ValidateSibaAccountNumberWithCache(...) {
    var cacheKey = $"siba:{nationalCode}:{bankAccountNumber}";
    var cached = await _cache.GetAsync<bool?>(cacheKey);
    if (cached.HasValue) return cached.Value;
    
    var isValid = await wsmService.ValidateSibaAccountNumber(...);
    await _cache.SetAsync(cacheKey, isValid, TimeSpan.FromMinutes(30));
    return isValid;
}
```

---

## 17. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | ثبت درخواست بروزرسانی حساب بانکی تکفل |
| **ورودی** | DependentId + Codm + BankAccountNumber + FileId |
| **خروجی** | void |
| **Workflow** | ✅ DirectRegistration یا StudentToEmployee |
| **Validation** | ✅ کامل (SIBA + تکراری + مدرک) |
| **Authorization** | ✅ بر اساس نوع کاربر |
| **Document** | ⚠️ اجباری برای غیرایرانی (مگر Senior) |
| **External API** | ✅ SIBA WebService |

---

**یادداشت**: این Command یک الگوی کامل و استاندارد برای ثبت درخواست با Validation جامع و Request Flow مناسب است. برای عملیات‌های مستقیم از `UpdateDependentBankAccountCommand` استفاده شود.

</div>
