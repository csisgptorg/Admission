using Csis.Admission.Application.Features.Students.Dtos;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>«ÿ·«⁄«  ‘‰«”‰«„Â  ò›· ÿ·»Â</summary>
public sealed record GetStudentDependentsByStudentCodmQuery(int Codm) : IRequest<StudentDependentDto[]>;

internal sealed class GetStudentDependentsByStudentCodmQueryHandler : IRequestHandler<GetStudentDependentsByStudentCodmQuery, StudentDependentDto[]>
{
    private readonly IMapper _mapper;
    private readonly IRepository<DependentSummary,long> _repo;

    private readonly Dictionary<DependentRelation, short> _customOrder = new() {
    { DependentRelation.Spouse, 1 },
    { DependentRelation.Child, 2 },
    { DependentRelation.Parent, 3 },
    { DependentRelation.Grandchild, 4 },
    { DependentRelation.AdoptedChild, 5 },};

    public GetStudentDependentsByStudentCodmQueryHandler(IMapper mapper, IRepository<DependentSummary, long> repo) {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<StudentDependentDto[]> Handle(GetStudentDependentsByStudentCodmQuery request, CancellationToken cancellationToken) {

        var dependents = await _repo.GetAllAsync<StudentDependentDto>(x=>x.Codm==request.Codm,cancellationToken:cancellationToken);
        return [.. dependents.OrderBy(dto => _customOrder.TryGetValue(dto.Relation, out var value) ? value : int.MaxValue).ThenBy(x=>x.Id)];
    }
}
