using System.Linq.Expressions;
using ConvaReload.Domain.Entities;

namespace ConvaReload.Services.Abstract;

public interface IUserService
{
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> exp);
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<User> RemoveAsync(User user);
    Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> users);
    Task<IEnumerable<User>> RemoveRangeAsync(IEnumerable<User> users);
    Task<IEnumerable<User>> UpdateRangeAsync(IEnumerable<User> users);
    string GetMyName();
}