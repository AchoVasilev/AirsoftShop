namespace AirsoftShop.Controllers.Models.Orders;

using System.ComponentModel.DataAnnotations;

public class OrderInputModel
{
    public OrderInputModel()
    {
        this.GunsIds = new List<string>();
    }
    
    [Required]
    public string? PaymentType { get; set; }

    public decimal TotalPrice { get; set; }

    public int CourierId { get; set; }

    public List<string> GunsIds { get; }
}