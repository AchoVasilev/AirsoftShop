namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations;
using Base;
using static Constants.Data.Constants;
public class SubCategory : DeletableEntity<int>
{
    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Name { get; init; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }
}