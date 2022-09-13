namespace AirsoftShop.Services.Common;

public interface IGunModel : IProduct
{
    public string? Manufacturer { get; init; }
    
    public string? Color { get; init; }
}