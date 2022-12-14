using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Altkom.Shopper.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Altkom.Shopper.Api.Controllers;

// GET api/customers/man
// GET api/customers/woman

// GET api/customers/{id}/orders


// customers 

// orders ++


// GET api/customers/{id}/orders/2021
// GET api/customers/{id}/orders/2021/10
// GET api/customers/{id}/orders/lastyear

[Route("api/orders")]

public class OrdersController
{
    // GET api/customers/{id}/orders
}

[Route("api/customers")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository repository;    

    public CustomersController(ICustomerRepository repository)
    {
        this.repository = repository;
    }

    // GET api/hellomvc
    [HttpGet("~/api/hellomvc")]
    public string Hello()
    {
        return "Hello MVC";
    }

    // GET api/customers
    // [Authorize(Roles = "manager")]
    [Authorize]
    [HttpGet]
    public ActionResult<IEnumerable<Customer>> Get()
    {
        // if (!this.User.Identity.IsAuthenticated)
        // {
        //     return Unauthorized();
        // }

        var customers = repository.GetAll();

        if (this.User.IsInRole("trainer"))
        {
            return Ok(customers);
        }
        else
        {
            var activeCustomers = customers.Where(c=>!c.IsRemoved);

            return Ok(activeCustomers);
        }

        

        return Ok(customers);
    }

    // GET api/customers/{id}
    [Authorize("Adult")]
    [HttpGet("{id:int}", Name = "GetCustomerById")]
    public ActionResult<Customer> Get(int id)
    {
        var customer = repository.Get(id);

        if (customer == null)
           return NotFound();

        return Ok(customer);
    }
    
    [HttpPost]
    public ActionResult<Customer> Post([FromBody] Customer customer, [FromServices] IMessageService messageService)
    {
        var existCustomer = repository.Get(1);

        // if (existCustomer != null)
        //     ModelState.AddModelError("customer", "Customer with email exists");
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string email = User.FindFirstValue(ClaimTypes.Email);

        repository.Add(customer);
        messageService.Send($"Send email to <{email}> dodano {customer}");

        return CreatedAtRoute("GetCustomerById", new { Id = customer.Id}, customer);
    }
    
}
