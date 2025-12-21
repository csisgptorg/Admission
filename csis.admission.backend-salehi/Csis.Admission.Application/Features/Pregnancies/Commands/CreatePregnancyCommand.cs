using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Pregnancies.Commands;

/// <inheritdoc/>
public sealed record class CreatePregnancyCommand : BaseCommandDto<CreatePregnancyCommand, Pregnancy>, IRequest
{
    /// <inheritdoc/>
    public int Codm { get; init; }

    /// <inheritdoc/>
    public string StartDate { get; init; }

    /// <inheritdoc/>
    public string EndDate { get; init; }

    /// <inheritdoc/>
    public long? RequestId { get; init; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<CreatePregnancyCommand, Pregnancy> mapping) {
        mapping.ForMember(model => model.StartDate, config => config.MapFrom(dto => dto.StartDate.StringDateToInt()));
        mapping.ForMember(model => model.EndDate, config => config.MapFrom(dto => dto.EndDate.StringDateToInt()));
    }
}


internal sealed class CreatePregnancyCommandHandler(
    IRepository<Pregnancy> repo,
    IRepository<UploadedFile> uploadRepo,
    IRepository<RequestDocument, long> documentRepo)
    : IRequestHandler<CreatePregnancyCommand>
{

    public async Task Handle(CreatePregnancyCommand command, CancellationToken cancellationToken) {
        var pregnancy = await repo.GetOneAsTrackingAsync(x => x.Codm == command.Codm, cancellationToken: cancellationToken);
        pregnancy = command.ToEntity();
        await repo.UpdateAsync(pregnancy, cancellationToken: cancellationToken);
    }
}
