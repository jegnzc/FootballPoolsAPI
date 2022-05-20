using System.Collections;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSolutionsAPI.ExceptionHandlers;

namespace RecruitmentSolutionsAPI.Middleware;

public class ErrorWrappingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorWrappingMiddleware> _logger;
    private readonly IHostEnvironment hostEnvironment;

    public ErrorWrappingMiddleware(RequestDelegate next, ILogger<ErrorWrappingMiddleware> logger, [FromServices] IHostEnvironment hostEnvironment)
    {
        _next = next;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.hostEnvironment = hostEnvironment;
    }

    public async Task Invoke(HttpContext context)
    {
        var jsonFormatOptions = new JsonSerializerOptions { WriteIndented = true };
        var json = string.Empty;
        try
        {
            await _next.Invoke(context);
        }
        catch (HttpResponseException ex)
        {
            var error = new Dictionary<string, string>
            {
                {"Type", ex.GetType().ToString()},
                {"Message", ex.Message},
                {"StackTrace", ex.StackTrace},
                {"Value", ex.Value.ToString()},
                {"ErrorCode", ex.StatusCode.ToString()}
            };

            if (!hostEnvironment.IsDevelopment())
            {
                var devJson = JsonSerializer.Serialize(error, jsonFormatOptions);
                error.Clear();
                error.Add("Type", "Internal Server Error");
            }

            json = JsonSerializer.Serialize(error, jsonFormatOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(context.TraceIdentifier, ex, ex.Message);

            var genericException = ex.GetType().GetProperties()
                .ToDictionary(x => x.Name, x => x.GetValue(ex)?.ToString() ?? "");

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
                {"Type", ex.GetType().ToString()},
                {"Message", ex.Message + " - Unexpected Exception"},
                {"StackTrace", ex.StackTrace}
            };
            foreach (DictionaryEntry data in ex.Data)
                error2.Add(data.Key.ToString(), data.Value.ToString());

            if (!hostEnvironment.IsDevelopment())
            {
                var devJson = JsonSerializer.Serialize(genericException, jsonFormatOptions);
                genericException.Clear();
                genericException.Add("Type", "Internal Server Error");
            }

            json = JsonSerializer.Serialize(genericException, jsonFormatOptions);
            context.Response.StatusCode = 500;

            //var response = new ApiResponse(context.Response.StatusCode);
            //json = JsonSerializer.Serialize(response);
        }

        if (!context.Response.HasStarted)
        {
            context.Response.ContentType = "application/json";

            //var response = new ApiResponse(context.Response.StatusCode);

            //var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}