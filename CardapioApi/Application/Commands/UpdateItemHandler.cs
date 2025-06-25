using Application.Services;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Commands;
public class UpdateItemHandler(IRabbitMqService rabbitMq, IConfiguration config) : IRequestHandler<UpdateItemCommand, Guid>
{
    private readonly string _queue = config["RabbitMQ:UpdateItemQueueName"] ?? "updateitem";

    public Task<Guid> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        request.Validate();

        var item = new UpdateItemDto
        {
            Id = request.Id,
            Nome = request.Nome,
            Descricao = request.Descricao,
            Disponivel = request.Disponivel,
            NomeCategoria = request.NomeCategoria,
            Preco = request.Preco
        };

        rabbitMq.Publish(item, _queue);

        return Task.FromResult(item.Id);
    }
}
