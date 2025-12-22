<div dir="rtl">

# CreateOrUpdateHouseCommand.cs

**مسیر**: `Csis.Admission.Application/Features/Houses/Commands/CreateOrUpdateHouseCommand.cs`

---

## 1. Purpose (هدف)

**ثبت یا بروزرسانی اطلاعات مسکن** دانشجو شامل وضعیت سکونت (شخصی، حمایتی، اجاره‌ای) و اطلاعات اجاره (در صورت وجود).

---

## 2. مستندات XML موجود

```csharp
/// <summary>
/// ایجاد یا ویرایش مسکن طلبه
/// </summary>
```

**کامل**: ثبت/بروزرسانی اطلاعات مسکن با پشتیبانی از Upsert Pattern.

---

## 3. خلاصه اتفاقات

```
1. جستجوی House موجود با Codm
2. if (exists) → Update
   else → Insert
3. مدیریت Tenant (اطلاعات اجاره) به صورت جداگانه
4. بازگشت HouseId
```

---

## 4. اجزای اصلی

### Command:
```csharp
sealed record CreateOrUpdateHouseCommand : BaseCommandDto<..., House>, IRequest<int>
{
    int Codm                        // کد مرکز
    HouseStatus HouseStatus         // شخصی، حمایتی، اجاره‌ای/رهنی
    HouseStatusItem? HouseStatusItem // سازمانی، پدری، همسر، سایر
    string? HouseStatusItemDesc     // توضیح (وقتی سایر)
    bool? HasHouse                  // دارای خانه شخصی
    bool? HasLand                   // دارای زمین شخصی
    bool? LiveInCell                // سکونت در حجره/خوابگاه
    TenantDto? Tenant               // اطلاعات اجاره (اختیاری)
    long? RequestId                 // شناسه درخواست
}
```

### Handler Dependencies:
- `IRepository<House>` - مدیریت خانه
- `IRepository<Tenant>` - مدیریت اجاره

---

## 5. Flow

```
1. جستجوی House
   └─> houseRepo.GetOneAsTrackingAsync(Codm)

2. Upsert House
   if (house == null)
       ├─> newHouse = ToEntity()
       └─> InsertAsync() → houseId
   else
       ├─> ToEntity(house) // Update existing
       └─> UpdateAsync() → houseId

3. مدیریت Tenant (اگر موجود)
   if (Tenant != null)
       ├─> جستجوی existingTenant با Codm
       ├─> tenantEntity.Codm = Codm
       └─> if (existingTenant == null)
               └─> InsertAsync()
           else
               └─> UpdateAsync()

4. بازگشت
   └─> return houseId
```

---

## 6. Business Rules

### BR-1: Upsert Pattern
- یک دانشجو فقط **یک** House دارد
- بر اساس Codm: اگر وجود داشته باشد → Update، وگرنه → Insert

### BR-2: Tenant Management
- Tenant (اطلاعات اجاره) اختیاری است
- فقط برای `HouseStatus = اجاره‌ای/رهنی` پر می‌شود
- Tenant هم بر اساس Codm مدیریت می‌شود (Upsert)

### BR-3: House Status Hierarchy
- **HouseStatus**: سطح اول (شخصی، حمایتی، اجاره‌ای)
- **HouseStatusItem**: سطح دوم (سازمانی، پدری، همسر، سایر)
- **HouseStatusItemDesc**: توضیح برای "سایر"

---

## 7. Risks & Notes

### Code Quality:
- ✅ **Upsert Pattern**: Clean Implementation
- ✅ **Separation**: House و Tenant جدا مدیریت می‌شوند

### Business Logic:
- ⚠️ **Tenant Relationship**: Tenant از طریق Codm به House لینک می‌شود (نه FK)
- **سؤال**: آیا می‌تواند Tenant بدون House باشد؟

### Transaction:
- ⚠️ **No Explicit Transaction**: House و Tenant در 2 عملیات جدا ذخیره می‌شوند
- **Risk**: اگر Insert Tenant ناموفق باشد، House ذخیره شده اما Tenant خیر
- **پیشنهاد**: استفاده از Transaction Scope

---

## 8. Use Case های مرتبط

- **UC-060**: ثبت/بروزرسانی اطلاعات مسکن
- **Scoring**: اطلاعات مسکن در محاسبه امتیاز استفاده می‌شود

---

## 9. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | ثبت/بروزرسانی مسکن |
| **ورودی** | Codm + HouseStatus + Details + Tenant? |
| **خروجی** | HouseId (int) |
| **Pattern** | ✅ Upsert (Insert or Update) |
| **Tenant** | اختیاری، Upsert جداگانه |
| **Transaction** | ⚠️ بدون Explicit Transaction |
| **امنیت** | Authorization نامشخص |

</div>
