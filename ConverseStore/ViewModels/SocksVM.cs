﻿namespace ConverseStore.ViewModels
{
    public class SocksVM
    {
        public int Id { get; set; }
        public int? Size { get; set; }
        public int Cost { get; set; }
        public int? Count { get; set; }
        public string Name { get; set; }
        public int? OFF { get; set; }
        public IFormFile? Image { get; set; }
    }
}
