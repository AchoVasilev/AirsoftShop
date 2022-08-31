namespace AirsoftShop.Controllers.Models.Dealers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using static Data.Constants.Data.Constants;
    using static Common.Constants.Messages;
    
    public class CreateDealerInputModel : BaseDealerModel
    {
        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
        public string Username { get; set; }

        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = LengthErrorMsg)]
        public string Password { get; set; }

        public IFormFile Image { get; set; }
    }
}