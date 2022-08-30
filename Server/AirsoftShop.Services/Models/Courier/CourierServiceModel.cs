namespace AirsoftShop.Services.Models.Courier;

public class CourierServiceModel
{
    public int Id { get; init; }

    public string Name { get; init; }

    public decimal DeliveryPrice { get; init; }

    public int DeliveryDays { get; init; }

    public string ImageUrl { get; init; }
}