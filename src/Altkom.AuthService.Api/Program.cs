using Altkom.AuthService.Api.Domain;
using Altkom.AuthService.Api.Infrastructure;
using Altkom.AuthService.Api.Models;
using Altkom.Shopper.Domain;
using Altkom.Shopper.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<IEnumerable<User>>(sp => new List<User>
{
    new User { 
        Id = 1, 
        Username = "john", 
        HashedPassword = "AQAAAAIAAYagAAAAEEGnl+/35TtTb8RTbz9j8TdKtRzy+EbfnEsPNKsjJz/mKjgDVy7zZK9aQHuVHkRuQQ==", 
        Email = "john@domain.com", 
        DateOfBirth = DateTime.Parse("2000-12-31"),
        },

    new User { 
        Id = 2, 
        Username = "kate", 
        HashedPassword = "AQAAAAIAAYagAAAAEO2EgnN2FfkSTEw4gab6CtcmYOs9H+P0c6JRcbMdfxS17Dx0wwnhb1froMte1wTYJQ==", 
        Email = "kate@domain.com", 
        DateOfBirth = DateTime.Parse("2010-12-31"),
        },

    new User { 
        Id = 3, 
        Username = "Bob", 
        HashedPassword = "AQAAAAIAAYagAAAAEN2jcUYIL4yE2zhWd69Q65TtDULGV3l3iCq5wW/ch+Nslt8AgW6rWbMfAIysHK5iag==", 
        Email = "bob@domain.com", 
        DateOfBirth = DateTime.Parse("1990-01-30"),
        },
});


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
