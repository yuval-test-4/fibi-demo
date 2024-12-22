using EventsWorker.Infrastructure;
using EventsWorker.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWorker.APIs;

public class EventDataService : EventDataServiceBase
{
    public EventDataService(EventsWorkerDbContext context)
        : base(context) { }
}
