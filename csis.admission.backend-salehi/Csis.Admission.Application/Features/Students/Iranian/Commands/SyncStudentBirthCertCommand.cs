using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>بروز رسانی اطلاعات شناسنامه ای براساس ثبت احوال یا المصطفی</summary>
public sealed record SyncStudentBirthCertCommand : IRequest
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }

    /// <summary>تایید</summary>
    [JsonIgnore]
    public bool? Confirmed { get; set; }
}

internal sealed class SyncStudentIdentityCommandHandler(IStudentRepository studentRepo, IRepository<StudentSummary> studentSummaryRpo,
    IBirthCertService birthCertService, ICurrentUserService currentUser)
    : IRequestHandler<SyncStudentBirthCertCommand>
{
    public async Task Handle(SyncStudentBirthCertCommand command, CancellationToken cancellation) {
        var student = await studentSummaryRpo.GetOneAsync(x => x.Codm == command.Codm, false, cancellation);
        await NotConfirmed(command.Confirmed, student, cancellation);

        if ( student.Citizenship == Citizenship.Iranian ) {
            await SyncIranian(student, cancellation);

        } else if ( student.Citizenship == Citizenship.NonIranian ) {
            await SyncNonIranian(student, cancellation);
        }
    }

    private async Task NotConfirmed(bool? confirmed, StudentSummary student, CancellationToken cancellation) {
        if ( confirmed == true ) { return; }

        if ( student.Citizenship == Citizenship.Iranian ) {
            var info = await birthCertService.Iranian(student.NationalCode, student.BirthDate.IntDateToString(), cancellation);
            throw new ConfirmedValidationException(info);

        } else if ( student.Citizenship == Citizenship.NonIranian ) {
            var info = await birthCertService.NonIranian(student.YektaCode, cancellation);
            throw new ConfirmedValidationException(info);
        }
    }

    private async Task SyncIranian(StudentSummary student, CancellationToken cancellation) {
        var info = await birthCertService.Iranian(student.NationalCode, student.BirthDate.IntDateToString(), cancellation);
        var userId = await currentUser.GetUserIdAsync() ?? 0;
        var personnelId = await currentUser.PersonnelId() ?? 0;

        var sync = new SetStudentWithSabteAhvalDataRepoCommand(student.Codm, info.FirstName, info.LastName,
            info.FatherName, info.BirthCertNumber, info.Gender, info.BirthCertSeri, info.BirthCertSerial,info.IsSadat,
            info.IsDead, info.DeathDate.StringDateToInt(), userId, personnelId, ApplicationId: 66, 0, DataSource.WebService);
        await studentRepo.SetStudentWithSabteAhvalData(sync);
    }

    private async Task SyncNonIranian(StudentSummary student, CancellationToken cancellation) {
        var info = await birthCertService.NonIranian(student.YektaCode, cancellation);
        var userId = await currentUser.GetUserIdAsync() ?? 0;
        var personnelId = await currentUser.PersonnelId() ?? 0;

        var sync = new SetStudentWithAlmostafaDataRepoCommand(student.Codm, info.FirstName, info.LastName,
            info.FatherName, PassportNumber:"!!!", info.Nationality, ResidenceExpireDate: 0, info.Gender,
            info.IsDead, info.DeathDate.StringDateToInt(), userId, personnelId, ApplicationId: 66, 0, DataSource.WebService);
        await studentRepo.SetStudentWithAlmostafaData(sync);
    }
}
