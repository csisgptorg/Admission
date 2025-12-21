using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.DependentCaseActive.Commands;

/// <summary>
/// درخواست باز شدن پرونده تکفل
/// </summary>
/// <param name="Codm">کد مرکز خدمات</param>
/// <param name="DependentId">شناسه تکفل</param>
public sealed record AutomaticOpenDependentCaseRequestCommand(int Codm, long DependentId) : IRequest<long>;

internal sealed class OpenDependentCaseRequestCommandHandler(
     IRepository<DependentSummary, long> dependentSummaryRepository,
     IRepository<DependentEmployment> dependentEmploymentRepository,
     ICsisWsmService csisWsmService,
    IRequestService requestService,
    ICurrentUserService currentUserService)
    : IRequestHandler<AutomaticOpenDependentCaseRequestCommand, long>
{
    public async Task<long> Handle(AutomaticOpenDependentCaseRequestCommand request, CancellationToken cancellationToken) {
        // دریافت اطلاعات تکفل
        var dependent = await dependentSummaryRepository.GetOneAsync(x => x.Id == request.DependentId, cancellationToken: cancellationToken);
        var dependentEmployement = await dependentEmploymentRepository.GetOneAsync(x => x.DependentId == request.DependentId, cancellationToken: cancellationToken);

        // بررسی وضعیت تکفل
        if ( dependent != null && dependent.IsDead ) {
            throw new CommandValidationException("امکان باز کردن پرونده تکفل فوت شده وجود ندارد.");
        }

        if ( dependent != null && dependent.IsActive ) {
            throw new CommandValidationException("پرونده تکفل در حال حاضر فعال می‌باشد.");
        }

        // تعیین جریان درخواست بر اساس نوع کاربر
        var personnelId = await currentUserService.PersonnelId();
        var isSenior = await currentUserService.IsSenior();

        RequestFlow requestFlow;

        if ( personnelId.HasValue ) {
            // کاربر
            if ( isSenior ) {
                // کاربر ارشد - مستقیماً باز می‌شود
                requestFlow = RequestFlow.DirectRegistration;
            } else {
                // کاربر معمولی - ارجاع به کاربر ارشد
                requestFlow = RequestFlow.EmployeeToSeniorEmployee;
            }
        } else {
            // بررسی دهک تکفل - اگر دهک 7 تا 10 باشد، درخواست مستقیم ثبت میشود

            if ( dependentEmployement != null && dependentEmployement.Decile.HasValue && dependentEmployement.Decile.Value >= 7 && dependentEmployement.Decile.Value <= 10 ) {
                requestFlow = RequestFlow.DirectRegistration;
            }
            // طلبه - ارجاع به کاربر و سپس کاربر ارشد
            requestFlow = RequestFlow.StudentToEmployeeToSeniorEmployee;
        }
        var tajmieiSummary = await GetTajmieiSummary(dependent.NationalCode, cancellationToken);
        var command = new AutomaticOpenDependentCaseCommand { Codm = request.Codm, DependentId = request.DependentId, Decile = dependentEmployement?.Decile, TajmieiSummary = tajmieiSummary };
        var requestCommand = new CreateRequestCommand(command, requestFlow, RequestType.AutomaticOpenDependentCase);
        var result = await requestService.Create(requestCommand, cancellationToken);

        return result;
    }

    private async Task<object> GetTajmieiSummary(string nationalCode, CancellationToken cancellationToken) {
        var summary = await csisWsmService.GetTajmieiSummary(nationalCode, cancellationToken);
        return summary;
    }
}
