using ConvaReload.Repositories;

namespace ConvaReload.Services.Abstract;

public interface IUserService
{
    UserRepository GetRepository();
    string GetMyName();
}