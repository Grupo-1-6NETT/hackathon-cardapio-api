using Application.Commands;
using FluentValidation;

namespace Application.Validators;
public class DeleteItemValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().WithMessage("Um Id válido deve ser informado");
    }
}
