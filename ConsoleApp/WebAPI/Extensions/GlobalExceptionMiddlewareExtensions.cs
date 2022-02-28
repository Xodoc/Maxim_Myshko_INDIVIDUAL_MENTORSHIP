using WebAPI.Middleware;

namespace WebAPI.Extensions
{
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<HandleExceptionMiddleware>();
        }
    }
}
