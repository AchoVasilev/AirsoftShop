namespace AirsoftShop.Services.Models.Dealers;

using Microsoft.AspNetCore.Http;

public class CreateDealerServiceModel
{
    public string Name { get; set; }

    public string DealerNumber { get; set; }

    public string Username { get; set; }

    public string Phone { get; set; }

    public string SiteUrl { get; set; }

    public string Email { get; set; }

    public string StreetName { get; set; }

    public string CityName { get; set; }

    public string Password { get; set; }

    public IFormFile Image { get; set; }
}