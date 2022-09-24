namespace AirsoftShop.Services.Models.Product.Guns;

using Base;

public class OwnerGunListServiceModel : BaseGunServiceModel
{
    public decimal Price { get; init; }
    
    public string? CreatedOn { get; init; }

    public string? DealerId { get; init; }

    public string? ImageUrl { get; init; }
}