using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Excel
{

    public interface IExcelConverter
    {
        byte[] Export<T>(IEnumerable<T> data, string sheetName) where T : class, new();

        MemoryStream ExportDataTableToXLSFromTable(DataSet dataSet);
        MemoryStream ExportDataTableToXLSFromDataSet(DataSet dataSet);

        IEnumerable<T> ExcelSheetToObject<T>(Stream excelStream, string sheetName, int skipColumnCount, int headerIndex, ExcelValidator<T> validator) where T : class, new();

        System.Data.DataTable ExcelSheetToDatatable(Stream stream, string sheetName, int skipColumnCount, int headerIndex);
    }
}
