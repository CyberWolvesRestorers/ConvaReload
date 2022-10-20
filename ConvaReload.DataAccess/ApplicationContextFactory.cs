using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConvaReload.DataAccess;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=convadb;Username=postgres;Password=1");

        return new ApplicationContext(optionsBuilder.Options);
    }
}