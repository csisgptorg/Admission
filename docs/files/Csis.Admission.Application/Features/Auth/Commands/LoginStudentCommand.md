<div dir="rtl">

# LoginStudentCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Auth/Commands/LoginStudentCommand.cs`

---

## 1. Purpose (هدف)

Command اختصاصی **ورود دانشجو** با اعتبارسنجی کپچا. این Command برای ورود دانشجویان از طریق پنل مخصوص دانشجویی استفاده می‌شود.

---

## 2. خلاصه اتفاقات

```
1. دریافت کد کپچا ذخیره شده از Cache
2. مقایسه با کد ورودی
3. اگر اشتباه → Exception
4. احراز هویت دانشجو (Username/Password)
5. حذف کپچا از Cache (یکبار مصرف)
6. بازگشت JWT Token
```

---

## 3. اجزای اصلی

### Command:
```csharp
sealed record LoginStudentCommand : IRequest<LoginResultDto>
{
    string Username        // نام کاربری
    string Password        // رمز عبور
    string Token           // توکن کپچا
    string CaptchaCode     // کد امنیتی
}
```

### Handler Dependencies:
- `ICsisAuthorizationService` - احراز هویت
- `IDistributedCacheService` - بازیابی کپچا (Redis)

---

## 4. Flow

```
1. دریافت کپچای ذخیره شده
   └─> distributedCacheService.GetAsync<string>(Token)

2. اعتبارسنجی کپچا
   if (storedCaptcha != CaptchaCode)
       └─> CommandValidationException("کد امنیتی اشتباه است")

3. احراز هویت
   └─> csisAuthorizationService.LoginStudentAsync(Username, Password)

4. در صورت موفقیت
   ├─> حذف کپچا: distributedCacheService.RemoveAsync(Token)
   └─> return LoginResultDto { TokenInfo, Succeeded = true }

5. در صورت ناموفق
   └─> return LoginResultDto { ErrorMessage, Succeeded = false }
```

---

## 5. Business Rules

### BR-1: کپچا الزامی
- دانشجویان **باید** کد کپچا را وارد کنند
- کپچا بعد از ورود موفق حذف می‌شود (یکبار مصرف)

### BR-2: Distributed Cache
- استفاده از **Redis** برای ذخیره کپچا (مقیاس‌پذیر)
- **تفاوت با LoginCommand**: آنجا MemoryCache، اینجا DistributedCache

### BR-3: تفاوت با LoginCommand
- `LoginCommand` → کپچا ندارد، ExternalToken دارد
- `LoginStudentCommand` → کپچا دارد، ExternalToken ندارد

---

## 6. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `CommandValidationException` | کپچا اشتباه | "کد امنیتی اشتباه است" |

---

## 7. Risks & Notes

### امنیت:
- ✅ کپچا برای جلوگیری از Brute Force
- ⚠️ **بدون Rate Limiting** - کپچا به تنهایی کافی نیست
- ⚠️ **عدم لاگ**: تلاش‌های ناموفق لاگ نمی‌شوند

### کارایی:
- ✅ استفاده از Distributed Cache (Redis) برای مقیاس‌پذیری

### Code Quality:
- ✅ ساده و واضح

---

## 8. Use Case های مرتبط

- **UC-002**: ورود دانشجو (با کپچا)
- مرتبط با: [GenerateCaptchaCommand](../../CaseFilings/Commands/Student/GenerateCaptchaCommand.md)

---

## 9. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | ورود دانشجو با کپچا |
| **ورودی** | Username + Password + Captcha |
| **خروجی** | LoginResultDto (JWT) |
| **امنیت** | ✅ Captcha، ⚠️ بدون Rate Limiting |
| **Cache** | Distributed (Redis) |
| **تفاوت با LoginCommand** | کپچا دارد، ExternalToken ندارد |

---

**تفاوت با LoginCommand**:

| ویژگی | LoginCommand | LoginStudentCommand |
|-------|-------------|-------------------|
| **کپچا** | ❌ | ✅ |
| **ExternalToken** | ✅ | ❌ |
| **Cache** | - | Distributed (Redis) |
| **کاربر** | Employee + Student | فقط Student |

</div>
