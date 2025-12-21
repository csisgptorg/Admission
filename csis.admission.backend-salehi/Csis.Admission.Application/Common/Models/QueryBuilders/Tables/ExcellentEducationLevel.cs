using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <summary>مقطع ممتازین</summary>
[QueryBuilder(Label = "مقطع", Name = "base.ExcellentEducationLevels")]
public class ExcellentEducationLevel : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "عنوان")]
    public string Title { get; set; }
}
