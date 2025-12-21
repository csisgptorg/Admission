using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Features.Divorce.Commands;

/// <summary>
/// ارتباط داده ای - طلاق تکفل
/// </summary>
public sealed record class UpdateDependentDivorceDataImportCommand : IRequest
{
    /// <summary>
    /// کد مرکز خدمات
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// شناسه تکفل
    /// </summary>
    public long DependentId { get; init; }

    /// <summary>
    /// تاریخ طلاق
    /// </summary>
    public int DivorceDate { get; init; }
}

internal sealed class DependentDivorceDataImportCommandHandler : IRequestHandler<UpdateDependentDivorceDataImportCommand>
{
    private readonly IStudentRepository _studentSpRepository;
    private readonly IStudentDependentRepository _dependentSpRepository;
    private readonly IRepository<StudentSummary, int> _studentRepository;
    private readonly IRepository<DependentSummary, long> _dependentRepository;


    public DependentDivorceDataImportCommandHandler(
        IStudentRepository studentSpRepository,
        IStudentDependentRepository dependentSpRepository,
        IRepository<StudentSummary, int> studentRepository,
        IRepository<DependentSummary, long> dependentRepository) {

        _studentSpRepository = studentSpRepository;
        _dependentSpRepository = dependentSpRepository;
        _studentRepository = studentRepository;
        _dependentRepository = dependentRepository;
    }

    public async Task Handle(UpdateDependentDivorceDataImportCommand request, CancellationToken cancellationToken) {


        var dependent = await _dependentRepository.GetOneAsync(x =>
        x.Id == request.DependentId &&
        x.Codm == request.Codm,
        cancellationToken: cancellationToken)
           ?? throw new CommandValidationException("شناسه تکفل نامعتبر می باشد.");

        var dependentDivorce = new SetDependentDivorceModel {
            Codm = request.Codm,
            DependentId = request.DependentId,
            DivorceDate = request.DivorceDate
        };

        if ( dependent.Relation == DependentRelation.Spouse ) {
            await _dependentSpRepository.UpdateDependentSpouseDivorceAsync(dependentDivorce);

        } else if ( dependent.Relation == DependentRelation.Child
            || dependent.Relation == DependentRelation.AdoptedChild
            || dependent.Relation == DependentRelation.Grandchild ) {

            await _dependentSpRepository.UpdateDependentChildDivorceAsync(dependentDivorce);

        }


    }
}
