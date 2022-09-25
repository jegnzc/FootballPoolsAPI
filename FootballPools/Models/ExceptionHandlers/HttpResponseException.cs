namespace FootballPools.Models.ExceptionHandlers;

public class HttpResponseException : Exception
{
    public int StatusCode { get; }
    public string? InternalCode { get; }
    public string PublicMessage { get; }
    public object? Request { get; }

    public HttpResponseException(int statusCode, string publicMessage, object? publicObject = null, string? internalCode = null)
    {
        InternalCode = internalCode;
        PublicMessage = publicMessage;
        Request = publicObject;
        StatusCode = statusCode;
    }
}