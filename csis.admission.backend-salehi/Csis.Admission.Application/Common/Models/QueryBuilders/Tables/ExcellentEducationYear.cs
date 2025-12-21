using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <summary>سال تحصیلی ممتازین</summary>
[QueryBuilder(Label = "سال تحصیلی ممتازین", Name = "base.ExcellentEducationYears")]
public class ExcellentEducationYear : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "عنوان")]
    public string Title { get; set; }
}
