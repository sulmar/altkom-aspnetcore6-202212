using Grpc.Core;
using Grpc.Net.Client;
using System.Threading.Tasks;

Console.WriteLine("Hello, gRPC Client!");

// dotnet add package Google.Protobuf
// dotnet add package Grpc.Tools
// dotnet add package Grpc.Net.ClientFactory

const string url = "http://localhost:5164";

using var channel = GrpcChannel.ForAddress(url);

var client = new Bookshop.Inventory.InventoryClient(channel);

while(true)
{
    var getBookRequest = new Bookshop.GetBookRequest { BookId = 1 };

    var response = client.GetBook(getBookRequest);

    System.Console.WriteLine(response.Title);

    await Task.Delay(100);
}
