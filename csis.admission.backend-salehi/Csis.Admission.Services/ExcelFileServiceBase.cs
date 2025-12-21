/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using Csis.Excel.Mapper;
using Csis.Excel.Mapper.Extensions;
using Csis.Utilities.Extensions;
using NPOI.OpenXml4Net.Exceptions;
using NPOI.SS.UserModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Csis.Admission.Services;

internal sealed partial class ExcelFileService : IExcelFileService
{
    public Task<byte[]> ExportToExcelAsync<T>(List<(List<T> data, string sheetName)> perSheetData) {
        var mapper = new Mapper();
        using var ms = new MemoryStream();

        ConfigureMapper(mapper, typeof(T));

        foreach ( var (data, sheetName) in perSheetData ) {
            mapper.Put(data, sheetName, overwrite: true);
        }

        mapper.Save(ms, leaveOpen: false);
        return Task.FromResult(ms.ToArray());
    }

    public async Task<byte[]> ExportToExcelAsync<T>(List<T> data, string sheetName) {
        return await ExportToExcelAsync([(data, sheetName)]);
    }

    public async Task<byte[]> PreprocessExcelHeaderAsync(byte[] fileBytes, Func<string, string> headerCellProcessor, Dictionary<string, string> headerMappings = null, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(fileBytes);

        using var ms = new MemoryStream();
        await ms.WriteAsync(fileBytes, cancellationToken);

        var workbook = WorkbookFactory.Create(ms);

        for ( var i = 0; i < workbook.NumberOfSheets; i++ ) {
            var sheet = workbook.GetSheetAt(i);

            if ( sheet.PhysicalNumberOfRows < 1 ) {
                continue;
            }

            var row = sheet.GetRow(0);

            if ( headerCellProcessor is not null ) {
                for ( var j = 0; j < row.Cells.Count; j++ ) {
                    row.Cells[j].SetCellValue(headerCellProcessor(row.Cells[j].StringCellValue ?? string.Empty));
                }
            }

            if ( headerMappings is not null && headerMappings.Count > 0 ) {
                foreach ( var headerMapping in headerMappings ) {
                    if ( !headerMapping.Key.HasValue() || headerMapping.Value is null ) {
                        continue;
                    }

                    var cell = row.Cells
                        .Where(x => x.StringCellValue.Equals(headerMapping.Key, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();

                    cell?.SetCellValue(headerMapping.Value);
                }
            }
        }

        using var outMs = new MemoryStream((int) ms.Length);
        workbook.Write(outMs, leaveOpen: false);

        return outMs.ToArray();
    }

    private static void ConfigureMapper(Mapper mapper, Type type) {
        foreach ( var prop in type.GetProperties() ) {
            var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
            var columnAttr = prop.GetCustomAttribute<ColumnAttribute>();

            if ( displayAttr is not null ) {

                mapper.Map(displayAttr.Name, prop);
            } else if ( columnAttr is not null ) {
                mapper.Map(columnAttr.Name, prop);
            }
        }
    }

    public List<T> ReadFile<T>(Stream stream) where T : class {
        try {
            var mapper = new Mapper(stream);
            return [.. mapper.Take<T>(0).Select(x => x.Value)];
        } catch ( InvalidFormatException ) {
            return null;
        }
    }

    public List<T> ReadFile<T>(byte[] bytes) where T : class {
        using var ms = new MemoryStream(bytes);
        return ReadFile<T>(ms);
    }
}
