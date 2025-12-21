using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <inheritdoc/>
[QueryBuilder(Label = "استان", Name = "base.Provinces")]
public class Province : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "عنوان")]
    public string Title { get; set; }
}
