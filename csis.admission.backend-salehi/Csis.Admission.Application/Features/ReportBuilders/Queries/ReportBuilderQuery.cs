using Csis.Admission.Application.Common.Models.QueryBuilders;
using Csis.Admission.Application.Features.ReportBuilders.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Features.ReportBuilders.Queries;

/// <summary>گزارش ساز</summary>
public sealed record ReportBuilderQuery(ReportBuilderModel.Table[] Tables, QueryBuilderFilter Filter, int PageSize = 10, int PageIndex = 1)
    : IRequest<ReportBuilderQueryDto>;

internal sealed class ReportBuilderQueryHandler : IRequestHandler<ReportBuilderQuery, ReportBuilderQueryDto>
{
    private readonly IQueryBuilderRepository _repo;
    public ReportBuilderQueryHandler(IQueryBuilderRepository repo) {
        _repo = repo;
    }

    public async Task<ReportBuilderQueryDto> Handle(ReportBuilderQuery request, CancellationToken cancellationToken) {
        var pagedQuery = "";
        try {
            pagedQuery = BuildPagedQuery(request, out var countQuery);

            var data = await _repo.ExecuteQuery(pagedQuery);
            var countResult = await _repo.ExecuteQuery(countQuery);
            var metadata = new ReportBuilderQueryDto.MetadataDto(request.PageIndex, request.PageSize, countResult);
            return new ReportBuilderQueryDto(data, metadata, pagedQuery);
        } catch ( Exception exception ) {
            return ReportBuilderQueryDto.FailedResponse(pagedQuery, exception.ToString());
        }
    }

    string BuildPagedQuery(ReportBuilderQuery request, out string countQuery) {
        var sqlBuilder = _repo.StudentLeftJoinBuilder(request.Tables);

        _repo.WhereClauseBuilder(request.Filter, ref sqlBuilder);

        sqlBuilder.AppendLine("ORDER BY [stu].[Studentsummary].[Codm]");
        sqlBuilder.AppendLine($"OFFSET {(request.PageIndex - 1) * request.PageSize} ROWS");
        sqlBuilder.AppendLine($"FETCH NEXT {request.PageSize} ROWS ONLY");

        var pagedQuery = sqlBuilder.ToString().Replace("\r\n", " ");
        
        countQuery = BuildCountQuery(pagedQuery);

        return pagedQuery;
    }

    private string BuildCountQuery(string query) {
        var selectIndex = query.IndexOf("SELECT", StringComparison.OrdinalIgnoreCase);
        var fromIndex = query.IndexOf("FROM", StringComparison.OrdinalIgnoreCase);
        var orderByIndex = query.IndexOf("ORDER BY", StringComparison.OrdinalIgnoreCase);

        // حذف ORDER BY برای کوئری شمارش
        var baseQuery = query.Substring(0, orderByIndex).Trim();

        // تولید کوئری شمارش
        var countQuery = baseQuery.Substring(0, selectIndex + "SELECT".Length)
                         + " CountResult = COUNT(*) "
                         + baseQuery.Substring(fromIndex);

        return countQuery;
    }
}
