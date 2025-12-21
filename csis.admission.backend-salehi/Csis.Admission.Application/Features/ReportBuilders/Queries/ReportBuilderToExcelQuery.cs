using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Models.QueryBuilders;
using Csis.Admission.Application.Features.ReportBuilders.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Features.ReportBuilders.Queries;

/// <summary>گزارش ساز</summary>
public sealed record ReportBuilderToExcelQuery(ReportBuilderModel.Table[] Tables, QueryBuilderFilter Filter)
    : IRequest<ReportBuilderToExcelQueryDto>;

internal sealed class ReportBuilderToExcelQueryHandler : IRequestHandler<ReportBuilderToExcelQuery, ReportBuilderToExcelQueryDto>
{
    private readonly IQueryBuilderRepository _repo;
    public ReportBuilderToExcelQueryHandler(IQueryBuilderRepository repo) {
        _repo = repo;
    }

    public async Task<ReportBuilderToExcelQueryDto> Handle(ReportBuilderToExcelQuery request, CancellationToken cancellationToken) {
        var sqlBuilder = _repo.StudentLeftJoinBuilder(request.Tables);
        _repo.WhereClauseBuilder(request.Filter, ref sqlBuilder);
        sqlBuilder.AppendLine("ORDER BY [stu].[Studentsummary].[Id]");
        var query = sqlBuilder.ToString().Replace("\r\n", " ");

        var data = await _repo.ExecuteQuery(query);
        if ( data.Length == 0 ) {
            throw new BadRequestException("گزارش نتیجه ای ندارد.");
        }

        return new ReportBuilderToExcelQueryDto($"گزارش پذیرش {DateTime.Now:yyyyMMdd HHmm}.xlsx", ExportToExcel(data, request));
    }

    private byte[] ExportToExcel(dynamic[] data, ReportBuilderToExcelQuery request) {
        var dataTable = new DataTable("Export");
        var firstRow = data.FirstOrDefault();
        if ( firstRow != null ) {

            var dict = (IDictionary<string, object>) firstRow;
            foreach ( var key in dict.Keys ) {
                dataTable.Columns.Add(key);
            }

            foreach ( var row in data ) {
                var rowDict = (IDictionary<string, object>) row;
                var values = rowDict.Values.ToArray();
                dataTable.Rows.Add(values);
            }

            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Data");
            sheet.IsRightToLeft = true;

            //TODO : Move styles to a separate method or class for better organization
            var headerStyle = workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = IndexedColors.Grey40Percent.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            var headerFont = workbook.CreateFont();
            headerFont.IsBold = true;
            headerFont.Color = IndexedColors.White.Index;
            headerStyle.SetFont(headerFont);

            var cellStyle = workbook.CreateCellStyle();
            var cellFont = workbook.CreateFont();
            cellFont.Color = IndexedColors.Black.Index;
            cellStyle.SetFont(cellFont);

            // Header
            var columns = new List<ReportBuilderModel.Column>() { new("Codm", "کد مرکز خدمات", ColumnType.Numeric) };
            foreach ( var requestTable in request.Tables ) {
                var table = ReportBuilderTables.GetAll().Single(x => x.Name == requestTable.Name);
                var tableColumns = table.Columns.Where(x => requestTable.Columns.Any(y => y.Name == x.Name));
                columns.AddRange([.. tableColumns]);
            }
            var headerRow = sheet.CreateRow(0);
            for ( var i = 0; i < columns.Count; i++ ) {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(columns[i].Label);
                cell.CellStyle = headerStyle;
            }

            // Data
            for ( var rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++ ) {
                var excelRow = sheet.CreateRow(rowIndex + 1);
                var row = dataTable.Rows[rowIndex];

                for ( var columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++ ) {

                    var objValu = row[columnIndex];
                    var column = columns[columnIndex];
                    var cell = excelRow.CreateCell(columnIndex);
                    cell.CellStyle = cellStyle;

                    if (string.IsNullOrWhiteSpace(objValu.ToString())) {
                        cell.SetCellValue("");

                    }
                    else if ( column.Type.EqualIgnoreCase(ColumnType.Numeric.ToString()) ) {
                        _ = long.TryParse(objValu.ToString(), out var value);
                        cell.SetCellValue(value);

                    } else if ( column.Type.EqualIgnoreCase(ColumnType.Decimal.ToString()) ) {
                        _ = double.TryParse(objValu.ToString(), out var value);
                        cell.SetCellValue(value);

                    } else if ( column.Type.EqualIgnoreCase(ColumnType.Boolean.ToString()) ) {
                        _ = bool.TryParse(objValu.ToString(), out var value);
                        cell.SetCellValue(value == true ? "هست" : "نیست");

                    } else if ( column.Type.EqualIgnoreCase(ColumnType.List.ToString()) && !string.IsNullOrEmpty(column.Source?.Enum) ) {
                        if ( int.TryParse(objValu.ToString(), out var key) ) {
                            var list = column.Source.Data as Dictionary<int, string>;

                            if ( list.TryGetValue(key, out var value) ) {
                                cell.SetCellValue(key > 0 ? Messages.ResourceManager.GetString(value) : "");
                            } else {
                                cell.SetCellValue($"مقدار نادرست ({key})");
                            }

                        } else {
                            cell.SetCellValue($"مقدار نادرست ({objValu})");
                        }

                    } else {
                        cell.SetCellValue(objValu.ToString());
                    }
                }
            }

            using var stream = new MemoryStream();
            workbook.Write(stream);
            return stream.ToArray();
        }

        return null;
    }
}
