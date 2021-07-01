using Api.Handlers;

namespace Api.Validation.Validators
{
    public interface IValidator<T> where T : class, new()
    {
        ResponseHandler ServiceResponse { get; }
        bool IsValid(T item);
    }
}
