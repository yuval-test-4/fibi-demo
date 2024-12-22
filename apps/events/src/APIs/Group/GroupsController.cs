using Microsoft.AspNetCore.Mvc;

namespace Events.APIs;

[ApiController()]
public class GroupsController : GroupsControllerBase
{
    public GroupsController(IGroupsService service)
        : base(service) { }
}
