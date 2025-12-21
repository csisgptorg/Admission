using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Students.NonIranian.Commands;

/// <summary>
/// ایجاد فوت طلبه غیرایرانی
/// </summary>
public sealed record CreateStudentDeathRequestCommand : IRequest
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

internal sealed class CreateStudentDeathRequestCommandHandler(IStudentRepository studentRepository, IRequestService requestService) : IRequestHandler<CreateStudentDeathRequestCommand>
{
    public async Task Handle(CreateStudentDeathRequestCommand request, CancellationToken cancellationToken) {

        var student = await studentRepository.GetByCodm(request.Codm) ?? throw new KeyNotFoundException("طلبه یافت نشد.");

        var command = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.CreateStudentDeath);
        await requestService.Create(command, cancellationToken);
    }
}
