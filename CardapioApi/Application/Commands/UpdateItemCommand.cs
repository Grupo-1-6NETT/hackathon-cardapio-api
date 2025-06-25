using Application.Exceptions;
using Application.Validators;
using MediatR;

namespace Application.Commands;
public class UpdateItemCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public string? NomeCategoria { get; set; }
    public decimal? Preco { get; set; }
    public bool? Disponivel { get; set; }

    public void Validate()
    {
        var validator = new UpdateItemValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationException(errors);
        }
    }
}
