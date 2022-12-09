using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Altkom.Shopper.Domain;
using Altkom.Shopper.Domain.SearchCriterias;
using Altkom.Shopper.Infrastructure;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
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

// GET /api/customers?lat={lat}&lng={lng}
app.MapGet("/api/customers", (HttpRequest req, HttpResponse res) =>
{    
    double.TryParse(req.Query["lat"], out double latitude);
    double.TryParse(req.Query["lng"], out double longitude);

    res.WriteAsync($"Hello {latitude} {longitude}");
});

app.MapGet("/api/vehicles", (HttpContext context) => 
{
 
});

// TODO: Post
// TODO: Put
// TODO: Patch
// TODO: Delete

// TODO: konfiguracja
// TODO: środowisko

// TODO: MVC

// GET /api/products
 // app.MapGet("/api/products", (IProductRepository repository) => repository.GetAll());



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
app.MapGet("/api/products/{id:int:max(100)}", (int id, IProductRepository repository) => 
    repository.Get(id) switch
    {
        Product product => Results.Ok(product),        
        null => Results.NotFound()        
    }).WithName("GetProductById");

// Route Parameters
app.MapGet("/api/products/{barcode:length(4)}", (string barcode, IProductRepository repository) =>
    repository.GetByBarcode(barcode) switch
    {
        Product product => Results.Ok(product),
        null => Results.NotFound()        
    });

// TODO: Query string
// app.MapGet("/api/products", (string color) => (string color, IProductRepository repository) => repository.GetByColor(color) );

// GET api/products?from=10&to=100&color=red

// w .NET 7.0 jest nowy atrybut [AsParameters]
app.MapGet("/api/products", (ProductSearchCriteria searchCriteria, [FromHeader] string version, IProductRepository repository) => 
{
    var criteria = new Altkom.Shopper.Domain.SearchCriterias.ProductSearchCriteria(searchCriteria.From, searchCriteria.To, searchCriteria.Color);

    return repository.Get(criteria);
});

// GET api/products/search?id=1,4,5

app.MapGet("/api/products/search", (ProductIds id) => {
    return string.Join(" ", id.Ids);
});

app.MapPost("/api/products", (Product product, IProductRepository repository) =>
{
    repository.Add(product);

    // zła praktyka
    // return Results.Created($"https://localhost:7119/api/products/{product.Id}", product);
    
    // dobra praktyka
    return Results.CreatedAtRoute("GetProductById", new { Id = product.Id }, product);
});

app.MapGet("/api/products/{id}/link", (int id, LinkGenerator linkGenerator) =>
{   
    var link = linkGenerator.GetPathByName("GetProductById", new { Id = id });

    return Results.Ok($"The link to product {link}");
});

app.MapPut("/api/products/{id}", (int id, Product product, IProductRepository repository) =>
{
    if (id != product.Id)
        return Results.BadRequest(new { ErrorMessage = "Invalid id"});

    repository.Update(product);

    return Results.NoContent();    
});


// app.MapMethods("/api/products/{id}", new [] { "PATCH" }, (int id) => {

//     return Results.Ok($"Patch {id}");

// });


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