namespace AirsoftShop.Services.Models.Product.Base;

public class ExtendedBaseGunServiceModel : BaseGunServiceModel
{
    public double Power { get; init; }
    
    public decimal Price { get; init; }
    
    public double Weight { get; init; }

    public string Magazine { get; init; }

    public int Capacity { get; init; }

    public int Speed { get; init; }

    public string Firing { get; init; }

    public int Length { get; init; }

    public int Barrel { get; init; }

    public string Propulsion { get; init; }

    public string Material { get; init; }

    public string Blowback { get; init; }

    public string Hopup { get; init; }
}