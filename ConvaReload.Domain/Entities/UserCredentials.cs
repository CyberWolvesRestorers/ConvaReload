using System.ComponentModel.DataAnnotations;

namespace ConvaReload.Domain.Entities;

public class UserCredentials
{
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public string? Patronymic { get; set; }
    [EmailAddress] public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? City { get; set; }
    
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}