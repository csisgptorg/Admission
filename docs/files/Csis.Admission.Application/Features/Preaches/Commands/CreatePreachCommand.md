<div dir="rtl">

# CreatePreachCommand

**مسیر**: `Csis.Admission.Application/Features/Preaches/Commands/CreatePreachCommand.cs`

## Purpose
ثبت **مستقیم** سابقه تبلیغ برای طلبه.

## Flow
```
CreatePreachCommand
  ↓
ToEntity() → Custom Mappings
  → StartDate: String → Int (StringDateToInt)
  → EndDate: String → Int
  ↓
preachRepo.InsertAsync(preach)
  ↓
return preach.Id
```

## Custom Mapping ⭐

```csharp
public override void ReverseCustomMappings(...)
{
    mapping.ForMember(
        model => model.StartDate,
        config => config.MapFrom(
            dto => dto.StartDate.StringDateToInt()
        )
    );
}
```

**تبدیل**:
```
"1403/09/15" → 14030915 (int)
```

**دلیل**: ذخیره‌سازی تاریخ به صورت Int برای Performance

## Properties

```csharp
- Codm (int)
- CountryId, ProvinceId, CityId
- StartDate, EndDate (string → int)
- Kind (PreachKind enum)
- ApprovalCenter (PreachApprovalCenter enum)
- RecordIdInApprovalCenter (string)
```

## Preach Types (PreachKind)

```
- مبلغ
- امام جماعت
- خطیب
- مدرس
- سایر...
```

</div>
