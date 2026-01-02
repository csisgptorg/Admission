# NotificationService.cs

**مسیر**: `Csis.Admission.Services/NotificationService.cs`

## 1. هدف (Purpose)

این سرویس برای **ارسال اعلان‌ها و پیام‌ها** به دانشجویان و کارمندان استفاده می‌شود.

### کاربرد اصلی:
- ارسال SMS
- ارسال ایمیل
- اعلان‌های داخل سیستم
- یادآوری‌ها

---

## 2. Interface

```csharp
public interface INotificationService
{
    Task<long> SendMessageToStudent(SendMessageToStudent command, CancellationToken ct);
    Task<long> SendMessageToEmployee(SendMessageToEmployee command, CancellationToken ct);
    // ... سایر متدها
}
```

---

## 3. کانال‌های ارسال

### DeliveryChannelEnum:
- **SMS**: پیامک
- **Email**: ایمیل
- **InApp**: اعلان داخل سیستم
- **Push**: Push Notification

---

## 4. وابستگی‌ها

**Dependencies:**
1. **ICsisNotificationService**: سرویس خارجی اعلان‌ها
2. **ICurrentUserService**: اطلاعات کاربر جاری

---

## 5. الگوهای طراحی

1. **Service Layer Pattern**
2. **External Service Integration**
3. **Multi-Channel Notification**

---

## 6. مثال استفاده

```csharp
var notification = new SendMessageToStudent(
    Message: "پیام شما",
    Codms: [123456],
    Channels: [DeliveryChannelEnum.Sms, DeliveryChannelEnum.InApp]
);

var messageId = await _notificationService.SendMessageToStudent(notification, ct);
```

---

## 7. Use Cases

- **UC-090**: ارسال اعلان‌ها
- یادآوری مهلت‌ها
- تایید عملیات
- اطلاع‌رسانی تغییرات

---

## نتیجه‌گیری

سرویس **حیاتی** برای ارتباط با کاربران.

### نقاط قوت:
✅ Multi-Channel  
✅ پشتیبانی SMS و Email  
✅ Bulk Messaging  

### توصیه:
⚠️ Rate Limiting  
⚠️ Queue برای پیام‌های انبوه
