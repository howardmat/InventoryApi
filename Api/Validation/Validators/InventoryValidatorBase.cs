using Api.Models.Results;

namespace Api.Validation.Validators;

public abstract class InventoryValidatorBase<T> : IValidator<T> where T : class, new()
{
    public ServiceResult ServiceResponse => _serviceResponse;

    private readonly ServiceResult _serviceResponse;

    public InventoryValidatorBase()
    {
        _serviceResponse = new ServiceResult();
    }

    public abstract bool IsValid(T item);
}
