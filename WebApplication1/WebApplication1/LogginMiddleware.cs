using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using System;
using System.Threading.Tasks; // Добавлено пространство имен

namespace WebApplication1
{
    public class LoggingMiddleware
    {
        readonly RequestDelegate next;
        readonly ILogger logger;
        public LoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogWarning("LogWarning: " + context.Request.Path + "Exception: " + ex);
            }
        }
    }
    public static class FileLoggerExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<LoggingMiddleware>();
            return builder;
        }
    }
}

