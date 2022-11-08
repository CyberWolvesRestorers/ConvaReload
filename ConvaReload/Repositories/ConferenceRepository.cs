using System.Linq.Expressions;
using ConvaReload.Abstract;
using ConvaReload.DataAccess;
using ConvaReload.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConvaReload.Repositories;

public class ConferenceRepository : IConferenceRepository
{
    private readonly ApplicationContext _context;

    public ConferenceRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Conference> GetByIdAsync(int id) => await _context.Conferences.FindAsync(id);
    public async Task<IEnumerable<Conference>> GetAllAsync() => await _context.Conferences.ToListAsync();
    public async Task<IEnumerable<Conference>> FindAsync(Expression<Func<Conference, bool>> exp) => await _context.Conferences.Where(exp).ToListAsync();

    public async Task<Conference> AddAsync(Conference conference)
    {
        _context.Conferences.Add(conference);
        await _context.SaveChangesAsync();
        return conference;
    }

    public async Task<Conference> UpdateAsync(Conference conference)
    {
        _context.Conferences.Update(conference);
        await _context.SaveChangesAsync();
        return conference;
    }

    public async Task<Conference> RemoveAsync(Conference conference)
    {
        _context.Conferences.Remove(conference);
        await _context.SaveChangesAsync();
        return conference;
    }

    public async Task<IEnumerable<Conference>> AddRangeAsync(IEnumerable<Conference> conferences)
    {
        _context.Conferences.AddRange(conferences);
        await _context.SaveChangesAsync();
        return conferences;
    }

    public async Task<IEnumerable<Conference>> UpdateRangeAsync(IEnumerable<Conference> conferences)
    {
        _context.Conferences.UpdateRange(conferences);
        await _context.SaveChangesAsync();
        return conferences;
    }

    public async Task<IEnumerable<Conference>> RemoveRangeAsync(IEnumerable<Conference> conferences)
    {
        _context.Conferences.RemoveRange(conferences);
        await _context.SaveChangesAsync();
        return conferences;
    }
}