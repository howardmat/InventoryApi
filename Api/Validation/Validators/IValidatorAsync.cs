using Api.Models.Results;
using System.Threading.Tasks;

namespace Api.Validation.Validators;

public interface IValidatorAsync<T>
{
    ServiceResult ServiceResponse { get; }
    Task<bool> IsValidAsync(T item);
}
