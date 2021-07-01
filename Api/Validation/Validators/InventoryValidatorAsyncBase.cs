using Api.Handlers;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public abstract class InventoryValidatorAsyncBase<T> : IValidatorAsync<T> 
    {
        public ResponseHandler ServiceResponse => _serviceResponse;

        private ResponseHandler _serviceResponse;

        public InventoryValidatorAsyncBase()
        {
            _serviceResponse = new ResponseHandler();
        }

        public abstract Task<bool> IsValidAsync(T item);
    }
}
