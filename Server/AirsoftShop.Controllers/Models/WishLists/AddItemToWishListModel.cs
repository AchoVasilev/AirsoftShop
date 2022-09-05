namespace AirsoftShop.Controllers.Models.WishLists;

using System.ComponentModel.DataAnnotations;

public class AddItemToWishListModel
{
    [Required]
    public string? Id { get; set; }
}