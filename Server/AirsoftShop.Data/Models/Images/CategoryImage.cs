namespace AirsoftShop.Data.Models.Images;

using System.ComponentModel.DataAnnotations.Schema;

public class CategoryImage : BaseImage
{
    [ForeignKey(nameof(Category))]
    public int? CategoryId { get; set; }

    public virtual Category Category { get; set; }
}