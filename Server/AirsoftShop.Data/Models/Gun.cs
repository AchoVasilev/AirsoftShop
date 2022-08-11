namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base;
using Enums;
using Images;
using static Constants.Data.Constants;

public class Gun : DeletableEntity<string>
{
    public Gun()
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
    
    [MaxLength(RangeMaxLength)] 
    public double Power { get; set; }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Color { get; set; }

    [MaxLength(NumbersMaxLength)] 
    public double Weight { get; set; }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Magazine { get; set; }

    [Required] 
    [MaxLength(RangeMaxLength)] 
    public int Capacity { get; set; }

    [Required] 
    [MaxLength(RangeMaxLength)] 
    public int Speed { get; set; }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Firing { get; set; }

    [Required]
    [MaxLength(NumbersMaxLength)]
    public int Length { get; set; }

    [Required]
    [MaxLength(NumbersMaxLength)]
    public int Barrel { get; set; }

    public Propulsion Propulsion { get; set; }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Material { get; set; }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Blowback { get; set; }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Hopup { get; set; }

    public decimal Price { get; set; }

    [ForeignKey(nameof(Dealer))] 
    public string DealerId { get; set; }

    public virtual Dealer Dealer { get; set; }

    [ForeignKey(nameof(SubCategory))] 
    public int SubCategoryId { get; set; }

    public virtual SubCategory SubCategory { get; set; }
    
    public ICollection<ItemImage> Images { get; set; }
}