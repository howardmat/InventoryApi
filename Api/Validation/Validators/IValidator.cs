using Api.Models.Results;

namespace Api.Validation.Validators;

public interface IValidator<T> where T : class, new()
{
    ServiceResult ServiceResponse { get; }
    bool IsValid(T item);
}
