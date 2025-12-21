using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Features.Students.Dtos;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>
/// کارت الکترونیکی طلبه
/// </summary>
/// <param name="Codm"></param>
public sealed record GetStudentElectronicCardByCodmQuery(int Codm) : IRequest<StudentECardDto>;
internal sealed class GetStudentElectronicCardByCodmQueryHandler : IRequestHandler<GetStudentElectronicCardByCodmQuery, StudentECardDto>
{
    private readonly IStudentSummaryRepository _studentSummaryRepository;

    public GetStudentElectronicCardByCodmQueryHandler(IMapper mapper, IStudentSummaryRepository studentSummaryRepository) {
        _studentSummaryRepository = studentSummaryRepository;
    }
    public async Task<StudentECardDto> Handle(GetStudentElectronicCardByCodmQuery request, CancellationToken cancellationToken) {
        var result = await _studentSummaryRepository.GetStudentElectronicCardByCodm(request.Codm);
        return result;
    }
}
