using System.Threading.Tasks;
using EventsApi.Brokers.Infrastructure;

namespace EventsApi.Brokers.Mymessagebroker;

public class MymessagebrokerMessageHandlersController
{
    [Topic("eventcreated")]
    public Task HandleEventcreated(string message)
    {
        //set your message handling logic here

        return Task.CompletedTask;
    }
}
