using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Features.CaseFilings.Dtos;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary> درخواست تأیید اشتغال </summary>
public sealed record ConfirmEmploymentCommand : IRequest
{
    /// <summary>توکن  </summary>
    public Guid Token { get; init; }

    /// <summary>آیا درآمد دارد؟</summary>
    public bool? HasIncome { get; init; }

    /// <summary>آیا کارمند است؟</summary>
    public bool IsEmployee { get; init; }

    /// <summary>نام محل کار</summary>
    public string? EmployeeName { get; init; }

    /// <summary>آدرس محل کار</summary>
    public string? EmployeeAddress { get; init; }

    /// <summary>آیا درآمد کافی دارد؟</summary>
    public bool? HasSufficientIncome { get; init; }

    /// <summary>آیا دارای بیمه پایه دیگر است؟</summary>
    public bool? HasAnotherBaseInsurance { get; init; }

    /// <summary>نام محل بیمه پایه دیگر</summary>
    public string? InsurancePlaceName { get; init; }

    /// <summary>آدرس محل بیمه پایه دیگر</summary>
    public string? InsurancePlaceAddress { get; init; }

    /// <summary>آیا دارای بیمه تکمیلی دیگر است؟</summary>
    public bool? HasAnotherSupInsurance { get; init; }

    /// <summary>آیا در حوزه مشغول به کار است؟</summary>
    public bool? IsEmployeeInHowze { get; init; }

    /// <summary>نوع اشتغال در حوزه</summary>
    public EmploymentHowzeType? HowzeTypeId { get; init; }

    /// <summary>آیا بازنشسته است؟</summary>
    public bool? IsRetried { get; init; }

    /// <summary>نوع بیمه اشتغال</summary>
    public EmploymentInsuranceType? InsuranceTypeId { get; init; }

    /// <summary>مرجع اشتغال</summary>
    public EmploymentReference? Reference { get; init; }

    /// <summary>شناسه فایل پیوست</summary>
    public Guid? FileId { get; init; }
}

internal sealed class ConfirmEmploymentCommandHandler(
    IRepository<AdmissionCaseUser, Guid> caseRepository)
    : IRequestHandler<ConfirmEmploymentCommand>
{
    public async Task Handle(ConfirmEmploymentCommand request, CancellationToken cancellationToken) {

        var admissionCaseUser = await caseRepository.GetByIdAsTrackingAsync(request.Token, cancellationToken: cancellationToken)
                                ?? throw new CommandValidationException("شناسه نامعتبر است.");

        admissionCaseUser.CaseStep = AdmissionCaseStep.EmploymentVerified;
        admissionCaseUser.Payloads = PayloadHelper.AddPayloadsToString(request, admissionCaseUser.Payloads, nameof(AdmissionCasePayloadName.Employment));
        await caseRepository.UpdateAsync(admissionCaseUser, cancellationToken: cancellationToken);
    }
}


