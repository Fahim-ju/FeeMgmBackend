using FeeMgmBackend.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeeMgmBackend.Controllers;

[ApiController]
[Route("[Controller]")]
public class FineController : ControllerBase
{
    private readonly DatabaseContext _context;
    public FineController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet("GetFines")]
    public async Task<IActionResult> GetFines()
    {
        var fines = await _context.Fines.ToListAsync();
        return Ok(fines);
    }

    [HttpPost("AddFine")]
    public async Task<IActionResult> AddFine(Fine fine)
    {
        await _context.Fines.AddAsync(fine);
        await _context.SaveChangesAsync();
        return Ok(fine);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> AddFine(Guid id)
    {
        var existingFine = await _context.Fines.FirstOrDefaultAsync(x => x.Id == id);
        existingFine.IsDeleted = true;
        _context.Fines.Update(existingFine);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
