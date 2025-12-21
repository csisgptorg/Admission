using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Persistence.Repositories.QueryBuilders;
internal sealed partial class QueryBuilderRepository : IQueryBuilderRepository
{
    public async Task<dynamic[]> ExecuteQuery(string query) {
        return await _dapper.ExecuteQuery(query);
    }
}
