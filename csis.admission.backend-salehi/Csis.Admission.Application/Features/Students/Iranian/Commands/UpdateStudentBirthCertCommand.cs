using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Extensions;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>بروز رسانی اطلاعات شناسنامه ای</summary>
public sealed record UpdateStudentBirthCertCommand : IRequest
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }

    /// <summary>کد ملی</summary>
    public string NationalCode { get; init; }

    /// <summary>تاریخ تولد</summary>
    public string BirthDate { get; init; }

    /// <summary>مذهب</summary>
    public Religion Religion { get; init; }

    /// <summary>توضیحات</summary>
    public string Description { get; init; }
}

internal sealed class UpdateStudentBirthCertCommandHandler(IStudentRepository studentRepo,
    IRepository<StudentSummary> studentSummaryRpo, IRepository<DependentSummary, long> dependentSummaryRpo,
    ICsisAuthenticatedUserService authenticatedUser, ICsisWsmService wsmService)
    : IRequestHandler<UpdateStudentBirthCertCommand>
{
    public async Task Handle(UpdateStudentBirthCertCommand command, CancellationToken cancellation) {

        var students = await studentSummaryRpo.GetAllAsync(x => x.Codm == command.Codm || x.NationalCode==command.NationalCode, false, cancellation);
        if ( students.Count > 1 ) {
            throw new CommandValidationException("این کد ملی قبلاً در سامانه ثبت شده است.");
        }

        var student = students.First(x => x.Codm == command.Codm);

        // is senior
        var isSenior = await authenticatedUser.IsAuthorizedToAsync(PermissionsEnum.SeniorPersonnel);
        if ( !isSenior && (command.NationalCode != student.NationalCode || command.BirthDate.StringDateToInt() != student.BirthDate) ) {
            throw new CommandValidationException("شما مجوز لازم برای تغییر کد ملی و تاریخ تولد را ندارید.");
        }

        await ValidateIdentityInfo(command, cancellation);

        // update
        var birthCertInfo = new UpdateStudentBirthCertInfoRepoCommand {
            Codm = command.Codm,
            NationalCode = command.NationalCode,
            YektaCode = null,
            BirthDate = command.BirthDate.StringDateToInt().Value,
            Religion = command.Religion,
            IsSadat = student.IsSadat,
            BirthCertDescription = command.Description
        };
        await studentRepo.UpdateStudentBirthCertInfo(birthCertInfo);
    }

    private async Task ValidateIdentityInfo(UpdateStudentBirthCertCommand command, CancellationToken cancellation) {
        var identityRequest = new GetIdentityInfoByNationalCodeRequestApiM(command.NationalCode, command.BirthDate.Replace("/", ""));
        var identityInfo = await wsmService.GetIdentityInfoByNationalCode(identityRequest, cancellation);
        if ( string.IsNullOrEmpty(identityInfo.Nin) ) {
            throw new CommandValidationException("کد ملی یا تاریخ تولد وارد شده در ثبت احوال یافت نشد.");
        }
    }
}
