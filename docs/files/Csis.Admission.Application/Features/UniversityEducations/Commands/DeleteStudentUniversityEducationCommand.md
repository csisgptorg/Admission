<div dir="rtl">

# DeleteStudentUniversityEducationCommand

## 📄 اطلاعات کلی

**مسیر:** `Features/UniversityEducations/Commands/DeleteStudentUniversityEducationCommand.cs`  
**نوع:** Command  
**هدف:** حذف تحصیلات دانشگاهی دانشجو

---

## 🎯 هدف

حذف **یک رکورد تحصیلات دانشگاهی** از لیست تحصیلات دانشجو

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record DeleteStudentUniversityEducationCommand(
    int Codm,
    int EducationId
) : IRequest<int>;
```

---

## ⚠️ نکات

### Codm استفاده نمی‌شود
```csharp
// Codm در ورودی است اما استفاده نمی‌شود!
```

**پیشنهاد:** بررسی Ownership

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **Simplicity** | 10/10 |
| **Security** | 5/10 |

</div>
