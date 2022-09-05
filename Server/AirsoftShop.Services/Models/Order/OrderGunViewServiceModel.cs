namespace AirsoftShop.Services.Models.Order;

public class OrderGunViewServiceModel
{
    public string? Id { get; init; }

    public string? Name { get; init; }

    public string? Color { get; init; }

    public decimal Price { get; init; }

    public string? Manufacturer { get; init; }

    public string? ImageUrl { get; init; }
}