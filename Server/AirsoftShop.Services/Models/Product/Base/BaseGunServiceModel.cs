namespace AirsoftShop.Services.Models.Product.Base;

using Common;

public class BaseGunServiceModel : IGunModel
{
    public string? Id { get; set; }
    
    public string? Name { get; init; }
    
    public string? Manufacturer { get; init; }
    
    public string? Color { get; init; }
    
}