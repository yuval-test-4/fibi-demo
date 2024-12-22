using Events.Infrastructure;

namespace Events.APIs;

public class EventsService : EventsServiceBase
{
    public EventsService(EventsDbContext context)
        : base(context) { }
}
