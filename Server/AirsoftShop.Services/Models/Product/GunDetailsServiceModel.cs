namespace AirsoftShop.Services.Models.Product
{
    using Base;

    public class GunDetailsServiceModel : ExtendedBaseGunServiceModel
    {
        public IEnumerable<string> ImageUrls { get; init; }

        public string DealerName { get; init; }

        public string DealerId { get; init; }

        public string? DealerUrl { get; init; }
    }
}