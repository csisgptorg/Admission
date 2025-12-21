using Csis.Authorization.Services;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>بروز رسانی تابعیت براساس ثبت احوال</summary>
public sealed record UpdateNonIranianStudentCitizenshipCommand : IRequest
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }
    /// <summary>کد ملی</summary>
    public string NationalCode { get; init; }
    /// <summary>تاریخ تولد</summary>
    public string BirthDate { get; init; }
}

internal sealed class UpdateNonIranianStudentCitizenshipCommandHandler(IStudentRepository studentRepo,
    IRepository<StudentSummary> studentSummaryRpo, IRepository<DependentSummary,long> dependentSummaryRpo,
    ICsisWsmService wsmService, IRepository<Request, long> requestRepository,ICsisAuthenticatedUserService authenticatedUser)
    : IRequestHandler<UpdateNonIranianStudentCitizenshipCommand>
{
    public async Task Handle(UpdateNonIranianStudentCitizenshipCommand command, CancellationToken cancellation) {

        var students = await studentSummaryRpo.GetAllAsync(x => x.Codm== command.Codm || x.NationalCode==command.NationalCode, false, cancellation);
        var dependnet = await dependentSummaryRpo.ExistsAsync(x => x.NationalCode==command.NationalCode, false, cancellation);
        if ( students.Count > 1  || dependnet) {
            throw new CommandValidationException("این کد ملی قبلاً در سامانه ثبت شده است.");
        }

        var student = students.First(x=>x.Codm==command.Codm);
        if ( student.Citizenship != Citizenship.NonIranian ) {
            throw new CommandValidationException("سرویس مخصوص طلاب غیرایرانی است.");
        }

        // is senior
        var isSenior = await authenticatedUser.IsAuthorizedToAsync(PermissionsEnum.SeniorPersonnel);
        if ( !isSenior && (command.NationalCode != student.NationalCode || command.BirthDate.StringDateToInt() != student.BirthDate) ) {
            throw new CommandValidationException("شما مجوز لازم برای تغییر کد ملی را ندارید.");
        }

        // get identity
        var info = await GetIdentityInfo(command.NationalCode, command.BirthDate, cancellation);
        var repoCommand = new UpdateNonIranianStudentCitizenshipRepoCommand(command.Codm, info.NationalCode, info.BirthDate.StringDateToInt().Value);
        await studentRepo.UpdateNonIranianStudentCitizenship(repoCommand);
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
