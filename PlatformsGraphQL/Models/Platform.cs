using System.ComponentModel.DataAnnotations;

namespace PlatformsGraphQL.Models;

public class Platform
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? LicenseKey { get; set; }
    public ICollection<Command> Commands { get; set; } = new List<Command>();

}
