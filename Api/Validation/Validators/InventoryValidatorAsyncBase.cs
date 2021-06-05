using Api.Models;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public abstract class InventoryValidatorAsyncBase<T> : IValidatorAsync<T> 
    {
        public ServiceResponse ServiceResponse => _serviceResponse;

        private ServiceResponse _serviceResponse;

        public InventoryValidatorAsyncBase()
        {
            _serviceResponse = new ServiceResponse();
        }

        public abstract Task<bool> IsValidAsync(T item);
    }
}
