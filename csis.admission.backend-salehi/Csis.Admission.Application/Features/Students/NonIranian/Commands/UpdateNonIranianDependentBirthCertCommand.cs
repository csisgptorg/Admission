using Csis.Authorization.Services;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>بروز رسانی اطلاعات شناسنامه ای تکفل</summary>
public sealed record UpdateNonIranianDependentBirthCertCommand : IRequest
{
    /// <summary>شناسه تکفل</summary>
    public long Id { get; init; }

    /// <summary>کد یکتا</summary>
    public string YektaCode { get; init; }

    /// <summary>مذهب</summary>
    public Religion Religion { get; init; }

    /// <summary>سید</summary>
    public bool IsSadat { get; init; }
    /// <summary>توضیحات شناسنامه ای</summary>
    public string? BirthCertDescription { get; init; }
}

internal sealed class UpdateNonIranianDependentBirthCertCommandHandler(IStudentRepository studentRepo, ICsisAuthenticatedUserService authenticatedUser,
    IRepository<StudentSummary> studentSummaryRpo, IRepository<DependentSummary, long> dependentSummaryRpo,
    IRepository<Request, long> requestRepository, ICsisWsmService wsmService)
    : IRequestHandler<UpdateNonIranianDependentBirthCertCommand>
{
    public async Task Handle(UpdateNonIranianDependentBirthCertCommand command, CancellationToken cancellation) {

        var dependents = await dependentSummaryRpo.GetAllAsync(x => x.Id == command.Id || x.YektaCode == command.YektaCode, false, cancellation);
        var student = await studentSummaryRpo.ExistsAsync(x => x.YektaCode == command.YektaCode, false, cancellation);
        if ( dependents.Count > 1 || student ) {
            throw new CommandValidationException("این کد یکتا قبلاً در سامانه ثبت شده است.");
        }

        var dependent = dependents.First(x => x.Id == command.Id);

        if ( dependent.Citizenship != Citizenship.NonIranian ) {
            throw new CommandValidationException("سرویس مخصوص تکفل غیرایرانی است.");
        }

        // is senior
        var isSenior = await authenticatedUser.IsAuthorizedToAsync(PermissionsEnum.SeniorPersonnel);
        if ( !isSenior && (command.YektaCode != dependent.YektaCode) ) {
            throw new CommandValidationException("شما مجوز لازم برای تغییر کد یکتا را ندارید.");
        }

        var identityInfo = await GetIdentityInfo(command.YektaCode, cancellation);

        // update
        var birthCertInfo = new UpdateDependentBirthCertInfoRepoCommand {
            Id = command.Id,
            Codm = dependent.Codm,
            NationalCode = null,
            YektaCode = command.YektaCode,
            BirthDate = identityInfo.BirthDatePersianDate,
            Religion = command.Religion,
            IsSadat = command.IsSadat,
            BirthCertDescription = command.BirthCertDescription
        };
        await studentRepo.UpdateDependentBirthCertInfo(birthCertInfo);
    }

    private async Task<GetIdentityInfoByYektaCodeResponse> GetIdentityInfo(string yektaCode, CancellationToken cancellation) {
        var identityInfo = await wsmService.GetIdentityInfoByYektaCode(yektaCode, cancellation);
        if ( string.IsNullOrWhiteSpace(identityInfo.YektaCode) ) {
            throw new CommandValidationException(nameof(identityInfo), "کد یکتا در المصطفی یافت نشد / کد یکتا معتبر نمی باشد.");
        }
        return identityInfo;
    }
}
