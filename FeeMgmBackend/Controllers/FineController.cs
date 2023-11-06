using FeeMgmBackend.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeeMgmBackend.Controllers;

[ApiController]
[Route("[Controller]")]
public class FineController: ControllerBase
{
    private readonly DatabaseContext _context;
    public FineController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get(){
        var fines = await _context.Fines.ToListAsync();
        return Ok(fines);
    }

   [HttpPost]
   public async Task<IActionResult> Post(Fine fine){
          await _context.Fines.AddAsync(fine);
          await _context.SaveChangesAsync();
          return Ok(fine);
      }
    
}
