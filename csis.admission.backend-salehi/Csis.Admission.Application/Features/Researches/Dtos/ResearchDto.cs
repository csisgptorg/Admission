using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Researches.Dtos;

/// <summary>
/// پژوهش
/// </summary>
public sealed record ResearchDto : BaseDto<ResearchDto, Research>
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// نوع پژوهش
    /// </summary>
    public ResearchType? Type { get; set; }

    /// <summary>
    /// آیدی عنوان پژوهشی
    /// </summary>
    public short? SubjectId { get; set; }

    /// <summary>
    /// عنوان پژوهشی
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Year
    /// </summary>
    public short? Year { get; set; }

    /// <summary>
    /// زبان
    /// </summary>
    public ResearchLanguage Language { get; set; }

    /// <summary>
    /// BookPublisher
    /// </summary>
    public string BookPublisher { get; set; }

    /// <summary>
    /// BookShabak
    /// </summary>
    public string BookShabak { get; set; }

    /// <summary>
    /// ArticlePublication
    /// </summary>
    public string ArticlePublication { get; set; }

    /// <summary>
    /// ProjectEmployer
    /// </summary>
    public string ProjectEmployer { get; set; }

    /// <summary>
    /// CustomMappings
    /// </summary>
    /// <param name="mapping"></param>
    public override void CustomMappings(IMappingExpression<Research, ResearchDto> mapping) {
        mapping.ForMember(dto => dto.Subject, config => config.MapFrom(model => model.Subject.Title));
    }
}
