using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data.models
{
    public class Customer
    {
        // primary key
        // entityframework increments type int automatically in db
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CustomerAddress PrimaryAddress { get; set; }
    }
}
