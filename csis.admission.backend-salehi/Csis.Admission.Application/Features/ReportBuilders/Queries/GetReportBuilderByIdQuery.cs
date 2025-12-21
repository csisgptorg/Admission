using Csis.Admission.Application.Features.ReportBuilders.Dtos;

namespace Csis.Admission.Application.Features.ReportBuilders.Queries;

/// <summary>دریافت گزارش ساز</summary>
public sealed record GetReportBuilderByIdQuery(long Id) : IRequest<ReportBuilderDto>;

internal sealed class GetReportBuilderByIdQueryHandler : IRequestHandler<GetReportBuilderByIdQuery, ReportBuilderDto>
{
    private readonly IRepository<ReportBuilder,long> _repo;
    public GetReportBuilderByIdQueryHandler(IRepository<ReportBuilder, long> repo) {
        _repo = repo;
    }

    public async Task<ReportBuilderDto> Handle(GetReportBuilderByIdQuery request, CancellationToken cancellationToken) {
        return await _repo.GetByIdAsync<ReportBuilderDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<ReportBuilder>(request.Id);
    }
}
