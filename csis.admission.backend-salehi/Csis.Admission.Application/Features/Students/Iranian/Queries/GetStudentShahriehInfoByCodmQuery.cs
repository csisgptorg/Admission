using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>دریافت اطلاعات شهریه طلبه</summary>
public sealed record GetStudentShahriehInfoByCodmQuery(int Codm) : IRequest<StudentShahriehInfoDto>;

internal sealed class GetStudentShahriehInfoByCodmQueryHandler : IRequestHandler<GetStudentShahriehInfoByCodmQuery, StudentShahriehInfoDto>
{
    private readonly IStudentRepository _studentRepo;
    public GetStudentShahriehInfoByCodmQueryHandler(IStudentRepository studentRepo) {
        _studentRepo = studentRepo;
    }

    public async Task<StudentShahriehInfoDto> Handle(GetStudentShahriehInfoByCodmQuery request, CancellationToken cancellationToken) {
        return await _studentRepo.GetShahriehInfo(request.Codm);
    }
}
