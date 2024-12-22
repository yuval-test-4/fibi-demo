using Events.APIs.Dtos;
using Events.APIs.Common;

namespace Events.APIs;

public interface IEventsService
{
    /// <summary>
    /// Create one event
    /// </summary>
    public Task<Event> CreateEvent(EventCreateInput event);
    /// <summary>
    /// Delete one event
    /// </summary>
    public Task DeleteEvent(EventWhereUniqueInput uniqueId);
    /// <summary>
    /// Find many events
    /// </summary>
    public Task<List<Event>> Events(EventFindManyArgs findManyArgs);
    /// <summary>
    /// Meta data about event records
    /// </summary>
    public Task<MetadataDto> EventsMeta(EventFindManyArgs findManyArgs);
    /// <summary>
    /// Get one event
    /// </summary>
    public Task<Event> Event(EventWhereUniqueInput uniqueId);
    /// <summary>
    /// Update one event
    /// </summary>
    public Task UpdateEvent(EventWhereUniqueInput uniqueId, EventUpdateInput updateDto);
    /// <summary>
    /// Get a group record for event
    /// </summary>
    public Task<Group> GetGroup(EventWhereUniqueInput uniqueId);
}
