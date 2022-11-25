using Microsoft.AspNetCore.Mvc;
using ConvaReload.Domain.Entities;
using ConvaReload.Services.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace ConvaReload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService usersService)
        {
            _userService = usersService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllAsync());
        }

        // GET: api/User/5
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
        
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostUser(User user)
        {
            await _userService.AddAsync(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/User/5
        [HttpPut("{id}"), Authorize(Roles="admin, user")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            return Ok(await _userService.UpdateAsync(user));
        }

        // DELETE: api/User/5
        [HttpDelete("{id}"), Authorize(Roles="admin, user")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(await _userService.RemoveAsync(user));
        }
        
        // POST: api/User/range
        [HttpPost("range")]
        public async Task<IActionResult> PostUsers(IEnumerable<User> users)
        {
            await _userService.AddRangeAsync(users);

            return CreatedAtAction(nameof(GetUsers), users.Select(u => u.Id), users);
        }
    
        // PUT: api/User/range
        [HttpPut("range"), Authorize(Roles="admin, user")]
        public async Task<IActionResult> PutUsers(IEnumerable<User> users)
        {
            return Ok(await _userService.UpdateRangeAsync(users));
        }
    
        // DELETE: api/User/range
        [HttpDelete("range"), Authorize(Roles="admin, user")]
        public async Task<IActionResult> DeleteUsers(IEnumerable<User> users)
        {
            return Ok(await _userService.RemoveRangeAsync(users));
        }
    }
}