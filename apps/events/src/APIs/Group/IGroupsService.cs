using Events.APIs.Common;
using Events.APIs.Dtos;

namespace Events.APIs;

public interface IGroupsService
{
    /// <summary>
    /// Create one group
    /// </summary>
    public Task<Group> CreateGroup(GroupCreateInput group);

    /// <summary>
    /// Delete one group
    /// </summary>
    public Task DeleteGroup(GroupWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many groups
    /// </summary>
    public Task<List<Group>> Groups(GroupFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about group records
    /// </summary>
    public Task<MetadataDto> GroupsMeta(GroupFindManyArgs findManyArgs);

    /// <summary>
    /// Get one group
    /// </summary>
    public Task<Group> Group(GroupWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one group
    /// </summary>
    public Task UpdateGroup(GroupWhereUniqueInput uniqueId, GroupUpdateInput updateDto);

    /// <summary>
    /// Connect multiple events records to group
    /// </summary>
    public Task ConnectEvents(GroupWhereUniqueInput uniqueId, EventWhereUniqueInput[] eventsId);

    /// <summary>
    /// Disconnect multiple events records from group
    /// </summary>
    public Task DisconnectEvents(GroupWhereUniqueInput uniqueId, EventWhereUniqueInput[] eventsId);

    /// <summary>
    /// Find multiple events records for group
    /// </summary>
    public Task<List<Event>> FindEvents(
        GroupWhereUniqueInput uniqueId,
        EventFindManyArgs EventFindManyArgs
    );

    /// <summary>
    /// Update multiple events records for group
    /// </summary>
    public Task UpdateEvents(GroupWhereUniqueInput uniqueId, EventWhereUniqueInput[] eventsId);
}
