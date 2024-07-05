using Microsoft.AspNetCore.Mvc;
using SalesAnalytics.API.Services;

namespace SalesAnalytics.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // This is just a placeholder for actual authentication logic
            if (model.Username == "Admin" && model.Password == "Password")
            {
                var token = _tokenService.GenerateToken(model.Username);

                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

    }


}
