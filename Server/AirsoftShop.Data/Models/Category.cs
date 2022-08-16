namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base;
using Images;
using static Constants.Data.Constants;
public class Category : DeletableEntity<int>
{
    public Category()
    {
        this.SubCategories = new HashSet<SubCategory>();
    }
    
    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Name { get; set; }

    [Required]
    [ForeignKey(nameof(Image))]
    public string ImageId { get; set; }

    public virtual CategoryImage Image { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; }
}