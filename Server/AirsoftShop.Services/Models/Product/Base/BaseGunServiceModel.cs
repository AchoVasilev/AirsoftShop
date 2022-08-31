namespace AirsoftShop.Services.Models.Product.Base;

public class BaseGunServiceModel
{
    public string? Id { get; set; }
    
    public string Name { get; set; }
    
    public string Manufacturer { get; set; }
    
    public string Color { get; init; }
    
}