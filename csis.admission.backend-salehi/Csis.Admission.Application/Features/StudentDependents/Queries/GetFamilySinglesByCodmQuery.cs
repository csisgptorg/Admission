using Csis.Admission.Application.Features.StudentDependents.Dtos;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.Marriages.Queries;

/// <summary>
/// لیست سرپرست و اعضای خانواده
/// </summary>
/// <param name="Codm"></param>
/// <param name="IncludeMarried"></param>
public sealed record GetFamilySinglesByCodmQuery : IRequest<FamilyInfoDto[]>
{
    /// <summary> </summary>
    public int Codm { get; init; }
}

internal sealed class GetFamilySinglesByCodmQueryHandler(IRepository<DependentSummary, long> dependentRepository)
    : IRequestHandler<GetFamilySinglesByCodmQuery, FamilyInfoDto[]>
{
    public async Task<FamilyInfoDto[]> Handle(GetFamilySinglesByCodmQuery request, CancellationToken cancellationToken) {

        var dependentList = await dependentRepository
            .GetAllAsync(x => x.Codm == request.Codm && x.Relation.Value == DependentRelation.Child, cancellationToken: cancellationToken);

        return dependentList
            .OrderBy(x => x.Relation)
            .ThenBy(x => x.RelationOrder)
            .Select(FamilyInfoDto.FromEntity)
            .ToArray();
    }
}
