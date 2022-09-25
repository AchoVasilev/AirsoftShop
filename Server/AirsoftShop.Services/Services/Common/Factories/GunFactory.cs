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

    public ProductResultModel CreateResultModel(Gun item)
    {
        var result = new ProductResultModel()
        {
            Id = item.Id,
            Name = item.Name
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

    public IProduct CreateDetailsModel(Gun item)
    {
        var model = new GunDetailsServiceModel
        {
            Id = item.Id,
            Name = item.Name,
            Manufacturer = item.Manufacturer,
            Power = item.Power,
            Barrel = item.Barrel,
            Propulsion = item.Propulsion.ToString(),
            Material = item.Material,
            Blowback = item.Blowback,
            Capacity = item.Capacity,
            Speed = item.Speed,
            Color = item.Color,
            Weight = item.Weight,
            Magazine = item.Magazine,
            DealerId = item.DealerId,
            DealerName = item.Dealer.Name,
            DealerUrl = item.Dealer.SiteUrl,
            Firing = item.Firing,
            Length = item.Length,
            Hopup = item.Hopup,
            Price = item.Price,
            Description = item.Description,
            ImageUrl = item.Images.Select(y => y.Url ?? y.RemoteImageUrl).FirstOrDefault()
        };

        return model;
    }
}