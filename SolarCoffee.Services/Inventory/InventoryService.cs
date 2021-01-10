using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(SolarDbContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Creates a Snapshot record using the provided ProductInventory instance
        /// </summary>
        /// <param name="inventory"></param>
        private void CreateSnapshot()
        {
            var now = DateTime.UtcNow;

            var inventories = _db.ProductInventories
                .Include(inv => inv.Product).ToList();

            foreach (var inventory in inventories)
            {
                var snapshot = new ProductInventorySnapshot
                {
                    SnapshotTime = now,
                    Product = inventory.Product,
                    QuantityOnHand = inventory.QuantityOnHand
                };
            _db.Add(snapshot);
            }


            // don't need to save changes because CreateSnapshot is follewed by _db.SaveChanges
        }

        /// <summary>
        /// Returns current inventory from the database
        /// </summary>
        /// <returns></returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }

        /// <summary>
        /// Gets a ProductInventory instance by Product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductInventory GetProductById(int productId)
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == productId);
        }



        /// <summary>
        /// Return Snapshot history for the previous 6 hours
        /// </summary>
        /// <returns></returns>
        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(2);
            return _db.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap => snap.SnapshotTime > earliest && !snap.Product.IsArchived)
                .ToList();
        }


        /// <summary>
        /// Updates number of units available of the provided product id
        /// Adjusts QuantityOnHand by adjustment value
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added/removed from inventory</param>
        /// <returns></returns>
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            DateTime now = DateTime.UtcNow;
            try
            {
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot();
                }
                catch (Exception e)
                {
                    // we will not prevent the application from running but provide a log for developers
                    _logger.LogError("Error creating inventory snapshot");
                    _logger.LogError(e.StackTrace);
                }

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Message = $"Product {id} inventory adjusted",
                    Time = now,
                    Data = inventory
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = false,
                    Message = $"Error updating ProductInventory QuantityOnHand",
                    Time = now,
                    Data = null
                };
            }
        }
    }
}
