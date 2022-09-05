namespace AirsoftShop.Services.Models.Client;

using Base;

public class CreateClientServiceModel : ExtendedBaseClientServiceModel
{
    public string? Username { get; init; }
    
    public string? Password { get; init; }
}