using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookshop;
using Grpc.Core;

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

        public override Task<GetBooksResponse> GetBooks(GetBooksRequest request, ServerCallContext context)
        {
            var books = new List<Book>
            {
                new Book { 
                    Id = 1,
                    Title = "ASP.NET Core 6",
                    Author = "John Smith",
                    PageCount = 500,
                    Price = 199d,
                },

                new Book { 
                    Id = 2,
                    Title = "gRPC in Action",
                    Author = "John Smith",
                    PageCount = 250,
                    Price = 99d,
                },

                  new Book { 
                    Id = 3,
                    Title = "REST API in Action",
                    Author = "Bob Smith",
                    PageCount = 450,
                    Price = 119d,
                },
            };

            var response = new GetBooksResponse();
            response.Books.AddRange(books);

            return Task.FromResult(response);
        }
    }
        
    
}