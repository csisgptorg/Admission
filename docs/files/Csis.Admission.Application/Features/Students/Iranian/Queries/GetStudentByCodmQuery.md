<div dir="rtl">

# GetStudentByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/Students/Iranian/Queries/GetStudentByCodmQuery.cs`

---

## 1. Purpose (هدف)

دریافت اطلاعات کامل یک دانشجو بر اساس CODM با **کنترل دسترسی بر اساس جنسیت** و **ثبت لاگ مشاهده**.

---

## 2. مستندات XML موجود

```csharp
/// <inheritdoc/>
```

**تکمیل شده**: Query برای دریافت اطلاعات دانشجو با محدودیت دسترسی به اطلاعات دانشجویان خانم.

---

## 3. خلاصه اتفاقات

```
1. دریافت دانشجو از Repository
2. ثبت لاگ مشاهده (CreateEmployeeViewStudentLogCommand)
3. بررسی مجوز برای دانشجویان خانم (FemaleInfoIdentity)
4. بازگشت StudentDto
```

---

## 4. اجزای اصلی

### Query:
```csharp
sealed record GetStudentByCodmQuery(int Codm) : IRequest<StudentDto>
```

### Handler Dependencies:
- `IStudentRepository` - دریافت دانشجو
- `ICsisAuthenticatedUserService` - بررسی مجوز
- `IMediator` - ثبت لاگ

---

## 5. Flow

```
1. دریافت دانشجو
   └─> studentRepo.GetByCodm(Codm)
   └─> if (null) → CommandValidationException

2. ثبت لاگ
   └─> mediator.Send(CreateEmployeeViewStudentLogCommand(Codm))

3. بررسی مجوز (برای دانشجویان خانم)
   if (Gender == Female && !HasPermission(FemaleInfoIdentity))
       └─> UnauthorizedActionException

4. بازگشت
   └─> return StudentDto
```

---

## 6. Business Rules

### BR-1: Gender-Based Access Control
- **دانشجویان آقا**: همه می‌توانند ببینند
- **دانشجویان خانم**: فقط کاربران با مجوز `FemaleInfoIdentity`

### BR-2: Audit Logging
- هر بار مشاهده دانشجو توسط کارمند، لاگ ثبت می‌شود

---

## 7. Error Handling

| Exception | شرط | پیام |
|-----------|------|------|
| `CommandValidationException` | دانشجو یافت نشد | "طلبه ای با کد '{Codm}' یافت نشد." |
| `UnauthorizedActionException` | عدم مجوز برای دانشجوی خانم | توضیح مجوز FemaleInfoIdentity |

---

## 8. Risks & Notes

### امنیت:
- ✅ **Gender-Based Access Control** - حریم خصوصی دانشجویان خانم
- ✅ **Audit Logging** - ردیابی دسترسی‌ها

### کارایی:
- ✅ Query ساده و سریع
- ⚠️ Logging به صورت Sync (می‌تواند کند کند)

### Code Quality:
- ⚠️ **Side Effect در Query**: ثبت لاگ در Query (بهتر است در Interceptor باشد)
- ✅ Permission Check واضح

---

## 9. Use Case های مرتبط

- **UC-010**: مشاهده اطلاعات کامل دانشجو
- **Audit**: لاگ مشاهده برای Compliance

---

## 10. خلاصه نکات کلیدی

| نکته | توضیح |
|------|-------|
| **نقش** | دریافت اطلاعات دانشجو |
| **ورودی** | Codm |
| **خروجی** | StudentDto |
| **Authorization** | FemaleInfoIdentity برای دانشجویان خانم |
| **Audit** | ✅ ثبت لاگ مشاهده |
| **امنیت** | ✅ Gender-Based Access Control |
| **Side Effect** | ⚠️ Logging در Query |

</div>
