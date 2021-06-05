using Api.Models;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public interface IValidatorAsync<T>
    {
        ServiceResponse ServiceResponse { get; }
        Task<bool> IsValidAsync(T item);
    }
}
