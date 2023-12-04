using OfficeOpenXml;

namespace FeeMgmBackend.Services
{
    public class ExcelService
    {
        public ExcelService() { }
        public List<List<object>> ParseExcel(Stream file)
        {
            ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
            List<List<object>> records = new List<List<object>>();
            List<int> skippedIndex = new List<int>();
            int rows = worksheet?.Dimension.Rows ?? 0;
            if (rows == 0) return records;
            int columns = worksheet?.Dimension.Columns ?? 0;
            if (columns == 0) return records;


            for (int i = 1; i <= rows; i++)
            {
                var singleRow = GetRow(worksheet, i, columns, skippedIndex);
                records.Add(singleRow);
            }
            return records;
        }
            


    private List<object> GetRow(ExcelWorksheet worksheet, int row, int columns, List<int> skippedIndex)
    {
        List<object> rowData = new List<object>();
        for (int j = 1; j <= columns; j++)
        {
            if (row == 1 && (worksheet.Cells[row, j] == null || string.IsNullOrEmpty(worksheet.Cells[row, j].Text))) skippedIndex.Add(j);

            if (skippedIndex.Contains(j)) continue;

            object content = worksheet.Cells[row, j].Text;
            rowData.Add(content);
        }
        return rowData;
    }
}
}
