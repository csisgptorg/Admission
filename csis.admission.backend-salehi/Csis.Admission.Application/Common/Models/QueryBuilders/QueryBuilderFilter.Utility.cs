using System.Text;
using System.Text.RegularExpressions;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <summary>فیلتر کوئری ساز</summary>
public partial class QueryBuilderFilter
{
    void LeftJoinOnCodm(string tableName, ref StringBuilder select) {
        var formattedTableName = Utilities.FormatSqlObjectName(tableName);
        if ( !formattedTableName.EqualIgnoreCase("[stu].[Studentsummary]") && !select.ToString().Contains($"LEFT JOIN {formattedTableName}") ) {
            select.AppendLine($"LEFT JOIN {formattedTableName} ON {formattedTableName}.[Codm]=[stu].[Studentsummary].[Codm]");
        }
    }

    void NormalizeConjunctionsAndOperator() {

        if ( Operators.TryGetValue(Operator, out var @operator) ) {
            Operator = @operator;
        } else {
            throw new CommandValidationException($"Operator '{Operator}' is not valid.");
        }

        if ( Operator.Contains("LIKE") ) {
            Value = Operator.Replace("LIKE", Value.ToString());
            Operator = Operator.Replace("%","");
        }

        if ( !string.IsNullOrWhiteSpace(Conjunction) && Conjunctions.TryGetValue(Conjunction, out var conjunction) ) {
            Conjunction = conjunction;
        } else if ( Filters?.Any() == true ) {
            throw new CommandValidationException($"Conjunction '{Conjunction}' is not valid.");
        }
    }

    void NormalizeValue(QueryBuilderModel.Column column) {
        if ( column.Type.EqualIgnoreCase(nameof(String))) {
            Value = $"'{Value}'";

        } else if ( column.Type.EqualIgnoreCase(nameof(DateTime)) ) {
            Value = $"{Value.ToString().Replace("/", "")}";
            Value = $"{Value}";

        } else if ( column.Type.EqualIgnoreCase(nameof(Boolean)) ) {
            Value = Value.ToString().EqualIgnoreCase("true") ? 1 : Value.ToString().EqualIgnoreCase("false") ? 0 : null;
        }

        if ( Operator == "IS NULL" || Operator == "IS NOT NULL" ) {
            Value = null;
        } else {
            CleanString(Value.ToString());
        }
    }

    static string CleanString(string input) {
        if ( string.IsNullOrWhiteSpace(input) ) {
            return input;
        }

        var result = input.Trim();
        result = result.Replace("\u200C", "");
        result = Regex.Replace(result, @"\s+", "");  // حذف همه فواصل سفید
        result = result.Replace("\u00A0", "");
        return result;
    }

    /// <summary>اپراتورهای شرطی اس کیو ال</summary>
    static readonly Dictionary<string, string> Operators = new(){
        { "=", "=" },
        { "!=", "<>" },
        { ">", ">" },
        { "<", "<" },
        { ">=", ">=" },
        { "<=", "<=" },
        { "%like%", "%LIKE%" },
        { "%like", "%LIKE" },
        { "like%", "LIKE%" },
        { "in", "IN" },
        { "not in", "NOT IN" },
        { "is null", "IS NULL" },
        { "is not null", "IS NOT NULL" }
    };

    /// <summary>رابط شروط</summary>
    static readonly Dictionary<string, string> Conjunctions = new(){
        { "and", "AND" },
        { "or", "OR" }
    };
}
