namespace AirsoftShop.Controllers.Models.Products.Clothings;

using System.ComponentModel.DataAnnotations;
using Base;
using Microsoft.AspNetCore.Http;
using static Data.Constants.Data.Constants;
using static Common.Constants.Messages;

public class CreateClothingInputModel : BaseProductModel
{
    public CreateClothingInputModel()
    {
        this.Images = new List<IFormFile>();
    }

    [Range(DefaultMinLength, maximum: MaxSize, ErrorMessage = LengthErrorMsg)]
    public int Size { get; set; }
    
    [Required(ErrorMessage = RequiredFieldErrorMsg)]
    [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
    public string? Material { get; set; }
    
    public IEnumerable<IFormFile> Images { get; set; }
}