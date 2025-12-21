using Csis.Admission.Application.Features.Employments.Dtos;

namespace Csis.Admission.Application.Features.Employments.Queries;

/// <summary>شناسایی موردی اشتغال</summary>
public record GetIdentifyStudentEmploymentQuery(int Codm) : IRequest<List<EmployeeIdentificationDto>>;

internal sealed class IdentifyStudentEmploymentQueryHandler(IRepository<EmployeeIdentification> employeeIdentificationRepository)
    : IRequestHandler<GetIdentifyStudentEmploymentQuery, List<EmployeeIdentificationDto>>
{
    public async Task<List<EmployeeIdentificationDto>> Handle(GetIdentifyStudentEmploymentQuery request, CancellationToken cancellationToken) {
        var result = await employeeIdentificationRepository.GetAllAsync<EmployeeIdentificationDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        return result;
    }
}
