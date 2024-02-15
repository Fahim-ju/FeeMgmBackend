using FeeMgmBackend.Entity;
using FeeMgmBackend.IService;
using Microsoft.AspNetCore.Mvc;

namespace FeeMgmBackend.Controllers;

[ApiController]
[Route("[Controller]")]
public class FineController : ControllerBase
{
    private readonly IFineService _fineService;

    public FineController(IFineService fineService)
    {
        _fineService = fineService;
    }

    [HttpGet("GetFines")]
    public async Task<IActionResult> GetFinesAsync()
    {
        try
        {
            var fines = await _fineService.GetFinesAsync();
            return Ok(fines);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("AddFine")]
    public async Task<IActionResult> AddAsync(Fine fine)
    {
        try
        {
            await _fineService.AddAsync(fine);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await _fineService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
