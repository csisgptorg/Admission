using Csis.Admission.Application.Features.HousingAdmissionInfo.Dtos;
using System.Linq;

namespace Csis.Admission.Application.Features.HousingAdmissionInfo.Commands;

/// <summary>دریافت اطلاعات کدمرکز طلاب بر اساس لیست کد ملی</summary>
/// <param name="NationalCodes"></param>
public sealed record GetBulkCodmByNationalCodeCommand(List<string> NationalCodes) : IRequest<List<GetStudentCodmByNationalCodeDto>>;
internal sealed class GetBulkCodmByNationalCodeCommandHandler(IRepository<StudentSummary> studentSummaryRepository, IRepository<DependentSummary, long> dependentSummaryRepository) : IRequestHandler<GetBulkCodmByNationalCodeCommand, List<GetStudentCodmByNationalCodeDto>>
{
    public async Task<List<GetStudentCodmByNationalCodeDto>> Handle(GetBulkCodmByNationalCodeCommand request, CancellationToken cancellationToken) {
        var dependents = await dependentSummaryRepository.GetAllAsync(x => request.NationalCodes.Contains(x.NationalCode) && x.Relation == DependentRelation.Spouse && x.IsActive, cancellationToken: cancellationToken);
        var students = await studentSummaryRepository.GetAllAsync(x => request.NationalCodes.Contains(x.NationalCode) || dependents.Select(x => x.Codm).Contains(x.Codm), cancellationToken: cancellationToken);

        var result = new List<GetStudentCodmByNationalCodeDto>();
        result = [.. students.Select(s => {
            var dependent = dependents.FirstOrDefault(d => d.Codm == s.Codm);
            return new GetStudentCodmByNationalCodeDto {
                NationalCode = s.NationalCode,
                Codm = s.Codm,
                Dependent = dependent != null ? new GetStudentDependentByNationalCodeDto {
                    NationalCode = dependent.NationalCode,
                    DependentId = dependent.Id
                } : null
            };
        })];

        return result;
    }
}
