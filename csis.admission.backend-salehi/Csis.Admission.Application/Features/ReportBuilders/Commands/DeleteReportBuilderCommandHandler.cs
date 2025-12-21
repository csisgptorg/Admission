namespace Csis.Admission.Application.Features.ReportBuilders.Commands;
/// <summary>حذف کزارش گزارش ساز</summary>
public sealed record DeleteReportBuilderCommand(long Id) : IRequest;

internal sealed class DeleteReportBuilderCommandHandler : IRequestHandler<DeleteReportBuilderCommand>
{
    private readonly IRepository<ReportBuilder,long> _repo;
    public DeleteReportBuilderCommandHandler(IRepository<ReportBuilder, long> repo) {
        _repo = repo;
    }

    public async Task Handle(DeleteReportBuilderCommand request, CancellationToken cancellationToken) {
        if ( !await _repo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new RecordNotFoundException<ReportBuilder>(request.Id);
        }
    }
}

