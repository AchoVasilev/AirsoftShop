namespace AirsoftShop.Data.Models.Products;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AirsoftShop.Data.Models.Base;
using AirsoftShop.Data.Models.Enums;
using AirsoftShop.Data.Models.Images;
using static Constants.Data.Constants;

public class Gun : Product
{
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
}