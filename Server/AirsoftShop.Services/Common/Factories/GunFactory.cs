namespace AirsoftShop.Services.Common.Factories;

using Data.Models;
using Data.Models.Enums;
using Data.Models.Images;
using Models.Product;

public class GunFactory : IProductFactory<Gun, ProductResultModel>
{
    public Gun CreateFromInputModel(IProduct product, string dealerId)
    {
        var model = (CreateGunServiceModel)product;
        var gun = new Gun()
        {
            DealerId = dealerId,
            Name = model.Name,
            Magazine = model.Magazine,
            Manufacturer = model.Manufacturer,
            Material = model.Material,
            Barrel = model.Barrel,
            Blowback = model.Blowback,
            Capacity = model.Capacity,
            Color = model.Color,
            SubCategoryId = model.SubCategoryId,
            Firing = model.Firing,
            Hopup = model.Hopup,
            Weight = model.Weight,
            Length = model.Length,
            Speed = model.Speed,
            Price = model.Price,
            Propulsion = Enum.Parse<Propulsion>(model.Propulsion!),
            Power = model.Power,
            Description = model.Description,
            Images = model.Images!.Select(x => new ItemImage()
            {
                Url = x.Uri,
                Name = x.Name,
                Extension = x.Extension
            }).ToList()
        };

        return gun;
    }

    public ProductResultModel CreateResultModel(Gun product)
    {
        var result = new ProductResultModel()
        {
            Id = product.Id,
            Name = product.Name
        };

        return result;
    }
}