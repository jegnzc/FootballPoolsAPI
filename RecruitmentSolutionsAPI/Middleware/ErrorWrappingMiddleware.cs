using System.Collections;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using RecruitmentSolutionsAPI.Extensions;
using RecruitmentSolutionsAPI.Models.ExceptionHandlers;
using RecruitmentSolutionsAPI.Models.Responses;

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
        var errorResponse = string.Empty;
        try
        {
            await _next.Invoke(context);
        }
        catch (HttpResponseException ex)
        {
            var apiBaseResponse = new ApiErrorResponse
                (ex.StatusCode, ex.StackTrace, ex.GetType().ToString(), ex.TargetSite.ToString(),
                    ex.PublicObject.ToString(), ex.InternalCode);

            if (hostEnvironment.IsDevelopment())
            {
                errorResponse = JsonSerializer.Serialize(apiBaseResponse.allProperties, jsonFormatOptions);
            }
            else
            {
                // hacer algo con allProperties... guardar en el log... etc
                errorResponse = JsonSerializer.Serialize(apiBaseResponse.publicProperties, jsonFormatOptions);
            }

            context.Response.StatusCode = apiBaseResponse.StatusCode;
        }
        catch (Exception ex)
        {
            //_logger.LogError(context.TraceIdentifier, ex, ex.Message);
            var apiBaseResponse = new ApiErrorResponse
                (500, ex.StackTrace, ex.GetType().ToString(), ex.TargetSite.ToString(), originalErrorMessage: ex.Message);

            if (!hostEnvironment.IsDevelopment())
            {
                errorResponse = JsonSerializer.Serialize(apiBaseResponse.allProperties, jsonFormatOptions);
            }
            else
            {
                // hacer algo con allProperties... guardar en el log... etc
                errorResponse = JsonSerializer.Serialize(apiBaseResponse.publicProperties, jsonFormatOptions);
            }

            context.Response.StatusCode = apiBaseResponse.StatusCode;
        }

        if (!context.Response.HasStarted)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorResponse);
        }
    }
}