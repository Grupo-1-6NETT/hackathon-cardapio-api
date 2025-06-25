using Application.Exceptions;
using Application.Validators;
using MediatR;

namespace Application.Commands;
public class AddItemCommand : IRequest<Guid>
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string NomeCategoria { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public bool Disponivel { get; set; }

    public void Validate()
    {
        var validator = new AddItemValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationException(errors);
        }
    }
}
