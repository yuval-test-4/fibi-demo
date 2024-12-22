using System.Text.Json;
using EventsWorker.APIs;
using EventsWorker.Brokers.Infrastructure;

namespace EventsWorker.Brokers.Mymessagebroker;

public class EventMessage
{
    public required string Message { get; set; }
    public string? Group { get; set; }
}

public class MymessagebrokerMessageHandlersController
{
    //protected readonly IEventDataService _eventDataService;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MymessagebrokerMessageHandlersController(
        IEventDataService service,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        //_eventDataService = service;
        _serviceScopeFactory = serviceScopeFactory;
    }

    [Topic("eventcreated")]
    public async Task HandleEventcreated(string message)
    {
        try
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var innerJson = JsonSerializer.Deserialize<string>(message);
            var eventMessage = JsonSerializer.Deserialize<EventMessage>(innerJson);

            if (eventMessage == null)
            {
                eventMessage = new EventMessage { Message = message, Group = null };
            }

            var eventDataService = scope.ServiceProvider.GetRequiredService<IEventDataService>();

            await eventDataService.HandleEventData(
                    eventMessage.Message,
                    eventMessage.Group
            );
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            //throw;
        }
        return;
    }
}
