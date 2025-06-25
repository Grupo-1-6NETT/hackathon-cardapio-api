using Domain;

namespace Application.Services;

public interface IRabbitMqService
{
    Task Publish<T>(T message, string queue) where T : class;    
}
