using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Features.Students.Dtos;

namespace Csis.Admission.Application.Features.Marriages.Commands;

/// <summary>
/// ارتباط داده ای - ثبت ازدواج سرپرست
/// </summary>
public sealed record class MarriageDataImportCommand : IRequest
{
    /// <summary>
    /// کد مرکز خدمات
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// تاریخ ازدواج
    /// </summary>
    public int MarriageDate { get; init; }

    /// <summary>
    /// شماره ملی همسر
    /// </summary>
    public string SpouseNationalCode { get; init; }

    /// <summary>
    /// تاریخ تولد همسر
    /// </summary>
    public int SpouseBirthDate { get; init; }

}

internal sealed class MarriageDataImportCommandHandler : IRequestHandler<MarriageDataImportCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IStudentDependentRepository _studentDependentRepository;

    public MarriageDataImportCommandHandler(IStudentRepository studentRepository, IStudentDependentRepository studentDependentRepository) {
        _studentRepository = studentRepository;
        _studentDependentRepository = studentDependentRepository;
    }

    public async Task Handle(MarriageDataImportCommand request, CancellationToken cancellationToken) {
        var student = await _studentRepository.GetByCodm(request.Codm)
            ?? throw new CommandValidationException("کد مرکز خدمات نامعتبر می باشد.");

        await SetMarriageAsync(student, request);
    }

    /// <summary>
    /// ثبت ازدواج
    /// </summary>
    private async Task SetMarriageAsync(StudentDto student, MarriageDataImportCommand command) {
        if ( student.Gender == Gender.Female ) {
            var request = new SisterStudentMarriagePrcRequest {
                Codm = student.Codm,
                MarriageDate = command.MarriageDate
            };

            await _studentDependentRepository.CreateSisterStudentMarriageAsync(request);
        } else
        if ( student.Gender == Gender.Male ) {
            throw new CommandValidationException("امکان ثبت ازدواج فقط برای طلاب خواهر امکان پذیر است.");
        }
    }
}
