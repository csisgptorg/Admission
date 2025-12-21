using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Features.Students.Validators;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>بروز رسانی اطلاعات شناسنامه ای</summary>
public sealed record UpdateDependentBirthCertCommand : IRequest
{
    /// <summary>کد مرکز خدمات</summary>
    public long Id { get; init; }

    /// <summary>کد ملی</summary>
    public string NationalCode { get; init; }

    /// <summary>تاریخ تولد</summary>
    public string BirthDate { get; init; }

    /// <summary>مذهب</summary>
    public Religion Religion { get; init; }

    /// <summary>توضیحات شناسنامه ای</summary>
    public string Description { get; init; }
}

internal sealed class UpdateDependentBirthCertCommandHandler(IStudentRepository studentRepo,ICurrentUserService currentUserService,
    IRepository<StudentSummary> studentSummaryRpo, IRepository<DependentSummary, long> dependentSummaryRpo,BirthCertValidator birthCertValidator)
    : IRequestHandler<UpdateDependentBirthCertCommand>
{
    public async Task Handle(UpdateDependentBirthCertCommand command, CancellationToken cancellation) {

        var dependents = await dependentSummaryRpo.GetAllAsync(x => x.Id == command.Id || x.NationalCode==command.NationalCode, false, cancellation);
        var student = await studentSummaryRpo.ExistsAsync(x => x.NationalCode == command.NationalCode, false, cancellation);

        // is senior
        var dependent = dependents.First(x => x.Id == command.Id);
        var isSenior = await currentUserService.IsSenior();
        if ( !isSenior && (command.NationalCode != dependent.NationalCode || command.BirthDate.StringDateToInt() != dependent.BirthDate) ) {
            throw new CommandValidationException("شما مجوز لازم برای تغییر کد ملی و تاریخ تولد را ندارید.");
        }

        var birthCertInfo=await birthCertValidator.DependentIdentityIranian(command.Id, command.NationalCode, command.BirthDate, cancellation);

        // update
        var update = new UpdateDependentBirthCertInfoRepoCommand {
            Codm = dependent.Codm,
            Id = command.Id,
            NationalCode = command.NationalCode,
            YektaCode = null,
            BirthDate = command.BirthDate.StringDateToInt().Value,
            Religion = command.Religion,
            IsSadat = birthCertInfo.IsSadat,
            BirthCertDescription = command.Description,
            ApplicationId = 66,
            DataSource = DataSource.Employee,
            PersonnelId =await currentUserService.PersonnelId() ?? 0,
            UserId = await currentUserService.GetUserIdAsync() ?? 0
        };
        await studentRepo.UpdateDependentBirthCertInfo(update);
    }
}
