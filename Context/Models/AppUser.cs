using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Models
{
    public class AppUser : IdentityUser
    {
        public Address?  Address { get; set; }
        [ForeignKey("AddressId")]
        public int AddressId { get; set; }
    }
}
