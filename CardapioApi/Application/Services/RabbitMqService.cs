using Domain;
using MassTransit;

namespace Application.Services;
public class RabbitMqService(IBus bus) : IRabbitMqService
{    
    public async Task Publish<T>(T message, string queue) where T : class
    {
        var endpoint = await bus.GetSendEndpoint(new Uri($"queue:{queue}"));
        await endpoint.Send(message);
    }
}
