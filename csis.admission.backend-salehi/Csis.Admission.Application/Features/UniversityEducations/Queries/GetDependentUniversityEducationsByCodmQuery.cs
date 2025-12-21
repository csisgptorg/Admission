using Csis.Admission.Application.Features.UniversityEducations.Dtos;

namespace Csis.Admission.Application.Features.UniversityEducations.Queries;

/// <inheritdoc/>
public sealed record GetDependentUniversityEducationsByCodmQuery(int Codm,long? dependentId=null) : IRequest<List<DependentUniversityEducationDto>>;

internal sealed class GetDependentUniversityEducationsByCodmQueryHandler(IRepository<UniversityEducation> repo) :
    IRequestHandler<GetDependentUniversityEducationsByCodmQuery, List<DependentUniversityEducationDto>>
{
    public async Task<List<DependentUniversityEducationDto>> Handle(GetDependentUniversityEducationsByCodmQuery query, CancellationToken cancellation) {
        var result = await repo.GetAllAsync<DependentUniversityEducationDto>
            (x=> x.Codm == query.Codm && x.DependentId!=null && (query.dependentId==null || x.DependentId==query.dependentId) &&
            (x.StudyLevel == StudyLevel.GraduateDiploma || x.StudyLevel == StudyLevel.BachelorDegree
            || x.StudyLevel == StudyLevel.MasterDegree || x.StudyLevel == StudyLevel.DoctoralDegree), false,cancellation);
        return [.. result.OrderBy(x => x.DependentId).ThenByDescending(x=>x.Id)];
    }
}
