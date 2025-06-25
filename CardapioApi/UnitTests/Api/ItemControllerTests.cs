using CardapioApi.Controllers;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Commands;

namespace UnitTests.Api;

public class ItemControllerTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly ItemController _sut;

    public ItemControllerTests()
    {
        _senderMock = new Mock<ISender>();
        _sut = new(_senderMock.Object);

    }

    #region Post
    [Fact]
    public async Task Post_InformadosDadosValidos_DeveRetornarCreatedResult()
    {
        var guid = Guid.Empty;

        var command = new AddItemCommand
        {
            Nome = "Novo Item",
            Descricao = "Test",
            Disponivel = true,
            NomeCategoria = "CategoriaA",
            Preco = 1.00m            
        };
        _senderMock.Setup(m => m.Send(It.IsAny<AddItemCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(guid);

        var result = _sut.Post(command);
        var createdResult = Assert.IsType<CreatedResult>(result);
        var resultValue = await ((Task<Guid>)createdResult.Value!)!;

        Assert.Equal(guid, resultValue);
    }

    [Fact]
    public void Post_DadosInvalidos_DeveRetornarBadRequest()
    {
        var command = new AddItemCommand
        {
            Nome = "Novo Item com nome grande justamente pra que a validação falhe"            
        };

        _senderMock
            .Setup(m => m.Send(It.IsAny<AddItemCommand>(), It.IsAny<CancellationToken>()))
            .Throws(new ValidationException(new List<string>()));

        var result = _sut.Post(command);

        Assert.IsType<BadRequestObjectResult>(result);
    }
    #endregion Post

    #region Patch
    [Fact]
    public async Task Patch_InformadosDadosValidos_DeveRetornarOk()
    {
        var guid = Guid.NewGuid();

        var command = new UpdateItemCommand
        {
            Id = guid,
            Nome = "Novo nome",
            Descricao = "Nova descricao",
            Disponivel = true,
            NomeCategoria = "CategoriaB",
            Preco = 1.00m
        };
        _senderMock.Setup(m => m.Send(It.IsAny<UpdateItemCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(guid);

        var result = _sut.Patch(command);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var resultValue = await ((Task<Guid>)okResult.Value!)!;

        Assert.Equal(guid, resultValue);
    }

    [Fact]
    public void Patch_DadosInvalidos_DeveRetornarBadRequest()
    {
        var guid = Guid.Empty;
        var command = new UpdateItemCommand
        {
            Id = guid,
            Nome = "Novo Item com nome grande justamente pra que a validação falhe"
        };

        _senderMock
            .Setup(m => m.Send(It.IsAny<UpdateItemCommand>(), It.IsAny<CancellationToken>()))
            .Throws(new ValidationException(new List<string>()));

        var result = _sut.Patch(command);

        Assert.IsType<BadRequestObjectResult>(result);
    }
    #endregion Patch
}