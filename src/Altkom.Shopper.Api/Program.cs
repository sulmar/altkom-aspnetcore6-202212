using System.Text.Json.Serialization;
using Altkom.Shopper.Domain;
using Altkom.Shopper.Infrastructure;
using Microsoft.AspNetCore.Http.Json;

// var app = WebApplication.Create();

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();

builder.Services.AddSingleton<IEnumerable<Product>>(sp => new List<Product>
{
    new Product { Id = 1, Name = "Product 1", Barcode = "1111", Color = "Blue", Size = ProductSize.M, Price = 100.99m },
    new Product { Id = 2, Name = "Product 2", Barcode = "2222", Color = "Green", Price = 10m },
    new Product { Id = 3, Name = "Product 3", Barcode = "3333", Color = "Red", Size = ProductSize.S, Price = 1.99m },
    new Product { Id = 4, Name = "Product 4", Barcode = "4444", Color = "Blue", Price = 2.99m },
    new Product { Id = 5, Name = "Product 5", Barcode = "5555", Color = "Green", Size = ProductSize.XL, Price = 10.99m },
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

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

// GET /api/products
app.MapGet("/api/products", (IProductRepository repository) => repository.GetAll());

// GET /api/products/{id}

/*
app.MapGet("/api/products/{id:int}", (int id, IProductRepository repository) => {
    
    var product = repository.Get(id);

    if (product == null)
        return Results.NotFound();

    return Results.Ok(product);

});
*/

/* Przykład z użyciem operatora is
app.MapGet("/api/products/{id:int}", (int id, IProductRepository repository) => 
    repository.Get(id) is Product product 
    ? Results.Ok(product) 
    : Results.NotFound());
*/ 

// Match Patterns
app.MapGet("/api/products/{id:int}", (int id, IProductRepository repository) => 
    repository.Get(id) switch
    {
        Product product => Results.Ok(product),        
        null => Results.NotFound()        
    });

app.Run();