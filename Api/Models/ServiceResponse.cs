using Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ServiceResponse
    {
        private ServiceResponseStatus _status = ServiceResponseStatus.Success;
        private ICollection<string> _errors = new HashSet<string>();

        public ServiceResponse() { }

        public ServiceResponse(ICollection<string> errors)
        {
            _errors = errors;
        }

        public ServiceResponse(ServiceResponseStatus status)
        {
            _status = status;
        }

        public ServiceResponse(ServiceResponseStatus status, ICollection<string> errors)
        {
            _status = status;
            _errors = errors;
        }

        public ServiceResponseStatus GetStatus() => _status;
        public ServiceResponseStatus SetError() => _status = ServiceResponseStatus.Error;
        public ServiceResponseStatus SetNotFound() => _status = ServiceResponseStatus.NotFound;
        public ServiceResponseStatus SetException() => _status = ServiceResponseStatus.Exception;

        public string[] GetErrors()
        {
            return _errors.ToArray();
        }

        public string GetErrorJson()
        {
            return JsonSerializer.Serialize(_errors);
        }

        public void AddError(string error)
        {
            _errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
                _errors.Add(error);
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
