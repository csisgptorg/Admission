<div dir="rtl">

# GetStudentProfileImageByCodmQuery

## 📄 اطلاعات کلی

**مسیر فایل:**
```
Csis.Admission.Application/Features/Students/Iranian/Queries/GetStudentProfileImageByCodmQuery.cs
```

**Feature:** Students  
**نوع:** Query  
**هدف:** دریافت تصویر پروفایل دانشجو با رعایت قوانین دسترسی جنسیتی

---

## 🎯 هدف (Purpose)

این Query برای **دریافت تصویر پروفایل دانشجو** استفاده می‌شود با ویژگی‌های منحصر به فرد زیر:

1. **رعایت حریم خصوصی جنسیتی:**
   - تصویر دانشجویان زن برای کارمندانی که مجوز ندارند مخفی می‌شود
   - نمایش تصویر جایگزین "عدم دسترسی"

2. **بازگشت Base64 String:**
   - تصویر به صورت Data URI برگردانده می‌شود
   - آماده برای نمایش مستقیم در HTML

**ویژگی‌های کلیدی:**
- ✅ Authorization بر اساس جنسیت
- ✅ تصویر جایگزین برای موارد محدود
- ✅ فرمت Data URI (Base64)
- ✅ سبک و سریع

---

## 📝 ساختار Query

### ورودی (Request)

```csharp
public sealed record GetStudentProfileImageByCodmQuery(int Codm) 
    : IRequest<string>;
```

**پارامترها:**
- `Codm`: کد مرکز خدمات دانشجو

### خروجی (Response)

```csharp
string  // Data URI format: "data:image/jpg;base64,{Base64String}"
        // یا null در صورت عدم وجود تصویر
```

**نمونه خروجی:**
```
"data:image/jpg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD..."
```

---

## 🔄 جریان اجرا (Execution Flow)

### مراحل:

```
1. دریافت تصویر پروفایل از Repository
   ├─> studentRepo.GetProfileImageByCodm(Codm)
   └─> شامل: Image (byte[]), Gender

2. بررسی وجود تصویر
   ├─> اگر null
   └──> Return null

3. شناسایی جنسیت دانشجو
   ├─> Gender == Female?
   └─> بررسی نقش کاربر

4. بررسی دسترسی (برای زنان)
   ├─> آیا کاربر کارمند است؟
   ├─> آیا کاربر مجوز مشاهده تصویر زنان دارد؟
   └─> اگر ندارد → Return "no-access-profile-picture.png"

5. تبدیل تصویر به Base64
   ├─> Convert.ToBase64String(Image)
   ├─> اضافه کردن Prefix: "data:image/jpg;base64,"
   └─> Return Data URI
```

### نمودار توالی (Sequence Diagram)

```mermaid
sequenceDiagram
    participant C as Controller
    participant H as Handler
    participant R as StudentRepo
    participant A as AuthService
    participant FS as FileSystem

    C->>H: GetStudentProfileImageByCodmQuery(Codm)
    H->>R: GetProfileImageByCodm(Codm)
    R-->>H: StudentProfile (Image, Gender)
    
    alt تصویر موجود نیست
        H-->>C: null
    end
    
    alt دانشجو زن است
        H->>A: GetPersonnelIdAsync()
        A-->>H: PersonnelId
        H->>A: IsAuthorizedToAsync(FemaleInfoProfilePicture)
        A-->>H: false (عدم دسترسی)
        H->>FS: ReadFile("no-access-profile-picture.png")
        FS-->>H: NoAccessImageBytes
        H->>H: Convert to Base64
        H-->>C: "data:image/jpg;base64,{NoAccessImage}"
    else دانشجو مرد یا کاربر مجاز است
        H->>H: Convert.ToBase64String(Image)
        H-->>C: "data:image/jpg;base64,{ImageData}"
    end
```

---

## 📦 وابستگی‌ها (Dependencies)

### Repository ها
- `IStudentRepository`: عملیات مربوط به دانشجو
  - متد: `GetProfileImageByCodm(Codm)`: دریافت تصویر و جنسیت

### سرویس‌ها
- `ICsisAuthenticatedUserService`: مدیریت احراز هویت و مجوزها
  - `GetPersonnelIdAsync()`: شناسه کارمند
  - `IsAuthorizedToAsync(permission)`: بررسی مجوز

### محیط
- `IWebHostEnvironment`: دسترسی به فایل‌های استاتیک
  - `WebRootPath`: مسیر ریشه فایل‌های وب

