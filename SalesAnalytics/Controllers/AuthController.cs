using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalesAnalytics.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        private async Task<string> GenerateToken(string user)
        {
            //Generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {

                var key = Encoding.ASCII.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);
        }
        public string GenerateToken1(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
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
                var token = GenerateToken(model.Username);

                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

    }


}
