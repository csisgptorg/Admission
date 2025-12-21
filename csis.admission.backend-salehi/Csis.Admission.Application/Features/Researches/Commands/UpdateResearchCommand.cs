using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;
using Csis.Utilities.Json;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.Researches.Commands;

/// <summary>
/// ویرایش پژوهش
/// </summary>
public sealed record UpdateResearchCommand : BaseCommandDto<UpdateResearchCommand, Research>, IRequest
{
    /// <summary>
    /// شناسه پژوهش
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// ArticlePublication
    /// </summary>
    [JsonConverter(typeof(TrimAndToPersianConverter))]
    public string ArticlePublication { get; init; }

    /// <summary>
    /// BookPublisher
    /// </summary>
    [JsonConverter(typeof(TrimAndToPersianConverter))]
    public string BookPublisher { get; init; }

    /// <summary>
    /// BookShabak
    /// </summary>
    [JsonConverter(typeof(TrimAndToPersianConverter))]
    public string BookShabak { get; init; }

    /// <summary>
    /// ProjectEmployer
    /// </summary>
    [JsonConverter(typeof(TrimAndToPersianConverter))]
    public string ProjectEmployer { get; init; }

    /// <summary>
    /// Title
    /// </summary>
    [JsonConverter(typeof(TrimAndToPersianConverter))]
    public string Title { get; init; }

    /// <summary>
    /// آیدی عنوان پژوهشی
    /// </summary>
    public short? SubjectId { get; init; }

    /// <summary>
    /// Year
    /// </summary>
    public short? Year { get; init; }

    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// زبان
    /// </summary>
    public ResearchLanguage? Language { get; init; }

    /// <summary>
    /// نوع پژوهش
    /// </summary>
    public ResearchType? Type { get; init; }
}

internal sealed class UpdateResearchCommandHandler(IRepository<Research> researchRepo, ILogger<UpdateResearchCommandHandler> logger) : IRequestHandler<UpdateResearchCommand>
{
    public async Task Handle(UpdateResearchCommand request, CancellationToken cancellationToken) {
        var research = await researchRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException($"پژوهش با شناسه {request.Id} یافت نشد.");

        logger.LogDebug("Research with id {id} before update: {@before}", request.Id, research);

        research = request.ToEntity(research);

        logger.LogDebug("Research with id {id} after update: {@after}", request.Id, research);

        await researchRepo.UpdateAsync(research, cancellationToken: cancellationToken);
    }
}
