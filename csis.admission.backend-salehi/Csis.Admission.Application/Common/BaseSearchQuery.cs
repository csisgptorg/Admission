/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common;

/// <summary>
/// Base search query
/// </summary>
public record BaseSearchQuery
{
    private static readonly List<int> _allowedPageSizes = [5, 10, 15, 20, 50];
    private int? _pageSize;
    private int _pageIndex = 1;
    private string _sortExpression = DefaultOrder;
    private List<SearchFilter> _searchFilters;

    /// <summary>
    /// Default order
    /// </summary>
    public const string DefaultOrder = "-id";

    /// <summary>
    /// Default property mappings
    /// </summary>
    public static Dictionary<string, string> DefaultPropertyMappings => new()
    {
        { "Date", "CreatedOn" }
    };

    /// <summary>
    /// Ctor
    /// </summary>
    public BaseSearchQuery() { }

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    public BaseSearchQuery(int pageIndex, int pageSize) {
        PageSize = pageSize;
        PageIndex = pageIndex;
    }

    #region Properties
    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize {
        get => _pageSize ?? DefaultPageSize;
        set => _pageSize = _allowedPageSizes.Contains(value) ? value : DefaultPageSize;
    }

    /// <summary>
    /// Page index
    /// </summary>
    public int PageIndex {
        get => _pageIndex;
        set {
            if ( value <= 0 ) {
                _pageIndex = 1;
            } else {
                _pageIndex = value;
            }
        }
    }

    /// <summary>
    /// Dynamic search filters
    /// </summary>
    public List<SearchFilter> SearchFilters {
        get => _searchFilters ?? [];
        set => _searchFilters = value;
    }

    /// <summary>
    /// Default page size
    /// </summary>
    public static int DefaultPageSize => _allowedPageSizes[0];

    /// <summary>
    /// Sort expression
    /// </summary>
    public string SortBy {
        get => GetSortExpression() ?? DefaultOrder;
        set => _sortExpression = !string.IsNullOrWhiteSpace(value) ? value : DefaultOrder;
    }
    #endregion

    #region Methods
    private string GetSortExpression() {
        return string.Join(',', _sortExpression.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
    }
    #endregion
}
