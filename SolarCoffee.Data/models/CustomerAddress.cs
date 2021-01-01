using System;
using System.ComponentModel.DataAnnotations;

namespace SolarCoffee.Data
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [MaxLength(100)] // data attributes (max string length)
        public string AddressLine1 { get; set; }
        [MaxLength(100)]
        public string AddressLine2 { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string State { get; set; }
        [MaxLength(100)]
        public string PostalCode { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
    }
}