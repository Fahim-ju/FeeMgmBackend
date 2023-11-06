using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

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
