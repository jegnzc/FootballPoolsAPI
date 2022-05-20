namespace RecruitmentSolutionsAPI.Models.Responses;

public class ApiResponse
{
    public int StatusCode { get; }
    public string Message { get; }
    public string Type { get; }
    public string StackTrace { get; }
    public string? Value { get; }
    public string InternalCode { get; }
    public Dictionary<string, string> properties { get; }

    public ApiResponse(int statusCode)
    {
        StatusCode = statusCode;
    }

    public ApiResponse(int statusCode, string stackTrace, string type, string value, string internalCode, string? message = null)
    {
        properties = new Dictionary<string, string>();
        StatusCode = statusCode;
        StackTrace = stackTrace;
        Type = type;
        Value = value;
        InternalCode = internalCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        properties.TryAdd("StatusCode", statusCode.ToString());
        properties.TryAdd("StackTrace", stackTrace);
        properties.TryAdd("Type", type);
        properties.TryAdd("Type", value);
        properties.TryAdd("InternalCode", internalCode);
        properties.TryAdd("Message", message ?? GetDefaultMessageForStatusCode(statusCode));
    }

    private static string GetDefaultMessageForStatusCode(int statusCode)
    {
        switch (statusCode)
        {
            case 400:
                return "Bad Request";

            case 404:
                return "Resource not found";

            case 500:
                return "An unhandled error occurred";

            default:
                return null;
        }
    }
}