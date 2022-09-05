namespace AirsoftShop.Services.Models.Dealer;

using Base;
using Microsoft.AspNetCore.Http;

public class CreateDealerServiceModel : BaseDealerServiceModel
{
    public string? Username { get; init; }

    public string? Password { get; init; }

    public IFormFile? Image { get; set; }
}