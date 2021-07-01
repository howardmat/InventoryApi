using Api.Handlers;

namespace Api.Validation.Validators
{
    public abstract class InventoryValidatorBase<T> : IValidator<T> where T : class, new()
    {
        public ResponseHandler ServiceResponse => _serviceResponse;

        private ResponseHandler _serviceResponse;

        public InventoryValidatorBase()
        {
            _serviceResponse = new ResponseHandler();
        }

        public abstract bool IsValid(T item);
    }
}
