using FeeMgmBackend.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FeeMgmBackend.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;
    public UserController(DatabaseContext context)
    {
        _context = context;
    }
    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.Where(x => !x.IsDeactivated).ToListAsync();
        return Ok(users);
    }

    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPost("UpdateUser")]
    public async Task<IActionResult> UpdateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPost("ActivateUser")]
    public async Task<IActionResult> ActivateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPost("DeactivateUser")]
    public async Task<IActionResult> DeactivateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }
}
