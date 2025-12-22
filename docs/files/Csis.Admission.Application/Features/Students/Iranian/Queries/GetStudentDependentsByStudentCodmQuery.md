<div dir="rtl">

# GetStudentDependentsByStudentCodmQuery

## 📄 اطلاعات کلی

**مسیر:** `Csis.Admission.Application/Features/Students/Iranian/Queries/GetStudentDependentsByStudentCodmQuery.cs`  
**نوع:** Query  
**هدف:** دریافت لیست افراد تحت تکفل دانشجو

---

## 🎯 هدف

دریافت **لیست تمامی افراد تحت تکفل** دانشجو (همسر، فرزندان، والدین، نوه‌ها) با **ترتیب منطقی**.

---

## 📝 ساختار

**ورودی:**
```csharp
public sealed record GetStudentDependentsByStudentCodmQuery(int Codm) 
    : IRequest<StudentDependentDto[]>;
```

**خروجی:**
```csharp
StudentDependentDto[]  // آرایه افراد تحت تکفل (مرتب شده)
```

---

## ⚙️ ویژگی‌های کلیدی

### مرتب‌سازی سفارشی (Custom Ordering)

```csharp
private readonly Dictionary<DependentRelation, short> _customOrder = new() {
    { DependentRelation.Spouse, 1 },      // همسر: اولویت 1
    { DependentRelation.Child, 2 },       // فرزند: اولویت 2
    { DependentRelation.Parent, 3 },      // والد: اولویت 3
    { DependentRelation.Grandchild, 4 },  // نوه: اولویت 4
    { DependentRelation.AdoptedChild, 5 } // فرزندخوانده: اولویت 5
};
```

**منطق مرتب‌سازی:**
1. **اولویت اول**: بر اساس نوع رابطه (Relation)
2. **اولویت دوم**: بر اساس Id (برای افراد با رابطه یکسان)
3. روابط تعریف نشده: اولویت `int.MaxValue` (آخر لیست)

**کد مرتب‌سازی:**
```csharp
dependents
    .OrderBy(dto => _customOrder.TryGetValue(dto.Relation, out var value) 
        ? value 
        : int.MaxValue)
    .ThenBy(x => x.Id)
```

---

## 🎯 قوانین کسب‌وکار

### BR-1: ترتیب منطقی نمایش
- **همسر**: همیشه در ابتدای لیست
- **فرزندان**: بعد از همسر
- **والدین**: بعد از فرزندان
- **سایرین**: در انتها

**دلیل:** راحتی استفاده برای کاربر

### BR-2: Projection
- استفاده از `GetAllAsync<StudentDependentDto>`
- فقط فیلدهای مورد نیاز دریافت می‌شود (بهینه)

---

## 📊 عملکرد

- ✅ **Projection**: SELECT فقط فیلدهای لازم
- ✅ **In-Memory Sorting**: مرتب‌سازی بعد از دریافت (حجم کم)
- 💡 **پیشنهاد**: اگر تعداد افراد تحت تکفل زیاد باشد، مرتب‌سازی در DB بهتر است

---

## 🧪 Use Cases

### UC-040: مشاهده لیست افراد تحت تکفل

**Actor**: دانشجو / کارمند

**Main Flow**:
1. کاربر وارد صفحه مشاهده افراد تحت تکفل می‌شود
2. سیستم این Query را اجرا می‌کند
3. لیست به ترتیب منطقی نمایش داده می‌شود:
   - همسر
   - فرزندان (مرتب بر اساس Id)
   - والدین
   - سایرین

**Postconditions:**
- لیست مرتب شده نمایش داده شده

---

## 💡 نکات ویژه

### ✅ نکته مثبت 1: Collection Expression
```csharp
return [.. dependents.OrderBy(...).ThenBy(...)];
```
- استفاده از syntax جدید C# 12
- خوانایی بالا

### ✅ نکته مثبت 2: TryGetValue Pattern
```csharp
_customOrder.TryGetValue(dto.Relation, out var value) ? value : int.MaxValue
```
- جلوگیری از Exception
- روابط جدید به طور خودکار در انتها قرار می‌گیرند

### 💡 پیشنهاد: Enum کامل‌تر
اگر روابط جدیدی اضافه شود، باید `_customOrder` بروز شود:
```csharp
// بهتر است این Dictionary در یک Config Class باشد
public static class DependentRelationPriority
{
    public static readonly Dictionary<DependentRelation, short> Order = new() { ... };
}
```

---

## 📚 مستندات مرتبط

- `GetStudentDependentByCodmQuery`: دریافت یک تحت تکفل خاص
- `CreateStudentDependentCommand`: افزودن تحت تکفل جدید
- `UpdateStudentDependentCommand`: بروزرسانی تحت تکفل

---

## 📊 خلاصه

| جنبه | نمره |
|------|------|
| **سادگی** | 9/10 |
| **عملکرد** | 8/10 |
| **کیفیت کد** | 9/10 |
| **UX** | 10/10 (ترتیب منطقی عالی) |

**نکته برجسته:** مرتب‌سازی سفارشی برای UX بهتر 👍

</div>
