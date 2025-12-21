using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;

// ReSharper disable All

namespace Csis.Admission.Application.Features.StudentDependents.Commands;

// Converted from positional record to unprimary constructor record
/// <summary>
/// ثبت همسر
/// </summary>
public record StudentSpouseRegistryCommand : IRequest<long>
{
    /// <summary>
    /// کد مرکز خدمات
    /// </summary>
    public int Codm { get; init; }
    /// <summary>
    /// اطلاعات درخواست ثبت همسر
    /// </summary>
    public StudentDependentRegistryPrcRequest StudentDependentRegistryPrcRequest { get; init; }
}

internal sealed class StudentSpouseRegistryCommandHandler(
    IStudentDependentRepository dependentRepository)
    : IRequestHandler<StudentSpouseRegistryCommand, long>
{
    public async Task<long> Handle(StudentSpouseRegistryCommand command, CancellationToken cancellationToken) {
        
        var result = await dependentRepository.Create(command.StudentDependentRegistryPrcRequest);
        return result.Id;
    }
}
