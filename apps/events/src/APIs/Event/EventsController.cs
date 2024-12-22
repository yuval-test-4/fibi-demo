using Microsoft.AspNetCore.Mvc;

namespace Events.APIs;

[ApiController()]
public class EventsController : EventsControllerBase
{
    public EventsController(IEventsService service)
        : base(service) { }
}
