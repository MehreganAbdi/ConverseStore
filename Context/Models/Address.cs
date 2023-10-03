using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string City { get; set; }
        public string? Street { get; set; }
        public string? AddressDetails { get; set; }
        public string? PostalCode { get; set; }
    }
}