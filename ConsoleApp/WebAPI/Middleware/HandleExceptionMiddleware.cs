using BL.Validators.CustomExceptions;
using Newtonsoft.Json;
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
            var status = exception switch
            {
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                ValidatorException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var responseError = new
            {
                Message = $"Internal Server Error: {exception.Message}"
            };

            context.Response.ContentType = JsonContentType;
            context.Response.StatusCode = status;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(responseError));
        }
    }
}
