namespace API.DataEntities;

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }

    // EF Navigations Properties
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
}