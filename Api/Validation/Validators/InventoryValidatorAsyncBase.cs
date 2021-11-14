using Api.Models.Results;
using System.Threading.Tasks;

namespace Api.Validation.Validators;

public abstract class InventoryValidatorAsyncBase<T> : IValidatorAsync<T> 
{
    public ServiceResult ServiceResponse => _serviceResponse;

    private ServiceResult _serviceResponse;

    public InventoryValidatorAsyncBase()
    {
        _serviceResponse = new ServiceResult();
    }

    public abstract Task<bool> IsValidAsync(T item);
}
