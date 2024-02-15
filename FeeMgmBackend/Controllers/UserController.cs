using AutoMapper;
using FeeMgmBackend.Entity;
using FeeMgmBackend.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FeeMgmBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace FeeMgmBackend.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(DatabaseContext context, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _roleManager = roleManager;
        _userManager = userManager;
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

            var applicationUserRoles = await _roleManager.Roles.ToListAsync();

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

            foreach (var memberDto in membersDto)
            {
                var user = await _userManager.FindByIdAsync(memberDto.ApplicationUserId.ToString());
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    memberDto.Roles = roles.ToList();
                }

                memberDto.Due = memberDto.TotalFine - memberDto.Paid;
            }

            return Ok(membersDto);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    [HttpGet("GetAplplicationUsers")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _userManager.Users.ToListAsync();
            var usersDto = users.Select(user => _mapper.Map<UserDto>(user)).ToList();

            return Ok(usersDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [HttpGet("GetUserRoleById/{userId}")]
    public async Task<IActionResult> GetUserRoleById(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new { UserId = user.Id, Roles = roles });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
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

    [HttpPost("ChangeUserRole")]
    public async Task<IActionResult> ChangeUserRole(UpdateUserRoleDto payload)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(payload.UserId);

            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            // Remove existing roles
            var existingRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, existingRoles);

            // Add the new role
            await _userManager.AddToRoleAsync(user, payload.selectedRole);

            return Ok(new { UserId = user.Id, Roles = new List<string> { payload.selectedRole } });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [HttpPost("UpdateUserName")]
    public async Task<IActionResult> UpdateUserName(Member member)
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

    [HttpPost("AddRole")]
    public async Task<IActionResult> AddRole(string roleName)
    {
        IdentityRole role = new IdentityRole
        {
            Name = roleName,
            NormalizedName = roleName.ToUpper()
        };
        await _roleManager.CreateAsync(role);
        return Ok(role);
    }


}
