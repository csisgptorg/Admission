using Csis.Admission.Application.Features.ReportBuilders.Dtos;

namespace Csis.Admission.Application.Features.ReportBuilders.Queries;

/// <summary>جستجو گزارش ساز</summary>
public sealed record GetReportBuildersQuery : IRequest<List<ReportBuilderTitleDto>>;

internal sealed class GetReportBuildersQueryHandler : IRequestHandler<GetReportBuildersQuery, List<ReportBuilderTitleDto>>
{
    private readonly IRepository<ReportBuilder, long> _repo;
    public GetReportBuildersQueryHandler(IRepository<ReportBuilder, long> repo) {
        _repo = repo;
    }

    public async Task<List<ReportBuilderTitleDto>> Handle(GetReportBuildersQuery query, CancellationToken cancellationToken) {
        var result = (await _repo.GetAllAsync<ReportBuilderTitleDto>(cancellationToken: cancellationToken)).OrderByDescending(x=>x.Id).ToList();
        return result;
    }
}
