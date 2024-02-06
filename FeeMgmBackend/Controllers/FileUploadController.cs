using Microsoft.AspNetCore.Mvc;
using FeeMgmBackend.Services;

namespace FeeMgmBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public FilesController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }
        [HttpPost("FineFileUpload")]
        public async Task<IActionResult> AnalyzeFineExcelFile(IFormFile file)
        {
            try
            {
                var result = await _fileUploadService.AnalyzeFineExcelFileAsync(file.OpenReadStream());
           
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }

        [HttpPost("PaymentFileUpload")]
        public async Task<IActionResult> AnalyzePaymentExcelFile(IFormFile file)
        {
            try
            {
                var result = await _fileUploadService.AnalyzePaymentExcelFileAsync(file.OpenReadStream());
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}
