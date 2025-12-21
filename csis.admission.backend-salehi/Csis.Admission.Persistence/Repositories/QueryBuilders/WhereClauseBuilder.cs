using System.Text;
using Csis.Admission.Application.Common.Models.QueryBuilders;
using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Persistence.Repositories.QueryBuilders;
internal sealed partial class QueryBuilderRepository : IQueryBuilderRepository
{
    public void WhereClauseBuilder(QueryBuilderFilter filter,ref StringBuilder select) {
        var whereClause = filter?.WhereClause(select);

        if ( !string.IsNullOrWhiteSpace(whereClause) ) {
            select.AppendLine($"WHERE {whereClause}");
        }
    }
}
