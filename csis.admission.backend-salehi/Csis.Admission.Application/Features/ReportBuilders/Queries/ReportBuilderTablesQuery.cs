using Csis.Admission.Application.Common.Models.QueryBuilders;

namespace Csis.Admission.Application.Features.ReportBuilders.Queries;

/// <summary>جداول گزارش ساز</summary>
public sealed record ReportBuilderTablesQuery() : IRequest<List<ReportBuilderModel.Table>>;

internal sealed class ReportBuilderTablesQueryHandler : IRequestHandler<ReportBuilderTablesQuery, List<ReportBuilderModel.Table>>
{
    /// <summary>جداول گزارش ساز</summary>
    public async Task<List<ReportBuilderModel.Table>> Handle(ReportBuilderTablesQuery query, CancellationToken cancellationToken) {
        var tables = ReportBuilderTables.GetAll().Where(x=>x.Tab != Enums.ReportBuilderTab.NotSet.ToString()).ToArray();

        var orderTableNames = new List<string>
            {
                "Student",
                "Dependent",
                "Address",
                "CulturalActivity",
                "DependentMemorizer",
                "DependentUniversityEducation",
                "Elite",
                "Excellent",
                "House",
                "Preach",
                "Pregnancy",
                "Protest",
                "Province",
                "Research",
                "StudentEmployment",
                "StudentMemorizer",
                "StudentUniversityEducation",
                "TargetedScoreHistory",
                "Teach",
                "Veteran"
            };

        var sortedTables = tables.OrderBy(item =>orderTableNames.Contains(item.Name) ? orderTableNames.IndexOf(item.Name) : int.MaxValue).ToList();

        await Task.CompletedTask;
        return sortedTables;
    }
}
