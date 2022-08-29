namespace AirsoftShop.Services.Models.Order;

public class OrderListServiceModel
{
    public string OrderId { get; set; }

    public string CreatedOn { get; set; }

    public decimal TotalPrice { get; set; }

    public IEnumerable<OrderGunViewServiceModel> Gun { get; set; }
}