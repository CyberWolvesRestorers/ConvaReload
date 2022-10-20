using System.Linq.Expressions;
using ConvaReload.Abstract;
using ConvaReload.DataAccess;
using ConvaReload.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConvaReload.Services;

public class UserService : CrudRepository<User>
{
    private readonly ApplicationContext _context;

    public UserService(ApplicationContext context)
    {
        _context = context;
    }

    public override async Task<User> GetByIdAsync(int id) => await _context.Users.FindAsync(id);
    public override async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();
    public override async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> exp) => await _context.Users.Where(exp).ToListAsync();

    public override async Task<User> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public override async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public override async Task<User> RemoveAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public override async Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> users)
    {
        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();
        return users;
    }

    public override async Task<IEnumerable<User>> UpdateRangeAsync(IEnumerable<User> users)
    {
        _context.Users.UpdateRange(users);
        await _context.SaveChangesAsync();
        return users;
    }

    public override async Task<IEnumerable<User>> RemoveRangeAsync(IEnumerable<User> users)
    {
        _context.Users.RemoveRange(users);
        await _context.SaveChangesAsync();
        return users;
    }
}