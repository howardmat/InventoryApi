using Api.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Api.Models
{
    public class ServiceResponse
    {
        private ServiceResponseStatus _status = ServiceResponseStatus.Success;
        private ICollection<string> _errors = new HashSet<string>();

        public ServiceResponse() { }

        public ServiceResponse(ServiceResponseStatus status, ICollection<string> errors)
        {
            _status = status;
            _errors = errors;
        }

        public ServiceResponseStatus GetStatus() => _status;

        public ServiceResponseStatus SetError() 
        {
            _status = ServiceResponseStatus.Error;

            return _status;
        }

        public void SetError(string error)
        {
            _status = ServiceResponseStatus.Error;

            _errors.Add(error);
        }

        public void SetError(IEnumerable<string> errors)
        {
            _status = ServiceResponseStatus.Error;

            foreach (var error in errors)
                _errors.Add(error);
        }

        public void SetNotFound()
        {
            _status = ServiceResponseStatus.NotFound;
        }

        public void SetNotFound(string error)
        {
            _status = ServiceResponseStatus.NotFound;

            _errors.Add(error);
        }

        public void SetNotFound(IEnumerable<string> errors)
        {
            _status = ServiceResponseStatus.NotFound;

            foreach (var error in errors)
                _errors.Add(error);
        }

        public void SetException()
        {
            _status = ServiceResponseStatus.Exception;
        }

        public void SetException(string error)
        {
            _status = ServiceResponseStatus.Exception;

            _errors.Add(error);
        }

        public void SetException(IEnumerable<string> errors)
        {
            _status = ServiceResponseStatus.Exception;

            foreach (var error in errors)
                _errors.Add(error);
        }

        public string[] GetErrors()
        {
            return _errors.ToArray();
        }

        public string GetErrorJson()
        {
            return JsonSerializer.Serialize(_errors);
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }

        public ServiceResponse ToNonGeneric()
        {
            return new ServiceResponse(GetStatus(), GetErrors());
        }
    }
}
