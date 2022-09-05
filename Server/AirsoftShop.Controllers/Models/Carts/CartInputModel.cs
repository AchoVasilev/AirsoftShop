namespace AirsoftShop.Controllers.Models.Carts;

using System.ComponentModel.DataAnnotations;

public class CartInputModel
{
    [Required]
    public string? ItemId { get; init; }
}