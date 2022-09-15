namespace AirsoftShop.Services.Models.Product.Base;

public abstract class BaseResultServiceModel
{
    public string? Id { get; set; }
    
    public string? Name { get; init; }
}