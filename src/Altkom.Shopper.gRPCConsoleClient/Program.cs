using Grpc.Net.Client;
using Grpc.Core;
using System.Threading.Tasks;
using Bogus;

Console.WriteLine("Hello, gRPC Client!");

// dotnet add package Google.Protobuf
// dotnet add package Grpc.Tools
// dotnet add package Grpc.Net.ClientFactory

const string url = "http://localhost:5164";

using var channel = GrpcChannel.ForAddress(url);

var client = new Bookshop.Inventory.InventoryClient(channel);

// await GetBookTest(client);
// await ServerStreamingTest(client);
await ClientStreamingTest(client);

System.Console.WriteLine("Press any key to exit.");
Console.ReadKey();

async Task ClientStreamingTest(Bookshop.Inventory.InventoryClient client)
{
    var call = client.UpdateBookProgress();

    var requests = new Faker<Bookshop.UpdateBookProgressRequest>()
        .RuleFor(p=>p.BookId, f => 1)
        .RuleFor(p=>p.PageCurrent, f => f.IndexFaker)
        .GenerateLazy(20);

    Random random = new Random();

    foreach(var request in requests)
    {
        await call.RequestStream.WriteAsync(request);

        System.Console.WriteLine($"Send book progress {request.BookId} {request.PageCurrent}");    
        await Task.Delay(TimeSpan.FromSeconds(random.Next(1, 10)));
    }

    await call.RequestStream.CompleteAsync();

    var response = await call;

    System.Console.WriteLine($"Recommendation bookId {response.BookId}");

}

async Task ServerStreamingTest(Bookshop.Inventory.InventoryClient client)
{
    var request = new Bookshop.SubscribeBookPriceChangedRequest { BookId = 1 };

    using var subscribe = client.SubscribeBookPriceChanged(request);

    var bookPriceChanges = subscribe.ResponseStream.ReadAllAsync(); //using Grpc.Core;

    // foreach -> IEnumerable
    // async foreach -> IAsyncEnumerable

    await foreach(var bookPriceChange in bookPriceChanges)
    {
        System.Console.WriteLine($"{bookPriceChange.BookId} {bookPriceChange.Price}");
    }
}


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
