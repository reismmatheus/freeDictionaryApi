using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace FreeDictionary.CrossCutting.Middlewares
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;
        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            await _next(httpContext);
            watch.Stop();
            httpContext.Request.Headers.Add("x-time", watch.ElapsedMilliseconds.ToString());
        }
    }
}
