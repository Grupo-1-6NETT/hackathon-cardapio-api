using Domain;

namespace Application.Services;

public interface IRabbitMqService
{
    Task PublishAdd(AddItemDto message, string queue);
}
