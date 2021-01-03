using SolarCoffee.Data;
using SolarCoffee.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext _db;
        
        public ProductService(SolarDbContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Archives a Product by setting boolean IsArchived to true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<Data.models.Product> ArchiveProduct(int id)
        {
            try
            {
                var product = _db.Products.Find(id);
                product.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<Data.models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Product archived",
                    IsSuccess = true
                };
            } catch(Exception e)
            {
                return new ServiceResponse<Data.models.Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }
        }

        /// <summary>
        /// Adds a new product to the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ServiceResponse<Data.models.Product> CreateProduct(Data.models.Product product)
        {
            try
            {
                _db.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                _db.ProductInventories.Add(newInventory);

                _db.SaveChanges(); // execute changes here

                return new ServiceResponse<Data.models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSuccess = true
                };
            } catch (Exception e)
            {
                return new ServiceResponse<Data.models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }
            
        }
        /// <summary>
        /// Retrieves all products from the database
        /// </summary>
        /// <returns></returns>
        public List<Data.models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        /// <summary>
        /// Retrieves a product from the database by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }
    }
}
