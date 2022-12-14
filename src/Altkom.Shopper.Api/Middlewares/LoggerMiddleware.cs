using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.Api.Middlewares;

public class LoggerMiddleware
{
    private readonly RequestDelegate next;
    public LoggerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"[{DateTime.Now}] {context.Request.Method} {context.Request.Path}");
   
        await next(context);

        System.Console.WriteLine($"[{DateTime.Now}] {context.Response.StatusCode}");
    }
    
}

public static class LoggerMiddlewareExtensions
{
    public static WebApplication UseLogger(this WebApplication app)
    {
        app.UseMiddleware<LoggerMiddleware>();

        return app;
    }
}
