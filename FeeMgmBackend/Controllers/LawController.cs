using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeeMgmBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class LawController : ControllerBase
{
    // inject database context in constructor
    private readonly DatabaseContext _context;
    
    public LawController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var laws = await _context.Laws.ToListAsync();
        return Ok(laws);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LawDto lawDto)
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
}