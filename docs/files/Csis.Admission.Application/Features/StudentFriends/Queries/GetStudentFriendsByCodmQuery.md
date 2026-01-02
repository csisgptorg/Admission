# GetStudentFriendsByCodmQuery.cs

**مسیر**: `Csis.Admission.Application/Features/StudentFriends/Queries/GetStudentFriendsByCodmQuery.cs`

## هدف
دریافت **لیست دوستان/همراهان دانشجو**.

## ورودی/خروجی
```csharp
public sealed record GetStudentFriendsByCodmQuery(int Codm) : IRequest<StudentFriendDto[]>;
```

## کاربرد
- مدیریت روابط دانشجویی
- شبکه اجتماعی
- ارتباطات

## الگوها
✅ CQRS ✅ Repository

## نتیجه
Query لیست دوستان/همراهان دانشجو.
