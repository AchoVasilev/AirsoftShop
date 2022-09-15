namespace AirsoftShop.Services.Models.Product.Guns
{
    using AirsoftShop.Services.Models.Product.Base;

    public class GunDetailsServiceModel : ExtendedBaseGunServiceModel
    {
        public string? ImageUrl { get; set; }
        
        public string? DealerName { get; init; }

        public string? DealerId { get; init; }

        public string? DealerUrl { get; init; }
    }
}