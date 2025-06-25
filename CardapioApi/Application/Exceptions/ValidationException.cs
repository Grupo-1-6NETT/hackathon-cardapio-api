namespace Application.Exceptions;

public class ValidationException(List<string> errors) : Exception("Erro(s) de validação encontrados.")
{
    public List<string> Errors { get; } = errors;
}
