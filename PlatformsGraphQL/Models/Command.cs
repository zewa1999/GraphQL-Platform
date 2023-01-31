using System.ComponentModel.DataAnnotations;

namespace PlatformsGraphQL.Models;

public class Command
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public required string HowTo { get; set; }
    [Required]
    public required string CommandLine { get; set; }
    [Required]
    public required Guid PlatformId { get; set; }
    public Platform Platform { get; set; } = null!;

}
