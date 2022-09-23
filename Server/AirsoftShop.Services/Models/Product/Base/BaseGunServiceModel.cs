namespace AirsoftShop.Services.Models.Product.Base;

using Services.Common;

public class BaseGunServiceModel : IGunModel
{
    public string? Id { get; set; }
    
    public string? Name { get; init; }
    
    public int SubCategoryId { get; set; }

    public string? Manufacturer { get; init; }
    
    public string? Color { get; init; }
    
}