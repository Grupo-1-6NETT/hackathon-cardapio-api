using Application.Commands;
using Application.Exceptions;
using Application.Services;
using Domain;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Application;
public class AddItemHandlerTests
{
    private readonly Mock<IRabbitMqService> _rabbitMqServiceMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AddItemHandler _sut;

    public AddItemHandlerTests()
    {
        _rabbitMqServiceMock = new Mock<IRabbitMqService>();
        _configurationMock = new Mock<IConfiguration>();
        _sut = new(_rabbitMqServiceMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task Handle_InformadosDadosValidos_DeveRetornarOk()
    {
        var command = new AddItemCommand
        {
            Nome = "Novo Item",
            Descricao = "Test",
            Disponivel = true,
            NomeCategoria = "CategoriaA",
            Preco = 1.00m
        };

        var result = await _sut.Handle(command, CancellationToken.None);

        _rabbitMqServiceMock.Verify(x => x.Publish(It.IsAny<AddItemDto>(), It.IsAny<string>()), Times.Once);

        _rabbitMqServiceMock.Invocations.Clear();
    }

    [Fact]
    public async Task Handle_InformadosDadosInvalidos_ValidationException()
    {
        var command = new AddItemCommand
        {
            Nome = "Novo Item com nome grande justamente pra que a validação falhe"
        };

        await Assert.ThrowsAsync<ValidationException>(async () => await _sut.Handle(command, CancellationToken.None));
    }
}
