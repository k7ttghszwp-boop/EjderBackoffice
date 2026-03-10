namespace Ejder.Domain.Entities;
using Ejder.Domain;

public class BackofficeUser : BaseEntity
{
    public string Email { get; set; } = string.Empty;

    // Seed bunu set ediyor
    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "User";

    public bool IsActive { get; set; } = true;
}
