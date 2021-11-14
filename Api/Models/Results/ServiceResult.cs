using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Api.Models.Results;

public class ServiceResult
{
    private ResultStatus _status = ResultStatus.Success;
    private readonly ICollection<string> _errors = new HashSet<string>();

    public ServiceResult() { }

    public ServiceResult(ResultStatus status, ICollection<string> errors)
    {
        _status = status;
        _errors = errors;
    }

    public ResultStatus GetStatus() => _status;

    public bool IsSuccess => GetStatus() == ResultStatus.Success;

    public ResultStatus SetError()
    {
        _status = ResultStatus.Error;

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
        _status = ResultStatus.NotFound;
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
        _status = ResultStatus.Exception;
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
            case ResultStatus.Success:
                result = new NoContentResult();
                break;
            case ResultStatus.NotFound:
                result = new NotFoundObjectResult(GetErrorJson());
                break;
            case ResultStatus.Error:
                result = new BadRequestObjectResult(GetErrorJson());
                break;
            case ResultStatus.Exception:
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

public class ServiceResult<T> : ServiceResult
{
    public T Data { get; set; }

    public ServiceResult ToNonGeneric()
    {
        return new ServiceResult(GetStatus(), GetErrors());
    }

    public ActionResult<T> ToActionResult(string uri = null)
    {
        ActionResult<T> result;

        // Handle the generic result
        var responseStatus = GetStatus();
        if (responseStatus == ResultStatus.Success)
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
