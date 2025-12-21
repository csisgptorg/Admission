using Csis.Admission.Application.Features.Protests.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Protests.Queries;

/// <summary>دریافت لیست اعتراضات طلبه</summary>
public sealed record GetProtestsByCodmQuery(int Codm) : IRequest<ProtestDto[]>;

internal sealed class GetProtestsByCodmQueryHandler(IStudentRepository repo, IRequestService requestService)
    : IRequestHandler<GetProtestsByCodmQuery, ProtestDto[]>
{
    public async Task<ProtestDto[]> Handle(GetProtestsByCodmQuery query, CancellationToken cancellationToken) {

        var requests=(await requestService.GetAllByCodmAsync(query.Codm, isCompleted: false,cancellationToken))
            .Where(x=>x.Type.ToString().Contains(nameof(Protest)));
        var protests = await repo.GetProtests(query.Codm);

        foreach (var protest in protests)
        {
            var requestType = Protest.GetRequestType(protest.FieldId);
            protest.RequestId= requests.FirstOrDefault(x => x.Type == requestType)?.Id;
        }

        return protests;
    }
}
