using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <summary>دهستان</summary>
[QueryBuilder(Label = "دهستان", Name = "base.Rurals")]
public class Rural : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "عنوان")]
    public string Title { get; set; }
}
