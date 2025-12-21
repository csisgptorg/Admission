using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.ReportBuilders.Commands;
/// <summary>ثبت کزارش گزارش ساز</summary>
public sealed record CreateReportBuilderCommand : BaseCommandDto<CreateReportBuilderCommand, ReportBuilder,long>, IRequest<long>
{
    /// <summary>عنوان</summary>
    public string Title { get; init; }

    /// <summary>جداول</summary>
    public object Tables { get; init; }

    /// <summary>فیلتر</summary>
    public object Filter { get; init; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<CreateReportBuilderCommand, ReportBuilder> mapping) {
        mapping.ForMember(model => model.Tables, config => config.MapFrom(dto => dto.Tables.ToJson(null)));
        mapping.ForMember(model => model.Filter, config => config.MapFrom(dto => dto.Filter.ToJson(null)));
    }
}

internal sealed class CreateReportBuildersCommandCommandHandler : IRequestHandler<CreateReportBuilderCommand, long>
{
    private readonly IRepository<ReportBuilder, long> _repo;
    public CreateReportBuildersCommandCommandHandler(IRepository<ReportBuilder, long> repo) {
        _repo = repo;
    }

    public async Task<long> Handle(CreateReportBuilderCommand request, CancellationToken cancellationToken) {

        if(await _repo.ExistsAsync(x=>x.Title == request.Title,cancellationToken:cancellationToken) ) {
            throw new CommandValidationException($"عنوان تکراری است.");
        }

        var reportBuilder = request.ToEntity();
        await _repo.InsertAsync(reportBuilder, cancellationToken: cancellationToken);
        return reportBuilder.Id;
    }
}

