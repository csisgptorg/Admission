using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.QueryBuilders;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Enums;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>بروز رسانی اطلاعات شناسنامه ای</summary>
public sealed record UpdateNonIranianStudentBirthCertCommand : IRequest
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }

    /// <summary>کد یکتا</summary>
    public string YektaCode { get; init; }

    /// <summary>مذهب</summary>
    public Religion Religion { get; init; }

    /// <summary>سید</summary>
    public bool IsSadat { get; init; }
    /// <summary>توضیحات شناسنامه ای</summary>
    public string? BirthCertDescription { get; init; }
}

internal sealed class UpdateNonIranianStudentBirthCertCommandHandler(IStudentRepository studentRepo,
    IRepository<StudentSummary> studentSummaryRpo, IRepository<DependentSummary, long> dependentSummaryRpo,
    IRepository<Request, long> requestRepository, ICsisAuthenticatedUserService authenticatedUser, ICsisWsmService wsmService)
    : IRequestHandler<UpdateNonIranianStudentBirthCertCommand>
{
    public async Task Handle(UpdateNonIranianStudentBirthCertCommand command, CancellationToken cancellation) {

        var students = await studentSummaryRpo.GetAllAsync(x => x.Codm == command.Codm || x.YektaCode==command.YektaCode, false, cancellation);
        var dependnet = await dependentSummaryRpo.ExistsAsync(x => x.YektaCode == command.YektaCode, false, cancellation);
        if ( students.Count > 1 || dependnet ) {
            throw new CommandValidationException("این کد یکتا قبلاً در سامانه ثبت شده است.");
        }

        var student = students.First(x => x.Codm == command.Codm);
        if ( student.Citizenship != Citizenship.NonIranian ) {
            throw new CommandValidationException("سرویس مخصوص طلاب غیرایرانی است.");
        }

        // is senior
        var isSenior = await authenticatedUser.IsAuthorizedToAsync(PermissionsEnum.SeniorPersonnel);
        if ( !isSenior && (command.YektaCode != student.YektaCode) ) {
            throw new CommandValidationException("شما مجوز لازم برای تغییر کد یکتا را ندارید.");
        }

        var IdentityInfo=await GetIdentityInfo(command.YektaCode, cancellation);

        // update
        var birthCertInfo = new UpdateStudentBirthCertInfoRepoCommand {
            Codm = command.Codm,
            NationalCode = null,
            YektaCode = command.YektaCode,
            BirthDate = IdentityInfo.BirthDatePersianDate,
            Religion = command.Religion,
            IsSadat = command.IsSadat,
            BirthCertDescription = command.BirthCertDescription
        };
        await studentRepo.UpdateStudentBirthCertInfo(birthCertInfo);
    }

    private async Task<GetIdentityInfoByYektaCodeResponse> GetIdentityInfo(string yektaCode, CancellationToken cancellation) {
        var identityInfo = await wsmService.GetIdentityInfoByYektaCode(yektaCode, cancellation);
        if ( string.IsNullOrWhiteSpace(identityInfo.YektaCode)) {
            throw new CommandValidationException(nameof(identityInfo), "کد یکتا در المصطفی یافت نشد / کد یکتا معتبر نمی باشد.");
        }
        return identityInfo;
    }
}
