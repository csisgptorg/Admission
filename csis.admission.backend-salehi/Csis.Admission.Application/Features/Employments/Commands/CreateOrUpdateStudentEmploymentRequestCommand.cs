using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>ثبت درخواست اشتغال طلبه</summary>
public sealed record CreateOrUpdateStudentEmploymentRequestCommand
    : BaseCommandDto<CreateOrUpdateStudentEmploymentRequestCommand, StudentEmployment>, IRequest<long>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }

    /// <summary>آیا درآمد دارد؟</summary>
    public bool HasIncome { get; init; }

    /// <summary>آیا کارمند است؟</summary>
    public bool IsEmployee { get; init; }

    /// <summary>نام محل کار</summary>
    public string EmployeeName { get; init; }

    /// <summary>آدرس محل کار</summary>
    public string EmployeeAddress { get; init; }

    /// <summary>آیا درآمد کافی دارد؟</summary>
    public bool HasSufficientIncome { get; init; }

    /// <summary>آیا دارای بیمه پایه دیگر است؟</summary>
    public bool HasAnotherBaseInsurance { get; init; }

    /// <summary>نام محل بیمه پایه دیگر</summary>
    public string InsurancePlaceName { get; init; }

    /// <summary>آدرس محل بیمه پایه دیگر</summary>
    public string InsurancePlaceAddress { get; init; }

    /// <summary>آیا دارای بیمه تکمیلی دیگر است؟</summary>
    public bool HasAnotherSupInsurance { get; init; }

    /// <summary>آیا در حوزه مشغول به کار است؟</summary>
    public bool IsEmployeeInHowze { get; init; }

    /// <summary>نوع اشتغال در حوزه</summary>
    public EmploymentHowzeType? HowzeTypeId { get; init; }

    /// <summary>آیا بازنشسته است؟</summary>
    public bool IsRetried { get; init; }

    /// <summary>نوع بیمه اشتغال</summary>
    public EmploymentInsuranceType? InsuranceTypeId { get; init; }

    /// <summary>مرجع اشتغال</summary>
    public EmploymentReference? Reference { get; init; }

    /// <summary>دهک درآمدی</summary>
    public short? Decile { get; init; }

    /// <summary>شناسه فایل پیوست</summary>
    public Guid? FileId { get; init; }

    /// <summary>تایید</summary>
    public bool? Confirmed { get; set; }
}

internal sealed class CreateOrUpdateStudentEmploymentRequestCommandHandler(
    IRequestService requestService,
    IRepository<StudentEmployment> repo,
    ICurrentUserService currentUser
    ) : IRequestHandler<CreateOrUpdateStudentEmploymentRequestCommand, long>
{
    public async Task<long> Handle(CreateOrUpdateStudentEmploymentRequestCommand command, CancellationToken cancellationToken) {

        if ( await currentUser.IsEmployee() && !await currentUser.IsSenior() && !command.FileId.HasValue && !command.IsEmployee) {
            throw new CommandValidationException("بارگذاری مدرک پایان اشتغال در محل کار قبلی الزامی می‌باشد.");
        }

        _ = await Common.Utilities.SetCodm(command, currentUser);
        var employment = await repo.GetOneAsTrackingAsync(x => x.Codm == command.Codm, false, cancellationToken);

        if ( command.Confirmed != true ) {
            var differences = Common.Utilities.GetDifferences(employment, command.ToEntity());
            throw new ConfirmedValidationException(differences);

        } else {
            var flow = await GetFlowAndValidation(command, employment);

            var requestCommand = new CreateRequestCommand(command, flow);
            if ( command.FileId.HasValue ) {
                requestCommand.AddDocument(command.FileId.Value);
            }
            return await requestService.Create(requestCommand, cancellationToken);
        }
    }

    private async Task<RequestFlow> GetFlowAndValidation(CreateOrUpdateStudentEmploymentRequestCommand command, StudentEmployment employment) {
        if ( await currentUser.IsSenior() || employment == null || command.IsEmployee == employment.IsEmployee || (command.IsEmployee == true && employment.IsEmployee == false) ) {
            return RequestFlow.DirectRegistration;
        }

        if ( await currentUser.IsEmployee() && command.FileId.HasValue ) {
            return RequestFlow.DirectRegistration;
        }

        if ( command.FileId == null ) {
            throw new CommandValidationException("بارگذاری مدرک پایان اشتغال در محل کار قبلی الزامی می‌باشد.");
        }

        return RequestFlow.StudentToEmployeeToSeniorEmployee;
    }
}


