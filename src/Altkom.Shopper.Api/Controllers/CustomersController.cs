using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Altkom.Shopper.Domain;

namespace Altkom.Shopper.Api.Controllers;

public class CustomersController
{
    private readonly ICustomerRepository repository;

    public CustomersController(ICustomerRepository repository)
    {
        this.repository = repository;
    }

    // GET api/hellomvc
    [HttpGet("api/hellomvc")]
    public string Hello()
    {
        return "Hello MVC";
    }

    // GET api/customers
    [HttpGet("api/customers")]
    public IEnumerable<Customer> Get()
    {
        var customers = repository.GetAll();

        return customers;
    }

    // GET api/customers/{id}
    [HttpGet("api/customers/{id:int}")]
    public ActionResult<Customer> Get(int id)
    {
        var customer = repository.Get(id);

        if (customer == null)
           return new NotFoundResult();

        return new OkObjectResult(customer);
    }
    
}
