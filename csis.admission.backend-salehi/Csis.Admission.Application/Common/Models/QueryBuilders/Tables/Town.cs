using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <summary>شهر</summary>
[QueryBuilder(Label = "شهر", Name = "base.Towns")]
public class Town : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "عنوان")]
    public string Title { get; set; }
}
