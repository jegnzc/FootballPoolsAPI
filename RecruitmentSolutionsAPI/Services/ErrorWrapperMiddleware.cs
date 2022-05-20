namespace RecruitmentSolutionsAPI.Middleware;

public static class ErrorWrapperMiddleware
{
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorWrappingMiddleware>();
    }
}