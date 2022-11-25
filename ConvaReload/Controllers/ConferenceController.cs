using ConvaReload.Abstract;
using Microsoft.AspNetCore.Mvc;
using ConvaReload.Domain.Entities;
using ConvaReload.Services.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace ConvaReload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConferenceController : ControllerBase
    {
        private readonly IConferenceService _conferenceService;

        public ConferenceController(IConferenceService conferenceService)
        {
            _conferenceService = conferenceService;
        }

        // GET: api/Conference
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetConferences()
        {
            return Ok(await _conferenceService.GetAllAsync());
        }

        // GET: api/Conference/5
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetConference(int id)
        {
            var conference = await _conferenceService.GetByIdAsync(id);
        
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
            await _conferenceService.AddAsync(conference);

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

            return Ok(await _conferenceService.UpdateAsync(conference));
        }

        // DELETE: api/Conference/5
        [HttpDelete("{id}"), Authorize(Roles="admin, user")]
        public async Task<IActionResult> DeleteConference(int id)
        {
            var conference = await _conferenceService.GetByIdAsync(id);

            if (conference is null)
            {
                return NotFound();
            }

            return Ok(await _conferenceService.RemoveAsync(conference));
        }
        
        // POST: api/Conference/range
        [HttpPost("range"), Authorize(Roles="user")]
        public async Task<IActionResult> PostConferences(IEnumerable<Conference> conferences)
        {
            await _conferenceService.AddRangeAsync(conferences);

            return CreatedAtAction(nameof(GetConferences), conferences.Select(c => c.Id), conferences);
        }
    
        // PUT: api/Conference/range
        [HttpPut("range"), Authorize(Roles="user")]
        public async Task<IActionResult> PutConferences(IEnumerable<Conference> conferences)
        {
            return Ok(await _conferenceService.UpdateRangeAsync(conferences));
        }
    
        // DELETE: api/Conference/range
        [HttpDelete("range"), Authorize(Roles="admin, user")]
        public async Task<IActionResult> DeleteConferences(IEnumerable<Conference> conferences)
        {
            return Ok(await _conferenceService.RemoveRangeAsync(conferences));
        }
    }
}