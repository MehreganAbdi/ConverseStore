namespace ConverseStore.ViewModels
{
    public class BackPackVM
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int? OFF { get; set; }
        public int Count { get; set; }
        public IFormFile? Image { get; set; }

    }
}
