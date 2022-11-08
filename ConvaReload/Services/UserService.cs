using System.Linq.Expressions;
using System.Security.Claims;
using ConvaReload.Abstract;
using ConvaReload.Domain.Entities;
using ConvaReload.Services.Abstract;

namespace ConvaReload.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _accessor;
    private readonly IUserRepository _userRepository;

    public UserService(IHttpContextAccessor accessor, IUserRepository userRepository)
    {
        _accessor = accessor;
        _userRepository = userRepository;
    }
    
    public async Task<User> GetByIdAsync(int id) => await _userRepository.GetByIdAsync(id);
    public async Task<IEnumerable<User>> GetAllAsync() => await _userRepository.GetAllAsync();
    public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> exp) => await _userRepository.FindAsync(exp);
    public async Task<User> AddAsync(User user) => await _userRepository.AddAsync(user);
    public async Task<User> UpdateAsync(User user) => await _userRepository.UpdateAsync(user);
    public async Task<User> RemoveAsync(User user) => await _userRepository.RemoveAsync(user);
    public async Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> users) => await _userRepository.AddRangeAsync(users);
    public async Task<IEnumerable<User>> UpdateRangeAsync(IEnumerable<User> users) => await _userRepository.UpdateRangeAsync(users);
    public async Task<IEnumerable<User>> RemoveRangeAsync(IEnumerable<User> users) => await _userRepository.RemoveRangeAsync(users);
    
    public string GetMyName()
    {
        string result = string.Empty;
        
        if (_accessor.HttpContext != null)
        {
            result = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        return result;
    }
}