namespace RecruitmentSolutionsAPI.Models.Responses;

public class ApiResponse
{
    public int StatusCode { get; }

    public string? Message { get; }

    public ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    private static string? GetDefaultMessageForStatusCode(int statusCode)
    {
        switch (statusCode)
        {
            case 200:
                return "Ok";

            case 404:
                return "Resource not found";

            case 500:
                return "An unhandled error occurred";

            default:
                return null;
        }
    }
}