using Api.Handlers;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public interface IValidatorAsync<T>
    {
        ResponseHandler ServiceResponse { get; }
        Task<bool> IsValidAsync(T item);
    }
}
