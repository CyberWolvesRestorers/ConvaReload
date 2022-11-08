using ConvaReload.Abstract;
using ConvaReload.Repositories;

namespace ConvaReload.Services.Abstract;

public interface IUserService : IService
{
    UserRepository GetRepository();
    string GetMyName();
}