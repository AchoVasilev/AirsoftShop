namespace AirsoftShop.Services.Models.Product;

public class GunViewServiceModel : GunDetailsServiceModel
{
    public IEnumerable<string> ImageUrls { get; init; }
}