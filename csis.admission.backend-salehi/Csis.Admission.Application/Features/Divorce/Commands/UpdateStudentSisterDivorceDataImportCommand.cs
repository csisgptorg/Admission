using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Features.Divorce.Commands;

/// <summary>
/// ارتباط داده ای - ثبت طلاق سرپرست طلاب خواهر
/// </summary>
public sealed record class UpdateStudentSisterDivorceDataImportCommand : IRequest
{
    /// <summary>
    /// کد مرکز خدمات
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// تاریخ طلاق
    /// </summary>
    public int DivorceDate { get; init; }
}

internal sealed class StudentDivorceDataImportCommandHandler : IRequestHandler<UpdateStudentSisterDivorceDataImportCommand>
{
    private readonly IStudentRepository _studentRepository;

    public StudentDivorceDataImportCommandHandler(
        IStudentRepository studentRepository) {
        _studentRepository = studentRepository;
    }

    public async Task Handle(UpdateStudentSisterDivorceDataImportCommand request, CancellationToken cancellationToken) {
        var student = await _studentRepository.GetStudentInfoByCodm(request.Codm)
            ?? throw new CommandValidationException("کد مرکز خدمات نامعتبر می باشد.");

        if ( student.IsMarried == false ) {
            throw new CommandValidationException(" طلبه مجرد می باشد ");
        }

        if (  student.IsDead ) {
            throw new CommandValidationException(" طلبه مرحوم می باشد ");
        }


        if ( student.Gender == Gender.Female ) {
            await _studentRepository.UpdateStudentSisterDivorceAsync(new SetStudentSisterDivorceModel {
                Codm = request.Codm,
                DivorceDate = request.DivorceDate,
            });
        } else {
            throw new CommandValidationException("امکان ثبت تاریخ طلاق فقط برای طلاب خواهر امکان پذیر است.");
        }
    }
}
