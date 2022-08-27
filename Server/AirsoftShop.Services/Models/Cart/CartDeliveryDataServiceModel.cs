namespace AirsoftShop.Services.Models.Cart;

using Couriers;

public class CartDeliveryDataServiceModel
{
    public ICollection<CourierServiceModel> Couriers { get; set; } = new List<CourierServiceModel>();

    public string CashPayment { get; set; }

    public string CardPayment { get; set; }
}