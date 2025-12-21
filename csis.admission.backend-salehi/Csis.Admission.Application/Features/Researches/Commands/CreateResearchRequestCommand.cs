using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Researches.Commands;

/// <summary>
/// ایجاد پژوهش
/// </summary>
public sealed record CreateResearchRequestCommand : BaseCommandDto<CreateResearchRequestCommand, Research>, IRequest<long>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// نوع پژوهش
    /// </summary>
    public ResearchType? Type { get; init; }

    /// <summary>
    /// آیدی عنوان پژوهشی
    /// </summary>
    public short? SubjectId { get; init; }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// Year
    /// </summary>
    public short? Year { get; init; }

    /// <summary>
    /// زبان
    /// </summary>
    public ResearchLanguage? Language { get; init; }

    /// <summary>
    /// BookPublisher
    /// </summary>
    public string BookPublisher { get; init; }

    /// <summary>
    /// BookShabak
    /// </summary>
    public string BookShabak { get; init; }

    /// <summary>
    /// ArticlePublication
    /// </summary>
    public string ArticlePublication { get; init; }

    /// <summary>
    /// ProjectEmployer
    /// </summary>
    public string ProjectEmployer { get; init; }

}

internal sealed class CreateResearchRequestCommandHandler(IRequestService requestService)
    : IRequestHandler<CreateResearchRequestCommand, long>
{
    public async Task<long> Handle(CreateResearchRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.CreateResearch);
        var result = await requestService.Create(requestCommand, cancellationToken);
        return result;
    }
}
