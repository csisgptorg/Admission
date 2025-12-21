using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <summary>نوع نخبگی</summary>
[QueryBuilder(Label = "نوع نخبگی", Name = "base.EliteTypes")]
public class EliteType : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "عنوان")]
    public string Title { get; set; }
}
