namespace Events.APIs.Dtos;

public class GroupCreateInput
{
    public DateTime CreatedAt { get; set; }

    public List<Event>? Events { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}
