using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>اطلاعات پرونده ای</summary>
public sealed record StudentSummaryCaseDto : BaseDto<StudentSummaryCaseDto, StudentSummary>
{
    /// <summary>کد مرکز مرتبط با پرونده</summary>
    public int Codm { get; init; }

    /// <summary>تاریخ تشکیل پرونده</summary>
    public string CaseCreationDate { get; init; }

    /// <summary>وضعیت فعال یا غیرفعال بودن پرونده</summary>
    public bool IsActive { get; init; }

    /// <summary>تاریخ اعتبار پرونده (در صورت وجود)</summary>
    public string CaseValidityDate { get; init; }

    /// <summary>نشان‌دهنده این که صاحب پرونده طلبه است یا خیر</summary>
    public bool IsStudent { get; init; }

    /// <summary>نشان‌دهنده اینکه پرونده مسدود شده است یا خیر</summary>
    public bool IsBlock { get; init; }

    /// <summary>تاریخ انسداد پرونده (در صورت وجود)</summary>
    public string BlockDate { get; init; }

    // / <summary> کد شعبه مرتبط با پرونده </summary>
    public int BranchId { get; init; }
    // / <summary> کد نمایندگی مرتبط با پرونده </summary>
    public int AgencyId { get; init; }

    /// <summary> شماره حساب بانکی مرتبط با پرونده (در صورت وجود) </summary>
    public string BankAccountNumber { get; init; }
    // توضیحات مربوط به پرونده
    public string CaseDescription { get; init; }
    /// <summary>
    /// مرکز حوزوی مرتبط با پرونده
    /// </summary>
    public ApprovalCenter ApprovalCenter { get; init; }
    /// <summary>
    /// شماره پرونده در مرکز حوزوی
    /// </summary>
    public long? CaseNumInApprovalCenter { get; init; }
    /// <summary>
    /// تاریخ پوشش دینی
    /// </summary>
    public string ReligiouslyDressedDate { get; init; }


    /// <inheritdoc/>
    /// <param name="mapping"></param>
    public override void CustomMappings(IMappingExpression<StudentSummary, StudentSummaryCaseDto> mapping) {
        mapping.ForMember(dto => dto.CaseCreationDate, config => config.MapFrom(model => model.CaseCreationDate.IntDateToString()));
        mapping.ForMember(dto => dto.BlockDate, config => config.MapFrom(model => model.BlockDate.IntDateToString()));
        mapping.ForMember(dto => dto.CaseValidityDate, config => config.MapFrom(model => model.CaseValidityDate.IntDateToString()));
        mapping.ForMember(dto => dto.ReligiouslyDressedDate, config => config.MapFrom(model => model.ReligiouslyDressedDate.IntDateToString()));
    }
}
