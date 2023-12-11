using AutoMapper;
using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeeMgmBackend.Controllers;

[ApiController]
[Route("[Controller]")]
[Authorize(Roles = "Admin")]
public class FineController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;
    public FineController(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("GetFines")]
    public async Task<IActionResult> GetFines()
    {
        var fines = await _context.Fines.ToListAsync();
        var members = await _context.Members.ToListAsync();
        var laws = await _context.Laws.ToListAsync();


        List<FineDto> fineList = new List<FineDto>();

        foreach (var fine in fines)
        {
            var fn = _mapper.Map<FineDto>(fine);
            fn.UserName = members.Find(member => member.Id == fine.MemberId).Name;
            Law law = laws.Find(law => law.Id == fine.LawId);
            fn.LawName = law.Name;
            fn.Amount = law.Amount;
            fineList.Add(fn);
        }
        return Ok(fineList);
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
