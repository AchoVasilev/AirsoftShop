namespace AirsoftShop.Services.Models.Cart;

using Courier;

public class CartDeliveryDataServiceModel
{
    public CartDeliveryDataServiceModel()
    {
        this.Couriers = new List<CourierServiceModel>();
    }
    
    public ICollection<CourierServiceModel> Couriers { get; }

    public string? CashPayment { get; set; }

    public string? CardPayment { get; set; }
}