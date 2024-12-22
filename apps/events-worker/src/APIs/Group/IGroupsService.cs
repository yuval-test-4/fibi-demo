using EventsWorker.APIs.Common;
using EventsWorker.APIs.Dtos;

namespace EventsWorker.APIs;

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
    /// Connect multiple EventData records to group
    /// </summary>
    public Task ConnectEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumWhereUniqueInput[] eventDataId
    );

    /// <summary>
    /// Disconnect multiple EventData records from group
    /// </summary>
    public Task DisconnectEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumWhereUniqueInput[] eventDataId
    );

    /// <summary>
    /// Find multiple EventData records for group
    /// </summary>
    public Task<List<EventDatum>> FindEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumFindManyArgs EventDatumFindManyArgs
    );

    /// <summary>
    /// Update multiple EventData records for group
    /// </summary>
    public Task UpdateEventData(
        GroupWhereUniqueInput uniqueId,
        EventDatumWhereUniqueInput[] eventDataId
    );
}
