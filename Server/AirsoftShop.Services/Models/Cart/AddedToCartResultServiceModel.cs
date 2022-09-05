namespace AirsoftShop.Services.Models.Cart;

public class AddedToCartResultServiceModel
{
    public string? CartId { get; set; }
    
    public string? GunId { get; set; }
    
    public int ItemsCount { get; set; }
}