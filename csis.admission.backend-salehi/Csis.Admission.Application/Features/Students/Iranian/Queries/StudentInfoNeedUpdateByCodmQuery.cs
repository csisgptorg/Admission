using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>اطلاعات طلبه که نیاز به بروزرسانی دارند</summary>
public sealed record StudentInfoNeedUpdateByCodmQuery(int Codm) : IRequest<StudentInfoNeedUpdateDto>;

internal sealed class StudentInfoNeedUpdateByCodmQueryHandler : IRequestHandler<StudentInfoNeedUpdateByCodmQuery, StudentInfoNeedUpdateDto>
{
    private readonly IStudentRepository _repo;
    public StudentInfoNeedUpdateByCodmQueryHandler(IStudentRepository repo) {
        _repo = repo;
    }

    public async Task<StudentInfoNeedUpdateDto> Handle(StudentInfoNeedUpdateByCodmQuery request, CancellationToken cancellationToken) {
        return await _repo.GetStudentInfoNeedUpdateByCodm(request.Codm);
    }
}
