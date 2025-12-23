<div dir="rtl">

# SendNotificationCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/Notifications/Commands/SendNotificationCommand.cs`  
**نوع:** Command  
**هدف:** ارسال پیام به دانشجو

---

## 🎯 هدف

ارسال **پیام SMS** به دانشجو از طریق سرویس notification

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record SendNotificationCommand(
    string Message,
    int Codm
) : IRequest<long>;
```

**خروجی:** `long` (شناسه Notification)

---

## 🔄 جریان اجرا

```
1. ساخت SendMessageToStudent
   ├─> Message
   ├─> [Codm]  (آرایه یک نفره)
   └─> [Sms]   (فقط SMS)

2. فراخوانی NotificationService
3. بازگشت Id
```

---

## ⚙️ قوانین کسب‌وکار

### BR-1: فقط SMS
```csharp
DeliveryChannelEnum.Sms
```
- فعلاً فقط پیامک
- احتمالاً بعداً Email/Push هم اضافه شود

### BR-2: یک گیرنده
```csharp
[command.Codm]  // آرایه یک نفره
```
- این Command برای یک دانشجو
- برای چند نفر باید Command دیگری باشد

---

## 💡 نکات

### Simple Wrapper
- این Command فقط یک wrapper ساده
- منطق اصلی در NotificationService

### ✅ استفاده از سرویس خارجی
```csharp
ICsisNotificationService
```
- جداسازی نگرانی‌ها
- قابل تست

---

## 📚 مستندات مرتبط

- `ICsisNotificationService`: سرویس notification
- `SendMessageToStudent`: DTO ارسال پیام

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Simplicity** | 10/10 |
| **Separation of Concerns** | 9/10 |

</div>
