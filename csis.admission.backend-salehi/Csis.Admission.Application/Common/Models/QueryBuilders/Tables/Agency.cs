using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <inheritdoc/>
[QueryBuilder(Label = "نمایندگی", Name = "base.Agencies")]
public class Agency : IQueryBuilderTable, IQueryBuilderRelationTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "عنوان")]
    public string Title { get; set; }
}
