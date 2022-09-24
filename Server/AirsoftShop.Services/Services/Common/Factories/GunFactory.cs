namespace AirsoftShop.Services.Services.Common.Factories;

using AirsoftShop.Data.Models.Enums;
using AirsoftShop.Data.Models.Images;
using AirsoftShop.Data.Models.Products;
using AirsoftShop.Services.Models.Product.Guns;
using Models.Product;

public class GunFactory : IProductFactory<Gun, ProductResultModel>
{
    public Gun CreateFromInputModel(IProduct productEntity, string dealerId)
    {
        var model = (CreateGunServiceModel)productEntity;
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

    public Gun CreateUpdatedModel(Gun item, IProduct product)
    {
        var model = (EditGunServiceModel)product;
        item.Name = model.Name;
        item.Magazine = model.Magazine;
        item.Manufacturer = model.Manufacturer;
        item.Material = model.Material;
        item.Barrel = model.Barrel;
        item.Blowback = model.Blowback;
        item.Capacity = model.Capacity;
        item.Color = model.Color;
        item.SubCategoryId = model.SubCategoryId;
        item.Firing = model.Firing;
        item.Hopup = model.Hopup;
        item.Weight = model.Weight;
        item.Length = model.Length;
        item.Speed = model.Speed;
        item.Price = model.Price;
        item.Propulsion = Enum.Parse<Propulsion>(model.Propulsion!);
        item.Power = model.Power;
        item.Description = model.Description;

        return item;
    }
}