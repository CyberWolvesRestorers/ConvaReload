using ConvaReload.Abstract;
using ConvaReload.Repositories;

namespace ConvaReload.Services.Abstract;

public interface IConferenceService : IService
{
    ConferenceRepository GetRepository();
}