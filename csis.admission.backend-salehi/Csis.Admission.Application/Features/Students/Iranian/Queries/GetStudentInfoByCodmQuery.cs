using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>
/// GetStudentInfoByCodmQuery
/// </summary>
/// <param name="Codm"></param>
public sealed record GetStudentInfoByCodmQuery(int Codm) : IRequest<StudentInfoDto>;

internal sealed class GetStudentInfoByCodmQueryHandler : IRequestHandler<GetStudentInfoByCodmQuery, StudentInfoDto>
{
    private readonly IStudentRepository _studentRepo;
    public GetStudentInfoByCodmQueryHandler(IStudentRepository studentRepo) {
        _studentRepo = studentRepo;
    }

    public async Task<StudentInfoDto> Handle(GetStudentInfoByCodmQuery request, CancellationToken cancellationToken) {

        return await _studentRepo.GetStudentInfoByCodm(request.Codm)
            ?? throw new RecordNotFoundException<StudentInfoDto>(request.Codm);
    }
}
