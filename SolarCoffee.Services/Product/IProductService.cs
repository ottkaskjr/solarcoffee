using System;
using System.Collections.Generic;
using System.Text;
using SolarCoffee.Data;

namespace SolarCoffee.Services.Product
{
    public interface IProductService
    {
        List<Data.models.Product> GetAllProducts();
        Data.models.Product GetProductById(int id);
        ServiceResponse<Data.models.Product> CreateProduct(Data.models.Product product);
        ServiceResponse<Data.models.Product> ArchiveProduct(int id);
    }
}
