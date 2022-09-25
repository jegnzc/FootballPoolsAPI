using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSolutionsAPI.Models.ExceptionHandlers;
using RecruitmentSolutionsAPI.Models.Responses;

namespace RecruitmentSolutionsAPI.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("/error-development")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleErrorDevelopment(
        [FromServices] IHostEnvironment hostEnvironment)
    {
        int? statusCode;
        ApiErrorResponse? apiBaseResponse = null;
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }
        var exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>();

        // entonces nosotros fuimos los que generamos el error...
        if (exceptionHandlerFeature?.Error is HttpResponseException handledException)
        {
            apiBaseResponse = new ApiErrorResponse
            (handledException.StatusCode, handledException.StackTrace, handledException.GetType().ToString(),
                handledException.TargetSite?.ToString(), handledException.Request,
                handledException.PublicMessage, handledException.InternalCode, handledException.Message);

            statusCode = handledException.StatusCode;
        }
        // error inesperado...
        else
        {
            apiBaseResponse = new ApiErrorResponse
                (500, exceptionHandlerFeature?.Error?.StackTrace, exceptionHandlerFeature?.Error?.GetType().ToString(), exceptionHandlerFeature?.Error?.TargetSite?.ToString(), originalErrorMessage: exceptionHandlerFeature?.Error?.Message);
            statusCode = 500;
        }

        return new ObjectResult(apiBaseResponse.AllProperties)
        {
            StatusCode = statusCode
        };
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError() =>
        Problem();
}