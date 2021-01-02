using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data.models
{
    public class SalesOrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
