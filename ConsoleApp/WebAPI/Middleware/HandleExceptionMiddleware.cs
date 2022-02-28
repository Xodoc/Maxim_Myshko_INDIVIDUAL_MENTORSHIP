using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace WebAPI.Middleware
{
    public class HandleExceptionMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate _next;

        public HandleExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = JsonContentType;

            var responseError = new
            {
                StatusCode = exception switch
                {
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    _ => (int)HttpStatusCode.InternalServerError
                },
                Message = $"Internal Server Error: {exception.Message}"
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(responseError));
        }
    }
}
