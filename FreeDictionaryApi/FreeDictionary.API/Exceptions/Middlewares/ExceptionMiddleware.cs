using Newtonsoft.Json;
using System.Net;

namespace FreeDictionary.API.Exceptions.Middlewares
{
    public class ExceptionMiddlaware
    {
        private readonly RequestDelegate _next;
        // private readonly ILoggerManager _logger;
        public ExceptionMiddlaware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = "Error message" }));
        }
    }
}
