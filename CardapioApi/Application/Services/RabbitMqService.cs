using Domain;
using MassTransit;

namespace Application.Services;
public class RabbitMqService(IBus bus) : IRabbitMqService
{    
    public async Task PublishAdd(AddItemDto message, string queue)
    {
        var endpoint = await bus.GetSendEndpoint(new Uri($"queue:{queue}"));
        await endpoint.Send(message);
    }
}
