using Grpc.Net.Client;
using Altkom.Shopper.Contracts;
using ProtoBuf;
using ProtoBuf.Grpc.Client;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// dotnet add package Grpc.Net.Client
// dotnet add package protobuf-net.Grpc

const string url = "http://localhost:5171";

using var channel = GrpcChannel.ForAddress(url);

var client = channel.CreateGrpcService<IDeliveryService>();

var request = new ConfirmDeliveryRequest() { ShippmentId = 1, ShippedDate = DateTime.Now, Sign = "John" };

var response = await client.ConfirmDeliveryAsync(request);


System.Console.WriteLine(response.Cost);

System.Console.WriteLine("Press any key to exit.");
Console.ReadKey();







