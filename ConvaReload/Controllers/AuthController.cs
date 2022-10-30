using System.Security.Cryptography;
using System.Text;
using ConvaReload.Domain.Entities;
using ConvaReload.Encryption.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConvaReload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(Credentials credentials)
        {
            var user = new User();
            CreatePasswordHash(credentials.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Ok(user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                
            }
        }
    }
}