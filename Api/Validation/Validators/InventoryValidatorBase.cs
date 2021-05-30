using Api.Models;

namespace Api.Validation.Validators
{
    public abstract class InventoryValidatorBase<T> : IValidator<T> where T : class, new()
    {
        public ServiceResponse ServiceResponse => _serviceResponse;

        private ServiceResponse _serviceResponse;

        public InventoryValidatorBase()
        {
            _serviceResponse = new ServiceResponse();
        }

        public abstract bool IsValid(T item);
    }
}
