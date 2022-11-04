using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ConvaReload.Domain.Entities;
using ConvaReload.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ConvaReload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet, Authorize]
        public ActionResult<object> GetMe()
        {
            return Ok(new
            {
                identity = _userService.GetMyName(),
                name = User.FindFirstValue(ClaimTypes.Name),
                role = User.FindFirstValue(ClaimTypes.Role)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<object>> Register(UserCredentials credentials)
        {
            var user = new User();
            
            CreatePasswordHash(credentials.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userService.GetRepository().AddAsync(user);
            
            return GetMe();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserCredentials credentials)
        {
            var user = (await _userService.GetRepository().FindAsync(u => u.Username == credentials.Username)).First();
            
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            if (!VerifyPasswordHash(credentials.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }
            
            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            
            var cookiesOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookiesOptions);
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;
            
            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = (await _userService.GetRepository().FindAsync(user => user.RefreshToken.Equals(refreshToken)))
                .First();
            if (user != null)
            {
                if (user.TokenExpires < DateTime.Now)
                {
                    return Unauthorized("Token expired.");
                }

                var token = CreateToken(user);
                
                var newRefreshToken = GenerateRefreshToken();
                var cookiesOptions = new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = newRefreshToken.Expires
                };
                Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookiesOptions);
                user.RefreshToken = newRefreshToken.Token;
                user.TokenCreated = newRefreshToken.Created;
                user.TokenExpires = newRefreshToken.Expires;

                return Ok(token);
            }
            return Unauthorized("Invalid Refresh token.");
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(1)
            };
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Member")
            };
            
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                claims: claims, 
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}