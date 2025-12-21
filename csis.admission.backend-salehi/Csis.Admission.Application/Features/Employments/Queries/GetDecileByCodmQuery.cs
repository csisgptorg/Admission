namespace Csis.Admission.Application.Features.Employments.Queries;

/// <summary>
/// دریافت دهک بر اساس لیست کدمرکز
/// </summary>
/// <param name="CodmList"></param>
public sealed record GetDecileByCodmQuery(List<int> CodmList) : IRequest<Dictionary<int, short?>>;
internal sealed class GetDecileByCodmQueryHandler(IRepository<StudentEmployment> employmentRepo) : IRequestHandler<GetDecileByCodmQuery, Dictionary<int, short?>>
{
    public async Task<Dictionary<int, short?>> Handle(GetDecileByCodmQuery query, CancellationToken cancellationToken) {
        var result = await employmentRepo.GetAllAsync(x => query.CodmList.Contains(x.Codm), cancellationToken: cancellationToken);
        return result.ToDictionary(x => x.Codm, x => x.Decile);
    }
}
