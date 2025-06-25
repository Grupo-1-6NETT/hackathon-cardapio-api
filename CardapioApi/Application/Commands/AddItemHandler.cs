using Application.Services;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands;
public class AddItemHandler(IRabbitMqService rabbitMq, IConfiguration config) : IRequestHandler<AddItemCommand, Guid>
{
    private readonly string _queue = config["RabbitMQ:AddItemQueueName"] ?? "additem";

    public Task<Guid> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        request.Validate();

        var item = new AddItemDto
        {
            TransportId = Guid.NewGuid(),
            Nome = request.Nome,
            Descricao = request.Descricao,
            Disponivel = request.Disponivel,
            NomeCategoria = request.NomeCategoria,
            Preco = request.Preco
        };

        rabbitMq.Publish(item, _queue);

        return Task.FromResult(item.TransportId);
    }
}
