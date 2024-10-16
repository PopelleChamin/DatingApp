namespace API.Entities;

using API.Extensions;

public class AppUser
{
    public int Id { get; set; }

    public required string UserNane { get; set; }
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public DateOnly Birthday { get; set; }
    public required string KnowAs { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public required string Gender { get; set; }
    public string? Introduction { get; set; }
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public List<Photo> Photos { get; set; } = [];

    public int GetAge() => Birthday.CalculateAge();
}

