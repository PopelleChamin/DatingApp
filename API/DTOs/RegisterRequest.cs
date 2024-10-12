namespace API.DTOs;
using System.ComponentModel.DataAnnotations;

public class RegisteRequest
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;
}
