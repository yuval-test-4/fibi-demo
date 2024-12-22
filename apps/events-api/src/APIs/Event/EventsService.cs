using EventsApi.Brokers.Mymessagebroker;
using EventsApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EventsApi.APIs;

public class EventsService : EventsServiceBase
{
    private readonly MymessagebrokerProducerService _producerService;

    public EventsService(EventsApiDbContext context, MymessagebrokerProducerService producerService)
        : base(context)
    {
        _producerService = producerService;
    }

    public override async Task<string> CreateEvent(EventCreateInput eventCreateInputDto)
    {
        var topic = "eventcreated";

        await _producerService.ProduceAsync(topic, eventCreateInputDto);

        return "Event created";
    }
}
