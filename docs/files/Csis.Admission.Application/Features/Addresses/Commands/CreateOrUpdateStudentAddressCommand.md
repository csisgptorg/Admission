<div dir="rtl">

# CreateOrUpdateStudentAddressCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Addresses/Commands/CreateOrUpdateStudentAddressCommand.cs`

---

## 1. Purpose (هدف)

**ثبت یا بروزرسانی آدرس دانشجو** با اطلاعات کامل جغرافیایی و **به‌روزرسانی خودکار شعبه و نمایندگی** بر اساس آدرس.

---

## 2. مستندات XML موجود

```csharp
/// <summary>ثبت/ویرایش آدرس</summary>
```

**کامل**: ثبت/بروزرسانی آدرس با Cascade Update شعبه و نمایندگی.

---

## 3. خلاصه اتفاقات

```
1. Upsert آدرس (Insert or Update بر اساس Codm)
2. به‌روزرسانی خودکار شعبه و نمایندگی (Branch & Agency)
3. بازگشت AddressId
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record CreateOrUpdateStudentAddressCommand : BaseCommandDto<..., Address>, IRequest<int>
{
    int Codm                          // کد مرکز
    short? ProvinceId                 // استان
    short? CityId                     // شهرستان
    short? PortionId                  // بخش
    short? TownId                     // شهر
    short? RuralId                    // دهستان
    string Township, Village, District // شهرک، روستا، محله
    string Avenue, Street             // خیابان اصلی، فرعی
    string Alley, Lane                // کوچه اصلی، فرعی
    string Number, Complex, Block, Unit // پلاک، مجتمع، بلوک، واحد
    short? Floor                      // طبقه
    long? ZipCode                     // کد پستی
    string ConfirmDate                // تاریخ تأیید
    short ProjectCode                 // همیشه 1
    bool? Flag                        // همیشه true
    bool? RequiresDualStudentApproval // نیاز به تأیید 2 طلبه
    int[] ConfirmedStudentCodms       // طلابی که تأیید می‌کنند
    long RequestId                    // شناسه درخواست
}
```

**یادداشت**: TODO در کد - RequestId نباید باشد (معماری نادرست)

### Handler Dependencies:
- `IRepository<Address>`
- `IHttpContextAccessor` - برای لاگ
- `IStudentRepository` - به‌روزرسانی Branch & Agency
- `ICsisAuthenticatedUserService` - اطلاعات کاربر

---

## 5. Flow

```
1. جستجوی Address
   └─> repo.GetOneAsTrackingAsync(Codm)

2. Upsert Address
   if (address == null)
       └─> Insert
   else
       └─> Update

3. به‌روزرسانی Branch & Agency ⭐
   ├─> UpdateBranchAndAgencyRepoCommand(Codm)
   ├─> SetLogParam (UserId, PersonnelId, ...)
   ├─> RequestId = command.RequestId
   └─> studentRepository.UpdateBranchAndAgency()

4. بازگشت
   └─> return addressId
```

---

## 6. Business Rules

### BR-1: Upsert Pattern
- یک دانشجو فقط **یک** آدرس فعال دارد
- بر اساس Codm

### BR-2: Cascade Update ⭐
- با تغییر آدرس، **شعبه و نمایندگی** خودکار به‌روزرسانی می‌شود
- احتمالاً بر اساس استان/شهر

### BR-3: Dual Student Approval
- برخی آدرس‌ها نیاز به تأیید 2 طلبه دیگر دارند
- `RequiresDualStudentApproval = true`
- `ConfirmedStudentCodms`: کد مرکز تأیید کنندگان

### BR-4: Custom Mapping
- `ConfirmDate` از string به int تبدیل می‌شود (PersianInteger)

---

## 7. Error Handling

- **هیچ Exception صریحی ندارد**
- خطاهای احتمالی از Repository

---

## 8. Risks & Notes

### امنیت:
- ⚠️ **Authorization**: چک نشده
- ⚠️ **Dual Approval**: لاجیک تأیید 2 طلبه در این Command نیست

### Code Quality:
- ❌ **TODO در کد**: RequestId نباید باشد
- **معماری نادرست**: با یک Request، دو Command اجرا می‌شود
- ⚠️ **Magic Values**: `ProjectCode = 1`, `Flag = true`

### Business Logic:
- ✅ **Auto Branch/Agency Update**: ویژگی خوب
- ⚠️ **Cascade در SP**: احتمالاً لاجیک در Stored Procedure است

---

## 9. Use Case های مرتبط

- **UC-065**: ثبت/بروزرسانی آدرس
- **Cascade**: تغییر شعبه/نمایندگی خودکار

---

## 10. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | ثبت/بروزرسانی آدرس |
| **ورودی** | Codm + Full Geographic Info |
| **خروجی** | AddressId (int) |
| **Upsert** | ✅ بر اساس Codm |
| **Cascade** | ⭐ Auto Branch/Agency Update |
| **TODO** | ❌ RequestId Architecture Issue |
| **Magic Values** | ⚠️ ProjectCode=1, Flag=true |
| **Dual Approval** | Logic نامشخص |

</div>
