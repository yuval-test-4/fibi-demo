using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Infrastructure.Models;

[Table("Groups")]
public class GroupDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public List<EventDbModel>? Events { get; set; } = new List<EventDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
