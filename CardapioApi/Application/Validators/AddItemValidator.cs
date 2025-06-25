using Application.Commands;
using FluentValidation;

namespace Application.Validators;
public class AddItemValidator : AbstractValidator<AddItemCommand>
{
    public AddItemValidator()
    {
        RuleFor(i => i.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100).WithMessage("O Nome pode ter no máximo 100 caracteres");
        RuleFor(i => i.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória")
            .MaximumLength(256).WithMessage("Descrição pode ter no máximo 256 caracteres");
        RuleFor(i => i.NomeCategoria)
            .NotEmpty().WithMessage("NomeCategoria é obrigatório")
            .MaximumLength(100).WithMessage("O NomeCategoria pode ter no máximo 100 caracteres");
        RuleFor(i => i.Preco)
            .GreaterThan(0).WithMessage("Preço deve ser maior que zero");

    }
}
