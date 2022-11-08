using System.Linq.Expressions;
using ConvaReload.Abstract;
using ConvaReload.Domain.Entities;
using ConvaReload.Services.Abstract;

namespace ConvaReload.Services;

public class ConferenceService : IConferenceService
{
    private readonly IConferenceRepository _conferenceRepository;

    public ConferenceService(IConferenceRepository conferenceRepository)
    {
        _conferenceRepository = conferenceRepository;
    }

    public async Task<Conference> GetByIdAsync(int id) => await _conferenceRepository.GetByIdAsync(id);
    public async Task<IEnumerable<Conference>> GetAllAsync() => await _conferenceRepository.GetAllAsync();
    public async Task<IEnumerable<Conference>> FindAsync(Expression<Func<Conference, bool>> exp) => await _conferenceRepository.FindAsync(exp);
    public async Task<Conference> AddAsync(Conference conference) => await _conferenceRepository.AddAsync(conference);
    public async Task<Conference> UpdateAsync(Conference conference) => await _conferenceRepository.UpdateAsync(conference);
    public async Task<Conference> RemoveAsync(Conference conference) => await _conferenceRepository.RemoveAsync(conference);
    public async Task<IEnumerable<Conference>> AddRangeAsync(IEnumerable<Conference> conferences) => await _conferenceRepository.AddRangeAsync(conferences);
    public async Task<IEnumerable<Conference>> UpdateRangeAsync(IEnumerable<Conference> conferences) => await _conferenceRepository.UpdateRangeAsync(conferences);
    public async Task<IEnumerable<Conference>> RemoveRangeAsync(IEnumerable<Conference> conferences) => await _conferenceRepository.RemoveRangeAsync(conferences);
}