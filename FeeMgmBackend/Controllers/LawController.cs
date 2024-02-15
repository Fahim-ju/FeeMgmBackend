using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;
using FeeMgmBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeeMgmBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class LawController : ControllerBase
{
    private readonly ILawService _lawService;

    public LawController(ILawService lawService)
    {
        _lawService = lawService;
    }

    [HttpGet("GetLaws")]
    public async Task<IActionResult> IndexAsync()
    {
        try
        {
            var laws = await _lawService.IndexAsync();
            return Ok(laws);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("AddLaw")]
    public async Task<IActionResult> AddAsync([FromBody] LawDto lawDto)
    {
        try
        {
            var law = new Law
            {
                Name = lawDto.Name,
                Description = lawDto.Description,
                IsDeleted = false
            };

            var data = await _lawService.AddAsync(law);
            return Ok(data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("UpdateLaw")]
    public async Task<IActionResult> UpdateAsync([FromBody] Law law)
    {
        try
        {
            await _lawService.UpdateAsync(law);
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
            await _lawService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}