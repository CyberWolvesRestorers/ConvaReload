using System.ComponentModel.DataAnnotations;

namespace ConvaReload.Domain.Entities;

public class UserCredentials
{
    [Required] public string Name { get; set; } = "";
    [Required] public string Surname { get; set; } = "";
    public string? Patronymic { get; set; }
    [EmailAddress] public string? Email { get; set; }
    [Phone] public string? Phone { get; set; }
    public string? City { get; set; }
    
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}