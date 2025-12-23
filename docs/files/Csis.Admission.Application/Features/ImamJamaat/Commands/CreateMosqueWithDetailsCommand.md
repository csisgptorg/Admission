<div dir="rtl">

# CreateMosqueWithDetailsCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/ImamJamaat/Commands/CreateMosqueWithDetailsCommand.cs`  
**نوع:** Command  
**هدف:** ثبت مسجد با تمام جزئیات

---

## 🎯 هدف

ثبت **کامل مسجد** شامل:
- اطلاعات مسجد
- امام جماعت
- فعالیت‌های مسجد
- آدرس مسجد

**Aggregate Command:** یک تراکنش کامل

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record CreateMosqueWithDetailsCommand : IRequest<int>
{
    public MosqueCommandDto Mosque { get; init; }
    public ImamJamaatCommandDto ImamJamaat { get; init; }
    public MosqueActivityCommandDto MosqueActivity { get; init; }
    public MosqueAddressCommandDto? MosqueAddress { get; init; }
    public int? MosqueAddressId { get; init; }
}
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: بررسی امکان ثبت
```csharp
ImamJamaatCanRegisterQuery
```

### BR-2: جلوگیری از تکرار
- PostalCode + CodM یکتا

### BR-3: روابط
- Mosque 1:N ImamJamaat
- Mosque 1:1 Activity
- Mosque 1:1 Address

---

## 💡 نکات

### ✅ Aggregate Pattern
- یک Command برای کل Aggregate
- Consistency بالا

### ✅ Logging
```csharp
logger.LogInformation("مسجد با ID {MosqueId}...", mosque.Id);
```

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Aggregate Pattern** | 10/10 |
| **Transaction** | 10/10 |

</div>
