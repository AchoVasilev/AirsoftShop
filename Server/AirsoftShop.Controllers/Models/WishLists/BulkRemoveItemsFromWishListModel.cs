namespace AirsoftShop.Controllers.Models.WishLists;

public class BulkRemoveItemsFromWishListModel
{
    public IEnumerable<string> Ids { get; set; }
}