using System.Text;
using Csis.Admission.Application.Common.Models.QueryBuilders;
using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Persistence.Repositories.QueryBuilders;
internal sealed partial class QueryBuilderRepository : IQueryBuilderRepository
{
    private readonly AppDapperContext _dapper;
    public QueryBuilderRepository(AppDapperContext dapper) {
        _dapper = dapper;
    }

    /// <summary>لفت جوین بیلدر</summary>
    public StringBuilder StudentLeftJoinBuilder(ReportBuilderModel.Table[] reportTables) {
        var joins = new List<string>();

        var selectColumnsQuery = new List<string>() { "Id=[stu].[Studentsummary].[Codm]" };

        foreach ( var reportTable in reportTables ) {
            var table = QueryBuilderTables.GetAll().Single(x => x.ReportTable == reportTable.Name);
            var formattedTableName = Application.Common.Utilities.FormatSqlObjectName(table.Name);
            joins.Add($"LEFT JOIN {formattedTableName} ON {formattedTableName}.[Codm]=[stu].[Studentsummary].[Codm]");
            selectColumnsQuery.AddRange(table.GenerateSelectColumnsQueryWithJoin(reportTable, ref joins));
        }

        var select = new StringBuilder();
        select.AppendLine("SELECT DISTINCT " + string.Join(", ", selectColumnsQuery));
        select.AppendLine($"FROM [stu].[Studentsummary]");

        foreach ( var join in joins.Where(x => !x.Contains("LEFT JOIN [stu].[StudentSummary]")).Distinct().ToArray() ) {
            select.AppendLine(join);
        }

        return select;
    }
}
