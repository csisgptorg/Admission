using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>تعداد رکوردهای طلبه در بخش های مختلف</summary>
public sealed record StudentRecordCountByCodmQuery(int Codm) : IRequest<StudentRecordCountDto>;

internal sealed class StudentRecordCountByCodmQueryHandler : IRequestHandler<StudentRecordCountByCodmQuery, StudentRecordCountDto>
{
    private readonly IStudentRepository _repo;
    public StudentRecordCountByCodmQueryHandler(IStudentRepository repo) {
        _repo = repo;
    }

    public async Task<StudentRecordCountDto> Handle(StudentRecordCountByCodmQuery request, CancellationToken cancellationToken) {

        return await _repo.GetStudentRecordCount(request.Codm)
            ?? throw new RecordNotFoundException<StudentRecordCountDto>(request.Codm);
    }
}
