using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.Api.Middlewares;

public class SecretKeyMiddleware
{
    private readonly RequestDelegate next;
    public SecretKeyMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("X-Secret-Key", out var secretKey) && secretKey == "123")
    {
        await next(context);
    }
    else
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
    }    
}

public static class SecretKeyMiddlewareExtensions
{
    public static WebApplication UseSecretKey(this WebApplication app)
    {
        app.UseMiddleware<SecretKeyMiddleware>();

        return app;
    }
}
