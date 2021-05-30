using Api.Models;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public interface IValidatorAsync<T> where T : class, new()
    {
        ServiceResponse ServiceResponse { get; }
        Task<bool> IsValidAsync(T item);
    }
}
