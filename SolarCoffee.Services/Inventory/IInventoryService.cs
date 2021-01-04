using System;
using System.Collections.Generic;
using System.Text;
using SolarCoffee.Data.models;

namespace SolarCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        List<ProductInventory> GetCurrentInventory();
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        public ProductInventory GetProductById(int productId);
        public List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}
