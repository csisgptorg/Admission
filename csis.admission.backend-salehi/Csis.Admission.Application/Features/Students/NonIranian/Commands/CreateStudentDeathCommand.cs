using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Students.NonIranian.Commands;

/// <summary>
/// ایجاد فوت طلبه غیرایرانی
/// </summary>
public sealed record CreateStudentDeathCommand : IRequest<long>
{
    /// <summary>
    /// کد مرکز طلبه
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// تاریخ فوت
    /// </summary>
    public string DeathDate { get; init; }
}

internal sealed class CreateStudentDeathCommandHandler(IStudentRepository studentRepository, ICurrentUserService currentUserService) : IRequestHandler<CreateStudentDeathCommand, long>
{
    public async Task<long> Handle(CreateStudentDeathCommand request, CancellationToken cancellationToken) {
        var command = new SetNonIranianStudentDeathPrc {
            Codm = request.Codm,
            DeathDate = request.DeathDate.StringDateToInt().Value,
            ApplicationId = 66,
            DataSource = DataSource.Employee,
            PersonnelId = (await currentUserService.PersonnelId()) ?? 0,
            UserId = int.TryParse((await currentUserService.GetUserIdAsync())?.ToString(), out var userId) ? userId : 0
        };
        var result = await studentRepository.CreateStudentDeath(command);
        return result.Id;
    }
}
