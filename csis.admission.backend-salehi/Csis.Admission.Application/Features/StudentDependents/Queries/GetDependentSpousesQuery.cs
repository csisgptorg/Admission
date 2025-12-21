using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.QueryBuilders;
using Csis.Admission.Application.Features.Marriages.Dtos;

namespace Csis.Admission.Application.Features.StudentDependents.Queries;

/// <summary>
/// نمایش لیست همسران هر مرد
/// </summary>
/// <param name="Codm"></param>
public sealed record GetDependentSpousesQuery(int Codm) : IRequest<List<DependentSpousesDto>>;

internal sealed class GetMaleSpousesQueryHandler(
    IStudentRepository studentRepository,
    IRepository<DependentSummary, long> studentDependentRepo)
    : IRequestHandler<GetDependentSpousesQuery, List<DependentSpousesDto>>
{
    public async Task<List<DependentSpousesDto>> Handle(GetDependentSpousesQuery request, CancellationToken cancellationToken) {
        var student = await studentRepository.GetStudentInfoByCodm(request.Codm)
            ?? throw new CommandValidationException("کد مرکز صحیح نیست");

        var dependentList = await studentDependentRepo.GetAllAsync(x => x.Codm == request.Codm && x.Relation == DependentRelation.Spouse && !x.DivorceDate.HasValue && x.IsMarried && !x.IsDead, cancellationToken: cancellationToken);

        return dependentList
            .OrderBy(x => x.Relation)
            .ThenBy(x => x.RelationOrder)
            .Select(DependentSpousesDto.FromEntity)
            .ToList();
    }
}
