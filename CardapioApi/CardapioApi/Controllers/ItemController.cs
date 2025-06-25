using Application.Commands;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardapioApi.Controllers;

//[Authorize]
[Route("[controller]")]
[ApiController]
public class ItemController(ISender sender) : ControllerBase
{
    //[Authorize]
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
		catch (Exception ex)
		{
			return StatusCode(500, new { Message = $"Ocorreu um erro interno no servidor." });
        }
    }
}
