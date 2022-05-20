namespace RecruitmentSolutionsAPI.Models.ExceptionHandlers;

public class HttpResponseException : Exception
{
    public int StatusCode { get; }
    public string InternalCode { get; }
    public object? Value { get; }

    public HttpResponseException(int statusCode, string internalCode = null, object? value = null)
    {
        InternalCode = internalCode;
        (StatusCode, Value) = (statusCode, value);
    }
}