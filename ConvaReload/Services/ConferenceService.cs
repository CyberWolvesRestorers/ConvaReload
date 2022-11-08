using ConvaReload.DataAccess;
using ConvaReload.Repositories;
using ConvaReload.Services.Abstract;

namespace ConvaReload.Services;

public class ConferenceService : IConferenceService
{
    private readonly ApplicationContext _context;
    private readonly IHttpContextAccessor _accessor;
    private readonly ConferenceRepository _conferenceRepository;

    public ConferenceService(ApplicationContext context, IHttpContextAccessor accessor)
    {
        _context = context;
        _accessor = accessor;
        _conferenceRepository = new ConferenceRepository(context);
    }

    public ConferenceRepository GetRepository() => _conferenceRepository;
}