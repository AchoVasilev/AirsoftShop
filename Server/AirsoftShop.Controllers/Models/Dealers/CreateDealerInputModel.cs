namespace AirsoftShop.Controllers.Models.Dealers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using static Data.Constants.Data.Constants;
    using static Common.Constants.Messages;
    
    public class CreateDealerInputModel
    {
        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
        public string Name { get; set; }

        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
        public string DealerNumber { get; set; }

        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
        public string Username { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public string SiteUrl { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
        public string StreetName { get; set; }

        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength, ErrorMessage = LengthErrorMsg)]
        public string CityName { get; set; }

        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = LengthErrorMsg)]
        public string Password { get; set; }

        public IFormFile Image { get; set; }
    }
}