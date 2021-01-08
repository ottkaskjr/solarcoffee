using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost("/api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating a new customer");
            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;
            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);
            return Ok(newCustomer);
        }

        [HttpGet("/api/customer")]
        public ActionResult GetCustomers()
        {
            _logger.LogInformation("Getting customers");
            var customers = _customerService.GetAllCustomers();
            var customerModels = customers.Select(customer => /*new CustomerModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                //PrimaryAddress = CustomerMapper.MapCustomerAddress(customer.PrimaryAddress),
                PrimaryAddress = new Data.CustomerAddress
                {
                    Id = customer.PrimaryAddress.Id,
                    AddressLine1 = customer.PrimaryAddress.AddressLine1,
                    AddressLine2 = customer.PrimaryAddress.AddressLine2,
                    City = customer.PrimaryAddress.City,
                    State = customer.PrimaryAddress.State,
                    Country = customer.PrimaryAddress.Country,
                    PostalCode = customer.PrimaryAddress.PostalCode,
                    CreatedOn = customer.PrimaryAddress.CreatedOn,
                    UpdatedOn = customer.PrimaryAddress.UpdatedOn,
                },// asendasin praegu sellega, sest mul on implicit error 
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn
            }*/CustomerMapper.SerializeCustomer(customer)).OrderByDescending(customer => customer.CreatedOn)
            .ToList();
            return Ok(customerModels);
        }

        //[HttpGet("/api/cu")]

        [HttpDelete("/api/customer/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Deleting a customer");
            var response = _customerService.DeleteCustomer(id);
            return Ok(response);
        }
    } 
}
