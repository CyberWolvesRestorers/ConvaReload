using Microsoft.AspNetCore.Identity;

namespace ConvaReload.Domain.Entities;

public class Roles
{
    public int UserId { get; set; }
    public List<IdentityRole> AllRoles { get; set; }
    public IList<string> UserRoles { get; set; }

    public Roles()
    {
        AllRoles = new List<IdentityRole>();
        UserRoles = new List<string>();
    }
}