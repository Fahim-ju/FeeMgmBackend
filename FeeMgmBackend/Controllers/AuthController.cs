using AutoMapper;
using FeeMgmBackend.Dtos;
using FeeMgmBackend.Models;
using FeeMgmBackend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FeeMgmBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, ILogger<AuthController> logger, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _authService = authService;
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid Payload");

                var (status, message) = await _authService.Login(loginModel);

                if (status == 0) return BadRequest(message);

                var applicationUser = await _userManager.FindByNameAsync(loginModel.Username);
                if (applicationUser == null) return BadRequest("Invalid Username");

                var authUser = _mapper.Map<AuthUserDto>(applicationUser);

                var Roles = await _userManager.GetRolesAsync(applicationUser);

                if (Roles.Count > 0) authUser.Role = Roles.First();

                return Ok(new { Message = message, User = authUser });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid Payload");

                var (status, message) = await _authService.Registration(registrationModel, UserRoles.User);

                if (status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(Register), registrationModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile(string userName)
        {
            try
            {
                var applicationUser = await _userManager.FindByNameAsync(userName);
                if (applicationUser == null) return BadRequest("Invalid Username");

                var authUser = _mapper.Map<AuthUserDto>(applicationUser);
                var Roles = await _userManager.GetRolesAsync(applicationUser);
                if (Roles.Count > 0) authUser.Role = Roles.First();

                return Ok(authUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
