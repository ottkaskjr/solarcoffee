using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data
{
    // inheritance
    // (child class) : (base class)
    public class SolarDbContext : IdentityDbContext
    {
        public SolarDbContext() {}
        
        // Takes in DbContextOptions and passes it to base class
        public SolarDbContext(DbContextOptions options) : base(options)
        {

        }

        // represents the tables that are in the db(created as properties of these class)
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
    }
}
