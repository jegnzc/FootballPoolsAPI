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
        var jsonFormatOptions = new JsonSerializerOptions { WriteIndented = true };
        var errorResponse = string.Empty;

        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }
        var exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>();
        // entonces nosotros fuimos los que generamos el error...
        if (exceptionHandlerFeature.Error is HttpResponseException expectedException)
        {
            var apiBaseResponse = new ApiErrorResponse
            (expectedException.StatusCode, expectedException.StackTrace, expectedException.GetType().ToString(),
                expectedException.TargetSite.ToString(),
                expectedException.ToString(), expectedException.InternalCode);

            //errorResponse = JsonSerializer.Serialize(apiBaseResponse.allProperties, jsonFormatOptions);
            errorResponse = JsonSerializer.Serialize(apiBaseResponse.allProperties, jsonFormatOptions);

            return new ObjectResult(errorResponse)
            {
                StatusCode = expectedException.StatusCode
            };
        }
        // error inesperado...
        else
        {
        }

        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message);
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError() =>
        Problem();
}