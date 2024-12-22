using Events.APIs.Common;
using Events.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class EventFindManyArgs : FindManyInput<Event, EventWhereInput> { }
