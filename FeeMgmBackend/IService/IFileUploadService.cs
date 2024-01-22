namespace FeeMgmBackend.Services;

public interface IFileUploadService
{
    Task<List<List<object>>> AnalyzeFineExcelFileAsync(Stream stream);
    Task<List<List<object>>> AnalyzePaymentExcelFileAsync(Stream stream);
}