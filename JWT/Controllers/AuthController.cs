using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {

            _configuration = configuration;

        }

        [HttpPost]
        public string Index([FromForm] string user,[FromForm] string password)
        {
            if (user == "pato" && password == "flow") {
                var tokenHandler = new JwtSecurityTokenHandler();
                var byteKey = Encoding.UTF8.GetBytes(_configuration["JwtSecurity:key"]);

                var TokenDes = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(TokenDes);
                return tokenHandler.WriteToken(token);
            }

            return "";
        }
    }
}
