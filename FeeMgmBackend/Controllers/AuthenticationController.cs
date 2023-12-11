using FeeMgmBackend.Models;
using FeeMgmBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeeMgmBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task< IActionResult > Login(LoginModel loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid Payload");
                var (status, message) = await _authService.Login(loginModel);
                if (status == 0)
                    return BadRequest(message);
                return Ok(message);
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
                var (status, message) = await _authService.Registration(registrationModel, UserRoles.Admin);
                if(status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(Register), registrationModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);    
            }
        }
    }
}
