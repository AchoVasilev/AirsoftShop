namespace AirsoftShop.Controllers.Models.Carts;

public class BulkCartInputModel
{
    public IEnumerable<string> ItemIds { get; set; }
}