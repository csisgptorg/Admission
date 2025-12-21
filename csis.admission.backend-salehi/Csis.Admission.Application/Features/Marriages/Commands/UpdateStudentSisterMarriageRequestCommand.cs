using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Marriages.Commands;

/// <summary>
/// ثبت ازدواج طلبه خواهر - درخواست
/// </summary>
public sealed record UpdateStudentSisterMarriageRequestCommand : IRequest
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string MarriageDate { get; init; }

    /// <inheritdoc/>
    public string SpouseNationalCode { get; init; }

    /// <inheritdoc/>
    public string SpouseBirthDate { get; init; }
}

internal sealed class UpdateStudentSisterMarriageRequestCommandHandler(IRequestService requestService, IStudentRepository studentRepository)
    : IRequestHandler<UpdateStudentSisterMarriageRequestCommand>
{
    public async Task Handle(UpdateStudentSisterMarriageRequestCommand command, CancellationToken cancellationToken) {

        var student = await studentRepository.GetStudentInfoByCodm(command.Codm)
                      ?? throw new CommandValidationException(" کد مرکز صحیح نیست ");


        if ( student.IsMarried ) {
            throw new CommandValidationException( " این طلبه قبلا ازدواج کرده است" );
        }


        if ( student.IsDead ) {
            throw new CommandValidationException(" طلبه مرحوم می باشد ");
        }

        if ( student.Gender == Gender.Male ) {
            throw new CommandValidationException(" امکان ثبت طلاق از این طریق، فقط برای طلاب خواهر امکان پذیر است");
        }

        var createRequest = new CreateRequestCommand(command, RequestFlow.DirectRegistration);
        await requestService.Create(createRequest, cancellationToken);
    }
}
