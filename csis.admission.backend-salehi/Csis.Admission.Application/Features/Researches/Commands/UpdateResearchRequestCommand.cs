using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;
using Csis.Utilities.Json;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.Researches.Commands;

/// <summary>
/// ویرایش پژوهش
/// </summary>
public sealed record UpdateResearchRequestCommand : BaseCommandDto<UpdateResearchRequestCommand, Research>, IRequest
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

internal sealed class UpdateResearchRequestCommandHandler(IRequestService requestService, ILogger<UpdateResearchRequestCommandHandler> logger) : IRequestHandler<UpdateResearchRequestCommand>
{
    public async Task Handle(UpdateResearchRequestCommand request, CancellationToken cancellationToken) {
        logger.LogInformation("Creating update research request for Research Id: {ResearchId}", request.Id);
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.UpdateResearch);
        var result = await requestService.Create(requestCommand, cancellationToken);
        logger.LogInformation("Update research request created with Request Id: {RequestId} for Research Id: {ResearchId}", result, request.Id);
    }
}
