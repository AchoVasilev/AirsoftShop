namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations;
using Base;

using static Constants.Data.Constants;
public class City : DeletableEntity<int>
{
    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Name { get; init; }

    [MaxLength(NumbersMaxLength)]
    public int ZipCode { get; set; }
}