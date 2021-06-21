using Api.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            SetError();

            _errors.Add(error);
        }

        public void SetError(IEnumerable<string> errors)
        {
            SetError();

            foreach (var error in errors)
                _errors.Add(error);
        }

        public void SetNotFound()
        {
            _status = ServiceResponseStatus.NotFound;
        }

        public void SetNotFound(string error)
        {
            SetNotFound();

            _errors.Add(error);
        }

        public void SetNotFound(IEnumerable<string> errors)
        {
            SetNotFound();

            foreach (var error in errors)
                _errors.Add(error);
        }

        public void SetException()
        {
            _status = ServiceResponseStatus.Exception;
        }

        public void SetException(string error)
        {
            SetException();

            _errors.Add(error);
        }

        public void SetException(IEnumerable<string> errors)
        {
            SetException();

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

        public ActionResult ToActionResult()
        {
            ActionResult result;

            // Handle the status result
            var responseStatus = GetStatus();
            switch (responseStatus)
            {
                case ServiceResponseStatus.Success:
                    result = new NoContentResult();
                    break;
                case ServiceResponseStatus.NotFound:
                    result = new NotFoundObjectResult(GetErrorJson());
                    break;
                case ServiceResponseStatus.Error:
                    result = new BadRequestObjectResult(GetErrorJson());
                    break;
                case ServiceResponseStatus.Exception:
                    result = new ObjectResult(GetErrorJson());
                    ((ObjectResult)result).StatusCode = StatusCodes.Status500InternalServerError;
                    break;
                default:
                    result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                    break;
            }

            return result;
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }

        public ServiceResponse ToNonGeneric()
        {
            return new ServiceResponse(GetStatus(), GetErrors());
        }

        public ActionResult<T> ToActionResult(string uri = null)
        {
            ActionResult<T> result;

            // Handle the generic result
            var responseStatus = GetStatus();
            if (responseStatus == ServiceResponseStatus.Success)
            {
                if (Data != null)
                {
                    if (!string.IsNullOrEmpty(uri))
                    {
                        result = new CreatedResult(uri, Data);
                    }
                    else
                    {
                        result = new OkObjectResult(Data);
                    }
                }
                else
                {
                    result = new NoContentResult();
                }
            }
            else
            {
                result = base.ToActionResult();
            }

            return result;
        }
    }
}
