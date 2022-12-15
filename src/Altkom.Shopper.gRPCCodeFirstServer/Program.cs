
// dotnet add package protobuf-net.Grpc.AspNetCore

using Altkom.Shopper.gRPCCodeFirstServer.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCodeFirstGrpc();

// dotnet add package System.ServiceModel.Primitives
// dotnet add package protobuf-net.Grpc.AspNetCore.Reflection
builder.Services.AddCodeFirstGrpcReflection();

var app = builder.Build();


app.MapGrpcService<ShopperDeliveryService>();
app.MapCodeFirstGrpcReflectionService();

// dotnet tool install -g dotnet-grpc-cli

app.MapGet("/", () => "Hello World!");

app.Run();
