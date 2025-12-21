using System.Text;
using Csis.Admission.Application.Common.Models.QueryBuilders;

namespace Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;
/// <summary>کوئری ساز</summary>
public interface IQueryBuilderRepository
{
    /// <summary>لفت جوین</summary>
    public StringBuilder StudentLeftJoinBuilder(ReportBuilderModel.Table[] tables);

    /// <summary>عبارت شرطی</summary>
    public void WhereClauseBuilder(QueryBuilderFilter condition,ref StringBuilder select);

    /// <summary>اجرای کوئری</summary>
    Task<dynamic[]> ExecuteQuery(string query);
}
