using API.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

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
    public async Task<IActionResult> Post([FromBody] Law law)
    {
        await _context.Laws.AddAsync(law);
        await _context.SaveChangesAsync();
        return Ok(law);
    }
}