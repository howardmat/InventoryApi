using Api.Models;

namespace Api.Validation.Validators
{
    public interface IValidator<T> where T : class, new()
    {
        ServiceResponse ServiceResponse { get; }
        bool IsValid(T item);
    }
}
