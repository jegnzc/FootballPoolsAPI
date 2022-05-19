using RecruitmentSolutionsAPI.Filters;

namespace RecruitmentSolutionsAPI.Middleware;

public static class MyMiddlewareExtensions
{
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorWrappingMiddleware>();
    }
}