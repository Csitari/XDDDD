using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using Vizsgaremek_Backend.Models;
using Vizsgaremek_Backend.Models.JWT;

namespace Vizsgaremek_Backend.Controllers.JWT
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [HttpPost("regisztracio")]
        public ActionResult<User> Register(UserDto request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.Username = request.Username;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            
            return Ok(user);
        }

        [HttpPost("bejelentkezes")]
        public ActionResult<User> Login(UserDto request)
        {
            //A context cuccal tudo megkeresni azt, hogy melyik felhaszáló szeretne utat nyitni az ánuszodba
            using (var context = new EsemenytarContext()) {
                var megtalalt = context.Felhasznaloks.FirstOrDefault(x=>x.Email == request.Email);
                //Ezzel megtöltöd az üres user objektumodat, hogy a tokennek legyen indója arról, hogy kit akarsz megbaszni
                user.Username = megtalalt.Felhasznalonev;
                user.Email = megtalalt.Email;
                user.PasswordHash = megtalalt.JelszoHash;

                if (megtalalt == null)
                {
                    return BadRequest("Felhasználó nem található");

                }

                //if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                //{
                //    return BadRequest("Hibás jelszó");
                //}

                string token = CreateToken(user);

                return Ok(token);
            }
           
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var kulcs = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var cr = new SigningCredentials(kulcs, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cr);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
