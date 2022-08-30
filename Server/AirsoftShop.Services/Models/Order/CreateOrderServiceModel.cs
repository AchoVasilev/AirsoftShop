namespace AirsoftShop.Services.Models.Order;

public class CreateOrderServiceModel
{
    public string PaymentType { get; init; }

    public decimal TotalPrice { get; set; }

    public int CourierId { get; init; }

    public List<string> GunsIds { get; init; } = new List<string>();
}