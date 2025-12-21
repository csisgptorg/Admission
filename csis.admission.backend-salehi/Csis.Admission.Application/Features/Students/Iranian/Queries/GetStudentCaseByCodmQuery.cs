using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>
/// Get student case by codm
/// </summary>
/// <param name="Codm"></param>
public sealed record GetStudentCaseByCodmQuery(int Codm) : IRequest<StudentCaseDto>;

internal sealed class GetStudentCaseByCodmQueryHandler(IStudentRepository studentRepo)
    : IRequestHandler<GetStudentCaseByCodmQuery, StudentCaseDto>
{
    public async Task<StudentCaseDto> Handle(GetStudentCaseByCodmQuery request, CancellationToken cancellationToken) {

        return await studentRepo.GetCaseByCodm(request.Codm)
            ?? throw new RecordNotFoundException<StudentCaseDto>(request.Codm);
    }
}
