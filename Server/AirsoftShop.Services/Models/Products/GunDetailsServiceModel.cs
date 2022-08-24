namespace AirsoftShop.Services.Models.Products
{
    public class GunDetailsServiceModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Manufacturer { get; init; }

        public IEnumerable<string> ImageUrls { get; init; }

        public double Power { get; init; }

        public string Color { get; init; }

        public double Weight { get; init; }

        public string Magazine { get; init; }

        public int Capacity { get; init; }

        public int Speed { get; init; }

        public string Firing { get; init; }

        public int Length { get; init; }

        public int Barrel { get; init; }

        public string Propulsion { get; init; }

        public string Material { get; init; }

        public string Blowback { get; init; }

        public string Hopup { get; init; }

        public decimal Price { get; init; }

        public string DealerName { get; init; }

        public string DealerId { get; init; }

        public string? DealerUrl { get; init; }
    }
}