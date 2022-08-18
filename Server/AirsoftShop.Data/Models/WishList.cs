namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base;

public class WishList : DeletableEntity<string>
{
    public WishList()
    {
        this.Id = Guid.NewGuid().ToString();
        this.ItemsInWishList = new HashSet<ItemInWishList>();
    }

    [ForeignKey(nameof(Client))]
    [Required]
    public string ClientId { get; set; }

    public virtual Client Client { get; set; }

    public virtual ICollection<ItemInWishList> ItemsInWishList { get; set; }
}