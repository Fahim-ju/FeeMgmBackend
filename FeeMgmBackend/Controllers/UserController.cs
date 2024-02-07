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
    public async Task<IActionResult> GetMembers()
    {
        try
        {
            var members = await _context.Members.ToListAsync();
            var membersDto = members.Select(user => _mapper.Map<MemberDto>(user)).ToList();
            var fines = await _context.Fines.ToListAsync();
            var laws = await _context.Laws.ToListAsync();
            var payments = await _context.Payments.ToListAsync();

            fines.ForEach(fine =>
            {
                var member = membersDto.Find(member => member.Id == fine.MemberId);
                member.TotalFine += fine.amount;
            });

            payments.ForEach(payment =>
            {
                var user = membersDto.Find(u => u.Id == payment.MemberId);
                user.Paid += payment.Amount;
            });
            membersDto.ForEach(user => user.Due = user.TotalFine - user.Paid);
            return Ok(membersDto);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    [HttpPost("AddUser")]
    public async Task<IActionResult> AddMember(Member member)
    {
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();
        return Ok(member);
    }

    [HttpPost("UpdateUser")]
    public async Task<IActionResult> UpdateUser(Member member)
    {
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
        return Ok(member);
    }

    [HttpPost("ActivateUser")]
    public async Task<IActionResult> ActivateUser(Member member)
    {
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
        return Ok(member);
    }

    [HttpPost("DeactivateUser")]
    public async Task<IActionResult> DeactivateUser(Member member)
    {
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
        return Ok(member);
    }

    [HttpPost("UpdateUserRole")]
    public async Task<IActionResult> UpdateUserRole(Member member)
    {
        var user = _context.Members.Update(member);
        await _context.SaveChangesAsync();
        return Ok(member);
    }
}
