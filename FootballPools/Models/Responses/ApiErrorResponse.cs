using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSolutionsAPI.Extensions;

namespace RecruitmentSolutionsAPI.Models.Responses;

public class ApiErrorResponse
{
    public string? Type { get; }
    public string? StackTrace { get; }
    public string? PublicMessage { get; }
    public string? InternalCode { get; }
    public string? OriginalErrorMessage { get; }
    public string? TargetSite { get; }
    public object? Request { get; }
    public ErrorModel AllProperties { get; set; }
    public ErrorModel PublicProperties { get; set; }

    public ApiErrorResponse(int statusCode, string? stackTrace, string? type, string? targetSite, object? request = null, string? publicMessage = null, string? internalCode = null, string? originalErrorMessage = null)
    {
        AllProperties = new ErrorModel();
        StackTrace = stackTrace;
        Type = type;
        TargetSite = targetSite;
        PublicMessage = publicMessage;
        InternalCode = internalCode;
        Request = request;
        OriginalErrorMessage = originalErrorMessage;
        if (internalCode != null)
        {
            AllProperties.InternalCode = internalCode;
        }

        if (publicMessage != null)
        {
            AllProperties.PublicMessage = publicMessage;
        }

        if (originalErrorMessage != null)
        {
            AllProperties.OriginalErrorMessage = originalErrorMessage;
        }

        if (request != null)
        {
            AllProperties.Request = request;
        }
        AllProperties.TargetSite = targetSite;

        AllProperties.Type = type;
        AllProperties.StackTrace = stackTrace;

        PublicProperties = new ErrorModel()
        {
            InternalCode = AllProperties.InternalCode,
            PublicMessage = AllProperties.PublicMessage
        };
    }
}