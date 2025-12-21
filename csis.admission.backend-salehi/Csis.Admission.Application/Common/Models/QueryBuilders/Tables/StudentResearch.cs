using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

/// <inheritdoc/>
[QueryBuilder(Label = "پژوهش", Name = "TbResearch",Tab =Enums.ReportBuilderTab.Student)]
public class StudentResearch : IQueryBuilderTable
{
    [QueryBuilder(Label = "نوع پژوهش", Name = "ResearchType")]
    public ResearchType? Type { get; set; }

    [QueryBuilder(Label = "عنوان پژوهش")]
    public string Title { get; set; }

    [QueryBuilder(Label = "سال انتشار")]
    public short? Year { get; set; }

    [QueryBuilder(Label = "زبان پژوهش")]
    public ResearchLanguage? Language { get; set; }
}
