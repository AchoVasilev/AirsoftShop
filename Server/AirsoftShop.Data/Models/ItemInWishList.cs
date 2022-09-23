namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Base;
using Products;

public class ItemInWishList : DeletableEntity<string>
{
    public ItemInWishList()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    [ForeignKey(nameof(Gun))]
    public string GunId { get; set; }

    public virtual Gun Gun { get; set; }
    
    [ForeignKey(nameof(Clothing))]
    public string ClothingId { get; set; }
    
    public virtual  Clothing Clothing { get; set; }

    [ForeignKey(nameof(WishList))]
    public string WishListId { get; set; }

    public virtual WishList WishList { get; set; }
}