### Enums
- `Gender`: جنسیت (Male, Female)
- `PermissionsEnum`: مجوزها
  - `FemaleInfoProfilePicture`: مجوز مشاهده تصویر زنان

---

## ⚙️ قوانین کسب‌وکار (Business Rules)

### قوانین دسترسی جنسیتی

```csharp
var isFemale = studentProfile.Gender == Gender.Female;
var isPersonnel = await _authenticatedUserService.GetPersonnelIdAsync() > 0;
var accessToFemalePicture = !await _authenticatedUserService
    .IsAuthorizedToAsync(PermissionsEnum.FemaleInfoProfilePicture);

if (isFemale && isPersonnel && accessToFemalePicture)
{
    // نمایش تصویر جایگزین
    return await GetNoAccessImageBase64("admission-files/no-access-profile-picture.png");
}
```

**منطق:**
1. اگر دانشجو **زن** باشد
2. و کاربر **کارمند** باشد (نه دانشجو)
3. و کارمند **مجوز مشاهده تصویر زنان** نداشته باشد
4. تصویر جایگزین "عدم دسترسی" نمایش داده می‌شود

**استثناها:**
- ✅ دانشجویان می‌توانند تصویر خودشان را ببینند
- ✅ کارمندان با مجوز می‌توانند تصاویر زنان را ببینند
- ✅ تصاویر مردان برای همه قابل مشاهده است

### فرمت خروجی

```csharp
const string imagePrefix = "data:image/jpg;base64,";
return imagePrefix + Convert.ToBase64String(studentProfile.Image);
```

**Data URI Scheme:**
```
data:[<mediatype>][;base64],<data>
```

- `data:`: پروتکل Data URI
- `image/jpg`: نوع MIME تصویر
- `base64`: encoding
- `{Base64String}`: داده‌های تصویر

**مزایا:**
- ✅ قابل استفاده مستقیم در HTML: `<img src="data:image/jpg;base64,..."/>`
- ✅ بدون نیاز به درخواست جداگانه برای فایل
- ✅ کش می‌شود

---

## 🔍 نکات پیاده‌سازی (Implementation Notes)

### 1. تصویر جایگزین

```csharp
private async Task<string> GetNoAccessImageBase64(string relativeImagePath)
{
    const string imagePrefix = "data:image/jpg;base64,";
    var fullPath = Path.Combine(_hostEnvironment.WebRootPath, relativeImagePath);
    var imageBytes = await File.ReadAllBytesAsync(fullPath);
    return imagePrefix + Convert.ToBase64String(imageBytes);
}
```

**مسیر فایل:**
```
{WebRootPath}/admission-files/no-access-profile-picture.png
```

⚠️ **نکته:**
- فایل باید در `wwwroot/admission-files/` موجود باشد
- در صورت عدم وجود فایل، Exception رخ می‌دهد

### 2. منطق معکوس Authorization

```csharp
var accessToFemalePicture = !await _authenticatedUserService
    .IsAuthorizedToAsync(PermissionsEnum.FemaleInfoProfilePicture);
```

⚠️ **توجه به `!` (NOT):**
- `IsAuthorizedToAsync` → true: کاربر مجاز است
- `accessToFemalePicture` → true: کاربر مجاز **نیست**
- منطق معکوس برای خوانایی کد

**بهتر:**
```csharp
var hasNoAccessToFemalePicture = !await ...
```

### 3. بررسی Personnel

```csharp
var isPersonnel = await _authenticatedUserService.GetPersonnelIdAsync() > 0;
```

- دانشجویان `PersonnelId = 0` یا `null` دارند
- کارمندان `PersonnelId > 0` دارند
- این بررسی نقش کاربر را مشخص می‌کند

### 4. Hardcoded MIME Type

```csharp
const string imagePrefix = "data:image/jpg;base64,";
```

⚠️ **محدودیت:**
- فقط JPG در نظر گرفته شده
- اگر تصویر PNG یا فرمت دیگر باشد، MIME Type اشتباه است

**بهبود پیشنهادی:**
```csharp
var mimeType = GetMimeType(studentProfile.ImageExtension);
var imagePrefix = $"data:{mimeType};base64,";
```

---

## 🎯 Use Cases

### UC-ViewProfileImage: مشاهده تصویر پروفایل

**Actor:** دانشجو، کارمند

**Preconditions:**
- کاربر احراز هویت شده باشد

