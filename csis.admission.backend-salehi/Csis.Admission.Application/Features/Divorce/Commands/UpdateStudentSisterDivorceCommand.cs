using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Features.Divorce.Commands;

/// <inheritdoc/>
public sealed record UpdateStudentSisterDivorceCommand : IRequest
{
    /// <summary>
    /// کد مرکز طلبه ی خواهر
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// تاریخ طلاق طلبه خواهر
    /// </summary>
    public string DivorceDate { get; init; }


    /// <summary>
    /// کد ملی همسر طلبه ی خواهر برای استعلام از ثبت احوال
    /// </summary>
    public string SpouseNationalCode { get; init; }

    /// <summary>
    /// تاریخ تولد همسر طلبه ی خواهر برای استعلام از ثبت احوال
    /// </summary>
    public string SpouseBirthDate { get; init; }
}

internal sealed class UpdateStudentSisterDivorceCommandHandler : IRequestHandler<UpdateStudentSisterDivorceCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICsisWsmService _csisWsmService;

    public UpdateStudentSisterDivorceCommandHandler(
        IStudentRepository studentRepository,
        ICsisWsmService csisWsmService,
        IStudentDependentRepository studentDependentRepository,
        IRepository<DependentSummary, long> studentDependentRepo,
        IRepository<Student> studentRepo) {
        _studentRepository = studentRepository;
        _csisWsmService = csisWsmService;
    }

    public async Task Handle(UpdateStudentSisterDivorceCommand request, CancellationToken cancellationToken) {

        await _studentRepository.UpdateStudentSisterDivorceAsync(new SetStudentSisterDivorceModel {
            Codm = request.Codm,
            DivorceDate = request.DivorceDate.Replace("/", "").ToInt()
        });
    }

}
