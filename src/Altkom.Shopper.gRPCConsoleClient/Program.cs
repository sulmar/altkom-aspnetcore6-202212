using Grpc.Net.Client;
using Grpc.Core;
using System.Threading.Tasks;

Console.WriteLine("Hello, gRPC Client!");

// dotnet add package Google.Protobuf
// dotnet add package Grpc.Tools
// dotnet add package Grpc.Net.ClientFactory

const string url = "http://localhost:5164";

using var channel = GrpcChannel.ForAddress(url);

var client = new Bookshop.Inventory.InventoryClient(channel);

// await GetBookTest(client);

var request = new Bookshop.SubscribeBookPriceChangedRequest { BookId = 1 };

var subscribe = client.SubscribeBookPriceChanged(request);

var bookPriceChanges = subscribe.ResponseStream.ReadAllAsync(); //using Grpc.Core;

// foreach -> IEnumerable
// async foreach -> IAsyncEnumerable

await foreach(var bookPriceChange in bookPriceChanges)
{
    System.Console.WriteLine($"{bookPriceChange.BookId} {bookPriceChange.Price}");
}

System.Console.WriteLine("Press any key to exit.");
Console.ReadKey();


async Task GetBookTest(Bookshop.Inventory.InventoryClient client)
{
    while(true)
    {
        var getBookRequest = new Bookshop.GetBookRequest { BookId = 1 };

        var response = client.GetBook(getBookRequest);

        System.Console.WriteLine(response.Title);

        await Task.Delay(100);
    }
}
