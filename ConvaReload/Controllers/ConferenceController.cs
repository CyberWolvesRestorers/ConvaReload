using ConvaReload.Abstract;
using Microsoft.AspNetCore.Mvc;
using ConvaReload.Domain.Entities;

namespace ConvaReload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceController : ControllerBase
    {
        private readonly CrudRepository<Conference> _conferences;

        public ConferenceController(CrudRepository<Conference> conferences)
        {
            _conferences = conferences;
        }

        // GET: api/Conference
        [HttpGet]
        public async Task<IActionResult> GetConferences()
        {
            return Ok(await _conferences.GetAllAsync());
        }

        // GET: api/Conference/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConference(int id)
        {
            var conference = await _conferences.GetByIdAsync(id);
        
            if (conference is null)
            {
                return NotFound();
            }

            return Ok(conference);
        }
        
        // POST: api/Conference
        [HttpPost]
        public async Task<IActionResult> PostConference(Conference conference)
        {
            await _conferences.AddAsync(conference);

            return CreatedAtAction(nameof(GetConference), new { id = conference.Id }, conference);
        }

        // PUT: api/Conference/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConference(int id, Conference conference)
        {
            if (id != conference.Id)
            {
                return BadRequest();
            }

            return Ok(await _conferences.UpdateAsync(conference));
        }

        // DELETE: api/Conference/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConference(int id)
        {
            var conference = await _conferences.GetByIdAsync(id);

            if (conference is null)
            {
                return NotFound();
            }

            return Ok(await _conferences.RemoveAsync(conference));
        }
        
        // POST: api/Conference/range
        [HttpPost("range")]
        public async Task<IActionResult> PostConferences(IEnumerable<Conference> conferences)
        {
            await _conferences.AddRangeAsync(conferences);

            return CreatedAtAction(nameof(GetConferences), conferences.Select(c => c.Id), conferences);
        }
    
        // PUT: api/Conference/range
        [HttpPut("range")]
        public async Task<IActionResult> PutConferences(IEnumerable<Conference> conferences)
        {
            return Ok(await _conferences.UpdateRangeAsync(conferences));
        }
    
        // DELETE: api/Conference/range
        [HttpDelete("range")]
        public async Task<IActionResult> DeleteConferences(IEnumerable<Conference> conferences)
        {
            return Ok(await _conferences.RemoveRangeAsync(conferences));
        }
    }
}