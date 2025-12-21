using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[QueryBuilder(Label = "مدرسه", Name = "base.Schools")]
public class School : IQueryBuilderTable
{
    [QueryBuilder(Label = "عنوان")]
    public string Title { get; set; }
}
