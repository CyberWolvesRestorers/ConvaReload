using System.Linq.Expressions;
using ConvaReload.Domain.Entities;

namespace ConvaReload.Services.Abstract;

public interface IConferenceService
{
    Task<Conference> GetByIdAsync(int id);
    Task<IEnumerable<Conference>> GetAllAsync();
    Task<IEnumerable<Conference>> FindAsync(Expression<Func<Conference, bool>> exp);
    Task<Conference> AddAsync(Conference conference);
    Task<Conference> UpdateAsync(Conference conference);
    Task<Conference> RemoveAsync(Conference conference);
    Task<IEnumerable<Conference>> AddRangeAsync(IEnumerable<Conference> conferences);
    Task<IEnumerable<Conference>> RemoveRangeAsync(IEnumerable<Conference> conferences);
    Task<IEnumerable<Conference>> UpdateRangeAsync(IEnumerable<Conference> conferences);
}