using Altkom.AuthService.Api.Domain;
using Altkom.AuthService.Api.Models;
using Altkom.Shopper.Domain;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/api/tokens", (AuthModel model, IAuthService authService, ITokenService tokenService)=>
{    
    if (authService.TryAuthorize(model.Login, model.Password, out User user))
    {        
        var token = tokenService.Create(user);
        return Results.Ok(token);
    }
    else
    {
        return Results.BadRequest(new { message = "Login or password is invalid."});
    }
});

app.Run();
