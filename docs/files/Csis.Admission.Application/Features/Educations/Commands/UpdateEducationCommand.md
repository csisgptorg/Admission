<div dir="rtl">

# UpdateEducationCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/Educations/Commands/UpdateEducationCommand.cs`  
**نوع:** Command  
**هدف:** بروزرسانی اطلاعات تحصیلات حوزوی

---

## 🎯 هدف

بروزرسانی **اطلاعات تحصیلات حوزوی** دانشجو:
- مرجع حوزوی (ApprovalCenter)
- شماره پرونده در مرجع

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record UpdateEducationCommand : IRequest
{
    public int Codm { get; init; }
    public ApprovalCenter? ApprovalCenter { get; init; }
    public long? CaseNumInApprovalCenter { get; init; }
}
```

---

## 🔄 جریان اجرا

```
1. دریافت Education با Tracking
2. بررسی وجود
3. بروزرسانی با ToEntity
4. UpdateAsync
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: Education موجود
- باید قبلاً ثبت شده باشد
- اگر نه → Exception

### BR-2: Nullable Fields
- ApprovalCenter و CaseNum اختیاری

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Simplicity** | 10/10 |

</div>
