using API.Middleware;

namespace API.Extension;

public static class ConfigureExceptionMiddleware
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
