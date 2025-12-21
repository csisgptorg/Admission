using Csis.Admission.Application.Features.Family.Dtos;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>
/// اطلاعات افراد و تکفل براساس شماره ملی
/// </summary>
/// <param name="NationalCode"></param>
public sealed record GetFamilyByNationalCodeQuery(string NationalCode) : IRequest<List<HealthInsuranceFamilyDto>>;

internal sealed class GetFamilyByNationalCodeQueryHandler : IRequestHandler<GetFamilyByNationalCodeQuery, List<HealthInsuranceFamilyDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<StudentSummary, int> _studentRepo;
    private readonly IRepository<DependentSummary, long> _dependentRepo;

    public GetFamilyByNationalCodeQueryHandler(IMapper mapper
                                                , IRepository<StudentSummary, int> studentRepo
                                                , IRepository<DependentSummary, long> dependentRepo

                                                ) {
        _mapper = mapper;
        _studentRepo = studentRepo;
        _dependentRepo = dependentRepo;
    }

    public async Task<List<HealthInsuranceFamilyDto>> Handle(GetFamilyByNationalCodeQuery request, CancellationToken cancellationToken) {

        var student = await _studentRepo.GetAllAsync(i => i.NationalCode == request.NationalCode);

        var studentDto = student.Select(x => new HealthInsuranceFamilyDto {
            Codm = x.Codm,
            DependentId = null,
            Relation = null,
            Gender = x.Gender,
            NationalCode = x.NationalCode,
            YektaCode = x.YektaCode,
            FirstName = x.FirstName,
            LastName = x.LastName,
            FatherName = x.FatherName,
            IsActive = !x.IsBlock // مسدود و غیر مسدود

        }).ToList();

        var dependent = await _dependentRepo.GetAllAsync(i => i.NationalCode == request.NationalCode);

        var dependentDto = dependent
            .OrderBy(x => x.Relation)
            .ThenBy(x => x.RelationOrder)
            .Select(x => new HealthInsuranceFamilyDto {
                Codm = x.Codm,
                DependentId = x.Id,
                Relation = x.Relation,
                Gender = x.Gender,
                NationalCode = x.NationalCode,
                YektaCode = x.YektaCode,
                FirstName = x.FirstName,
                LastName = x.LastName,
                FatherName = x.FatherName,
                IsActive = x.IsActive

            }).ToList();


        return [.. studentDto, .. dependentDto];
    }
}
