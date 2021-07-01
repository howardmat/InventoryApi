using Api.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Handlers
{
    public class ResponseHandler
    {
        private ResponseHandlerStatus _status = ResponseHandlerStatus.Success;
        private ICollection<string> _errors = new HashSet<string>();

        public ResponseHandler() { }

        public ResponseHandler(ResponseHandlerStatus status, ICollection<string> errors)
        {
            _status = status;
            _errors = errors;
        }

        public ResponseHandlerStatus GetStatus() => _status;

        public ResponseHandlerStatus SetError() 
        {
            _status = ResponseHandlerStatus.Error;

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
            _status = ResponseHandlerStatus.NotFound;
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
            _status = ResponseHandlerStatus.Exception;
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
                case ResponseHandlerStatus.Success:
                    result = new NoContentResult();
                    break;
                case ResponseHandlerStatus.NotFound:
                    result = new NotFoundObjectResult(GetErrorJson());
                    break;
                case ResponseHandlerStatus.Error:
                    result = new BadRequestObjectResult(GetErrorJson());
                    break;
                case ResponseHandlerStatus.Exception:
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

    public class ResponseHandler<T> : ResponseHandler
    {
        public T Data { get; set; }

        public ResponseHandler ToNonGeneric()
        {
            return new ResponseHandler(GetStatus(), GetErrors());
        }

        public ActionResult<T> ToActionResult(string uri = null)
        {
            ActionResult<T> result;

            // Handle the generic result
            var responseStatus = GetStatus();
            if (responseStatus == ResponseHandlerStatus.Success)
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
