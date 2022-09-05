namespace AirsoftShop.Services.Models.Courier;

using Base;

public class CourierServiceModel : BaseCourierServiceModel
{
    public int Id { get; init; }

    public int DeliveryDays { get; init; }

    public string? ImageUrl { get; init; }
}