using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using WellFlix.Catalog.Api.Common.Http;

namespace WellFlix.Catalog.Api.Controllers;

/// <summary>
/// Base api controller class
/// </summary>
public class ApiController : ControllerBase
{
    /// <summary>
    /// Problem return base
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firsError = errors[0];

        var statusCode = firsError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode,
                       title: firsError.Description);
    }
}