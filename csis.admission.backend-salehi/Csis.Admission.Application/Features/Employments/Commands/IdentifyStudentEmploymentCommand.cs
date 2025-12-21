using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>شناسایی موردی اشتغال</summary>
public record IdentifyStudentEmploymentCommand : BaseCommandDto<IdentifyStudentEmploymentCommand, EmployeeIdentification>, IRequest<int>
{
    /// <summary>کد مرکز</summary>
    public int Codm { get; init; }
    /// <summary>محل اشتغال</summary>
    public string EmployeeName { get; init; }
    /// <summary>توضیحات</summary>
    public string Description { get; init; }
}

internal sealed class IdentifyStudentEmploymentCommandHandler(IRepository<EmployeeIdentification> employeeIdentificationRepository, ICurrentUserService currentUserService)
    : IRequestHandler<IdentifyStudentEmploymentCommand, int>
{
    public async Task<int> Handle(IdentifyStudentEmploymentCommand command, CancellationToken cancellationToken) {
        var identification = command.ToEntity();
        identification.PersonnelId = (await currentUserService.PersonnelId()).Value;
        await employeeIdentificationRepository.InsertAsync(identification, cancellationToken: cancellationToken);
        return identification.Id;
    }
}
