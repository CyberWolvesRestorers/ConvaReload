using ConvaReload.Abstract;
using Microsoft.AspNetCore.Mvc;
using ConvaReload.Domain.Entities;

namespace ConvaReload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CrudRepository<User> _users;

        public UserController(CrudRepository<User> users)
        {
            _users = users;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _users.GetAllAsync());
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _users.GetByIdAsync(id);
        
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
            await _users.AddAsync(user);

            return Ok(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            return Ok(await _users.UpdateAsync(user));
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _users.GetByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(await _users.RemoveAsync(user));
        }
        
        // POST: api/User/range
        [HttpPost("range")]
        public async Task<IActionResult> PostUsers(IEnumerable<User> users)
        {
            await _users.AddRangeAsync(users);

            return Ok(users);
        }
    
        // PUT: api/User/range
        [HttpPut("range")]
        public async Task<IActionResult> PutUsers(IEnumerable<User> users)
        {
            return Ok(await _users.UpdateRangeAsync(users));
        }
    
        // DELETE: api/User/range
        [HttpDelete("range")]
        public async Task<IActionResult> DeleteUsers(IEnumerable<User> users)
        {
            return Ok(await _users.RemoveRangeAsync(users));
        }
    }
}