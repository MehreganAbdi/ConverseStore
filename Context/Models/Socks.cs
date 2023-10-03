﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Models
{
    public class Socks
    {

        [Key]
        public string Id { get; set; }
        public int? Size { get; set; }
        public int Cost { get; set; }
        public int? Count { get; set; }
        public string Name { get; set; }
    }
}
