namespace AirsoftShop.Data.Models.Products;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base;
using Images;
using static Constants.Data.Constants;

public class Product : DeletableEntity<string>
{
    public Product()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Images = new HashSet<ItemImage>();
    }
    
    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Name { get; set; }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Manufacturer { get; set; }
    
    public decimal Price { get; set; }
    
    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; }

    [ForeignKey(nameof(Dealer))]
    [Required]
    public string DealerId { get; set; }

    public virtual Dealer Dealer { get; set; }

    [ForeignKey(nameof(SubCategory))] 
    public int SubCategoryId { get; set; }

    public virtual SubCategory SubCategory { get; set; }
    
    public IEnumerable<ItemImage> Images { get; init; }
}