namespace AirsoftShop.Controllers.Models.WishLists;

using System.ComponentModel.DataAnnotations;

public class BulkRemoveItemsFromWishListModel
{
    [Required]
    public IEnumerable<string>? Ids { get; set; }
}