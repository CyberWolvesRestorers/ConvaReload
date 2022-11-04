using System.Linq.Expressions;
using System.Security.Claims;
using ConvaReload.Abstract;
using ConvaReload.DataAccess;
using ConvaReload.Domain.Entities;
using ConvaReload.Repositories;
using ConvaReload.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ConvaReload.Services;

public class UserService : IUserService
{
    private readonly ApplicationContext _context;
    private readonly IHttpContextAccessor _accessor;
    private readonly UserRepository _userRepository;

    public UserService(ApplicationContext context, IHttpContextAccessor accessor)
    {
        _context = context;
        _accessor = accessor;
        _userRepository = new UserRepository(context);
    }

    public UserRepository GetRepository() => _userRepository;

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