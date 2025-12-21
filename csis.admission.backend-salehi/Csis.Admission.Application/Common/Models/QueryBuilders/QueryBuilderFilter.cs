using System.Text;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <summary>فیلتر کوئری ساز</summary>
public partial class QueryBuilderFilter
{
    /// <summary>ایجاد شرط</summary>
    public string WhereClause(StringBuilder select, string logicalOperator = null) {
        var whereClause = "";

        if ( !string.IsNullOrWhiteSpace(Table) ) {
            var table = QueryBuilderTables.GetAll().Single(x => x.ReportTable == Table);
            var column = table.Columns.Single(x => x.ReportColumn == Column);

            NormalizeConjunctionsAndOperator();
            NormalizeValue(column);

            whereClause += $"{Utilities.FormatSqlObjectName(table.Name)}.{Utilities.FormatSqlObjectName(column.Name)} {Operator} {Value}";
            LeftJoinOnCodm(table.Name, ref select);
        }

        if ( Filters?.Any()==true ) {
            var subClauses = Filters.Select(x => x.WhereClause(select, Conjunction)).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            if ( subClauses.Any() ) {
                var joinedSubClauses = string.Join($" {Conjunction} ", subClauses);
                var prefix =!string.IsNullOrWhiteSpace(whereClause) ? (logicalOperator ?? Conjunction) : "";
                whereClause += $"{prefix}({joinedSubClauses})";
            }
        }

        return whereClause;
    }

    /// <summary>جدول</summary>
    public string Table { get; set; }
    /// <summary>ستون</summary>
    public string Column { get; set; }
    /// <summary>اپراتور</summary>
    public string Operator { get; set; }
    /// <summary>مقدار</summary>
    public object Value { get; set; }


    /// <summary>اپراتور گروه شروط</summary>
    public string Conjunction { get; set; }
    /// <summary>گروه شروط</summary>
    public QueryBuilderFilter[] Filters { get; set; }
}
