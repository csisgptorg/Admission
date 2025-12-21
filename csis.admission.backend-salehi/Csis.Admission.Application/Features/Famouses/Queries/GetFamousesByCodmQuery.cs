using Csis.Admission.Application.Features.Famouses.Dtos;

namespace Csis.Admission.Application.Features.Famouses.Queries;

/// <summary>دریافت مشاهیر ثبت شده برای طلبه</summary>
public sealed record GetStudentFamousesByCodmQuery(int Codm) : IRequest<StudentFamousDto[]>;

internal sealed class GetFamousesByCodmQueryHandler : IRequestHandler<GetStudentFamousesByCodmQuery, StudentFamousDto[]>
{
    private readonly IRepository<Famous> _repo;
    public GetFamousesByCodmQueryHandler(IRepository<Famous> repo) {
        _repo = repo;
    }

    public async Task<StudentFamousDto[]> Handle(GetStudentFamousesByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<StudentFamousDto>(x=>x.Codm==request.Codm,cancellationToken:cancellationToken);
        return result.ToArray();
    }
}
