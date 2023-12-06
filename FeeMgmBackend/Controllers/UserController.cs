using AutoMapper;
using FeeMgmBackend.Entity;
using FeeMgmBackend.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FeeMgmBackend.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;
    public UserController(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        var usersDto = users.Select(user => _mapper.Map<UserDto>(user)).ToList();
        var fines = await _context.Fines.ToListAsync();
        var laws = await _context.Laws.ToListAsync();
        var payments = await _context.Payments.ToListAsync();

        fines.ForEach(fine =>
        {
            var user = usersDto.Find(users => users.Id == fine.UserId);
            var law = laws.Find(law => law.Id == fine.LawId);
            user.TotalFine += law.Amount;
        });

        payments.ForEach(payment =>
        {
            var user = usersDto.Find(u => u.Id == payment.UserId);
            user.Paid += payment.Amount;
        });
        usersDto.ForEach(user => user.Due = user.TotalFine - user.Paid);
        return Ok(usersDto);
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