**Main Flow:**
1. کاربر درخواست مشاهده تصویر دانشجو را ارسال می‌کند
2. سیستم تصویر را از دیتابیس دریافت می‌کند
3. سیستم جنسیت دانشجو را بررسی می‌کند
4. سیستم مجوز کاربر را بررسی می‌کند
5. سیستم تصویر مناسب را برمی‌گرداند

**Postconditions:**
- تصویر پروفایل یا تصویر جایگزین نمایش داده می‌شود

**Alternative Flows:**
- A1: تصویر موجود نیست → `null`
- A2: کاربر مجوز ندارد → تصویر "عدم دسترسی"

---

## ⚠️ ریسک‌ها و نکات (Risks & Notes)

### امنیتی (Security)

1. ✅ **Gender-Based Authorization:**
   ```csharp
   if (isFemale && isPersonnel && accessToFemalePicture)
   ```
   - رعایت حریم خصوصی جنسیتی
   - کاربران غیرمجاز نمی‌توانند تصاویر زنان را ببینند

2. ✅ **Permission-Based Access:**
   - استفاده از `PermissionsEnum.FemaleInfoProfilePicture`
   - قابل مدیریت از طریق سیستم نقش‌ها

3. ⚠️ **Missing Input Validation:**
   - بدون بررسی Codm معتبر
   - ممکن است درخواست‌های نامعتبر ارسال شوند

### عملکردی (Performance)

1. ⚠️ **Large Images:**
   ```csharp
   Convert.ToBase64String(studentProfile.Image)
   ```
   - تبدیل Base64 برای تصاویر بزرگ هزینه‌بر است
   - Base64 حجم را ~33% افزایش می‌دهد

2. ⚠️ **No Caching:**
   - هر بار تصویر از DB دریافت و تبدیل می‌شود
   - نیاز به Cache (Redis یا Memory)

   **پیشنهاد:**
   ```csharp
   var cacheKey = $"student_profile_image_{request.Codm}";
   var cachedImage = await _cache.GetAsync(cacheKey);
   if (cachedImage != null) return cachedImage;
   ```

3. ⚠️ **File I/O for No-Access Image:**
   ```csharp
   var imageBytes = await File.ReadAllBytesAsync(fullPath);
   ```
   - خواندن فایل هر بار
   - بهتر است یک بار خوانده و کش شود

### کیفیت کد (Code Quality)

1. ⚠️ **Confusing Logic:**
   ```csharp
   var accessToFemalePicture = !await _authenticatedUserService.IsAuthorizedToAsync(...);
   ```
   - نام متغیر گمراه‌کننده است
   - بهتر: `hasNoAccessToFemalePicture`

2. ⚠️ **Hardcoded MIME Type:**
   - فرض JPG برای همه تصاویر
   - ممکن است اشتباه باشد

3. ✅ **Separation of Concerns:**
   - متد جداگانه برای تصویر جایگزین
   - خوانایی خوب

---

## 📊 خلاصه نکات کلیدی

| جنبه | توضیح |
|------|-------|
| **الگوی طراحی** | CQRS + Permission-Based Access |
| **Authorization** | ✅ بر اساس جنسیت و مجوز |
| **فرمت خروجی** | Data URI (Base64) |
| **Privacy** | ✅ رعایت حریم خصوصی جنسیتی |
| **Fallback Image** | ✅ تصویر جایگزین برای عدم دسترسی |
| **Caching** | ⚠️ ندارد (نیاز به بهبود) |
| **Performance** | ⚠️ تبدیل Base64 هزینه‌بر |
| **Validation** | ⚠️ ندارد |
| **مستندات XML** | ✅ موجود (inheritdoc) |

---

## 🔗 لینک‌های مرتبط

### Commands مرتبط
- [UpdateStudentProfilePictureCommand.md](../Commands/UpdateStudentProfilePictureCommand.md) - بروزرسانی تصویر پروفایل
- [UpdateStudentProfilePictureRequestCommand.md](../Commands/UpdateStudentProfilePictureRequestCommand.md) - درخواست بروزرسانی تصویر

### Queries مرتبط
- [GetStudentInfoByCodmQuery.md](./GetStudentInfoByCodmQuery.md) - اطلاعات کامل دانشجو

### Repositories
- [StudentRepository.md](../../../../Persistence/StudentRepository.md) - Repository دانشجو

---

**نسخه مستندات:** 1.0  
**تاریخ ایجاد:** 2026-01-03

</div>
