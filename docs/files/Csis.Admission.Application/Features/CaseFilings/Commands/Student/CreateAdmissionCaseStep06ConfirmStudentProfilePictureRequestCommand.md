<div dir="rtl">

# CreateAdmissionCaseStep06ConfirmStudentProfilePictureRequestCommand.cs

**مسیر**: `Csis.Admission.Application/Features/CaseFilings/Commands/Student/CreateAdmissionCaseStep06ConfirmStudentProfilePictureRequestCommand.cs`

---

## 1. Purpose (هدف)

**گام ششم Wizard**: آپلود و تأیید تصویر پروفایل دانشجو. برای دانشجویان ایرانی، تصویر با عکس شناسنامه‌ای موجود در ثبت احوال **با استفاده از هوش مصنوعی** مقایسه می‌شود.

---

## 2. خلاصه اتفاقات

**جریان اصلی**:
1. دریافت `Token` و `FileId` (تصویر آپلود شده)
2. دریافت فایل از File Management Service
3. اعتبارسنجی تصویر (حجم < 20KB، نسبت ابعاد 3×4)
4. ذخیره موقت تصویر در دیتابیس
5. **برای ایرانی‌ها**:
   - دریافت تصویر شناسنامه‌ای از ثبت احوال
   - آپلود تصویر قدیمی به File Management
   - **مقایسه AI**: تصویر جدید vs تصویر شناسنامه
   - ذخیره نتیجه AI در `Payloads`
6. **برای غیرایرانی‌ها**: فقط ذخیره FileId
7. تغییر `CaseStep` به `PictureUploaded`

---

## 3. اجزای اصلی

### Command:
```csharp
sealed record ConfirmStudentProfilePictureRequestCommand(Guid Token, Guid FileId) : IRequest
```

### Handler Dependencies:
- `ICsisWsmService` - دریافت تصویر شناسنامه
- `IStudentRepository` - ذخیره تصویر موقت
- `IRepository<AdmissionCaseUser, Guid>`
- `ICsisFileManagementService` - مدیریت فایل‌ها
- `IFaceCompareImageClient` - **مقایسه تصویر با AI**

---

## 4. Flow

```
1. دریافت فایل
   ├─> fileManagementService.GetPrivateFileById(FileId)
   └─> fileManagementService.GetFileInfoById(FileId)

2. اعتبارسنجی
   ├─> حجم < 20KB
   └─> نسبت ابعاد: 3/4 ± 0.3

3. ذخیره موقت
   └─> repo.SaveTemporaryProfilePicture(FileId, base64)

4. switch (Citizenship)
   
   case Iranian:
   ├─> csisWsmService.GetIdentityInfoByNationalCode()
   ├─> دریافت آخرین تصویر شناسنامه
   ├─> آپلود تصویر قدیمی: "old_{filename}"
   ├─> compareImageClient.AnalyzeWithBase64Async(new, old)
   └─> ذخیره Similarity Score در Payload
   
   case NonIranian:
   └─> فقط FileId ذخیره می‌شود

5. به‌روزرسانی
   ├─> CaseStep = PictureUploaded
   ├─> Payloads += { FileId, OldImageFileId?, ImageAnalysis? }
   └─> UpdateAsync()
```

---

## 5. Business Rules

### BR-1: اعتبارسنجی تصویر
- **حجم**: حداکثر 20 کیلوبایت
- **نسبت ابعاد**: 3×4 (با Tolerance 0.3)

### BR-2: AI Face Comparison (فقط ایرانی‌ها)
- تصویر جدید با آخرین تصویر شناسنامه‌ای مقایسه می‌شود
- نتیجه شامل: Similarity Score, Match Status, ...
- اگر تصویر شناسنامه‌ای وجود نداشته باشد → AI Check انجام نمی‌شود

### BR-3: ذخیره دوگانه
- تصویر جدید: `FileId`
- تصویر قدیمی (ایرانی‌ها): `OldImageFileId`

---

## 6. Dependencies

| Dependency | Purpose |
|-----------|---------|
| `IFaceCompareImageClient` | **مقایسه تصویر با AI** ⭐ |
| `ICsisFileManagementService` | آپلود و دریافت فایل |
| `ICsisWsmService` | دریافت تصویر شناسنامه |
| `IStudentRepository` | ذخیره موقت تصویر |

**لینک**:
- [IFaceCompareImageClient](#) - **سرویس AI تشخیص چهره**
- [Csis.CompareImageAi](#) - کتابخانه AI

---

## 7. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `CommandValidationException` | Token نامعتبر | "شناسه نامعتبر است." |
| `CommandValidationException` | حجم > 20KB | "حجم فایل بیش از ۲۰ کیلوبایت است." |
| `CommandValidationException` | نسبت ابعاد اشتباه | "نسبت ابعاد تصویر باید ۳×۴ باشد." |

---

## 8. Risks & Notes

### امنیت:
- ✅ استفاده از AI برای جلوگیری از تقلب
- ✅ اعتبارسنجی نسبت ابعاد و حجم

### کارایی:
- ⚠️ **درخواست به 3 سرویس خارجی**:
  1. File Management (2 درخواست)
  2. CSIS WSM (تصویر شناسنامه)
  3. **AI Service** (مقایسه تصویر)
- **پیشنهاد**: Async Processing + Queue

### Platform Compatibility:
- ⚠️ `#pragma warning disable CA1416`
- استفاده از `System.Drawing.Image` (نیاز به libgdiplus در Linux)

### Code Quality:
- ❌ پارامتر `-1` به سرویس‌ها (مشکوک)
- ✅ استفاده از AI برای امنیت

---

## 9. Use Case های مرتبط

- **UC-030**: تشکیل پرونده (Wizard)
  - **مرحله 6**: آپلود و تأیید تصویر پروفایل
  - **ویژگی خاص**: **مقایسه AI با تصویر شناسنامه**
  - مرحله قبل: [Step05 - آدرس](./CreateAdmissionCaseStep05ConfirmAddressByPostalCodeCommand.md)
  - مرحله بعد: [Step07 - حساب بانکی](#)

---

## 10. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | گام ششم Wizard: تصویر پروفایل |
| **ورودی** | Token + FileId |
| **خروجی** | Unit |
| **اعتبارسنجی** | حجم < 20KB، نسبت 3×4 |
| **AI Feature** | ⭐ مقایسه چهره با هوش مصنوعی |
| **State Transition** | → PictureUploaded |
| **Payload** | { FileId, OldImageFileId?, ImageAnalysis? } |
| **کارایی** | ⚠️ 3 سرویس خارجی |

---

**نکته ویژه**: این تنها مرحله‌ای است که از **هوش مصنوعی** برای اعتبارسنجی استفاده می‌کند.

</div>
