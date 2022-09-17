using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace FreeDictionary.CrossCutting.Middlewares
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                httpContext.Response.OnStarting(() =>
                {
                    watch.Stop();
                    httpContext.Response.Headers.Add("x-response-time", watch.ElapsedMilliseconds.ToString());
                    return Task.CompletedTask;
                });
                await _next(httpContext);
                watch.Stop();
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
