using Csis.Admission.Application.Features.ExcellentEducationYears.Dtos;

namespace Csis.Admission.Application.Features.ExcellentEducationYears.Queries;

/// <summary>دریافت لیست سال تحصیلی ممتازین</summary>
public sealed record GetExcellentEducationYearsQuery : IRequest<ExcellentEducationYearDto[]>;

internal sealed class GetExcellentEducationLevelsQueryHandler : IRequestHandler<GetExcellentEducationYearsQuery, ExcellentEducationYearDto[]>
{
    private readonly IRepository<ExcellentEducationYear, short> _repo;
    public GetExcellentEducationLevelsQueryHandler(IRepository<ExcellentEducationYear, short> repo) {
        _repo = repo;
    }

    public async Task<ExcellentEducationYearDto[]> Handle(GetExcellentEducationYearsQuery request, CancellationToken cancellationToken) {
        var result = await _repo.GetAllAsync<ExcellentEducationYearDto>(cancellationToken:cancellationToken);
        return [.. result];
    }
}


