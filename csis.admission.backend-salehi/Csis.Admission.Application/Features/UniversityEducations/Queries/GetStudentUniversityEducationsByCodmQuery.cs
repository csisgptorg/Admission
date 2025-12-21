using Csis.Admission.Application.Features.UniversityEducations.Dtos;

namespace Csis.Admission.Application.Features.UniversityEducations.Queries;

/// <inheritdoc/>
public sealed record GetStudentUniversityEducationsByCodmQuery(int Codm) : IRequest<List<StudentUniversityEducationDto>>;

internal sealed class GetStudentUniversityEducationsByCodmQueryHandler : IRequestHandler<GetStudentUniversityEducationsByCodmQuery, List<StudentUniversityEducationDto>>
{
    private readonly IRepository<UniversityEducation> _repo;
    public GetStudentUniversityEducationsByCodmQueryHandler(IRepository<UniversityEducation> repo) {
        _repo = repo;
    }

    public async Task<List<StudentUniversityEducationDto>> Handle(GetStudentUniversityEducationsByCodmQuery request, CancellationToken cancellation) {
        var result = await _repo.GetAllAsync<StudentUniversityEducationDto>
            (x=> x.Codm == request.Codm && x.DependentId==null && (x.StudyLevel == StudyLevel.GraduateDiploma || x.StudyLevel == StudyLevel.BachelorDegree
            || x.StudyLevel == StudyLevel.MasterDegree || x.StudyLevel == StudyLevel.DoctoralDegree), false,cancellation);
        return [.. result.OrderByDescending(x=>x.Id)];
    }
}
