using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeeMgmBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class LawController : ControllerBase
{
    private readonly DatabaseContext _context;

    public LawController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet("GetLaws")]
    public async Task<IActionResult> GetLaws()
    {
        var laws = await _context.Laws.Where(x => !x.IsDeleted).ToListAsync();
        return Ok(laws);
    }

    [HttpPost("AddLaw")]
    public async Task<IActionResult> AddLaw([FromBody] LawDto lawDto)
    {
        var law = new Law
        {
            Name = lawDto.Name,
            Description = lawDto.Description,
            Amount = lawDto.Amount
        };

        await _context.Laws.AddAsync(law);
        await _context.SaveChangesAsync();

        return Ok(law);
    }

    [HttpPost("UpdateLaw")]
    public async Task<IActionResult> UpdateLaw([FromBody] Law law)
    {
        _context.Laws.Update(law);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLaw(Guid id)
    {
        var existingLaw = await _context.Laws.FirstOrDefaultAsync(x => x.Id == id);
        existingLaw.IsDeleted = true;
        _context.Laws.Update(existingLaw);
        await _context.SaveChangesAsync();
        return Ok();
    }
}