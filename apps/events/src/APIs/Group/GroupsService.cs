using Events.Infrastructure;

namespace Events.APIs;

public class GroupsService : GroupsServiceBase
{
    public GroupsService(EventsDbContext context)
        : base(context) { }
}
