namespace AirsoftShop.Services.Models.Order;

public class CreateOrderServiceModel
{
    public string PaymentType { get; set; }

    public decimal TotalPrice { get; set; }

    public int CourierId { get; set; }

    public List<string> GunsIds { get; set; } = new List<string>();
}