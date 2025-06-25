using Application.Commands;
using FluentValidation;

namespace Application.Validators;
public class UpdateItemValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().WithMessage("Um Id válido deve ser informado");

        RuleFor(i => i.Nome)            
            .MaximumLength(100).WithMessage("O Nome pode ter no máximo 100 caracteres")
            .When(i => !string.IsNullOrWhiteSpace(i.Nome));

        RuleFor(i => i.Descricao)            
            .MaximumLength(256).WithMessage("Descrição pode ter no máximo 256 caracteres")
            .When(i => !string.IsNullOrWhiteSpace(i.Descricao));

        RuleFor(i => i.NomeCategoria)            
            .MaximumLength(100).WithMessage("O NomeCategoria pode ter no máximo 100 caracteres")
            .When(i => !string.IsNullOrWhiteSpace(i.NomeCategoria));

        RuleFor(i => i.Preco)
            .GreaterThan(0).WithMessage("Preço deve ser maior que zero")
            .When( i => i.Preco != null);

    }
}
