using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>اطلاعات مهم</summary>
public sealed record StudentCaseDto : BaseDto<StudentCaseDto, StudentCase>
{
    /// <summary>کد مرکز مرتبط با پرونده</summary>
    public int Codm { get; init; }

    /// <summary>تاریخ تشکیل پرونده</summary>
    public string CaseCreationDate { get; init; }

    /// <summary>وضعیت فعال یا غیرفعال بودن پرونده</summary>
    public bool IsActive { get; init; }

    /// <summary>تاریخ اعتبار پرونده (در صورت وجود)</summary>
    public string CaseValidityDate { get; init; }

    /// <summary>علت تمدید اعتبار پرونده</summary>
    public string ValidityExtensionReasonTitle { get; init; }

    /// <summary>نشان‌دهنده این که صاحب پرونده طلبه است یا خیر</summary>
    public bool IsStudent { get; init; }

    /// <summary>نشان‌دهنده اینکه پرونده مسدود شده است یا خیر</summary>
    public bool IsBlock { get; init; }

    /// <summary>تاریخ انسداد پرونده (در صورت وجود)</summary>
    public string BlockDate { get; init; }

    /// <summary>علت انسداد پرونده</summary>
    public string BlockReasonTitle { get; init; }

    /// <summary>امکان تمدید پرونده وجود دارد</summary>
    public bool CanExtensionCase => CalcCanExtensionCase();

    /// <summary>امتیاز هدفمندی</summary>
    public float TotalScore { get; set; }

    /// <inheritdoc/>
    /// <param name="mapping"></param>
    public override void CustomMappings(IMappingExpression<StudentCase, StudentCaseDto> mapping) {
        mapping.ForMember(dto => dto.CaseCreationDate, config => config.MapFrom(model => model.CaseCreationDate.IntDateToString()));
        mapping.ForMember(dto => dto.BlockDate, config => config.MapFrom(model => model.BlockDate.IntDateToString()));
        mapping.ForMember(dto => dto.CaseValidityDate, config => config.MapFrom(model => model.CaseValidityDate.IntDateToString()));
    }

    private bool CalcCanExtensionCase() {
        if ( IsBlock ) {
            return false;
        }
        var caseCreationDate = Utilities.PersianDateTime.ParseToDateTime(CaseValidityDate);
        return caseCreationDate <= DateTime.Now.AddMonths(3);
    }
}
