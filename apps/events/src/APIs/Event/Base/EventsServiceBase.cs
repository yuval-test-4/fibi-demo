using Events.APIs;
using Events.Infrastructure;
using Events.APIs.Dtos;
using Events.Infrastructure.Models;
using Events.APIs.Errors;
using Events.APIs.Extensions;
using Events.APIs.Common;
using Microsoft.EntityFrameworkCore;

namespace Events.APIs;

public abstract class EventsServiceBase : IEventsService
{
    protected readonly EventsDbContext _context;
    public EventsServiceBase(EventsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one event
    /// </summary>
    public async Task<Event> CreateEvent(EventCreateInput createDto)
    {
        var event = new EventDbModel
                    {
                CreatedAt = createDto.CreatedAt,
Message = createDto.Message,
UpdatedAt = createDto.UpdatedAt
};
      
            if (createDto.Id != null){
              event.Id = createDto.Id;
}
            if (createDto.Group != null)
            {
                event.Group = await _context
                    .Groups.Where(group => createDto.Group.Id == group.Id)
                    .FirstOrDefaultAsync();
}

_context.Events.Add(event);
await _context.SaveChangesAsync();

var result = await _context.FindAsync<EventDbModel>(event.Id);
      
              if (result == null)
              {
    throw new NotFoundException();
}
      
              return result.ToDto();
}

/// <summary>
/// Delete one event
/// </summary>
public async Task DeleteEvent(EventWhereUniqueInput uniqueId)
{
    var event = await _context.Events.FindAsync(uniqueId.Id);
    if (event == null)
        {
        throw new NotFoundException();
    }

    _context.Events.Remove(event);
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find many events
/// </summary>
public async Task<List<Event>> Events(EventFindManyArgs findManyArgs)
{
    var events = await _context
          .Events
  .Include(x => x.Group)
  .ApplyWhere(findManyArgs.Where)
  .ApplySkip(findManyArgs.Skip)
  .ApplyTake(findManyArgs.Take)
  .ApplyOrderBy(findManyArgs.SortBy)
  .ToListAsync();
    return events.ConvertAll(event => event.ToDto());
}

/// <summary>
/// Meta data about event records
/// </summary>
public async Task<MetadataDto> EventsMeta(EventFindManyArgs findManyArgs)
{

    var count = await _context
.Events
.ApplyWhere(findManyArgs.Where)
.CountAsync();

    return new MetadataDto { Count = count };
}

/// <summary>
/// Get one event
/// </summary>
public async Task<Event> Event(EventWhereUniqueInput uniqueId)
{
    var events = await this.Events(
              new EventFindManyArgs { Where = new EventWhereInput { Id = uniqueId.Id } }
  );
    var event = events.FirstOrDefault();
    if (event == null)
      {
        throw new NotFoundException();
    }

    return event;
}

/// <summary>
/// Update one event
/// </summary>
public async Task UpdateEvent(EventWhereUniqueInput uniqueId, EventUpdateInput updateDto)
{
    var event = updateDto.ToModel(uniqueId);

    if (updateDto.Group != null)
    {
                event.Group = await _context
                    .Groups.Where(group => updateDto.Group == group.Id)
                    .FirstOrDefaultAsync();
    }

    _context.Entry(event).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Events.Any(e => e.Id == event.Id))
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
/// Get a group record for event
/// </summary>
public async Task<Group> GetGroup(EventWhereUniqueInput uniqueId)
{
    var event = await _context
          .Events.Where(event => event.Id == uniqueId.Id)
  .Include(event => event.Group)
  .FirstOrDefaultAsync();
    if (event == null)
  {
        throw new NotFoundException();
    }
    return event.Group.ToDto();
}

}
