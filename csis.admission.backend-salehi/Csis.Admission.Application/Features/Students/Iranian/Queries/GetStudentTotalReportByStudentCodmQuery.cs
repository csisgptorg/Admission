using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Features.Students.Dtos;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>
/// تراز , هدفمندی , معیشت طلبه
/// </summary>
/// <param name="Codm"></param>
public sealed record GetStudentTotalReportByStudentCodmQuery(int Codm) : IRequest<StudentTotalReportDto>;
internal sealed class GetStudentTotalReportByStudentCodmQueryHandler : IRequestHandler<GetStudentTotalReportByStudentCodmQuery, StudentTotalReportDto>
{
    private readonly IStudentRepository _repository;

    public GetStudentTotalReportByStudentCodmQueryHandler(IMapper mapper, IStudentRepository repository) {
        _repository = repository;
    }

    public async Task<StudentTotalReportDto> Handle(GetStudentTotalReportByStudentCodmQuery request, CancellationToken cancellationToken) {

        var result = await _repository.GetStudentTarazAndLivelihoodTotalScore(request.Codm);
        return result;
    }
}
