using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookshop;

namespace Altkom.Shopper.gRPCServer.Services
{
    public class InventoryService : Bookshop.Inventory.InventoryBase    
    {
        private readonly ILogger<InventoryService> logger;

        public InventoryService(ILogger<InventoryService> logger)
        {
            this.logger = logger;
        }

        public override Task<Book> GetBook(GetBookRequest request, Grpc.Core.ServerCallContext context)
        {           
            logger.LogInformation("GetBook by {id}", request.BookId);

            var book = new Book { 
                Id = request.BookId,
                Title = "ASP.NET Core 6",
                Author = "John Smith",
                PageCount = 500,
                Price = 199d,
            };
            
            return Task.FromResult(book);
        }
    }
        
    
}