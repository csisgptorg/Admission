using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>
/// Get student phone by codm
/// </summary>
/// <param name="Codm"></param>
public sealed record GetStudentPhoneByCodmQuery(int Codm) : IRequest<StudentPhoneDto>;

internal sealed class GetStudentPhoneByCodmQueryHandler : IRequestHandler<GetStudentPhoneByCodmQuery, StudentPhoneDto>
{
    private readonly IStudentRepository _studentRepo;
    public GetStudentPhoneByCodmQueryHandler(IStudentRepository studentRepo) {
        _studentRepo = studentRepo;
    }

    public async Task<StudentPhoneDto> Handle(GetStudentPhoneByCodmQuery request, CancellationToken cancellationToken) {

        return await _studentRepo.GetPhoneByCodm(request.Codm)
            ?? throw new CommandValidationException("شماره تلفن طلبه یافت نشد.");
    }
}
