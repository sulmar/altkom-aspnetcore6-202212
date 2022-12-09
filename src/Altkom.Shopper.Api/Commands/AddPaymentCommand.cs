using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Altkom.Shopper.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Altkom.Shopper.Api.Commands;

// dotnet add Ardalis.ApiEndpoints
public class AddPaymentCommand : EndpointBaseAsync.WithRequest<Payment>.WithActionResult<Payment>
{
    private readonly IPaymentRepository repository;

    [HttpPost("api/payments")]
    public override async Task<ActionResult<Payment>> HandleAsync(Payment request, CancellationToken cancellationToken = default)
    {
        repository.Add(request);

        return CreatedAtRoute("GetPaymentById", new { Id = request.Id}, request);
    }
}

// dotnet add package MinimalApi.Endpoint 
