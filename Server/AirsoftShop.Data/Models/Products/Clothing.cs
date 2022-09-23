namespace AirsoftShop.Data.Models.Products;

using System.ComponentModel.DataAnnotations;
using static Constants.Data.Constants;

public class Clothing : Product
{
    public int Size { get; set; }
    
    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Material { get; set; }
    
    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Color { get; set; }
}