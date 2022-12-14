using Altkom.Shopper.Domain;
using Microsoft.AspNetCore.Mvc;

public static class MapEndpoints
{
    public static WebApplication MapProducts(this WebApplication app)
    {
        
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
    }).WithName("GetProductById")
    .RequireAuthorization();

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


        return app;
    }
}