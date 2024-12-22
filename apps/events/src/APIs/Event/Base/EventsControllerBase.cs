using Microsoft.AspNetCore.Mvc;
using Events.APIs;
using Events.APIs.Dtos;
using Events.APIs.Errors;
using Events.APIs.Common;

namespace Events.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EventsControllerBase : ControllerBase
{
    protected readonly IEventsService _service;
    public EventsControllerBase(IEventsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one event
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Event>> CreateEvent(EventCreateInput input)
    {
        var event = await _service.CreateEvent(input);
        
    return CreatedAtAction(nameof(Event), new { id = event.Id }, event); }

    /// <summary>
    /// Delete one event
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteEvent([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteEvent(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many events
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Event>>> Events([FromQuery()]
    EventFindManyArgs filter)
    {
        return Ok(await _service.Events(filter));
    }

    /// <summary>
    /// Meta data about event records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EventsMeta([FromQuery()]
    EventFindManyArgs filter)
    {
        return Ok(await _service.EventsMeta(filter));
    }

    /// <summary>
    /// Get one event
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Event>> Event([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Event(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one event
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateEvent([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    EventUpdateInput eventUpdateDto)
    {
        try
        {
            await _service.UpdateEvent(uniqueId, eventUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a group record for event
    /// </summary>
    [HttpGet("{Id}/group")]
    public async Task<ActionResult<List<Group>>> GetGroup([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        var group = await _service.GetGroup(uniqueId);
        return Ok(group);
    }

}
