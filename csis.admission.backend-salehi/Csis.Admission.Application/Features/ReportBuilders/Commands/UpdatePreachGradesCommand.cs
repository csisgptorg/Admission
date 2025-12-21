using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ReportBuilders.Commands;

/// <summary>بروز رسانی</summary>
public sealed record UpdateReportBuilderCommand : BaseCommandDto<UpdateReportBuilderCommand, ReportBuilder, long>, IRequest
{
    /// <summary>شناسه</summary>
    public long Id { get; init; }

    /// <summary>عنوان</summary>
    public string Title { get; init; }

    /// <summary>جداول</summary>
    public object Tables { get; init; }

    /// <summary>فیلتر</summary>
    public object Filter { get; init; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<UpdateReportBuilderCommand, ReportBuilder> mapping) {
        mapping.ForMember(model => model.Tables, config => config.MapFrom(dto => dto.Tables.ToJson(null)));
        mapping.ForMember(model => model.Filter, config => config.MapFrom(dto => dto.Filter.ToJson(null)));
    }
}

internal sealed class UpdateReportBuilderCommandHandler : IRequestHandler<UpdateReportBuilderCommand>
{
    private readonly IRepository<ReportBuilder, long> _repo;
    public UpdateReportBuilderCommandHandler(IRepository<ReportBuilder, long> repo) {
        _repo = repo;
    }

    public async Task Handle(UpdateReportBuilderCommand request, CancellationToken cancellationToken) {

        if ( await _repo.ExistsAsync(x => x.Title == request.Title && x.Id != request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException("عنوان تکراری است.");
        }

        var reportBuilder = await _repo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<ReportBuilder>(request.Id);
        
        request.ToEntity(reportBuilder);
        await _repo.UpdateAsync(reportBuilder, true, cancellationToken);
    }
}
