namespace RecruitmentSolutionsAPI.Models.ExceptionHandlers;

public class HttpResponseException : Exception
{
    public int StatusCode { get; }
    public string InternalCode { get; }
    public object? PublicObject { get; }

    public HttpResponseException(int statusCode, string internalCode = null, object? publicObject = null)
    {
        InternalCode = internalCode;
        (StatusCode, PublicObject) = (statusCode, publicObject);
    }
}