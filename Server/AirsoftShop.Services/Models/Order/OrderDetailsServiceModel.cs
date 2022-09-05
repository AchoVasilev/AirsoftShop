namespace AirsoftShop.Services.Models.Order;

using Courier;

public class OrderDetailsServiceModel
{
    public string? Id { get; init; }

    public decimal TotalPrice { get; init; }

    public CourierOrderViewServiceModel? Courier { get; init; }

    public IEnumerable<OrderDetailsGunServiceModel>? Guns { get; init; }

    public string? PaymentType { get; init; }

    public string? OrderStatus { get; init; }
}