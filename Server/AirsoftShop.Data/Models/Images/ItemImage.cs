namespace AirsoftShop.Data.Models.Images;

using System.ComponentModel.DataAnnotations.Schema;
using Products;

public class ItemImage : BaseImage
{
    [ForeignKey(nameof(Gun))]
    public string GunId { get; set; }
    
    public virtual Gun Gun { get; set; }
}