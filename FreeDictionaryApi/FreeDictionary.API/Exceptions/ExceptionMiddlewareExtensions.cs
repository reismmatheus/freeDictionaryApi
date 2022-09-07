using FreeDictionary.API.Exceptions.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace FreeDictionary.API.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app/*, ILoggerManager logger*/)
        {
            app.UseMiddleware<ExceptionMiddlaware>();
        }
    }
}
