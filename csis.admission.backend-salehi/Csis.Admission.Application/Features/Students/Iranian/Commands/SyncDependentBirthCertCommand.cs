using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>بروز رسانی اطلاعات شناسنامه ای براساس ثبت احوال یا المصطفی</summary>
public sealed record SyncDependentBirthCertCommand : IRequest
{
    /// <summary>شناسه عضو خانواده</summary>
    public long Id { get; init; }

    /// <summary>تایید</summary>
    [JsonIgnore]
    public bool? Confirmed { get; set; }
}

internal sealed class SyncDependentIdentityCommandHandler(IRepository<DependentSummary, long> dependentSummaryRpo, IStudentRepository studentRepo, 
    ICurrentUserService currentUser,IBirthCertService birthCertService)
    : IRequestHandler<SyncDependentBirthCertCommand>
{
    public async Task Handle(SyncDependentBirthCertCommand command, CancellationToken cancellation)
    {
        var dependent = await dependentSummaryRpo.GetOneAsync(x => x.Id == command.Id, false, cancellation);
        await NotConfirmed(command.Confirmed, dependent, cancellation);

        if ( dependent.Citizenship == Citizenship.Iranian ) {
            await SyncIranian(dependent, cancellation);

        } else if ( dependent.Citizenship == Citizenship.NonIranian ) {
            await SyncNonIranian(dependent, cancellation);
        }
    }

    private async Task NotConfirmed(bool? confirmed, DependentSummary dependent, CancellationToken cancellation) {
        if ( confirmed == true ) { return; }

        if ( dependent.Citizenship == Citizenship.Iranian ) {
            var info = await birthCertService.Iranian(dependent.NationalCode, dependent.BirthDate.IntDateToString(), cancellation);
            throw new ConfirmedValidationException(info);

        } else if ( dependent.Citizenship == Citizenship.NonIranian ) {
            var info = await birthCertService.NonIranian(dependent.YektaCode, cancellation);
            throw new ConfirmedValidationException(info);
        }
    }

    private async Task SyncIranian(DependentSummary dependent, CancellationToken cancellation) {
        var info = await birthCertService.Iranian(dependent.NationalCode, dependent.BirthDate.IntDateToString(), cancellation);
        var userId = await currentUser.GetUserIdAsync() ?? 0;
        var personnelId = await currentUser.PersonnelId() ?? 0;

        var sync = new SetDependentWithSabteAhvalDataRepoCommand(dependent.Codm ,dependent.Id, info.FirstName, info.LastName,
            info.FatherName, info.BirthCertNumber, info.Gender, info.BirthCertSeri, info.BirthCertSerial,info.IsSadat, 
            info.IsDead, info.DeathDate.StringDateToInt(), userId, personnelId, ApplicationId: 66, 0, DataSource.WebService);
        await studentRepo.SetDependentWithSabteAhvalData(sync);
    }

    private async Task SyncNonIranian(DependentSummary dependent, CancellationToken cancellation) {
        var info = await birthCertService.NonIranian(dependent.YektaCode, cancellation);
        var userId = await currentUser.GetUserIdAsync() ?? 0;
        var personnelId = await currentUser.PersonnelId() ?? 0;

        var sync = new SetDependentWithAlmostafaDataRepoCommand(dependent.Codm, dependent.Id, info.FirstName, info.LastName,
            info.FatherName, PassportNumber: "!!!", info.Nationality, ResidenceExpireDate: 0, info.Gender,
            info.IsDead, info.DeathDate.StringDateToInt(), userId, personnelId, ApplicationId: 66, 0, DataSource.WebService);
        await studentRepo.SetDependentWithAlmostafaData(sync);
    }
}
