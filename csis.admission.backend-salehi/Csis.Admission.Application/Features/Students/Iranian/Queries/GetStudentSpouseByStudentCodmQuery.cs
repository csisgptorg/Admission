using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Features.Students.Dtos;

namespace Csis.Admission.Application.Features.Students.Queries;
/// <summary>
/// اطلاعات همسر طلبه
/// </summary>
/// <param name="Codm"></param>
public sealed record GetStudentSpouseByStudentCodmQuery(int Codm) : IRequest<StudentSpouseDto[]>;

internal sealed class GetStudentSpouseByStudentCodmQueryHandler : IRequestHandler<GetStudentSpouseByStudentCodmQuery, StudentSpouseDto[]>
{
    private readonly IMapper _mapper;
    private readonly IStudentRepository _repository;

    public GetStudentSpouseByStudentCodmQueryHandler(IMapper mapper, IStudentRepository repository) {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<StudentSpouseDto[]> Handle(GetStudentSpouseByStudentCodmQuery request, CancellationToken cancellationToken) {

        var result = await _repository.GetSpousesByStudentCodm(request.Codm);
        return result;
    }
}


