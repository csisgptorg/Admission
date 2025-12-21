using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Researches.Commands;

/// <summary>
/// ایجاد پژوهش
/// </summary>
public sealed record CreateResearchCommand : BaseCommandDto<CreateResearchCommand, Research>, IRequest<int>
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
internal sealed class CreateResearchCommandHandler(IRepository<Research> researchRepo)
    : IRequestHandler<CreateResearchCommand, int>
{
    public async Task<int> Handle(CreateResearchCommand request, CancellationToken cancellationToken) {
        var research = request.ToEntity();
        await researchRepo.InsertAsync(research, cancellationToken: cancellationToken);
        return research.Id;
    }
}
