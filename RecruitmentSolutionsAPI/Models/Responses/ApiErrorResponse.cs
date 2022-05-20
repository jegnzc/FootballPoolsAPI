using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSolutionsAPI.Extensions;

namespace RecruitmentSolutionsAPI.Models.Responses;

public class ApiErrorResponse
{
    public string Type { get; }
    public string StackTrace { get; }
    public string PublicMessage { get; }
    public string InternalCode { get; }
    public string OriginalErrorMessage { get; }
    public string TargetSite { get; }
    public Dictionary<string, string> allProperties { get; }
    public Dictionary<string, string> publicProperties { get; }
    public Exception Error { get; }

    public ApiErrorResponse(int statusCode, string stackTrace, string type, string targetSite, string? publicMessage = null, string? internalCode = null, string? originalErrorMessage = null)
    {
        allProperties = new Dictionary<string, string>();
        StackTrace = stackTrace;
        Type = type;
        TargetSite = targetSite;
        PublicMessage = publicMessage;
        InternalCode = internalCode;
        OriginalErrorMessage = originalErrorMessage;
        if (internalCode != null)
        {
            allProperties.TryAdd("InternalCode", internalCode);
        }

        if (publicMessage != null)
        {
            allProperties.TryAdd("PublicMessage", publicMessage);
        }

        if (originalErrorMessage != null)
        {
            allProperties.TryAdd("OriginalErrorMessage", originalErrorMessage);
        }
        //allProperties.TryAdd("StatusCode", statusCode.ToString());
        //allProperties.TryAdd("Message", base.Message);
        allProperties.TryAdd("TargetSite", targetSite);

        allProperties.TryAdd("Type", type);
        allProperties.TryAdd("StackTrace", stackTrace);

        publicProperties = allProperties.CloneDictionaryCloningValues();
        var confidentialKeywords = new List<string> { "StackTrace", "Type", "OriginalErrorMessage", "TargetSite" };
        publicProperties.RemoveKeywords(confidentialKeywords);

        InjectToErrorDic(allProperties);
    }

    private string GenerateUsefulJsonField(string stackTrace, string initialMessage)
    {
        const string patternMatchLine = @"line\s\d*";
        var regexMatchLine = new Regex(patternMatchLine, RegexOptions.IgnoreCase);
        var lineError = regexMatchLine.Match(stackTrace).ToString();

        const string patternMatchController = @"\S*\d*\S*\d*.cs";
        var regexMatchController = new Regex(patternMatchController, RegexOptions.IgnoreCase);
        var controllerError = regexMatchController.Match(stackTrace).ToString();
        if (lineError == "" && controllerError == "")
        {
            return "";
        }
        return initialMessage + " " + lineError + " inside " + controllerError;
    }

    private void InjectToErrorDic(Dictionary<string, string> dicToAlter)
    {
        if (!dicToAlter.ContainsKey("StackTrace")) return;
        var useful = GenerateUsefulJsonField(dicToAlter["StackTrace"], "Error on");
        if (useful != "")
        {
            dicToAlter.TryAdd("Line", useful);
        }
    }
}