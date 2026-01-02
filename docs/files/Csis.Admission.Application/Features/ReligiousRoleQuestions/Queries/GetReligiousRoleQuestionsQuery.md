# GetReligiousRoleQuestionsQuery.cs

**مسیر**: `Csis.Admission.Application/Features/ReligiousRoleQuestions/Queries/GetReligiousRoleQuestionsQuery.cs`

## هدف
دریافت **سوالات نقش‌های دینی**.

## ورودی/خروجی
```csharp
public sealed record GetReligiousRoleQuestionsQuery : IRequest<ReligiousRoleQuestionDto[]>;
```

## کاربرد
- پرسشنامه نقش‌های دینی
- ارزیابی فعالیت‌های دینی
- فرم‌های ثبت نقش دینی

## الگوها
✅ CQRS ✅ Repository

## نتیجه
Query Master Data سوالات نقش‌های دینی.  
⚠️ پیشنهاد: Caching
