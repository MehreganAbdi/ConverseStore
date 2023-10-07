using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Models
{
    public class BackPack
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public string? Image { get; set; }
        public int? OFF { get; set; }
        public int? Count { get; set; }
    }
}
