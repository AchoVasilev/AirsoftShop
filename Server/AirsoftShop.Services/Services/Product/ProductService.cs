namespace AirsoftShop.Services.Services.Product;

using Common.Models;
using Data.Models;
using Data.Models.Enums;
using Data.Models.Images;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Products;
using static Common.Constants.Messages;
public class ProductService : IProductService
{
    private readonly ApplicationDbContext data;

    public ProductService(ApplicationDbContext data) 
        => this.data = data;

    public async Task<IEnumerable<InitialGunViewModel>> GetNewestEightGuns()
        => await this.data.Guns
            .Select(x => new InitialGunViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Color = x.Color,
                Manufacturer = x.Manufacturer,
                Power = x.Power,
                DealerName = x.Dealer.Name,
                DealerSiteUrl = x.Dealer.SiteUrl,
                ImageUrl = x.Images.Select(i => i.Url).FirstOrDefault()
            })
            .AsNoTracking()
            .ToListAsync();

    public async Task<OperationResult<ResultGunServiceModel>> CreateGun(CreateGunServiceModel model, string dealerId)
    {
        var dealer = await this.data.Dealers
            .FirstOrDefaultAsync(x => x.Id == dealerId);

        if (dealer is null)
        {
            return InvalidUserMsg;
        }
        
        var subCategoryId = await this.data.SubCategories
            .Where(x => x.Name == model.SubCategoryName)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        if (subCategoryId == 0)
        {
            return InvalidSubcategoryErrorMsg;
        }

        var gun = new Gun()
        {
            Name = model.Name,
            Magazine = model.Magazine,
            Manufacturer = model.Manufacturer,
            Material = model.Material,
            Barrel = model.Barrel,
            Blowback = model.Blowback,
            Capacity = model.Capacity,
            Color = model.Color,
            SubCategoryId = subCategoryId,
            Firing = model.Firing,
            Hopup = model.Hopup,
            Weight = model.Weight,
            Length = model.Length,
            Speed = model.Speed,
            Price = model.Price,
            Propulsion = Enum.Parse<Propulsion>(model.Propulsion),
            Power = model.Power,
            Images = model.ImageIds.Select(x => new ItemImage()
            {
                Id = x
            }).ToList(),
        };

        dealer.Guns.Add(gun);
        await this.data.SaveChangesAsync();

        var result = new ResultGunServiceModel()
        {
            Id = gun.Id,
            Name = gun.Name
        };
        
        return result;
    }

    public async Task<GunDetailsServiceModel?> GetDetails(string gunId)
        => await this.data.Guns
            .Where(x => x.Id == gunId)
            .Select(x => new GunDetailsServiceModel
            {
                Id = x.Id,
                Name = x.Name,
                Manufacturer = x.Manufacturer,
                Power = 0,
                Barrel = x.Barrel,
                Propulsion = x.Propulsion.ToString(),
                Material = x.Material,
                Blowback = x.Blowback,
                Capacity = x.Capacity,
                Speed = 0,
                Color = x.Color,
                Weight = 0,
                Magazine = x.Magazine,
                DealerId = x.DealerId,
                DealerName = x.Dealer.Name,
                DealerUrl = x.Dealer.SiteUrl,
                Firing = x.Firing,
                Length = 0,
                Hopup = x.Hopup,
                Price = 0,
                ImageUrls = x.Images.Select(y => y.Url).ToList()
            })
            .FirstOrDefaultAsync();

    public async Task<OperationResult<ResultGunServiceModel>> Edit(string dealerId, EditGunServiceModel model)
    {
        var dealer = await this.data.Dealers
            .FirstOrDefaultAsync(x => x.Id == dealerId);

        if (dealer is null)
        {
            return InvalidUserMsg;
        }
        
        var subCategoryId = await this.data.SubCategories
            .Where(x => x.Name == model.SubCategoryName)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        if (subCategoryId == 0)
        {
            return InvalidSubcategoryErrorMsg;
        }
        
        var gun = await this.data.Guns
            .FirstOrDefaultAsync(x => x.Name == model.Name && dealer.Id == x.DealerId);
        
        if (gun is null)
        {
            return InvalidGun;
        }
        
        gun.Name = model.Name;
        gun.Magazine = model.Magazine;
        gun.Manufacturer = model.Manufacturer;
        gun.Material = model.Material;
        gun.Barrel = model.Barrel;
        gun.Blowback = model.Blowback;
        gun.Capacity = model.Capacity;
        gun.Color = model.Color;
        gun.SubCategoryId = subCategoryId;
        gun.Firing = model.Firing;
        gun.Hopup = model.Hopup;
        gun.Weight = model.Weight;
        gun.Length = model.Length;
        gun.Speed = model.Speed;
        gun.Price = model.Price;
        gun.Propulsion = Enum.Parse<Propulsion>(model.Propulsion);
        gun.Power = model.Power;
        
        await this.data.SaveChangesAsync();

        var result = new ResultGunServiceModel()
        {
            Name = gun.Name,
            Id = gun.Id
        };

        return result;
    }
}