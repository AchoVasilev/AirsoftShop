namespace AirsoftShop.Data.Models.Images;

using System.ComponentModel.DataAnnotations.Schema;

public class Image : BaseImage
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }

    public virtual ApplicationUser User { get; set; }
    
    [ForeignKey(nameof(Courier))]
    public int? CourierId { get; set; }

    public virtual Courier Courier { get; set; }
    
    [ForeignKey(nameof(Field))]
    public int? FieldId { get; set; }

    public virtual Field Field { get; set; }
}