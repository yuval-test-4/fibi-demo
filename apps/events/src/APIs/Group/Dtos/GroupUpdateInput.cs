namespace Events.APIs.Dtos;

public class GroupUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public List<string>? Events { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
