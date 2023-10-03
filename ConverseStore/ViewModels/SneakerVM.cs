namespace ConverseStore.ViewModels
{
    public class SneakerVM
    {
        public string Id { get; set; }
        public string? Color { get; set; }
        public int? Size { get; set; }
        public int Cost { get; set; }
        public int? Count { get; set; }
        public string Name { get; set; }
        public int? OFF { get; set; } = 0;
    }
}
