using System.Threading.Tasks;
using Events.Brokers.Infrastructure;

namespace Events.Brokers.Mymessagebroker;

public class MymessagebrokerMessageHandlersController
{
    [Topic("eventcreated")]
    public Task HandleEventcreated(string message)
    {
        //set your message handling logic here

        return Task.CompletedTask;
    }
}
