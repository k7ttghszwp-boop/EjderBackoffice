namespace Ejder.Domain.Entities;

public class BackofficeUser
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    // Seed bunu set ediyor
    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "User";

    public bool IsActive { get; set; } = true;
}
