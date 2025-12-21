using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>
/// «ÿ·«⁄«  ‘‰«”‰«„Â  ò›· ÿ·»Â
/// </summary>
/// <param name="Codm"></param>
public sealed record GetStudentInfoDependentsByStudentCodmQuery(int Codm) : IRequest<StudentInfoDependentDto[]>;

internal sealed class GetStudentInfoDependentsByStudentCodmQueryHandler : IRequestHandler<GetStudentInfoDependentsByStudentCodmQuery, StudentInfoDependentDto[]>
{
    private readonly IMapper _mapper;
    private readonly IRepository<DependentSummary, long> _studentRepo;
    public GetStudentInfoDependentsByStudentCodmQueryHandler(IMapper mapper,IRepository<DependentSummary, long> studentRepo) {
        _mapper = mapper;
        _studentRepo = studentRepo;
    }

    public async Task<StudentInfoDependentDto[]> Handle(GetStudentInfoDependentsByStudentCodmQuery request, CancellationToken cancellationToken) {

        var studentDependents = await _studentRepo.GetAllAsync(x => x.Codm == request.Codm);
        var result = studentDependents.Select(_mapper.Map<StudentInfoDependentDto>).ToArray();
        return result;
    }
}
