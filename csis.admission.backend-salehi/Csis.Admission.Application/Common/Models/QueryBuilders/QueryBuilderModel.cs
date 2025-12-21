using System.Reflection;
using Csis.Admission.Domain.Common;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <summary>کوئری ساز</summary>
public class QueryBuilderModel
{
    /// <summary>جدول</summary>
    public class Table
    {
        /// <inheritdoc/>
        public Table(Type tableType) {
            Name = tableType.GetCustomAttribute<QueryBuilderAttribute>().Name ?? tableType.Name;
            ReportTable = tableType.Name;
            Columns = tableType.GetProperties().Select(x => new Column(x)).ToArray();
        }

        /// <summary>ساخت کوئری برای ستون ها با جوین</summary>
        public string[] GenerateSelectColumnsQueryWithJoin(ReportBuilderModel.Table reportTable, ref List<string> joinsQuery) {
            var selectColumns = new List<string>();
            var columns = Columns.Where(x => reportTable.Columns.Any(y => y.Name == x.ReportColumn) && (reportTable.Name.EqualIgnoreCase("Student") || !x.Name.EqualIgnoreCase("Codm"))).ToArray();
            foreach ( var column in columns ) {

                if ( column.RelationTable is not null ) {
                    var relationTable = QueryBuilderTables.GetAll().Single(x => x.ReportTable == column.RelationTable);
                    var relationTableAlias = $"{reportTable.Name}{relationTable.Name.Replace("base.", "")}";
                    joinsQuery.Add(GenerateJoinQuery(relationTable.Name, relationTableAlias, nameof(BaseEntity.Id), Name, column.Name));

                    // column
                    var relationColumn = relationTable.Columns.Single(x => x.ReportColumn == "Title");
                    var aliasRelationColumn = $"[{ReportTable}.{column.RelationTable}]";
                    selectColumns.Add(GenerateSelectColumnQuery(relationTable,relationTableAlias, relationColumn, aliasRelationColumn));
                } else {
                    selectColumns.Add(GenerateSelectColumnQuery(this,tableAlias:null, column));
                }
            }

            return [.. selectColumns];
        }

        /// <summary>ساخت نام کوئری برای ستون ها</summary>
        static string GenerateSelectColumnQuery(Table table, string tableAlias, Column column, string alias = null) {
            alias ??= $"[{table.ReportTable}.{column.ReportColumn}]";

            var formattedtableColumn = Utilities.FormatSqlObjectName($"{tableAlias ?? table.Name}.{column.Name}");
            var selectColumn = column.Type == nameof(DateTime) ? $"FORMAT({formattedtableColumn},'0000/00/00')" : $"{formattedtableColumn}";
            var selectQuery = $"{alias}={selectColumn}";
            return selectQuery;
        }

        /// <summary>نام</summary>
        public string Name { get; }
        /// <summary>جدول گزارش</summary>
        public string ReportTable { get; }
        /// <summary>ستون ها</summary>
        public Column[] Columns { get; }
    }

    /// <summary>ستون</summary>
    public class Column
    {
        /// <inheritdoc/>
        public Column(PropertyInfo property) {
            var attribute = property.GetCustomAttribute<QueryBuilderAttribute>();
            Name = attribute.Name ?? property.Name;
            ReportColumn = property.Name;
            Type = attribute.Type?.Name ?? property.GetUnderlyingType().Name;
            RelationTable = attribute.RelationTable;
        }

        /// <summary>نام</summary>
        public string Name { get; }
        /// <summary>ستون گزارش</summary>
        public string ReportColumn { get; }
        /// <summary>تایپ</summary>
        public string Type { get; }
        /// <summary>لیبل</summary>
        public string RelationTable { get; }
    }

    /// <summary>ساخت کوئری جوین</summary>
    static string GenerateJoinQuery(string leftTable, string leftTableAlias, string leftTableColumn, string sourceTable, string sourceTableColumn) {
        var formattedLeftTable = Utilities.FormatSqlObjectName(leftTable);
        var formattedLeftTableAlias = Utilities.FormatSqlObjectName(leftTableAlias);
        var formattedLeftColumn = Utilities.FormatSqlObjectName(leftTableColumn);
        var formattedSourceTable = Utilities.FormatSqlObjectName(sourceTable);
        var formattedSourceColumn = Utilities.FormatSqlObjectName(sourceTableColumn);

        return $"LEFT JOIN {formattedLeftTable} {formattedLeftTableAlias} ON {formattedLeftTableAlias ?? formattedLeftTable}.{formattedLeftColumn}={formattedSourceTable}.{formattedSourceColumn}";
    }
}

/// <summary>کوئری ساز ساز</summary>
public static class QueryBuilderTables
{
    /// <summary>جداول</summary>
    private static QueryBuilderModel.Table[] ـtables { get; set; }

    /// <summary>لیست جداول</summary>
    public static QueryBuilderModel.Table[] GetAll() {
        if ( ـtables == null ) {
            ـtables = Assembly.GetAssembly(typeof(IQueryBuilderRepository)).GetTypes()
                .Where(x => x.IsClass && typeof(IQueryBuilderTable).IsAssignableFrom(x))
                .Select(x => new QueryBuilderModel.Table(x)).ToArray();
        }
        return ـtables;
    }
}
