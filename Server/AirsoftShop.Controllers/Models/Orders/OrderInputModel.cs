namespace AirsoftShop.Controllers.Models.Orders;

using System.ComponentModel.DataAnnotations;

public class OrderInputModel
{
    [Required]
    public string PaymentType { get; set; }

    public decimal TotalPrice { get; set; }

    public int CourierId { get; set; }

    public List<string> GunsIds { get; set; } = new List<string>();
}