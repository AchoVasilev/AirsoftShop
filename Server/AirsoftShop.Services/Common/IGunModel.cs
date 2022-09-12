namespace AirsoftShop.Services.Common;

public interface IGunModel
{
    string? Id { get; }
    
     string? Name { get; }
    
     string? Manufacturer { get; }
    
     string? Color { get; }
}