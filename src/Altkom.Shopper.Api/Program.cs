using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Altkom.Shopper.Domain;
using Altkom.Shopper.Domain.SearchCriterias;
using Altkom.Shopper.Infrastructure;
using Bogus;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// var app = WebApplication.Create();

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();

builder.Services.AddSingleton<IEnumerable<Product>>(sp => new List<Product>
{
    new Product { Id = 1, Name = "Product 1", Barcode = "1111", Color = "Blue", Size = ProductSize.M, Price = 100.99m },
    new Product { Id = 2, Name = "Product 2", Barcode = "2222", Color = "Green", Price = 10m },
    new Product { Id = 3, Name = "Product 3", Barcode = "3333", Color = "Red", Size = ProductSize.S, Price = 1.99m },
    new Product { Id = 4, Name = "Product 4", Barcode = "4444", Color = "Blue", Price = 2.99m },
    new Product { Id = 5, Name = "Product 5", Barcode = "5555", Color = "Green", Size = ProductSize.XL, Price = 10.99m },
});

// dotnet add package Bogus
builder.Services.AddSingleton<IEnumerable<Customer>>(sp => 
{
    var customers = new Faker<Customer>()
        .UseSeed(1)
        .StrictMode(true)
        .RuleFor(p => p.Id, f => f.IndexFaker)
        .RuleFor(p => p.FirstName, f => f.Person.FirstName)
        .RuleFor(p => p.LastName, f => f.Person.LastName)
        .RuleFor(p => p.Email, (f, customer) => $"{customer.FirstName}.{customer.LastName}@domain.com")   // {firstname}.{lastname}@domain.com
        .RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f))
        .Generate(100);

    return customers;

});

builder.Services.AddSingleton<IMessageService, EmailMessageService>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers();

var app = builder.Build();

var lambda = () => "Hello from lambda variable";

string LocalFunction() => "Hello from local function";

// GET /
app.MapGet("/", () => "Hello .NET6!!!");

// GET /hello
app.MapGet("/hello", () => "Hello World!");

// GET /hello/{name}
app.MapGet("/hello/{name}", (string name) => $"Hello {name}!");

// GET /lambda
app.MapGet("/lambda", lambda);

// GET /function
app.MapGet("/function", LocalFunction);

// GET /api/shops?lat={lat}&lng={lng}
app.MapGet("/api/shops", (HttpRequest req, HttpResponse res) =>
{    
    double.TryParse(req.Query["lat"], out double latitude);
    double.TryParse(req.Query["lng"], out double longitude);

    res.WriteAsync($"Hello {latitude} {longitude}");
});

app.MapGet("/api/vehicles", (HttpContext context) => 
{
 
});

// TODO: MVC

app.MapControllers();

// TODO: konfiguracja
// TODO: środowisko


app.MapProducts();

// JSON Patch (RFC 6902)
// https://jsonpatch.com/
// Content-Type: application/json-patch+json

// dotnet add package Microsoft.AspNetCore.JsonPatch --version 6.0.10
app.MapPatch("/api/products/{id}", (int id, IProductRepository repository) =>
{
    JsonPatchDocument<Product> patchDocument;

    var product = repository.Get(id);

    // TODO: jsonpatch
    // patchDocument.ApplyTo(product);

    return Results.Ok($"Patch {id}");
});

// JSON Merge Patch (RFC 7386)
// https://github.com/Morcatko/Morcatko.AspNetCore.JsonMergePatch

// od .NET 7
// app.MapGet("/api/products/search", (int[] id) => { });


// GET api/shops?loc=52.01,28.04


app.Run();