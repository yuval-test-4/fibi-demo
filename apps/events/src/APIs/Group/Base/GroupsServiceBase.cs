using Events.APIs;
using Events.Infrastructure;
using Events.APIs.Dtos;
using Events.Infrastructure.Models;
using Events.APIs.Errors;
using Events.APIs.Extensions;
using Events.APIs.Common;
using Microsoft.EntityFrameworkCore;

namespace Events.APIs;

public abstract class GroupsServiceBase : IGroupsService
{
    protected readonly EventsDbContext _context;
    public GroupsServiceBase(EventsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one group
    /// </summary>
    public async Task<Group> CreateGroup(GroupCreateInput createDto)
    {
        var group = new GroupDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            group.Id = createDto.Id;
        }
        if (createDto.Events != null)
        {
            group.Events = await _context
                .Events.Where(event => createDto.Events.Select(t => t.Id).Contains(event.Id))
                      .ToListAsync();
              }

_context.Groups.Add(group);
              await _context.SaveChangesAsync();

var result = await _context.FindAsync<GroupDbModel>(group.Id);
      
              if (result == null)
              {
                  throw new NotFoundException();
              }
      
              return result.ToDto();}

    /// <summary>
    /// Delete one group
    /// </summary>
    public async Task DeleteGroup(GroupWhereUniqueInput uniqueId)
{
    var group = await _context.Groups.FindAsync(uniqueId.Id);
    if (group == null)
    {
        throw new NotFoundException();
    }

    _context.Groups.Remove(group);
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find many groups
/// </summary>
public async Task<List<Group>> Groups(GroupFindManyArgs findManyArgs)
{
    var groups = await _context
          .Groups
  .Include(x => x.Events)
  .ApplyWhere(findManyArgs.Where)
  .ApplySkip(findManyArgs.Skip)
  .ApplyTake(findManyArgs.Take)
  .ApplyOrderBy(findManyArgs.SortBy)
  .ToListAsync();
    return groups.ConvertAll(group => group.ToDto());
}

/// <summary>
/// Meta data about group records
/// </summary>
public async Task<MetadataDto> GroupsMeta(GroupFindManyArgs findManyArgs)
{

    var count = await _context
.Groups
.ApplyWhere(findManyArgs.Where)
.CountAsync();

    return new MetadataDto { Count = count };
}

/// <summary>
/// Get one group
/// </summary>
public async Task<Group> Group(GroupWhereUniqueInput uniqueId)
{
    var groups = await this.Groups(
              new GroupFindManyArgs { Where = new GroupWhereInput { Id = uniqueId.Id } }
  );
    var group = groups.FirstOrDefault();
    if (group == null)
    {
        throw new NotFoundException();
    }

    return group;
}

/// <summary>
/// Update one group
/// </summary>
public async Task UpdateGroup(GroupWhereUniqueInput uniqueId, GroupUpdateInput updateDto)
{
    var group = updateDto.ToModel(uniqueId);

    if (updateDto.Events != null)
    {
        group.Events = await _context
            .Events.Where(event => updateDto.Events.Select(t => t).Contains(event.Id))
            .ToListAsync();
    }

    _context.Entry(group).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Groups.Any(e => e.Id == group.Id))
        {
            throw new NotFoundException();
        }
        else
        {
            throw;
        }
    }
}

/// <summary>
/// Connect multiple events records to group
/// </summary>
public async Task ConnectEvents(GroupWhereUniqueInput uniqueId, EventWhereUniqueInput[] childrenIds)
{
    var parent = await _context
          .Groups.Include(x => x.Events)
  .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Events.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();
    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    var childrenToConnect = children.Except(parent.Events);

    foreach (var child in childrenToConnect)
    {
        parent.Events.Add(child);
    }

    await _context.SaveChangesAsync();
}

/// <summary>
/// Disconnect multiple events records from group
/// </summary>
public async Task DisconnectEvents(GroupWhereUniqueInput uniqueId, EventWhereUniqueInput[] childrenIds)
{
    var parent = await _context
                            .Groups.Include(x => x.Events)
                    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Events.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();

    foreach (var child in children)
    {
        parent.Events?.Remove(child);
    }
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find multiple events records for group
/// </summary>
public async Task<List<Event>> FindEvents(GroupWhereUniqueInput uniqueId, EventFindManyArgs groupFindManyArgs)
{
    var events = await _context
          .Events
  .Where(m => m.GroupId == uniqueId.Id)
  .ApplyWhere(groupFindManyArgs.Where)
  .ApplySkip(groupFindManyArgs.Skip)
  .ApplyTake(groupFindManyArgs.Take)
  .ApplyOrderBy(groupFindManyArgs.SortBy)
  .ToListAsync();

    return events.Select(x => x.ToDto()).ToList();
}

/// <summary>
/// Update multiple events records for group
/// </summary>
public async Task UpdateEvents(GroupWhereUniqueInput uniqueId, EventWhereUniqueInput[] childrenIds)
{
    var group = await _context
            .Groups.Include(t => t.Events)
    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (group == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Events.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
      .ToListAsync();

    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    group.Events = children;
    await _context.SaveChangesAsync();
}

}
