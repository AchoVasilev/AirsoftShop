namespace AirsoftShop.Services.Models.Dealer;

using Microsoft.AspNetCore.Http;

public class CreateDealerServiceModel
{
    public string Name { get; init; }

    public string DealerNumber { get; init; }

    public string Username { get; init; }

    public string Phone { get; init; }

    public string SiteUrl { get; init; }

    public string Email { get; init; }

    public string StreetName { get; init; }

    public string CityName { get; init; }

    public string Password { get; init; }

    public IFormFile Image { get; set; }
}