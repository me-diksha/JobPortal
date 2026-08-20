using JobPortalAPI.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ILogger _logger;
        public AuthController(
         IAuthService authService,
         ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var response = await _authService.Login(request);

            if (response == null)
            {
                _logger.LogWarning("Invalid login attempt ");
                return Unauthorized(new
                {
                    Message = "Invalid Username and password"
                });

            }
            _logger.LogInformation("User logged in successfully ");
            return Ok(response);

        }

        [AllowAnonymous]
        [HttpPost("register")]
        
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _authService.Register(request);
            if (response == null)
            {
                return BadRequest(new
                {
                    Message = "User already exists or invalid role"
                });
            }
            _logger.LogInformation("User registered successfully");
            return Ok(response);
        }
    }
}
