using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Customer
{
    public interface ICustomerService
    {
        List<Data.models.Customer> GetAllCustomers();
        ServiceResponse<Data.models.Customer> CreateCustomer(Data.models.Customer customer);
        ServiceResponse<bool> DeleteCustomer(int id);
        Data.models.Customer GetById(int id);
    }
}
