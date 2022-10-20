using ConvaReload.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConvaReload.DataAccess;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Conference> Conferences { get; set; }
}