using Application.Commands;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardapioApi.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class ItemController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Adiciona um Item na base de dados 
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///  {
    ///     "nome": "Hamburguer de Siri",
    ///     "descricao": "Especialidade do Crusty Crab",
    ///     "nomeCategoria": "Lanche",
    ///     "preco" : "35.99",
    ///     "disponivel" : "true"
    ///  }
    /// </remarks>
    /// <param name="command">Comando com os dados do Item</param>
    /// <returns>O Id do Item adicionado</returns>
    /// <response code="201">Item adicionado na base de dados</response>
    /// <response code="400">Falha ao processar a requisição</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="403">Usuário não autorizado</response>
    /// <response code="500">Erro inesperado</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpPost]
    public IActionResult Post([FromBody] AddItemCommand request)
    {
		try
		{
			var itemId = sender.Send(request);
			return Created("", itemId);
		}
		catch (ValidationException ex)
		{
			return BadRequest(new { ex.Errors});
		}
		catch (Exception)
		{
			return StatusCode(500, new { Message = $"Ocorreu um erro interno no servidor." });
        }
    }

    /// <summary>
    /// Atualiza um Item na base de dados 
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///  {
    ///     "id": "1991dcff-06a9-4b09-9e16-79f76055a21b",
    ///     "nome": "Hamburguer de Siri",
    ///     "descricao": "Especialidade do Crusty Crab",
    ///     "nomeCategoria": "Lanche",
    ///     "preco" : "35.99",
    ///     "disponivel" : "true"
    ///  }
    /// </remarks>
    /// <param name="command">Comando com os dados do Item</param>
    /// <returns>O Id do Item atualizado</returns>
    /// <response code="200">Item atualizado na base de dados</response>
    /// <response code="400">Falha ao processar a requisição</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="403">Usuário não autorizado</response>
    /// <response code="404">Item não encontrado</response>
    /// <response code="500">Erro inesperado</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpPatch]
    public IActionResult Patch([FromBody] UpdateItemCommand request)
    {
        try
        {
            var itemId = sender.Send(request);
            return Ok(itemId);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { ex.Errors });
        }
        catch (Exception)
        {
            return StatusCode(500, new { Message = $"Ocorreu um erro interno no servidor." });
        }
    }
}
