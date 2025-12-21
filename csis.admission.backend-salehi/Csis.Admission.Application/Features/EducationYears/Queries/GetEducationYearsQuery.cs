using Csis.Admission.Application.Features.EducationYears.Dtos;

namespace Csis.Admission.Application.Features.EducationYears.Queries;

/// <summary>دریافت لیست سال های تحصیلی</summary>
public sealed record GetEducationYearsQuery : IRequest<EducationYearDto[]>;

internal sealed class GetEducationLevelsQueryHandler(IRepository<EducationYear, short> repo)
    : IRequestHandler<GetEducationYearsQuery, EducationYearDto[]>
{
    public async Task<EducationYearDto[]> Handle(GetEducationYearsQuery request, CancellationToken cancellationToken) {
        var result = await repo.GetAllAsync<EducationYearDto>(cancellationToken:cancellationToken);
        return [.. result];
    }
}


