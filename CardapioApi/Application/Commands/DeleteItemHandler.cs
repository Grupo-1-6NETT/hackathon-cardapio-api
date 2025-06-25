using Application.Services;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands;
public class DeleteItemHandler(IRabbitMqService rabbitMq, IConfiguration config) : IRequestHandler<DeleteItemCommand, Guid>
{
    private readonly string _queue = config["RabbitMQ:DeleteItemQueueName"] ?? "deleteitem";

    public Task<Guid> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        request.Validate();

        var item = new DeleteItemDto
        {
            Id = request.Id,
        };

        rabbitMq.Publish(item, _queue);

        return Task.FromResult(item.Id);
    }
}
