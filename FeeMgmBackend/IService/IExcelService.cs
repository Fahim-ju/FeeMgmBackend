using OfficeOpenXml;

namespace FeeMgmBackend.IService;

public interface IExcelService
{
    List<List<object>> ParseExcel(Stream file);
    List<object> GetRow(ExcelWorksheet worksheet, int row, int columns, List<int> skippedIndex);
}