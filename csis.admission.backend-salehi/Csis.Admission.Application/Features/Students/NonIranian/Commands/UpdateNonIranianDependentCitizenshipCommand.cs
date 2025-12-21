using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Extensions;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>بروز رسانی تابعیت براساس ثبت احوال</summary>
public sealed record UpdateNonIranianDependentCitizenshipCommand : IRequest
{
    /// <summary>کد مرکز خدمات</summary>
    public long Id { get; init; }
    /// <summary>کد ملی</summary>
    public string NationalCode { get; init; }
    /// <summary>تاریخ تولد</summary>
    public string BirthDate { get; init; }
}

internal sealed class UpdateNonIranianDependentCitizenshipCommandHandler(IStudentRepository studentRepo,
    IRepository<StudentSummary> studentSummaryRpo, IRepository<DependentSummary, long> dependentSummaryRpo,
    ICsisWsmService wsmService, IRepository<Request, long> requestRepository, ICsisAuthenticatedUserService authenticatedUser)
    : IRequestHandler<UpdateNonIranianDependentCitizenshipCommand>
{
    public async Task Handle(UpdateNonIranianDependentCitizenshipCommand command, CancellationToken cancellation) {
        var dependents = await dependentSummaryRpo.GetAllAsync(x => x.Id == command.Id || x.NationalCode == command.NationalCode, false, cancellation);
        var student = await studentSummaryRpo.ExistsAsync(x => x.NationalCode == command.NationalCode, false, cancellation);
        if ( dependents.Count > 1 || student) {
            throw new CommandValidationException("این کد ملی قبلاً در سامانه ثبت شده است.");
        }

        var dependent = dependents.First(x => x.Id == command.Id);
        if ( dependent.Citizenship != Citizenship.NonIranian ) {
            throw new CommandValidationException("سرویس مخصوص تکفل غیرایرانی است.");
        }

        // is senior
        var isSenior = await authenticatedUser.IsAuthorizedToAsync(PermissionsEnum.SeniorPersonnel);
        if ( !isSenior && (command.NationalCode != dependent.NationalCode || command.BirthDate.StringDateToInt() != dependent.BirthDate) ) {
            throw new CommandValidationException("شما مجوز لازم برای تغییر کد ملی را ندارید.");
        }

        // get identity
        var info = await GetIdentityInfo(command.NationalCode, command.BirthDate, cancellation);
        var repoCommand = new UpdateNonIranianDependentCitizenshipRepoCommand(command.Id, info.NationalCode, info.BirthDate.StringDateToInt().Value);
        await studentRepo.UpdateNonIranianDependentCitizenship(repoCommand);
    }

    private async Task<BirthCertInfo> GetIdentityInfo(string nationalCode, string birthDate, CancellationToken cancellation) {
        var identityRequest = new GetIdentityInfoByNationalCodeRequestApiM(nationalCode, birthDate.Replace("/", ""));
        var identityInfo = await wsmService.GetIdentityInfoByNationalCode(identityRequest, cancellation);
        if ( string.IsNullOrEmpty(identityInfo.Nin) ) {
            throw new CommandValidationException(nameof(identityInfo), "اطلاعات در ثبت احوال یافت نشد/ کد ملی و تاریخ تولد معتبر نمی باشند.");
        }

        return identityInfo.BirthCertInfo();
    }
}
