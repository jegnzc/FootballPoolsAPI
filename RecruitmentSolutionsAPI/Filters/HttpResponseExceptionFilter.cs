using System.Collections;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecruitmentSolutionsAPI.ExceptionHandlers;
using RecruitmentSolutionsAPI.Models;

namespace RecruitmentSolutionsAPI.Filters;

public class HttpResponseExceptionFilter : ActionFilterAttribute, IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;
    private readonly IHostEnvironment hostEnvironment;

    public HttpResponseExceptionFilter([FromServices] IHostEnvironment hostEnvironment)
    {
        this.hostEnvironment = hostEnvironment;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(context.ModelState));
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        //if (hostEnvironment.IsDevelopment())
        //{
        //    context.Result = new ObjectResult("json2")
        //    {
        //        StatusCode = 500,
        //    };
        //}
        var jsonFormatOptions = new JsonSerializerOptions { WriteIndented = true };

        if (context.Exception is Exception unhandledException)
        {
            var genericException = unhandledException.GetType().GetProperties()
                .ToDictionary(x => x.Name, x => x.GetValue(unhandledException)?.ToString() ?? "");

            string text = genericException["StackTrace"];
            string patternMatchLine = @"line\s\d*";

            var regexMatchLine = new Regex(patternMatchLine, RegexOptions.IgnoreCase);
            var lineError = regexMatchLine.Match(text).ToString();

            var patternMatchController = @"\S*\d*\S*\d*.cs";
            var regexMatchController = new Regex(patternMatchController, RegexOptions.IgnoreCase);
            var controllerError = regexMatchController.Match(text).ToString();
            genericException.Add("Useful", "Unexpected Exception on " + lineError + " inside " + controllerError + ": " + genericException["Message"]);

            var error2 = new Dictionary<string, string>
            {
                {"Type", unhandledException.GetType().ToString()},
                {"Message", unhandledException.Message + " - Unexpected Exception"},
                {"StackTrace", unhandledException.StackTrace}
            };
            foreach (DictionaryEntry data in unhandledException.Data)
                error2.Add(data.Key.ToString(), data.Value.ToString());

            if (!hostEnvironment.IsDevelopment())
            {
                var devJson = JsonSerializer.Serialize(genericException, jsonFormatOptions);
                genericException.Clear();
                genericException.Add("Type", "Internal Server Error");
            }

            string json2 = JsonSerializer.Serialize(genericException, jsonFormatOptions);
            context.Result = new ObjectResult(json2)
            {
                StatusCode = 500,
            };

            context.ExceptionHandled = true;
        }

        if (context.Exception is not HttpResponseException httpResponseException) return;

        //var error = JsonSerializer.Deserialize<Dictionary<string, string>>(s);
        var error = new Dictionary<string, string>
        {
            {"Type", httpResponseException.GetType().ToString()},
            {"Message", httpResponseException.Message},
            {"StackTrace", httpResponseException.StackTrace},
            {"Value", httpResponseException.Value.ToString()},
            {"ErrorCode", httpResponseException.StatusCode.ToString()}
        };

        if (!hostEnvironment.IsDevelopment())
        {
            var devJson = JsonSerializer.Serialize(error, jsonFormatOptions);
            error.Clear();
            error.Add("Type", "Internal Server Error");
        }

        var json = JsonSerializer.Serialize(error, jsonFormatOptions);

        context.Result = new ObjectResult(json)
        {
            StatusCode = httpResponseException.StatusCode,
        };

        context.ExceptionHandled = true;
    }
}