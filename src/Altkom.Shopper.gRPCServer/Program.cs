using Altkom.Shopper.gRPCServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

// dotnet add package Grpc.AspNetCore.Server.Reflection
builder.Services.AddGrpcReflection();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

// grpcurl
// https://github.com/fullstorydev/grpcurl/releases

// grpcurl --plaintext -d {\"book_id\":1}  localhost:5164 Inventory/GetBook

app.MapGrpcService<InventoryService>();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.Run();
